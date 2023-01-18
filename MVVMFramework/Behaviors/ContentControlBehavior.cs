using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MVVMFramework.Behaviors;

/// <summary>
/// ContentControlBehavior
/// </summary>
public class ContentControlBehavior : Behavior<ContentControl>
{
    public string ControlName
    {
        get { return (string)GetValue(ControlNameProperty); }
        set { SetValue(ControlNameProperty, value); }
    }

    /// <summary>
    /// ControlName DP
    /// </summary>
    public static readonly DependencyProperty ControlNameProperty =
        DependencyProperty.Register(nameof(ControlName), typeof(string), typeof(ContentControlBehavior), new PropertyMetadata(null, ControlNameChanged));

    private static void ControlNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (ContentControlBehavior)d;
        behavior.ResolveControl();
    }

    /// <summary>
    /// ControlName을 이용해서 컨트를 인스턴스 시켜서 사용
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ResolveControl()
    {
        if (string.IsNullOrEmpty(ControlName))
        {
            AssociatedObject.Content = null;
        }
        else
        {
            // Gettype을 이용하기 위해서 AssemblyQualifiedName이 필요합니다.
            // 예) typeof(AboutControl).AssemblyQualifiedName
            // 다른 클래스라이브러리에 있는 컨트롤도 이름만 알면 만들 수 있습니다.
            var type = Type.GetType($"MVVMFramework.Controls.{ControlName}, MVVMFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (type == null) 
            {
                return;
            }

            var control = App.Current.Services.GetService(type);
            AssociatedObject.Content = control;
        }
    }

    public bool ShowLayerPopup
    {
        get { return (bool)GetValue(ShowLayerPopupProperty); }
        set { SetValue(ShowLayerPopupProperty, value); }
    }

    /// <summary>
    /// ShowLayerPopup DP
    /// </summary>
    public static readonly DependencyProperty ShowLayerPopupProperty =
        DependencyProperty.Register(nameof(ShowLayerPopup), typeof(bool), typeof(ContentControlBehavior), new PropertyMetadata(false, ShowLayerPopupChanged));

    private static void ShowLayerPopupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        var behavior = (ContentControlBehavior)d;
        behavior.CheckShowLayerPopup();
    }


    private void CheckShowLayerPopup()
    {
        if (ShowLayerPopup == false)
        {
            AssociatedObject.Content = null;
        }
    }
}
