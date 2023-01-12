using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using MVVMFramework.Interfaces;

namespace MVVMFramework.Behaviors;

/// <summary>
/// Frame 비헤이비어
/// </summary>
public class FrameBehavior : Behavior<Frame>
{
    /// <summary>
    /// NavigationSource DP 변경 때문에 발생하는 프로퍼티 체인지 이벤트를 막기 위해 사용
    /// </summary>
    private bool _IsWork;

    protected override void OnAttached()
    {
        // 네비게이션 시작
        AssociatedObject.Navigating += AssociatedObject_Navigating;
        // 네비게이션 종료
        AssociatedObject.Navigated += AssociatedObject_Navigated;
    }

    /// <summary>
    /// 네비게이션 종료 이벤트 핸들러
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AssociatedObject_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        _IsWork = true;
        // 네비게이션이 완료된 Uri를 NavigationSource에 입력
        NavigationSource = e.Uri.ToString();
        _IsWork= false;
        // 네비게이션이 완료된 상황을 뷰모델에 알려주기
        if (AssociatedObject.Content is Page pageContent
            && pageContent.DataContext is INavigationAware navigationAware)
        {
            navigationAware.OnNavigated(sender, e);
        }
    }

    /// <summary>
    /// 네비게이션 시작 이벤트 핸들러
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AssociatedObject_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
    {
        // 네비게이션 시작전 상황을 뷰모델에 알려주기
        if (AssociatedObject.Content is Page pageContent
            && pageContent.DataContext is INavigationAware navigationAware)
        {
            navigationAware?.OnNavigating(sender, e);
        }
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Navigating -= AssociatedObject_Navigating;
        AssociatedObject.Navigated -= AssociatedObject_Navigated;
    }

    public string NavigationSource
    {
        get { return (string)GetValue(NavigationSourceProperty); }
        set { SetValue(NavigationSourceProperty, value); }
    }

    /// <summary>
    /// NavigationSource DP
    /// </summary>
    public static readonly DependencyProperty NavigationSourceProperty =
        DependencyProperty.Register(nameof(NavigationSource), typeof(string), typeof(FrameBehavior), new PropertyMetadata(null, NavigationSourceChanged));

    /// <summary>
    /// NavigationSource PropertyChanged
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void NavigationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        var behavior = (FrameBehavior)d;
        if(behavior._IsWork)
        {
            return;
        }
        behavior.Navigate();
    }
    
    private void Navigate()
    {
        switch(NavigationSource)
        {
            case "GoBack":
                // GoBack으로 오면 뒤로가기
                if(AssociatedObject.CanGoBack)
                {
                    AssociatedObject.GoBack();
                }
                break;
            case null:
            case "":
                // 아무것도 안함
                return;
            default:
                // navigate
                AssociatedObject.Navigate(new Uri(NavigationSource, UriKind.RelativeOrAbsolute));
                break;
        }
    }
}
