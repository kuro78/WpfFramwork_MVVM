using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVMFramework.Models;

/// <summary>
/// Customer 모델
/// </summary>
public partial class Customer : ObservableObject
{
    public string CustomerID;
    [ObservableProperty]
    private string _CompanyName;
    [ObservableProperty]
    private string _ContactName;
    [ObservableProperty]
    private string _ContactTitle;
    [ObservableProperty]
    private string _Address;
    [ObservableProperty]
    private string _City;
    [ObservableProperty]
    private string _Region;
    [ObservableProperty]
    private string _PostalCode;
    [ObservableProperty]
    private string _Country;
    [ObservableProperty]
    private string _Phone;
    [ObservableProperty]
    private string _Fax;
}
