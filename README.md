# MyToDo WPF Ӧ�ó���

## ��Ŀ����
MyToDo ��һ������ WPF �����Ĵ�����������ߣ����� MVVM �ܹ�ģʽ����������Ŀּ���ṩһ����ࡢ��Ч�Ĵ�������ͱ���¼������������

## ����ջ
- .NET 8.0
- WPF (Windows Presentation Foundation)
- CommunityToolkit.Mvvm (MVVM���)
- MaterialDesignThemes (UI���)
- Microsoft.Extensions.DependencyInjection (����ע��)
- MvvmDialogs (�Ի������)

## ��Ŀ�ṹ
```
MyToDo/
������ Assets/ # ��Դ�ļ�
�� ������ Images/ # ͼƬ��Դ
��
������ Common/ # �������
�� ������ Converters/ # ֵת����
�� ������ Models/ # ǰ��ģ��
��
������ Services/ # �����
�� ������ Core/ # ���ķ���
�� �� ������ HttpClientService.cs
�� �� ������ NavigationService.cs
�� �� ������ UIStateService.cs
�� ��
�� ������ Implementations/ # ����ʵ��
�� �� ������ BaseService.cs
�� �� ������ ToDoService.cs
�� �� ������ MemoService.cs
�� ��
�� ������ Interfaces/ # ����ӿ�
��   ������ IBaseService.cs
������ ViewModels/ # ��ͼģ��
�� ������ Dialogs/ # �Ի�����ͼģ��
�� ������ [��ҳ��ViewModel]
��
������ Views/ # ��ͼ
������ Dialogs/ # �Ի�����ͼ
������ [��ҳ��View]
```

## ��Ŀ�����

### 1. ������Ŀ
1. ʹ�� Visual Studio 2022 ���� WPF Ӧ�ó���
2. ѡ�� .NET 8.0 ���
3. ���������������Ŀ

### 2. ��װNuGet��

1. ��װ CommunityToolkit.Mvvm
2. ��װ MaterialDesignThemes
3. ��װ Microsoft.Extensions.DependencyInjection
4. ��װ MvvmDialogs
5. ��װ XamlAnimatedGif

### 3. ������Ŀ�ṹ
1. ��������Ŀ¼�ṹ
2. ���� MaterialDesign ����

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

### 4. ��������ע��
�� App.xaml.cs �����÷���:

```csharp
public App()
{
var container = new ServiceCollection();
// ע����ķ���
container.AddSingleton<NavigationService>();
container.AddSingleton<UIStateService>();
// ע��ҵ�����
container.AddSingleton<ToDoService>();
container.AddSingleton<MemoService>();
// ע����ͼ����ͼģ��
container.AddSingleton<MainView>();
container.AddSingleton<MainViewModel>();
Services = container.BuildServiceProvider();
}
```

### 5. ʵ�ֻ�������
1. ʵ�� NavigationService ����ҳ�浼��
2. ʵ�� HttpClientService ���� HTTP ����
3. ʵ�� UIStateService ����UI״̬

### 6. ������ͼ����ͼģ��
1. ����������ͼ(MainView, LoginView��)
2. ʵ�ֶ�Ӧ��ViewModel
3. ʹ�� CommunityToolkit.Mvvm ���Լ򻯿���

## ����ʵ��˵��

### Snackbar��Ϣ��ʾʵ��
Snackbar������ʾ��̵���ʾ��Ϣ��ʵ��˼·���£�

1. XAML�ж���Snackbar�ؼ�
```xaml
<materialDesign:Snackbar
    x:Name="MainSnackbar"
    Margin="20"
    MessageQueue="{Binding MessageQueue}" />
```

2. ViewModel��������Ϣ����
```csharp
// ������Ϣ��ʾʱ��Ϊ2��
MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
```

3. ʹ��UIStateService������Ϣ״̬
```csharp
public class UIStateService : INotifyPropertyChanged
{
    private string? snacbarMessage;
    
    // Snackbar��Ϣ����
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

    // ������Ϣ�ķ���
    public void SetSnacbarMessage(string? message)
    {
        SnacbarMessage = message;
    }
}
```

4. ������Ϣ�仯����ʾ
```csharp
// ����UIStateService�����Ա仯�¼�
uIStateService.PropertyChanged += OnUIStateServicePropertyChanged;

// ������Ϣ�仯
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

ʵ�����̣�
1. ͨ��UIStateService������Ϣ
2. PropertyChanged�¼�����
3. ��Ϣ����MaterialDesign��Snackbar����
4. Snackbar��ʾ��Ϣ2����Զ���ʧ

ʹ��ʾ����
```csharp
// ����Ҫ��ʾ��ʾ�ĵط�����
uIStateService.SetSnacbarMessage("�����ɹ���");
```