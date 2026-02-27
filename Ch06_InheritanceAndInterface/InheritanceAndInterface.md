### 상속(Inheritance)이란?
**정의**
- 상속은 기존 클래스(부모/기반 클래스)의 멤버를 새 클래스(자식/파생 클래스)가 물려받아  
재사용하고, 필요한 부분만 추가하거나 변경하는 개념

**상속의 핵심 개념**
1. 코드 재사용
	- 공통 기능을 부모에 한 번만 작성
	- 자식은 자동으로 물려받음
2. 계층 구조
	- A(자식)는 B(부모)의 부분 집합이다.
3. 확장성
	- 기존 코드 수정 없이 새로운 종류 추가 가능

**c#의 상속 문법**
```
public class TodoItem
{
	public string Title {get; set;}

	public TodoItem(string title)
	{
		Title = title;
	}
}
// : 뒤에 부모 클래스 이름을 작성하여 상속 구현
public class DeadlineTodoItem : TodoItem
{
	public DateTime Deadline {get; set;}

	// base(title): 부모 생성자를 호출하며 title 전달
	public DeadlineTodoItem(string title, DataeTime deadline) : base(title)
	{
		Deadline = deadline;
	}
}
```
**base키워드**
 - base는 부모 클래스를 카리키는 키워드 (this가 자신 객체를 가르키 듯), 부모의 멤버 접근, 부모 생성자 호출
 - 부모 클래스에 파라미터가 없는 기본생성자가 정의되지 않은 상태에서 자식 클래스에서 부모 클래스의 생성자를 정의하면 컴파일 에러
 - 간단히, 자식 클래스는 부모 클래스를 메모리에 포함하고 있는데 부모 클래스가 먼저 생성되지 않으면 오류이다.
 - 부모 클래스에 기본 생성자만 있다면 자식 클래스에서 생략 가능

 **메서드 재정의-vitual과 override**  
 : 부모의 메서드를 자식이 다르게 동작하도록 바꾸고 싶을 때 사용  
 |키워드|위치|의미|
 |:---:|:---:|:---:|
 | virtual | 부모메서드 | "자식이 재정의 해도 된다." |
 | override | 자식메서드 | "부모메서드를 재정의 한다." |  

 ```
 public class TodoItem
 {
	public virtual string GetDisplayText()
	{
		return $"할일: {Title}";
	}
 }

 public class DeadlineTodoItem : TodoItem
 {
	public override string GetDisplayTest()
	{
		return "마감: {Title} - {Deadline:yyyy-MM-DD}"; // :yyyy-MM-DD 문자열 포맷 지정 문법
	}
 }
 ```

**다형성(Polymorphism)**  
- 다형성이란 부모 타입 변수에 자식 객체를 담아 사용할 수 있는 특성  
```csharp
// 부모 타입 변수에 자식 객체 대입 가능
TodoItem item1 = new TodoItem("일반 할일");  
TodoItem item2 = new DeadlineTodoItem("마감 할일", DateTime.Now);  
TodoItem item3 = new RepeatingTodoItem("반복 할일", 7);  
  
// 같은 메서드 호출, 다른 동작
Console.WriteLine(item1.GetDisplayText()); // [할일] 일반 할일
Console.WriteLine(item2.GetDisplayText()); // [마감] 마감 할일 - 2026-02-27
Console.WriteLine(item3.GetDisplayText()); // [반복] 반복 할일 - 7일마다
```  
- 핵심은 호출하는 코드는 같지만, 실제 객체 타입에 따라 알맞은(다른) 메서드가 실행 됨  
  
**인터페이스(interface)란?**  
- 간단히 말해서, 메소드 로직은 없고 정의만 있는 interface 구현하여 interface를 상속받은 클래스에서  
강제로 interface에 정의된 필드, 메소드 등을 구현하도록 강제하는 것  
- 이건 필수로 개발해! 라고 강제하는 개념  
```csharp
// 인터페이스 정의  
public interface IPrintable // 관례로 이름앞에 I붙임  
{
	void print(); // 내용은 없음
}
  
// 인터페이스 구현  
public class TodoItem : IPrintable
{
	public string Title {get; set;}  
	  
	// 인터페이스에 선언된 메서드를 반드시 구현해야 함
	public void print()
	{
		Console.WriteLine($"[할일] {Title}");  
	}
}  
```  
** \*상속은 1개만, 인터페이스는 다중 상속 가능 **  
```csharp  
public class DeadlineTodoItem : TodoItem, IPrintable, IComparable // TodoItem -> 부모 클래스, IPrintable, IComparable -> 인터페이스
{ ... }  
```  


