using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBasicApp
{
    /// <summary>
    /// Ch13INotifyChanged.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Ch13INotifyChanged : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public ObservableCollection<Ch13Character> ch13Characters { get; set; } = new ObservableCollection<Ch13Character>();

        private Ch13Character _selectedChar;
        public Ch13Character SelectedChar
        {
            get
            {
                return _selectedChar;
            }
            set
            {
                _selectedChar = value;
                OnPropertyChanged(nameof(SelectedChar));
            }
        }
        public Ch13INotifyChanged()
        {
            InitializeComponent();

            // 캐릭터 3명 초기 데이터 추가
            ch13Characters.Add(new Ch13Character("홍길동", "도적", 10, 100, 50));
            ch13Characters.Add(new Ch13Character("이순신", "전사", 20, 300, 25));
            ch13Characters.Add(new Ch13Character("전우치", "마법사", 5, 30, 100));

            DataContext = this;

        }

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int radNum = new Random().Next();
            ch13Characters.Add(new Ch13Character($"캐릭터{radNum}", "무직", radNum, radNum * 10, radNum * 5));

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedChar == null) return;
            ch13Characters.Remove(SelectedChar);
        }

        private void btnLevelUp_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedChar == null) return;
            SelectedChar.Level += 1;
            SelectedChar.Hp += 10;
        }
    }

    public class Ch13Character : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { get; set; }
        public string CharClass { get; set; }
        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        private int _hp;
        public int Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnPropertyChanged(nameof(Hp));
            }
            
            
        }
        public int Mp { get; set; }

        public Ch13Character(string name, string charClass, int level, int hp, int mp)
        {
            Name = name;
            CharClass = charClass;
            Level = level;
            Hp = hp;
            Mp = mp;
        }
    }
}
