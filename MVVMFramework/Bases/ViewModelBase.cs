
using CommunityToolkit.Mvvm.ComponentModel;
using MVVMFramework.Interfaces;

namespace MVVMFramework.Bases;

/// <summary>
/// ViewModel 베이스
/// </summary>
public abstract class ViewModelBase : ObservableObject, INavigationAware
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

    private string _Message;
    /// <summary>
    /// 메시지
    /// </summary>
    public string Message
    {
        get { return _Message; }
        set { SetProperty(ref _Message, value);}
    }

    /// <summary>
    /// 네비게이션 완료시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="navigatedEventArgs"></param>
    public virtual void OnNavigated(object sender, object navigatedEventArgs)
    { }

    /// <summary>
    /// 네비게이션 시작시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="navigatingEventArgs"></param>
    public virtual void OnNavigating(object sender, object navigatingEventArgs)
    { }
}
