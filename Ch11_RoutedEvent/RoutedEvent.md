# 코드비하인드에서의 이벤트 처리
## WPF 이벤트 시스템의 구조
WPF의 이벤트도 근본적으로 C# 이벤트와 델리게이트와 같은 메커니즘 위에 동작함  
다만, WPF는 일반 C# 이벤트를 확장한 라우티드 이벤트(Routed Event)라는 고유한 이벤트 시스템을 사용  

---

## WFP의 두 가지 트리 구조
### 논리적 트리(Logical Tree)
XAML에 작성한 그대로의 구조  
개발자가 XAML에서 직접 다루는 트리

``` xml
<StackPanel>
	<Button Content="버튼"/>
</StackPanel>
```
> 이 XAML의 논리적 트리는 작성된 그대로의 'StackPanel -> Button'이다.
  
### 시각적 트리(Visual Tree)
WPF가 실제로 화면을 렌더링할 때 사용하는 트리  
논리적 트리보다 훨씬 상세하고 싶음  
Button 하나가 화면에 표시되려면 내부적으로 'Border', 'ContentPresenter', 'TextBlock' 등 여러 시작적 요소가 조합됨
이러한 내부 구조를 **Control Template** 라고 함

```xml
StackPanel
	ㄴButton
		ㄴBorder(버튼의 테두리/배경)
			ㄴContentPresenter(Content 표시 역할)
				ㄴTextBlock("클릭"텍스트 렌더링 역할)
```
> 이 구조와 같이 사용자가 Button을 클릭했을 때, 마우스가 실제로 닿는 것은 Button이 아닌 Button 내부의
TextBlock이나 Border 등 내부 요소이다. 라우티드 이벤트(RoutedEvent)는 이 시각적 트리를 따라 전파 됨  

---

## 라우티드 이벤트(Routed Event)란?
일반 c# 이벤트는 이벤트가 발생한 컨트롤에서 끝나지 않고, XAML 트리(부모-자식 관계)를 따라서 위 또는 아래로 전달 된다.
이 전달 과정을 라우팅(Routing)이라고 한다.

### 라우팅 전력 3가지
- 버블링(Bubbling)  
이벤트가 발생한 자식 컨트롤에서 시작하여 부모 방향으로 올라가며 전달 됨  
WPF에서 가장 흔한 방식으로 예를 들어 StackPanel 안의 Button을 클릭하면, Button -> StackPanel -> Grid -> Window 순서로 이벤트가 전달 됨

- 터널링(Tunneling)  
버블링의 반대 방향. 최상위 부모에서 시작하여 자식 방향으로 내려가며 전달 됨  
이벤트 이름이 Preview로 시작하는것이 터널링 이벤트(ex. PreviewMouseDown, PreviewKeyDown, ...)  
터널링은 부모가 자식보다 먼저 이벤트를 가로채서 처리하거나 차단해야할 때 사용  

- 직접(Direct)  
라우팅 없이 이벤트가 발생한 컨트롤에서 이벤트 처리. 일반 C# 이벤트와 동일한 동작

---

## 라우티드 이벤트가 필요한 이유
WPF의 UI는 컨트롤이 중첩된 트리 구조. Button 안에 StackPanel이 있고 그 안에 Image와 TextBlock이 있는 복잡한 구조에서,
사용자가 Image를 클릭해도 "Button이 클릭됐다"는 것을 인식해야 함  
버블링을 통해 Image에서 발생한 클릭 이벤트가 Button까지 올라가면서 Button의 Click핸들러가 정상 실행 됨  
또한 부모 패널 하나에 핸들러를 연결해두면, 자식 컨트롤 여러 개의 이벤트를 한 곳에서 처리할 수 있어 코드 관리에 효율적  

---  

## 이벤트 연결 방법 3가지  

### 1. XAML에서 연결  
```xml
<Button Content="생성" Click="btnCreate_Click" />
```  
XAML 속성으로 발생할 이벤트(Click)와 실행할 메서드(이벤트 핸들러) 이름(btnCreate_Click)을 직접 작성  

### 2. 코드비하인드에서 연결(동적 제어)  
```csharp
btnCreate.Click += btnCreate_Click;
```
이벤트 연산자를 사용한다. 실행 중에 조건에 따라 이벤트를 연결(+=)하거나 해제(-=)할 수 있음  
반드시 InitializeComponent() 호출 이후에 작성해야 함  

