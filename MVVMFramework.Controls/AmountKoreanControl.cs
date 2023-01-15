using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMFramework.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MVVMFramework.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MVVMFramework.Controls;assembly=MVVMFramework.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = _textBlockName, Type = typeof(TextBlock))]
    [TemplatePart(Name = _textBoxName, Type = typeof(TextBox))]
    public class AmountKoreanControl : Control
    {
        // AmountKoreanControl 컨트롤에서 제어할 컨트롤들의 이름
        private const string _textBlockName = "PART_KoreanDisplay";
        private const string _textBoxName = "PART_Amount";
        // AmountKoreanControl 컨트롤에서 제어할 컨트롤들
        private TextBlock _koreanDisplayTextBlock;
        private TextBox _amountTextBox;
        private bool _isWork;

        static AmountKoreanControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AmountKoreanControl), new FrameworkPropertyMetadata(typeof(AmountKoreanControl)));
        }

        /// <summary>
        /// 커스텀 컨트롤에 템플릿이 적용될때
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public override void OnApplyTemplate()
        {
            // 커스텀 컨트롤 각 PART를 내부에서 사용할 수 있도록 가져옴
            _koreanDisplayTextBlock = GetTemplateChild(_textBlockName) as TextBlock;
            _amountTextBox = GetTemplateChild(_textBoxName) as TextBox;

            if (_amountTextBox == null || _koreanDisplayTextBlock == null)
            {
                throw new NullReferenceException("컨트롤의 PART를 찾을 수 없습니다.");
            }

            _amountTextBox.TextChanged += AmountTextBox_TextChanged;
            _amountTextBox.PreviewKeyDown += AmountTextBox_PreviewKeyDown;
        }

        /// <summary>
        /// 키다운 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountTextBox_PreviewKeyDown(object sender, KeyEventArgs e) 
        {
            bool isDigit = false;
            switch ((int)e.Key)
            {
                // D0~D9 NumPad0~9 74~83
                case int n when ((34 <= n && 43 >= n) || (74 <= n && 83 >= n)):
                    isDigit = true;
                    break;
            }

            if (!(isDigit || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 텍스트 체인지 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            // 중복실행방지
            if (_isWork)
            {
                return;
            }

            _isWork = true;

            // 전처리
            var numberTextOnly = _amountTextBox.Text.Trim().Replace(",", "");
            if(decimal.TryParse(numberTextOnly, out decimal decimalValue))
            {
                DecimalToFormatString(decimalValue);
            }
            else
            {
                Amount = 0;
                _koreanDisplayTextBlock.Text = "";
            }

            _isWork = false;
        }

        /// <summary>
        /// 데시멀을 포맷스트링으로 입력과 한글 금액 출력
        /// </summary>
        /// <param name="decimalValue"></param>
        private void DecimalToFormatString(decimal decimalValue)
        {
            // DP 변경 이벤트로 호출되는 경우 초기화 전에 들어오는 것 방지
            if (_amountTextBox == null)
            {
                return;
            }

            // StringFormat 출력
            _amountTextBox.Text = string.Format("{0:#,##0}", decimalValue);
            // 캐럿을 맨뒤로
            _amountTextBox.SelectionStart = _amountTextBox.Text.Length;


            // DP에 값 입력
            Amount = decimalValue;
            var korean = Number2Hangle(Amount);
            _koreanDisplayTextBlock.Text = korean;
        }

        /// <summary>
        /// 컨트롤 종료자
        /// </summary>
        ~AmountKoreanControl()
        {
            _amountTextBox.TextChanged -= AmountTextBox_TextChanged;
            _amountTextBox.PreviewKeyDown -= AmountTextBox_PreviewKeyDown;
        }

        /// <summary>
        /// 숫자를 한글로 - 이 부분은 따로 Utility로 빼서 사용하는 것이 더 좋을 듯
        /// </summary>
        /// <param name="lngNumber"></param>
        /// <returns></returns>
        private string Number2Hangle(decimal lngNumber)
        {
            string sign = "";
            string[] numberChar = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            string[] levelChar = new string[] { "", "십", "백", "천" };
            string[] decimalChar = new string[] { "", "만", "억", "조", "경" };

            string strvalue = string.Format("{0}", lngNumber);
            string numToKorea = sign;
            bool useDecimal = false;

            for(int i = 0; i < strvalue.Length; i++)
            {
                int level = strvalue.Length - i;
                if (strvalue.Substring(i, 1) != "0")
                {
                    useDecimal = true;
                    if (((level - 1) % 4) == 0)
                    {
                        numToKorea = numToKorea + numberChar[int.Parse(strvalue.Substring(i, 1))] + decimalChar[(level - 1) / 4];
                        useDecimal = false;
                    }
                    else
                    {
                        if (strvalue.Substring(i, 1) == "1")
                        {
                            numToKorea = numToKorea + levelChar[(level - 1) % 4];
                        }
                        else
                        {
                            numToKorea = numToKorea + numberChar[int.Parse(strvalue.Substring(i, 1))] + levelChar[(level -1) % 4];
                        }
                    }
                }
                else
                {
                    if ((level % 4) == 0 && useDecimal) 
                    {
                        numToKorea = numToKorea + decimalChar[level / 4];
                        useDecimal = false;
                    }
                }
            }

            return numToKorea;
        }

        public decimal Amount
        {
            get { return (decimal)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        /// <summary>
        /// Amount DP
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(nameof(Amount), typeof(decimal), typeof(AmountKoreanControl), new PropertyMetadata(decimal.Zero, AmountChanged));

        private static void AmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (AmountKoreanControl)d;
            control.SetAmount();
        }

        private void SetAmount()
        {
            if(_isWork)
            {
                return;
            }
            
            _isWork = true;
            DecimalToFormatString(Amount);
            _isWork = false;
        }
    }
}
