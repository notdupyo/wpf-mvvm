using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ch11_RoutedEvent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 선택된 학년을 저장하는 필드
        private string _selectedGrade = "";
        public MainWindow()
        {

            InitializeComponent();

            // 코드비하인드에서 이벤트 연결
            // XAML에서 Click 속성 대신, C#코드에서 += 연산자로 핸들러를 연결
            // 반드시 InitializeComponent() 호출 이후 작성해야 한다.
            btnRegister.Click += BtnRegister_Click;
        }

        // sender 활용 - GotFocus / LostFocus
        // plaseholder 구현
        // 1) sender를 접근하고자하는 컨트롤로 캐스팅
        // 2) 직접캐스팅 보다 as 키워드를 사용한 간접캐스팅 활용
        private void tbxName_GotFocus(object sender, RoutedEventArgs e)
        {
            // sender: 이 핸들러가 연결된 컨트롤
            TextBox textBox = sender as TextBox;

            if(textBox.Text == "이름을 입력하세요.")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black; // 글자 색을 검정으로 변경
                // Brushes: WPF에 정의된 색상 브러시 모음
            }
        }

        private void tbxName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if(string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "이름을 입력하세요."; // placeholder 텍스트 복원
                textBox.Foreground = Brushes.Gray;
            }

        }

        // e.Handled - PreviewKeyDown
        // 이벤트 전파 차단
        // 키 입력 검증 구현
        private void tbxScore_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // KeyEventArgs : 키보드 이벤트 전용 EventArgs(RoutedArgs 상속)
            // e.Key: 눌린 키를 나타내는 Key enum 값

            // Key.D0 ~ Key.D9: 키보드 상단의 숫자 키(0~9)
            // Key.NumPad0 ~ Key.NumPad9: 숫자 패드의 숫자 키
            bool isDigit = (e.Key >= Key.D0 && e.Key <= Key.D9);
            bool isNumPad = (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            bool isControl = (e.Key == Key.Back || e.Key == Key.Tab || e.Key == Key.Enter);

            if (!isDigit && !isNumPad && !isControl)
            {
                e.Handled = true;
            }
        }

        // e.Key 활용 - KeyDown으로 Enter 감지
        // PreivewKeyDown에서 Enter를 isControl로 허용했으므로
        // KeyDown까지 도달하여 이 핸들러가 실행됨
        private void tbxScore_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                RegisterStudent();
            }
        }

        // sender + e.Source - 첨부 이벤트에서 클릭된 버튼 구별
        // 트리구조에서 실제 이벤트를 발생시킨 컨트롤과
        // 이벤트 핸들러가 연결되어 있는 컨트롤의 구분
        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            // e.Source: 이벤트가 실제로 발생한 컨트롤 (클리된 Button)
            Button clickedButton = e.Source as Button;

            // Content는 object 타입이므로 ToString()으로 변환
            _selectedGrade = clickedButton.Content.ToString();
            lblGrade.Content = $"선택된 학년: {_selectedGrade}";

        }


        //  코드 비하인드에서 연결한 이벤트 핸들러
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterStudent();
        }

        // 등록 메소드
        private void RegisterStudent()
        {
            string name = tbxName.Text;
            string scoreText = tbxScore.Text;

            // 유효성 검사
            if(name == "이름을 입력하세요." || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("이름을 입력해주세요.");
                return;
            }

            if(!int.TryParse(scoreText, out int score))
            {
                MessageBox.Show("점수를 입력해주세요.");
                return;
            }

            if(string.IsNullOrEmpty(_selectedGrade))
            {
                MessageBox.Show("학년을 선택해주세요.");
                return;
            }

            lstStudents.Items.Add($"[{_selectedGrade}] {name} - {score}점");

            // 입력필드 초기화
            tbxName.Text = "";
            tbxScore.Text = "";
            tbxName.Focus(); // 해당 컨트롤에 포커스를 프로그래밍 방식으로 설정
        }

        // 버블링 확인 - sender와 e.Source 차이
        // Grid에 MouseDown 핸들러가 연결되어 있으므로
        // Grid의 자식(Label, ListBox)을 클릭해도 버블링에 의해 이 핸들러가 실행됨
        // 여기서 sender는 항상 Grid (핸들러가 연결된 컨트롤)
        // 여기서 e.Source는 실제 클릭된 컨트롤(논리적 트리 기준)
        // 여기서 e.OriginalSource는 가장 깊은 요소(시각적 트리 기준)

        private void grdListArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string senderType = sender.GetType().Name;
            string sourceType = e.Source.GetType().Name;
            string originalSourceType = e.OriginalSource.GetType().Name;

            lblStatus.Content = $"sender: {senderType} | Source: {sourceType} | OriginalSource: {originalSourceType}";
        }

        // SelectionChangedEventArgs - 선택 변경 정보
        // SelectionChanged 이벤트 전용 EventArgs
        private void lstStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstStudents.SelectedItem != null)
            {
                lblStatus.Content = $"선택됨: {lstStudents.SelectedItem} (인덱스: {lstStudents.SelectedIndex})";
            }
        }
    }
}