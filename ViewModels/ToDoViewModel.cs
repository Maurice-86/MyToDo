using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public partial class ToDoViewModel : ViewModelBase
    {
        private readonly ToDoService toDoService;
        private readonly UIStateService uIStateService;

        public ToDoViewModel(ToDoService toDoService, UIStateService uIStateService)
        {
            this.toDoService = toDoService;
            this.uIStateService = uIStateService;

            Task.Run(async () =>
            {
                await LoadTodosAsync();
            }).ConfigureAwait(false);
        }

        #region 属性
        [ObservableProperty]
        private string? searchKeyword;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private bool isRightDrawerOpen;

        [ObservableProperty]
        private ObservableCollection<TodoDto> todoDtos = [];

        [ObservableProperty]
        private string drawerContentTitle = string.Empty;

        [ObservableProperty]
        private int dtoId;

        [ObservableProperty]
        private int status;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string? content;

        #endregion

        [RelayCommand]
        private async Task Search(string? keyword)
        {
            var queryParameter = new QueryParameter();
            if (!string.IsNullOrEmpty(keyword))
                queryParameter.Keyword = keyword;

            switch (SelectedIndex)
            {
                case 1:
                    queryParameter.Status = 0;
                    break;
                case 2:
                    queryParameter.Status = 1;
                    break;
            }
            await LoadTodosAsync(queryParameter);
        }

        async partial void OnSelectedIndexChanged(int value)
        {
            await Search(SearchKeyword);
        }

        [RelayCommand]
        private void OpenDrawer(TodoDto? dto)
        {
            DrawerContentTitle = dto is null ? "添加待办" : "修改待办";
            DtoId = dto is null ? 0 : dto.Id;
            Status = dto?.Status ?? 0;
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
                        resultMessage = await toDoService.AddDtoAsync("api/todo/add", new TodoDto
                        {
                            Uid = App.Current.Uid,
                            Title = Title,
                            Content = Content,
                            Status = Status
                        }, TodoDtos);
                        break;
                    default:
                        resultMessage = await toDoService.UpdateDtoAsync("api/todo/update", new TodoDto
                        {
                            Id = DtoId,
                            Uid = App.Current.Uid,
                            Title = Title,
                            Content = Content,
                            Status = Status
                        }, TodoDtos);
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
        private async Task DeleteToDo(TodoDto dto)
        {
            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                resultMessage = await toDoService.DeleteDtoAsync("api/todo/delete", App.Current.Uid, dto.Id, TodoDtos);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        private async Task LoadTodosAsync(QueryParameter? queryParameter = null)
        {
            try
            {
                uIStateService.IsLoadingVisible = true;
                if (queryParameter != null)
                {
                    await toDoService.GetDtosAsync("api/todo/getallbyqueryparameter", App.Current.Uid, TodoDtos, queryParameter: queryParameter);
                }
                else
                {
                    await toDoService.GetDtosAsync("api/todo/getall", App.Current.Uid, TodoDtos);
                }
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
            }
        }
    }
}
