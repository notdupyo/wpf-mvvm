using System.Collections;
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

namespace WpfBasicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Character> list = new List<Character>();

        public MainWindow()
        {
            

            InitializeComponent();

            // 코드비하인드 연결
            btnCreate.Click += btnCreate_Click;
            
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxCharName.Text) || tbxCharName.Text == "캐릭터 이름을 입력하세요.")
            {
                MessageBox.Show("캐릭터 이름을 입력하세요.");
                tbxCharName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbxCharLevel.Text))
            {
                MessageBox.Show("레벨을 입력 하세요");
                tbxCharLevel.Focus();
                return;
            }

            RegisterCharacter();            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int idx = lstbxChars.SelectedIndex;
            lstbxChars.Items.Remove(lstbxChars.SelectedItem);
            if(idx == -1)
            {
                return;
            }
            else
            {
                list.RemoveAt(idx);
            }

        }

        private void tbxCharName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text == "캐릭터 이름을 입력하세요.")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void tbxCharName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if(string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "캐릭터 이름을 입력하세요.";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void tbxCharLevel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            bool isEnter = e.Key == Key.Enter;
            bool isBack = e.Key == Key.Back;
            bool isNum = (e.Key >= Key.D0) && (e.Key <= Key.D9) || (e.Key >= Key.NumPad0) && (e.Key <=Key.NumPad9);
            //textBox.Text = $"{isEnter} {isBack} {isNum}";
            if(!isEnter && !isBack && !isNum)
            {
               e.Handled = true;
            }
            
        }

        private void tbxCharLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 생성 버튼 기능 수행(기능을 메소드로 따로 빼서 처리)
                RegisterCharacter();
            }
        }

        private void RegisterCharacter()
        {
            

            string name = tbxCharName.Text;
            string charClass = cmbbxCharClass.Text;
            bool parseLevelResult = int.TryParse(tbxCharLevel.Text, out int level);
            int hp = level * 10;
            int mp = level * 5;
            string displayFormat = "";

            Character character = new Character(name, charClass, level, hp, mp);
            list.Add(character);

            if (chkImportant.IsChecked == true)
            {
                displayFormat = $"[중요] {list[list.Count - 1].CharClass} {list[list.Count - 1].CharName} (Lv.{list[list.Count - 1].Level})";
            }
            else
            {
                displayFormat = $"{list[list.Count - 1].CharClass} {list[list.Count - 1].CharName} (Lv.{list[list.Count - 1].Level})";
            }

            lstbxChars.Items.Add(displayFormat);
        }

        /*private void cmbbxCharClass_Selected(object sender, RoutedEventArgs e)
        {

        }*/

        private void cmbbxCharClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 아래 두 단계 캐스팅은 중요한 개념
            // 1. 호출자 타입으로 캐스팅
            ComboBox comboBox = sender as ComboBox;

            // 2. 접근하려는 요소의 타입으로 한번 더 캐스팅
            ComboBoxItem seletedBoxItem = comboBox.SelectedItem as ComboBoxItem;

            lblCharClass.Content = seletedBoxItem.Content;
        }

        private void stackPnlCharlst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            lblSender.Content = sender.GetType().ToString();
            lblSource.Content = e.Source.GetType().ToString();
        }
    }

    public class Character
    {
        public string CharName { get; set; }
        public string CharClass { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }

        public Character(string charName, string charClass, int level, int hp, int mp)
        {
            CharName = charName;
            CharClass = charClass;
            Level = level;
            Hp = hp;
            Mp = mp;
        }
    }

    public class Warrior : Character
    {
        public Warrior(string charName, string charClass, int level, int hp, int mp) : base(charName, charClass, level, hp, mp)
        {
        }
    }

    public class Mage : Character
    {
        public Mage(string charName, string charClass, int level, int hp, int mp) : base(charName, charClass, level, hp, mp)
        {
        }
    }
}