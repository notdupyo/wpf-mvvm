using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ch08_BasicXAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    // XAML 파일과 이 파일이 하나의 MainWindow 클래스를 구성
    // partial로 선언되어 있는 이유가  XAML과 하나의 클래스를 구성하기 위함
    // XAML은 자동으로 c#코드로 변한되어 코드비하인드 c#파일과 합쳐진다.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // InitializeComponet : XAML에 정의된 UI를 생성하고 초기화
            // 이 메서드가 없으면 XAML의 컨트롤들이 생성되지 않음
            InitializeComponent();
        }

        // 버튼클릭 이벤트 헨들러
        // EvnetHandler와 시그니처가 같음, 파라미터로 object sender, EventArgs e 와 비슷하게 전달받음
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // xaml에 정의된 TextBox에 Text속성에 입력된 값을 받아옴
            string input = txtInput.Text;

            // null 체크
            if(string.IsNullOrEmpty(input))
            {
                // MessageBox 윈도우 기본 컨트롤
                MessageBox.Show("할 일을 입력해주세요.");
                return;
            }

            // XAML의 ListBox에 항목 추가
            // List 컬랙션이다.
            listTodos.Items.Add(input);

            // 입력창 비움
            txtInput.Text = "";

            // 상태 업데이트
            txtStatus.Text = $"총 {listTodos.Items.Count}개 항목";
        }
    }
}