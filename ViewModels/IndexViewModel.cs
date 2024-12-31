using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using MyToDo.Common.Models;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;
using MyToDo.Shared.Dtos;
using MyToDo.ViewModels.Dialogs;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public partial class IndexViewModel : ViewModelBase
    {
        private readonly ToDoService toDoService;
        private readonly MemoService memoService;
        private readonly IDialogService dialogService;
        private readonly UIStateService uIStateService;

        public IndexViewModel(ToDoService toDoService, MemoService memoService, IDialogService dialogService, UIStateService uIStateService)
        {
            this.toDoService = toDoService;
            this.memoService = memoService;
            this.dialogService = dialogService;
            this.uIStateService = uIStateService;

            InitializeTaskBars();

            Task.Run(async () =>
            {
                try
                {
                    uIStateService.IsLoadingVisible = true;
                    await LoadTodosAsync();
                    await LoadMemosAsync();
                    UpdateTaskBarsContent();
                }
                finally
                {
                    uIStateService.IsLoadingVisible = false;
                }

            }).ConfigureAwait(false);
        }

        #region 属性
        [ObservableProperty]
        private string welcomeMessage = $"你好，{App.Current.Username}！今天是" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("dddd");

        [ObservableProperty]
        private ObservableCollection<TaskBarModel>? taskBars;

        [ObservableProperty]
        private ObservableCollection<TodoDto> todoDtos = [];

        [ObservableProperty]
        private ObservableCollection<MemoDto> memoDtos = [];

        [ObservableProperty]
        private TaskBarModel? totalBar;

        [ObservableProperty]
        private TaskBarModel? finishedBar;

        [ObservableProperty]
        private TaskBarModel? finishRateBar;

        [ObservableProperty]
        private TaskBarModel? memoBar;
        #endregion

        [RelayCommand]
        private async Task AddToDo()
        {
            var dialogViewModel = new AddToDoDialogViewModel();

            string? resultMessage = null;
            try
            {
                uIStateService.IsMaskVisible = true;

                var success = dialogService.ShowDialog(this, dialogViewModel);
                if (success == true)
                {
                    uIStateService.IsLoadingVisible = true;
                    resultMessage = await toDoService.AddDtoAsync("api/todo/add", new TodoDto
                    {
                        Uid = App.Current.Uid,
                        Title = dialogViewModel.Title!,
                        Content = dialogViewModel.Content,
                        Status = dialogViewModel.Status
                    }, TodoDtos);
                }
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                uIStateService.IsMaskVisible = false;
                UpdateTaskBarsContent();
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        [RelayCommand]
        private async Task AddMemo()
        {
            var dialogViewModel = new AddMemoDialogViewModel();

            string? resultMessage = null;
            try
            {
                uIStateService.IsMaskVisible = true;

                var success = dialogService.ShowDialog(this, dialogViewModel);
                if (success == true)
                {
                    uIStateService.IsLoadingVisible = true;
                    resultMessage = await memoService.AddDtoAsync("api/memo/add", new MemoDto
                    {
                        Uid = App.Current.Uid,
                        Title = dialogViewModel.Title!,
                        Content = dialogViewModel.Content,
                    }, MemoDtos);
                }
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                uIStateService.IsMaskVisible = false;
                UpdateTaskBarsContent();
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        [RelayCommand]
        private async Task SwitchToDoStatus(TodoDto todoDto)
        {
            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                resultMessage = await toDoService.UpdateDtoAsync("api/todo/update", todoDto, TodoDtos);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                UpdateTaskBarsContent();
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);
            }
        }

        private async Task LoadTodosAsync()
            => await toDoService.GetDtosAsync("api/todo/getall", App.Current.Uid, TodoDtos);

        private async Task LoadMemosAsync()
            => await memoService.GetDtosAsync("api/memo/getall", App.Current.Uid, MemoDtos);

        private void UpdateTaskBarsContent()
        {
            var totalNum = TodoDtos.Count;
            var finishedNum = TodoDtos.Where(x => x.Status == 1).Count();

            TotalBar!.Content = totalNum.ToString();
            FinishedBar!.Content = finishedNum.ToString();
            if (totalNum > 0)
                FinishRateBar!.Content = $"{((double)finishedNum / totalNum):P2}";

            MemoBar!.Content = MemoDtos.Count.ToString();
        }

        private void InitializeTaskBars()
        {
            TotalBar = new TaskBarModel { Color = "#FF0CA0FF", Icon = "ClockFast", Name = "汇总", Content = "0" };
            FinishedBar = new TaskBarModel { Color = "#FF1ECA3A", Icon = "ClockCheckOutline", Name = "已完成", Content = "0" };
            FinishRateBar = new TaskBarModel { Color = "#FF02C6DC", Icon = "ChartLineVariant", Name = "完成率", Content = "0.00%" };
            MemoBar = new TaskBarModel { Color = "#FFFFA000", Icon = "PlaylistStar", Name = "备忘录", Content = "0" };

            TaskBars = [TotalBar, FinishedBar, FinishRateBar, MemoBar];
        }
    }
}
