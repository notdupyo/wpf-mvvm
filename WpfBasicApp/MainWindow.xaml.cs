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
            
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
    
            string charName = tbxCharName.Text;
            string charClass = cmbbxCharClass.Text;
            bool parseResult = int.TryParse(tbxCharLevel.Text, out int charLevel);
            string displayFormat = "";
            

            if (string.IsNullOrEmpty(charName) || !parseResult)
            {
                MessageBox.Show("모든 입력정보 입력");
                return;
            }

            switch (charClass)
            {
                case "전사":
                    list.Add(new Warrior(charName, charClass, charLevel, 150, 10));
                    break;
                case "마법사":
                    list.Add(new Mage(charName, charClass, charLevel, 100, 50));
                    break;
                default:
                    break;
            }

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