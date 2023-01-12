using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;


public class CustomerViewModel : ViewModelBase
{
    public ICommand BackCommand { get; set; }

    public CustomerViewModel() 
    {
        Init();
    }

    private void Init()
    {
        Title = "Customer";
        BackCommand = new RelayCommand(OnBack);
    }


    private void OnBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("GoBack"));
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Message = "Navigated";
    }
}
