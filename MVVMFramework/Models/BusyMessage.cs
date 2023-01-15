using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MVVMFramework.Models;

/// <summary>
/// 비지 메시지
/// </summary>
public class BusyMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// BusyId
    /// </summary>
    public string BusyID { get; set; }
    /// <summary>
    /// BusyText
    /// </summary>
    public string BusyText { get; set; }

    public BusyMessage(bool value) : base(value)
    { }
}
