# Lambda(람다)식

## Lambda(람다)식 이란?  
**람다식(Lambda Expression)** 은 본질적으로 **익명 함수(Anonymous Funtion)** 를 간결하게 표현하는 문법  
수학자 알론조 처치(Alonzo Church)가 1930년대에 고안한 **람다 대수(Lambda Calculus)** 에서 유래  
C#에서 람다식은 Delegate(델리게이트)나 Expression Tree(식 트리) 타입을 만드는 데 사용 되며, => (람다 선언 연산자, "goes to")를 사용함  

---

## Lambda Expression 기본 사용
람다식은 크게 **람다식(Lambda Expression)** 과 **람다문(Lambda State)** 형태로 나뉜다.
기본형은 람다문, 특정 조건을 만족하는 람다문을 더 간결히 표현한게 람다식

### 람다식 (Lambda Expression)
=>(람다 선언 연산자) 오른쪽에 단일 식이 있는 형태. 결과값이 암시적으로 반환 됨  

```csharp
// 기본, 람다식은 익명함수이므로 메서드의 이름을 적지 않음
(매개변수) => { 본문 }
(x) => { x * x; }

// 매개변수가 1개일 때 괄호 생략 가능
매개변수 1개 => { 본문 }
x => { x * x; }

// 매개변수가 2개 이상일 때 괄호안에 모두 작성
(x, y, z) => { x + y * z;}

// 매개변수가 없을 때 괄호만 사용
() => { 본문 }
() => { Console.WriteLine("매개변수 없음"); }

// 본문이 한 줄이라면 본문을 감싸는 중괄호 생략 가능
(매개변수) => 본문;
(x, y) => x + y;

// 타입명시가 필요한 경우(보통의 경우 생략함)
(int x, string y) => y.Length > x;
```

### 람다문 (Lambda State)
=>(람다 선언 연산자) 오른쪽에 여러줄의 본문을 작성하는 형태, \{ }(중괄호)를 사용해 본문을 감쌈.
특히 반환값이 있다면 return을 반드시 작성해야한다.  

```csharp
// 기본
(매개변수) => { 
	// 여러줄의 본문
	
	// 반환값 명시
	return 반환값;
}

// delegate에 람다식을 사용해 함수를 담는 예
Action<stirng> greet = (msg) => {
	string greetMsg = $"Hello, {msg}";
	Console.WriteLine(greetMsg);
}
greet("반가워요.");

```

### Lambda Expreesion과 Delegate  
람다식 자체는 이름이 없는 익명함수이므로 직접 호출할 수 없음.  
반드시 어딘가에 담아야 호출할 수 있으며 담을 그릇이 바로 Delegate타입 변수.  

```csharp
// 기본 사용법
// 델리게이트 타입 변수 = (매개변수) => {본문}
Func<int, int, int> add = (x,y) => x + y;

// 과거 c#2.0 에선 익명함수에 delegate 키워드와 매개변수 타입을 명시해야 했음
Func<int, int, int> add = delegate(int x, int y) => x + y; 

// c#3.0 이후 현재는 -=> 연산자로 매개변수와 본문만 작성
// 컴파일러가 타입을 추론
Func<int, int, int> add = (x,y) => x + y;

// Action<T> 종류별 람다식 작성

// Action: 매개변수 없음, 반환값 없음
Action printHello = () => Console.WriteLine("Hello");

// Action<T>: 매개변수 1개, 반환값 없음
Action<string> greet = name => Console.WriteLine($"Hello, {name}");

// Action<T, T, ...> 매개변 수 복수 개, 반환값 없음
Aciton<int, int> printScore = (name,score) => Console.WriteLine($"{name}의 점수 {score}");

// Func<T> 종류별 람다식 작성
// Func<T>: 매개변수 없음, 반환값 1개
Func<int> square = x => x * x;

// Func<T, T, ..., T>: 매개변수 n-1개, 마지막 T타입으로 반환값 1개
Func<int, int, int> add = (x, y) => { x + y; };

```

---

## Lambda Expression의 핵심 활용
핵심은 "한 번만 쓸 로직을 메서드 매개변수에 직접 전달할 때"

```csharp
// 아래의 예제를 items 라는 List에서 저장된 string 값의 길이가 3 초과인 것을 필터링 하는 예제이다.
// FindAll() 이라는 메서드에 파라미터로 람다식인 item => item.length > 3을 넘기고 있다.
// FindAll이란 메서드가 item 매개변수에 자동으로 list에 저장된 각각의 데이터를 순회하며 넘긴다.
List<string> longNames = items.FindAll(item => item.Length > 3);

// 위 예제에 쓰인 로직은 한 번만 쓰이는데 따로 메서드로 정의하기엔 비효율적
// 아래와 같이 한 번만 쓸 로직을 따로 정의하기엔 귀찮음이 크다
bool IsLongName(Item item)
{
	return item.Length > 3;
}

List<stirng> longNames = items.FindAll(IsLongName);
```

---

## 람다식 활용 응용(LINQ 기초)
람다식은 LINQ를 활용한 데이터 정제의 목적과 함께 가장 많이 사용되는 조합이다.

### LINQ란?
LINQ(Language Integrated Query)는 컬렉션을 질의(query)하는 기능    
컬렉션에 담긴 데이터를 특정 조건으로 필터링 한다고 생각하면 됨  

- 자주 쓰는 LINQ 메서드  

메서드 | 역할 | 반환 
---|---|---
Where(조건) | 조건을 만족하는 항목만 필터 | 컬렉션
Select(변환) | 각 항목을 다른 형태로 변환 | 컬렉션
FirstOrDefault(조건) | 조건을 만족하는 첫 번째 항목 반환, 없으면 기본값 | 단일 항목
Count(조건) | 조건을 만족하는 항목 수 | int
Any(조건) | 조건을 만족하는 항목이 하나라도 있는지 확인 | bool
OrderBy(기준) | 기준 오름차순 정렬 | 컬렉션
OrderByDescending(기준) | 기준 내림차순 정렬 | 컬레션
ToList() | LINQ결과를 List\<T>로 변환 |  List\<T>

> 컬렉션을 반환하는 Where, Select, OrderBy 등등은 바로 List\<T>로 변환되지 않는다.
ToList()를 호출해야 List\<T>로 변환되는데 그 이유는 LINQ 심화 챕터에서 다룬다.  

