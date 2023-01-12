using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;

/// <summary>
/// 메인 뷰모델 클래스
/// </summary>
public class MainViewModel : ViewModelBase
{
    private string _NavigationSource;
    /// <summary>
    /// 네비게이션 소스
    /// </summary>
    public string NavigationSource
    {
        get { return _NavigationSource; }
        set { SetProperty(ref _NavigationSource, value); }
    }

    /// <summary>
    /// 네비게이트 커맨드
    /// </summary>
    public ICommand NavigateCommand { get; set; }
    
    /// <summary>
    /// 생성자
    /// </summary>
    public MainViewModel() 
    {
        Title = "Main View";
        Init();
    }

    private void Init()
    {
        // 시작 페이지 설정
        NavigationSource = "Views/HomePage.xaml";
        NavigateCommand = new RelayCommand<string>(OnNavigate);

        // 네비게이션 메시지 수신 등록
        WeakReferenceMessenger.Default.Register<NavigationMessage>(this, OnNavigationMessage);
    }

    /// <summary>
    /// 네비게이션 메시지 수신 처리
    /// </summary>
    /// <param name="recipent"></param>
    /// <param name="message"></param>
    private void OnNavigationMessage(object recipent, NavigationMessage message)
    {
        NavigationSource = message.Value;
    }

    private void OnNavigate(string pageUri)
    {
        NavigationSource = pageUri;
    }
}
