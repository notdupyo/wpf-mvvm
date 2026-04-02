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

namespace Ch14_ViewModelAndICommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class RelayCommand:ICommand
    {
        private readonly Action<object> _excute;
        private readonly Func<object, bool> _canExcute;

        public RelayCommand(Action<object> excute, Func<object, bool> canExcute = null)
        {
            _excute = excute ?? throw new ArgumentNullException(nameof(excute));
            _canExcute = canExcute;
        }

        // 실행이 가능한지 여부를 반환하는 메서드
        public bool CanExecute(object? parameter)
        {
            return _canExcute == null || _canExcute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _excute(parameter);
        }

        /// <summary>
        /// 실행 가능 여부가 변경되었을 때 발생하는 이벤트이다.
        /// 명령의 실행 가능 여부가 변경되었을 때, 이 이벤트를 발생시켜 UI 요소가 업데이트되도록 한다.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            // (+=) 구독할 때 호출됨
            add
            {
                CommandManager.RequerySuggested += value;
            }
            // (-=) 구독 해제할 때 호출됨
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    } // end of RelayCommand

    public class Student : INotifyPropertyChanged
    {
        // INotifyProertyChanged 인터페이스를 상속한 클래스는
        // 속성 값이 변경될 때 PropertyChanged를 발생시켜야 한다.
        // PropertyChanged는 event이며 PropertyChagnedEventHandler delegate타입니다.
        // delegate이므로 목적은 외부의 메소드를 Student 클래스 내부에서 호출하기 위함이다.
        // Student 클래스 내부에서 외부의 메서드를 호출하는 이유는
        // 원하는 타이밍에 외부의 메서드를 호출하기 위함이다.
        // 여기서 원하는 타이밍은 속성 값이 변경될 때이다.
        // 속성값이 변한다는 말은 UI에서 TextBox와 같은 Control에 바인딩된 속성값이 변경된다는 뜻이다.
        // 또는 코드 내부에서 직접 클래스의(ex. Student 클래스) 속성값(Name, Age)를 변경하는 경우도 포함된다.
        // 즉 UI에서 바인딩된 Property의 값이 변경되면 Property의 Setter에서 PropertyChanged를 발생 시켜야 한다.
        // 즉 PropertyChanged에 구독되는 메서드는 WPF의 데이터 바인딩 엔진이 알아서 구독하는 메서드이다.
        // 일단 WPF의 데이터바인딩 엔진이 구독하는 메서드가 어떤 메서드인지는 심화 과정에서 알아보도록 한다.
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged 메서드는 PropertyChanged 이벤트를 발생시키는 메서드이다.
        // Property의 setter에서 PropertyChanged.Invoke()로 직접 이벤트를 발생시키는 대신,
        // OnPropertyChanged 메서드를 호출하여 내부에 정의한 ProeprtyChanged를 발생시키는 것이 일반적이다.
        // 이렇게 하는 가장 중요한 이유는 OnPropertyChanged 메서드에서 property값이 변경될 때
        // 공통으로 처리해야 하는 로직을 한 곳에 모아서 관리할 수 있기 때문이다.
        // 또한 PropertyChanged 이벤트를 발생시키는 명령어가 너무 길다.
        // =>  PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName))를 매번 setter에 작성하는 것은 번거롭다.
        // OnPropertyChanged 메서드를 사용하면 setter에서 간단하게 OnPropertyChanged(nameof(PropertyName))로 작성할 수 있다.
        protected void OnPropertyChanged(string propertyName)
        {
            // PropertyChanged의 값이 null일 경우 Invoke() 메서드를 호출하면 NullReferenceException이 발생한다.
            // 따라서 if문으로 null 체크를 하던가, null 조건부 연산자(?.)를 사용하여 null 체크를 한다.
            // PropertyChanged가 null인 경우는 '이 객체의 변화를 감시하는 구독자가 아무도 없는 상태'를 의미한다.
            // 대표적으로:
            // 1. 객체가 생성된 직후, 아직 UI 요소(TextBox 등)와 바인딩이 연결되지 않았을 때.
            // 2. UI와 상관없는 백그라운드 로직에서 객체의 속성값을 변경할 때.
            // 3. UI 바인딩이 해제되어 더 이상 감시자가 없을 때.
            // 이 상황에서 Invoke를 호출하면 에러가 나므로 반드시 null 체크가 필요하다.

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Property에 get, set을 자동으로 만들어주는 자동 구현 속성을 사용하지 않을 경우
        // 즉 get과 set 절을 직접 작성하는 경우에는 private 필드를 만들어서 속성값을 저장해야한다.
        // 여기서 _name과 Name은 연결되어 있다고 생각하자.
        private string _name;

        // WPF는 public으로 선언된 Property에 대해서만 바인딩을 지원한다.
        // 따라서 Name 속성은 public으로 선언되어야 한다.
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                // 속성값이 변경될 때마다 OnPropertyChanged 메서드를 호출하여 PropertyChanged 이벤트를 발생시킨다.
                // 매겨변수로 일반 문자열인 ""Name""을 직접 작성하는 대신, nameof 연산자를 사용하여 작성하는 것이 좋다.
                // 그 이유는 :
                // 1. 오타 방지: 문자열로 작성할 경우 오타가 발생할 수 있다. 
                //               nameof연산자는 컴파일 타임에 해당 클래스의 멤버(필드, 속성, 메서드 등)의 이름을 찾기 때문에
                //               만약 존재하지 않는 멤버 이름을 작성하면 컴파일 에러가 발생한다.
                // 2. 리팩토링 지원: 클래스 멤버의 이름이 변경
                //                   예를 들어 Name 속성의 이름이 변경될 때, 문자열로 작성된 "Name"은 자동으로 업데이트되지 않지만,
                //                   nameof(Name)로 작성된 부분은 자동으로 업데이트된다.
                //                   자동으로 업데이트 된다는 뜻은 멤버 이름 고치면 자동으로 변하는게 아니라
                //                   IDE에서 제공하는 리팩토링 기능을 사용하여 참조한 모든 멤버 이름을 자동으로 변경할 수 있다는 뜻
                // 왜 nameof 연산자를 사용하는지 체감하는게 아직은 어렵다.
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
            _name = name;
            _score = score;
        }
    }
}