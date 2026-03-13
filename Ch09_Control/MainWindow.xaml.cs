using System.Data;
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

namespace Ch09_Control
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string todoTitle = txtTitle.Text;

            if(string.IsNullOrEmpty(todoTitle))
            {
                MessageBox.Show("할 일 입력");
                return;
            }

            // 우선순위 가져오기
            // SelectedItem: 선택된 ComboBoxItem 객체
            // as ComboBoxItem : 안전한 캐스팅
            ComboBoxItem seletedPriority = cmbPriority.SelectedItem as ComboBoxItem;
            string priority = seletedPriority.Content.ToString();

            // 중요 여부 체크
            // IsChecked는 bool? 타입
            string important = chkImportant.IsChecked == true ? "[중요]" : "";

            // 표시할 문자열 구성
            string displayText = $"{important}[{priority}] {todoTitle}";

            // ListBox에 항목 추가
            lstTodos.Items.Add(displayText);
            
            // 입력필드 초기화 
            txtTitle.Text = "";
            chkImportant.IsChecked = false;

            // 상태업데이트
            UpdateStatus();
        }

        private void lstTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 선택 항목이 있으면 버튼 활성화 용도
            bool hasSelection = lstTodos.SelectedItem != null;
            btnToggle.IsEnabled = hasSelection;
            btnDelete.IsEnabled = hasSelection;
            
        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{lstTodos.SelectedItem}");

            // 선택 항목 확인
            if(lstTodos.SelectedItem == null)
            {
                return;
            }

            // 현재 선택된 항목과 인덱스
            string current = lstTodos.SelectedItem.ToString();
            int index = lstTodos.SelectedIndex;

            // 완료 표시 토글
            // StartsWith(): 문자열이 특정 문자로 시작하는지 체크
            if(current.StartsWith("[완료]"))
            {
                // 완료 표시 제거
                // substring(5): 앞에서 5글자 제거
                current = current.Substring(5);
            }
            else
            {
                // 완료 표시 추가
                current = "[완료]" + current;
            }

            // 항목 교체
            lstTodos.Items[index] = current;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int lstIndex = lstTodos.SelectedIndex;

            if (lstIndex == -1)
            {
                MessageBox.Show("삭제할 할 일 선택");
                return;
            }

            MessageBoxResult msgResult =  MessageBox.Show("삭제하시겠습니까?", "삭제 확인",MessageBoxButton.YesNo);

            if(msgResult == MessageBoxResult.No)
            {
                return;
            }

            lstTodos.Items.RemoveAt(lstIndex);

            UpdateStatus();

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("모든 항목을 삭제하시겠습니까", "", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                lstTodos.Items.Clear();
                UpdateStatus();
            }
        }

        // 상태 업데이트
        private void UpdateStatus()
        {
            txtStatus.Text = $"{lstTodos.Items.Count}개 항목";
        }
    }
}