### 3. 첨부 이벤트(Attached Event) : 부모에서 자식 이벤트를 일괄 수신  
버블링의 성질을 이용한 강력한 방법
```xml
<StackPanel Button.Click="CommonButton_Click">
	<Button Content="A" />
	<Button Content="B" />
</StackPanel>
```  
여러개의 자식 버튼에 일일이 이벤트를 다는 대신, 부모 컨트롤에서 첨부 이벤트를 사용하여 한 번에 처리  

---

## 이벤트 핸들러의 매개 변수
이벤트를 연결하여 이벤트 핸들러가 실행되면 두 개의 매개변수를 받는다.  
이 두 매개변수가 핸들러에게 제공되는 정보의 전부이다.  
```csharp
private void btnCreacte_Click(object sender, RoutedEventArgs e)
```  
- **object sender**: 핸들러가 연결된 컨트롤  
이 이벤트 핸들러가 연결된 컨트롤의 참조, 즉 이 이벤트를 발생시킨 컨트롤이다.  
objcet 타입이므로 실제 컨트롤 타입으로 사용하기 위해선 캐스팅이 필요하다.  
예를 들어 Button에서 발생한 이벤트라면 sender as Button 로 캐스팅하여 Button의 속성인 Content, Name 등에 접근할 수 있다.  
캐스팅할때에는 직접캐스팅( (Button)sender )할 수 있지만 타입이 일치하지 않는 경우 Exception이 발생한다.
따라서 as 키워드를 사용해 간접캐스팅(sender as Button)하면 캐스팅 실패 시 null을 반환받아 안전하게 처리 가능하다.

- **RoutedEventArgs e**: 이벤트에 대한 부가 정보
이벤트에 대한 세부 정보를 담고 있는 객체이다. EventArgs를 상속받은 구현체이므로 EventArgs의 속성을 사용 가능하다.  
대표적으로 Source, OriginalSource, Handled 등의 속성이 있다.

속성 | 가르키는 대상 | 기준 트리
---|---|---
sender | 핸들러가 연결된 컨트롤 | -
e.Source | 이벤트가 발생한 논리적 컨트롤 | 논리적 트리
e.OriginalSource | 이벤트가 발생한 시각적 요소 (최하단) | 시각적 트리

이 차이는 이벤트 전파를 더욱 정밀히 제어하기 위해 알아야할 지식으로 나중에 지금은 그냥 개념만 알고 넘어간다.  

---

## 주요 이벤트 종류별 EventArgs
기본적인 RoutedEventArgs 외에도, 마우스나 키보드처럼 구체적인 정보가 필요할 때는 이를 상속 받은
전용 EventArgs를 받는다.(WPF가 알아서 알맞은 객체를 넘긴다.)
정말 많고 다양한 EvnetArgs 종류가 있으나 핵심은 이벤트 종류별로 상세한 제어가 가능한 별도의 EventArgs가 존재한다는 것만 알면 된다.  
사용법은 연습/실무로 익히면 된다.  
- keyEventArgs: 어떤 키가 눌렸는지 (e.Key)
- MouseEventArgs: 마우스 위치가 어디인지 (e.GetPosition())
- MouseButtonEventArgs: 왼쪽/오른쪽 어떤 버튼인지 (e.ChangedButton)

대표적으로 위와 같은 EventArgs들이 있고 다양한 속성을 제공한다.

---

## 라우팅 제어 - e.Handled
이벤트가 트리를 따라 전파되는 것은 유용하지만, 항상 원하는 동작은 아닐 수 있음  
이벤트의 전파를 막기 위한 방법 중 가장 많이 사용하는 방식이 RoutedEventArgs의 Handled 속성을 사용하는 것  
이벤트 핸들러 안에서 e.Handled = true로 설정하면 라우팅이 중단 됨  

```csharp
// 자식(버튼) 이벤트
private void Button_Click(object sender, RoutedEventArgs e)
{
	MessageBox.Show("1. 버튼을 클릭했습니다.");

	e.Handled = true; // 전파 중단
}

// 부모(바탕) 이벤트
private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
{
	MessageBox.Show("2. 바탕을 클릭했습니다.");
}
```  
- 언제 쓰나?  
부모와 자식 "둘 다" 같은 이벤트를 듣고 있을 때  
컨트롤의 "기본 내장 동작"을 취소하고 싶을 때

- 어디서 쓰나?  
내가 흐름을 끊고 싶은 바로 그 지점(이벤트 핸들러 내부)에 작성

추가로 AddHandler라는 함수를 이용해 전파가 중단된 이벤트를 강제로 수신받는 방법이 있지만
이는 심화 과정에서 해당 함수가 필요한 상황이 생길때 배우자.

---



