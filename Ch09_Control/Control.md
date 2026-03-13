# Ch09. 컨트롤(Button, TextBox, ListBox 등)
## 컨트롤이란?  
Control(컨트롤)은 사용자와 상호작용하는 UI요소이다.  
- 주요 컨트롤 목록  

컨트롤 | 용도 | 주요속성
---|---|---|  
Button | 클릭 동작 | Content, Click  
TextBox | 텍스트 입력 | Text, TextChanged  
TextBlock | 텍스트 표시(읽기 전용) | Text
Label | 텍스트 표시(접근성 지원) | Content  
CheckBox | 체크 선택 | IsChecked, Checked, Unchecked  
RadioButton | 단일 선태(그룹 중 하나) | IsChecked, GroupName  
ComboBox | 드롭다운 목록 | SelectedItem, SelectedIndex  
ListBox | 목록 표시 | Items, SelectedItem  
ProgressBar | 진행률 표시 | Value, Minimum, Maximum  
Slider | 슬라이더 | Value, Minimum, Maximum  

---

## 주요 컨트롤 설명  
### Button  
```xml
<Button Content="버튼"
		Width="100"
		Height="50"
		Click="Button_Click"/>
```
- 주요 속성  

속성 | 설명    
--|--  
Content | 버튼에 표시되는 내용(텍스트, 이미지 등)  
IsEnabled | 버튼 활성화 여부(false 시 비활성화)  
Click | 클릭 시 실행할 이벤트 핸들러  

```csharp
// 코드 비하인드
private void Button_Click(object sender, RoutedEventArgs e)
{
	MessageBox.Show("버튼을 클릭했습니다.");
}
```  
### TextBox  
```xml
<TextBox x:Name="txtInput"
		 Text="초기값"
		 Width="200"
		 TextChanged="txtInput_TextChanged"/>
```  
- 주요 속성  

속성 | 설명  
--|--  
Text | 입력된 텍스트  
IsReadOnly | true면 읽기 전용  
MaxLength | 최대 입력 글자 수 제한  
TextChanged | 텍스트 변경 시 이벤트  

### TextBlock  
```xml  
<TextBlock Text="표시할 텍스트"
		   FontSize="18"
		   FontWeight="Bold"
		   ForeGround="Blue"/>
```  
- 주요 속성  

속성 | 설명  
--|--  
Text | 표시할 텍스트  
FontSize | 글자 크기  
FontWeight | 글자 굵기(Normal, Bold 등)  
Foregroud | 글자 색상  

### CheckBox  
```xml
<CheckBox x:Name="chkComplete"
		  Content="완료"
		  IsChecked="False"
		  Checked="chkComplete_Checked"
		  UnChecked="chkComplete_Unchecked"/>
```  
- 주요 속성

속성 | 설명  
--|--
Content | 체크박스 옆에 표시할 텍스트  
IsChecked | 체크 상태(true, false, null)  
Checked | 체크될 때 이벤트  
Unchecked | 체크 해제될 때 이벤트

### ListBox  
여러 항목을 목록으로 표시하고 선택할 수 있느 컨트롤  
```xml  
<ListBox x:Name="listItems"
		 SelectionChanged="lstItems_SelectionChanged">
		 <!-- XAML에서 직접 항목 추가 가능 -->
	<LixtBoxItem Content="항목 1"/>
	<LixtBoxItem Content="항목 2"/>
	<LixtBoxItem Content="항목 3"/>
</ListBox>
```  
- 주요 속성/메서드  

속성/메서드 | 설명  
--|--  
Items | 항목 컬렉션
Items.Add() | 항목 추가
Items.Remove() | 항목 제거
Items.Clear() | 전체 삭제
SelectedItem | 선택된 항목
SelectedIndex | 선택된 항목의 인덱스 (-1이면 선택 없음)
SelectionChanged | 선택 변경 시 이벤트

```csharp
// c#에서 항목 추가
lstItems.Items.Add("새 항목");

// 선택된 항목 가져오기
string selected = lstItems.SelectedItem as string;

// 선택된 항목 삭제
if(lstItems.SelectedItem != null)
{
	lstItems.Items.Remove(lstItems.SelectedItm);
}
```  

### ComboBox  
드롭다운 목록에서 하나를 선택하는 컨트롤  
```xml
<ComboBox x:Name="cmbPriority"
		  Width="50"
		  SelectedIndex="0"
		  SelectionChanged="cmbPriority_SelectionChanged">
	<ComboBoxItem Content="높음"/>
	<ComboBoxItem Content="보통"/>
	<ComboBoxItem Content="낮음"/>
</ComboBox>
```  
- 주요 속성  

속성 | 설명  
--|--  
SelectedIndex | 선택된 항목 인덱스  
SelectedItem | 선택된 항목  
SelectionChanged | 선택 변경 시 이벤트  

```csharp
// 선택된 항목 가져오기
ComboBoxItem selected = cmbPriority.SelectedItem as ComboBoxItem;
string priority = selected.Content.ToString();
```  

### 주요 공통 속성  
모든 컨트롤에서 사용 가능한 주요 속성  

속성 | 설명
--|--  
Width, Height | 넓이, 높이 크기
Margin | 바깥 여백
Padding | 안쪽 여백  
HorizontalAlignment | 수평 정렬(Left, Center, Right, Stretch)
VerticalAlignment | 수직 정렬(Top, Center, Bottom, Stretch)  
IsEnabled | 활성화 여부  
Visibility | 표시 상태(Visible, Hidden, Collapsed)

- Visible 값 별 차이  
Visible : 표시 됨  
Hidden : 숨김 (공간 차지)  
Collapsed | 숨김 (공간도 사라짐)