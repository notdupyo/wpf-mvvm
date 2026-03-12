# XAML
## XAML이란?
- XAML은 UI를 정의하는 언어  
- html과 비슷한 구조  
---
## WPF란?
- WPF(Windows Presentation Foundation)는 Windows 데스크탑 애플리케이션을 만들기 위한 프레임 워크  
---
## WPF 프로젝트 구조  
- Visual Studio에서 WPF 프로젝트를 생성하면 다음 파일들이 생성됨  
WpfApp/  
├── App.xaml              ← 앱 전체 설정  
├── App.xaml.cs           ← 앱 시작점  
├── MainWindow.xaml       ← 메인 화면 UI  
└── MainWindow.xaml.cs    ← 메인 화면 동작 (코드비하인드)  
---
## XAML 기본 문법  
```xaml
<!-- 1. Element(요소) 선언 -->
<Button>클릭</Button>  

<!-- 2. Attribute(속성) 지정 -->
<Button Content="클릭" Width="100" Height="30"/>

<!-- 3. 자식요소 포함 -->
<Grid>
	<Button Content="버튼1">
	<Button Content="버튼2">
</Grid>
```  
---
## c#코드와 XAML 비교
- 아래의 두 코드는 같은 버튼을 생성하지만 생성하는 방식에 차이가 있음  
```xml
xaml 방식

<Button Content="버튼" Width="100" Height="30">  
```
```csharp
c# 방식

Button btn = new Button();
btn.Content = "클릭";
btn.Width = 100;
btn.Height = 30;
```  
---  
## 레이아웃 패널  
- WPF에서 커튼롤(버튼, 텍스트박스 등)을 배치하려면 **레이아웃 패널**이 필요함  
- 각 Element들의 사용법은 직접 개발하면서 익혀볼 것

| 패널 | 설명 | 용도 |
|------|------|------|
| `Grid` | 행/열로 구성된 표 형태 | 복잡한 레이아웃 |
| `StackPanel` | 수직 또는 수평으로 쌓기 | 단순한 목록 |
| `DockPanel` | 상/하/좌/우에 고정 | 메뉴바, 상태바 |
| `WrapPanel` | 자동 줄바꿈 | 태그, 썸네일 |
| `Canvas` | 절대 좌표 배치 | 그래픽, 게임 |

