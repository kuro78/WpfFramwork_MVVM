using MVVMFramework.Bases;

namespace MVVMFramework.ViewModels;


public class HomeViewModel : ViewModelBase
{
    public static int Count { get; set; }

    public HomeViewModel() 
    {
        Title = "Home";
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Count++;
        Message = $"{Count} Navigated";
    }
}
