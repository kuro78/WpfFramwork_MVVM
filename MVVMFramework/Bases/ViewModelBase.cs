
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVMFramework.Bases;

/// <summary>
/// ViewModel 베이스
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
    private string _Title;
    /// <summary>
    /// 타이틀
    /// </summary>
    public string Title
    { 
        get { return _Title; } 
        set { SetProperty(ref _Title, value); } 
    }
}
