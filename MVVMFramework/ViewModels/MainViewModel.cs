using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;

/// <summary>
/// 메인 뷰모델 클래스
/// </summary>
public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Busy 목록
    /// </summary>
    private IList<BusyMessage> _busyMessages = new List<BusyMessage>();
    
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
    /// 비지 표시 여부
    /// </summary>
    private bool _IsBusy;
    public bool IsBusy
    {
        get { return _IsBusy; }
        set { SetProperty(ref _IsBusy, value); }
    }

    /// <summary>
    /// 레이어 팝업 출력 여부
    /// </summary>
    private bool _ShowLayerPopup;
    public bool ShowLayerPopup
    {
        get { return _ShowLayerPopup; }
        set { SetProperty(ref _ShowLayerPopup, value); }
    }

    /// <summary>
    /// 레이어 팝업 내부 컨트롤 이름
    /// </summary>
    private string _ControlName;
    public string ControlName
    {
        get { return _ControlName; }
        set { SetProperty(ref _ControlName, value); }
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

        // BusyMessage 수신 등록
        WeakReferenceMessenger.Default.Register<BusyMessage>(this, OnBusyMessage);

        // LayerPopupMessage 수신 등록
        WeakReferenceMessenger.Default.Register<LayerPopupMessage>(this, OnLayerPopupMessage);
    }

    private void OnLayerPopupMessage(object sender, LayerPopupMessage message)
    {
        ShowLayerPopup = message.Value;
        ControlName = message.ControlName;
    }
    
    /// <summary>
    /// 비지 메시지 수신 처리
    /// </summary>
    /// <param name="recipient"></param>
    /// <param name="message"></param>
    private void OnBusyMessage(object recipient, BusyMessage message)
    {
        if (message.Value)
        {
            var existsBusy = _busyMessages.FirstOrDefault(b => b.BusyID == message.BusyID);
            if (existsBusy != null)
            {
                // 이미 추가된 녀석이기 때문에 추가하지 않음
                return;
            }

            _busyMessages.Add(message);
        }
        else
        {
            var existValue = _busyMessages.FirstOrDefault(b => b.BusyID == message.BusyID);
            if (existValue == null)
            {
                // 없기 때문에 나감
                return;
            }
            _busyMessages.Remove(existValue);
        }
        // _busyMessage에 아이템이 있으면 true, 없으면 false
        IsBusy = _busyMessages.Any();
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
