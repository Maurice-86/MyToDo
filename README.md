# MyToDo WPF 应用程序

## 项目介绍
MyToDo 是一个基于 WPF 开发的待办事项管理工具，采用 MVVM 架构模式开发。本项目旨在提供一个简洁、高效的待办事项和备忘录管理解决方案。

## 技术栈
- .NET 8.0
- WPF (Windows Presentation Foundation)
- CommunityToolkit.Mvvm (MVVM框架)
- MaterialDesignThemes (UI框架)
- Microsoft.Extensions.DependencyInjection (依赖注入)
- MvvmDialogs (对话框服务)

## 项目结构
```
MyToDo/
├── Assets/ # 资源文件
│ └── Images/ # 图片资源
│
├── Common/ # 公共组件
│ ├── Converters/ # 值转换器
│ └── Models/ # 前端模型
│
├── Services/ # 服务层
│ ├── Core/ # 核心服务
│ │ ├── HttpClientService.cs
│ │ ├── NavigationService.cs
│ │ └── UIStateService.cs
│ │
│ ├── Implementations/ # 服务实现
│ │ ├── BaseService.cs
│ │ ├── ToDoService.cs
│ │ └── MemoService.cs
│ │
│ └── Interfaces/ # 服务接口
│   ├── IBaseService.cs
├── ViewModels/ # 视图模型
│ ├── Dialogs/ # 对话框视图模型
│ └── [各页面ViewModel]
│
└── Views/ # 视图
├── Dialogs/ # 对话框视图
└── [各页面View]
```

## 项目搭建步骤

### 1. 创建项目
1. 使用 Visual Studio 2022 创建 WPF 应用程序
2. 选择 .NET 8.0 框架
3. 创建解决方案和项目

### 2. 安装NuGet包

1. 安装 CommunityToolkit.Mvvm
2. 安装 MaterialDesignThemes
3. 安装 Microsoft.Extensions.DependencyInjection
4. 安装 MvvmDialogs
5. 安装 XamlAnimatedGif

### 3. 配置项目结构
1. 创建基础目录结构
2. 配置 MaterialDesign 主题

```xaml
<Application.Resources>
<ResourceDictionary>
<ResourceDictionary.MergedDictionaries>
<materialDesign:BundledTheme BaseTheme="Light" PrimaryColor="DeepPurple" SecondaryColor="Lime" />
<ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign2.Defaults.xaml" />
</ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
</Application.Resources>
```

### 4. 配置依赖注入
在 App.xaml.cs 中配置服务:

```csharp
public App()
{
var container = new ServiceCollection();
// 注册核心服务
container.AddSingleton<NavigationService>();
container.AddSingleton<UIStateService>();
// 注册业务服务
container.AddSingleton<ToDoService>();
container.AddSingleton<MemoService>();
// 注册视图和视图模型
container.AddSingleton<MainView>();
container.AddSingleton<MainViewModel>();
Services = container.BuildServiceProvider();
}
```

### 5. 实现基础服务
1. 实现 NavigationService 用于页面导航
2. 实现 HttpClientService 处理 HTTP 请求
3. 实现 UIStateService 管理UI状态

### 6. 开发视图和视图模型
1. 创建基础视图(MainView, LoginView等)
2. 实现对应的ViewModel
3. 使用 CommunityToolkit.Mvvm 特性简化开发

## 功能实现说明

### Snackbar消息提示实现
Snackbar用于显示简短的提示信息，实现思路如下：

1. XAML中定义Snackbar控件
```xaml
<materialDesign:Snackbar
    x:Name="MainSnackbar"
    Margin="20"
    MessageQueue="{Binding MessageQueue}" />
```

2. ViewModel中配置消息队列
```csharp
// 设置消息显示时长为2秒
MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
```

3. 使用UIStateService管理消息状态
```csharp
public class UIStateService : INotifyPropertyChanged
{
    private string? snacbarMessage;
    
    // Snackbar消息属性
    public string? SnacbarMessage
    {
        get => snacbarMessage;
        set
        {
            if (snacbarMessage != value)
            {
                snacbarMessage = value;
                OnPropertyChanged();
            }
        }
    }

    // 设置消息的方法
    public void SetSnacbarMessage(string? message)
    {
        SnacbarMessage = message;
    }
}
```

4. 监听消息变化并显示
```csharp
// 订阅UIStateService的属性变化事件
uIStateService.PropertyChanged += OnUIStateServicePropertyChanged;

// 处理消息变化
private void OnUIStateServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
{
    if (e.PropertyName == nameof(UIStateService.SnacbarMessage))
    {
        var message = uIStateService.SnacbarMessage;
        if (message != null)
        {
            MessageQueue.Enqueue(message);
            uIStateService.SnacbarMessage = null;
        }
    }
}
```

实现流程：
1. 通过UIStateService设置消息
2. PropertyChanged事件触发
3. 消息进入MaterialDesign的Snackbar队列
4. Snackbar显示消息2秒后自动消失

使用示例：
```csharp
// 在需要显示提示的地方调用
uIStateService.SetSnacbarMessage("操作成功！");
```