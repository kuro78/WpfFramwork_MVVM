using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using MVVMFramework.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;


public partial class CustomerViewModel : ViewModelBase
{
    private readonly IDatabaseService _dbService;

    [ObservableProperty]
    private IList<Customer> _Customers;

    public ICommand BackCommand { get; set; }

    public CustomerViewModel(IDatabaseService databaseService) 
    {
        _dbService = databaseService;
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

    public override async void OnNavigated(object sender, object navigatedEventArgs)
    {
        Message = "Navigated";

        var datas = await _dbService.GetDatasAsync<Customer>("select * from [Customers]");
        Customers = datas;
    }
}
