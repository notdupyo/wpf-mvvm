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

namespace Ch12_DataBiding_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // DataContext 설정
            // "이 Window의 데이터 소스는 이 Student 객체이다"
            // Window에 설정하면 모든 자식 컨틀롤 이 이 객체를 바인딩 소스로 사용

            Student student = new Student("김철수", 3, 95);
            this.DataContext = student;
        }

        // TwoWay 바인딩 확인
        // TextBox에서 값을 변경하면 소스 객체의 프로퍼티도 변경됨
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            // this.DataContext에 저장된 객체를 Student로 캐스팅
            Student current = this.DataContext as Student;

            // TextBox에서 수정한 값이 소스 객체에 반영되었는지 확인
            lblResult.Content = $"소스 객체: {current.Name}, {current.Grade}학년, {current.Score}";
        }

        // DataContext 교체
        // DataContext의 객체가 교체되면 바인딩된 모든 UI가 새 값으로 갱신됨
        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student("김영희", 1, 80);
            this.DataContext = newStudent;

            lblResult.Content = "DataContext가 새 학생으로 교체됐습니다.";
        }
    }

    // 바인딩은 반드시 속성(Property)에만 가능
    // 일반 필드변수로는 바인딩 되지 않음
    internal class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public int Score { get; set; }

        public Student(string v1, int v2, int v3)
        {
            this.Name = v1;
            this.Grade = v2;
            this.Score = v3;
        }
    }
}