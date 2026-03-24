using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Ch13_INotifyPropertyChangedAndObservableCollection
{
    /// <summary>
    /// INotifyPropertyChanged 를 구현한 데이터 클래스 Student
    /// </summary>

    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // OnPropertyChanged 메소드를 호출하기 위해 setter를 사용한다.
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public Student(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ObservableCollection => 컬렉션 데이터 변경시 UI 자동갱신
        public ObservableCollection<Student> Students { get; set; }

        // 현재 표시 중인 학생
        private Student _currentStudent;
        public Student CurrentStudent
        {
            get
            {
                return _currentStudent;
            }
            set
            {
                _currentStudent = value;
                OnPropertyChanged(nameof(CurrentStudent));
            }
        }

        private int _addCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<Student>();
            Students.Add(new Student("김철수", 80));
            Students.Add(new Student("이영희", 90));
            Students.Add(new Student("박민수", 66));

            CurrentStudent = Students[0];

            this.DataContext = this;

        }

        private void btnAddScore_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStudent != null)
            {
                CurrentStudent.Score = CurrentStudent.Score + 10;
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (Students.Count > 0)
            {
                Students.RemoveAt(Students.Count - 1);
            }
           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _addCount++;
            Students.Add(new Student($"신규학생{_addCount}", 50));
        }
    }
}