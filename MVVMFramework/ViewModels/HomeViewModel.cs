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
    /// Busy 테스트 커맨드
    /// </summary>
    public ICommand BusyTestCommand { get; set; }
    /// <summary>
    /// Layer Popup 테스트 커맨드
    /// </summary>
    public ICommand LayerPopupTestCommand { get; set; }

    public HomeViewModel() 
    {
        Title = "Home";

        Init();
    }

    private void Init()
    {
        BusyTestCommand = new AsyncRelayCommand(OnBusyTestAsync);
        LayerPopupTestCommand = new RelayCommand(OnLayerPopupTest);
    }

    public void OnLayerPopupTest()
    {
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(true) { ControlName = "AboutControl" });
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
