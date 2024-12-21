using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;
using MyToDo.Shared.Dtos;

namespace MyToDo.ViewModels
{
    public partial class MemoViewModel : ViewModelBase
    {
        private readonly MemoService memoService;
        private readonly UIStateService uIStateService;

        public MemoViewModel(MemoService memoService, UIStateService uIStateService)
        {
            this.memoService = memoService;
            this.uIStateService = uIStateService;

            Task.Run(async () =>
            {
                await LoadMemosAsync();
            });
        }

        #region 属性
        [ObservableProperty]
        private bool isRightDrawerOpen;

        [ObservableProperty]
        private ObservableCollection<MemoDto> memoDtos = [];

        [ObservableProperty]
        private string drawerContentTitle = string.Empty;

        [ObservableProperty]
        private int dtoId;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string? content;

        #endregion

        [RelayCommand]
        private void OpenDrawer(MemoDto? dto)
        {
            DrawerContentTitle = dto is null ? "添加备忘录" : "修改备忘录";
            DtoId = dto is null ? 0 : dto.Id;
            Title = dto?.Title ?? string.Empty;
            Content = dto?.Content;
            IsRightDrawerOpen = true;
        }

        [RelayCommand]
        private async Task Ok()
        {
            if (string.IsNullOrEmpty(Title))
                return;

            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                switch (DtoId)
                {
                    case 0:
                        resultMessage = await memoService.AddDtoAsync("api/memo/add", new MemoDto
                        {
                            Title = Title,
                            Content = Content
                        }, MemoDtos);
                        break;
                    default:
                        resultMessage = await memoService.UpdateDtoAsync("api/memo/update", new MemoDto
                        {
                            Id = DtoId,
                            Title = Title,
                            Content = Content
                        }, MemoDtos);
                        break;
                }
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                IsRightDrawerOpen = false;
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        [RelayCommand]
        private async Task DeleteToDo(MemoDto dto)
        {
            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                resultMessage = await memoService.DeleteDtoAsync("api/memo/delete", dto.Id, MemoDtos);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        private async Task LoadMemosAsync()
        {
            try
            {
                uIStateService.IsLoadingVisible = true;
                await memoService.GetDtosAsync("api/memo/getall", MemoDtos);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
            }
        }

    }
}
