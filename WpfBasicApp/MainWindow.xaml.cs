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
        public MainWindow()
        {
            InitializeComponent();

            Warrior warrior = new Warrior("아서", "전사", 10, 150, 10);

            charName.Text = warrior.CharName;
            charClass.Text = warrior.CharClass;
            level.Text = warrior.Level.ToString();
            Hp.Text = warrior.Hp.ToString();

            
        }

        
    }

    public partial class Character
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