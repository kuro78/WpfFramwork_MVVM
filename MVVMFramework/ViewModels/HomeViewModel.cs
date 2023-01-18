using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;


public class HomeViewModel : ViewModelBase
{
    public static int Count { get; set; }

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
    /// Busy 테스트 커맨드
    /// </summary>
    public ICommand BusyTestCommand { get; set; }
    /// <summary>
    /// Layer Popup 테스트 커맨드
    /// </summary>
    public ICommand LayerPopupTest1Command { get; set; }
    /// <summary>
    /// Layer Popup 테스트 커맨드
    /// </summary>
    public ICommand LayerPopupTest2Command { get; set; }

    public HomeViewModel() 
    {
        Title = "Home";

        Init();
    }

    private void Init()
    {
        BusyTestCommand = new AsyncRelayCommand(OnBusyTestAsync);
        LayerPopupTest1Command = new RelayCommand(OnLayerPopupTest1);
        LayerPopupTest2Command = new RelayCommand(OnLayerPopupTest2);

        WeakReferenceMessenger.Default.Register<LayerPopupMessage, string>(this, "TEST2", OnLayerPopupMessage);
    }

    private void OnLayerPopupMessage(object sender, LayerPopupMessage message)
    {
        ShowLayerPopup = message.Value;
        ControlName = message.ControlName;
    }

    public void OnLayerPopupTest1()
    {
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(true) { ControlName = "AboutControl" }, "TEST1");
    }

    public void OnLayerPopupTest2()
    {
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(true) { ControlName = "AboutControl" }, "TEST2");
    }


    private async Task OnBusyTestAsync()
    {
        WeakReferenceMessenger.Default.Send(new BusyMessage(true) { BusyID = "OnBusyTestAsync" });
        await Task.Delay(5000);
        WeakReferenceMessenger.Default.Send(new BusyMessage(false) { BusyID = "OnBusyTestAsync" });
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Count++;
        Message = $"{Count} Navigated";
    }
}
