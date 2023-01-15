using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MVVMFramework.Models;

/// <summary>
/// 레이어 팝업 메시지
/// </summary>
public class LayerPopupMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// 컨트롤 이름
    /// </summary>
    public string ControlName { get; set; }
    /// <summary>
    /// 컬트롤에 전달할 파라미터
    /// </summary>
    public object parameter { get; set; }
    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="value">true : 레이어팝업 오픈, false : 레이어 팝업 닫기</param>
    public LayerPopupMessage(bool value) : base(value)
    { }
}
