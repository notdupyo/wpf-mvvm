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

## 라우팅 제어 - e.Handled
이벤트가 트리를 따라 전파되는 것은 유용하지만, 항상 원하는 동작은 아닐 수 있음  
