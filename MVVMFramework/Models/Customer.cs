using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVVMFramework.Models;

/// <summary>
/// Customer 모델
/// </summary>
public class Customer : ObservableValidator
{
    private string _CustomerID;
    [Required]
    [MaxLength(5)]
    public string CustomerID
    {
        get { return _CustomerID; }
        set { SetProperty(ref _CustomerID, value, true); }
    }
    
    private string _CompanyName;
    [Required]
    [MaxLength(40)]
    public string CompanyName
    {
        get { return _CompanyName; }
        set { SetProperty(ref _CompanyName, value, true); }
    }
    
    private string _ContactName;
    [MaxLength(30)]
    public string ContactName
    {
        get { return _ContactName; }
        set { SetProperty(ref _ContactName, value, true); }
    }

    private string _ContactTitle;
    [MaxLength(30)]
    public string ContactTitle
    {
        get { return _ContactTitle; }
        set { SetProperty(ref _ContactTitle, value, true); }
    }

    private string _Address;
    [MaxLength(60)]
    public string Address
    {
        get { return _Address; }
        set { SetProperty(ref _Address, value, true); }
    }

    private string _City;
    [MaxLength(15)]
    public string City
    {
        get { return _City; }
        set { SetProperty(ref _City, value, true); }
    }
    
    private string _Region;
    [MaxLength(15)]
    public string Region
    {
        get { return _Region; }
        set { SetProperty(ref _Region, value, true); }
    }
    
    private string _PostalCode;
    [MaxLength(10)]
    public string PostalCode
    {
        get { return _PostalCode; }
        set { SetProperty(ref _PostalCode, value, true); }
    }
    
    private string _Country;
    [MaxLength(15)]
    public string Country
    {
        get { return _Country; }
        set { SetProperty(ref _Country, value, true); }
    }
    
    private string _Phone;
    [MaxLength(24)]
    [Phone]
    public string Phone
    {
        get { return _Phone; }
        set { SetProperty(ref _Phone, value, true); }
    }
    
    private string _Fax;
    [MaxLength(24)]
    [Phone]
    public string Fax
    {
        get { return _Fax; }
        set { SetProperty(ref _Fax, value, true); }
    }
}
