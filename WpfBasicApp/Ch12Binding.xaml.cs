using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Ch12Binding.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Ch12Binding : Window
    {
        Character character = new Character("홍길동", "전사", 10, 500, 50);
        public Ch12Binding()
        {
            InitializeComponent();

            this.DataContext = character;
        }


        private void btnRegist_Click(object sender, RoutedEventArgs e)
        {
           Character current = (Character)this.DataContext;

            tblckResult.Text = $"{current.CharName} {current.CharClass} {current.Level} {current.Hp} {current.Mp}";
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            character = new Character("이순신", "마법사", 10, 50, 500);

            this.DataContext = character;
        }

    }
    
}
