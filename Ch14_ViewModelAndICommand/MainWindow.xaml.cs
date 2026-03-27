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
        public event EventHandler? CanExecuteChanged;

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

        
    }
}