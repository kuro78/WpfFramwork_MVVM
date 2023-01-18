using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using MVVMFramework.Models;

namespace MVVMFramework.Controls;

/// <summary>
/// AboutControl.xaml에 대한 상호 작용 논리
/// </summary>
public partial class AboutControl : UserControl
{
    public AboutControl()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(false), "TEST1");
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(false), "TEST2");
    }
}
