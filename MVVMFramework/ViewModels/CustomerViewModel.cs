using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MVVMFramework.Bases;
using MVVMFramework.Models;
using MVVMFramework.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMFramework.ViewModels;


public partial class CustomerViewModel : ViewModelBase
{
    private readonly IDatabaseService _dbService;

    [ObservableProperty]
    private IList<Customer> _Customers;

    [ObservableProperty]
    private Customer _SelectedCustomer;

    [ObservableProperty]
    private string _ErrorMessage;

    public IRelayCommand SaveCommand { get; set; }

    public CustomerViewModel(IDatabaseService databaseService) 
    {
        _dbService = databaseService;
        Init();
    }

    private void Init()
    {
        Title = "Customer";
        
        SaveCommand = new RelayCommand(Save,
                        () => Customers != null
                        && Customers.Any(c => string.IsNullOrWhiteSpace(c?.CustomerID)));

        PropertyChanging += CustomerViewModel_PropertyChanging;
        PropertyChanged += CustomerViewModel_PropertyChanged;
    }

    private void CustomerViewModel_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(SelectedCustomer):
                if (SelectedCustomer != null)
                {
                    SelectedCustomer.ErrorsChanged -= SelectedCustomer_ErrorChanged;
                    ErrorMessage = string.Empty;
                }
                break;
        }
    }

    private void CustomerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(SelectedCustomer):
                if (SelectedCustomer != null)
                {
                    SelectedCustomer.ErrorsChanged += SelectedCustomer_ErrorChanged;
                    SetErrorMessage(SelectedCustomer);
                }
                break;
        }
    }

    private void SelectedCustomer_ErrorChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        SetErrorMessage(sender as Customer);
    }

    private void SetErrorMessage(Customer customer)
    {
        if (customer == null)
        {
            return;
        }

        var errors = customer.GetErrors();
        ErrorMessage = string.Join("\n", errors.Select(e => e.ErrorMessage));
    }

    private void Save()
    {
        MessageBox.Show("Save");
        SaveCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// AddCommand
    /// </summary>
    [RelayCommand]
    private void Add()
    {
        var newCustomer = new Customer();
        Customers.Insert(0, newCustomer);
        SelectedCustomer = newCustomer;
        SaveCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// BackCommand
    /// </summary>
    [RelayCommand]
    private void Back()
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
