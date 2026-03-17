# Exception and try-catch  

## Exception(예외)란?  
프로그램 실행 중에 발생하는 비정상적인 상황.  
컴파일은 정상적으로 되지만, 실행할 때 문제가 생겨서 프로그램이 중단되는 것.  

---  

## 예외 처리 기본 구조  

```csharp
try
{
	// 예외가 발생할 수 있는 코드
}
catch (예외타입 변수)
{
	// 예외가 발생했을 때 실행할 코드
}
finally
{
	// 예외 발생 여부와 관계없이 항상 실행되는 코드
}
```  

- try 블록: 예외가 발생할 가능성이 있는 코드를 감쌈.  
예외가 발생하면 try블록의 나머지 코드는 실행되지 않고, 즉시 catch 블록으로 이동  

- catch 블록: 발생한 예외를 받아서 처리하는 코드.  
매개변수로 예외 객체를 받으며, 이 객체에서 오류 정보를 확인할 수 있음.  
catch블록이 없으면 예외가 처리되지 않고 프로그램이 중단 됨.  

- finally 블록: 예외 발생 여부와 무관하게 항상 실행. 선택사항이며 생략 가능  
 파일 닫기, 네트워크 연결 해제 등 "어떤 상황에서든 반드시 실행해야 하는 정리 작업"에 사용  


### 예외 클래스 계층 구조  
c#의 모든 예외는 Exception 클래스를 상속한다. 자주 마나는 예외는 다음과 같음  

예외 타입 | 발생 상황
---|---
NullReferenceException | null인 객체의 멤버에 접근할 때
IndexOutOfRangeException | 배열/리스트의 범위를 벗어난 인덱스 접근
ArgumentOutOfRangeException | 메서드에 허용 범위 밖의 인수를 전달할 때
KeyNotFoundException | Dictionary에서 존재하지 않는 키로 접근할 때
InvalidCastException | 잘못된 타입으로 캐스팅할 때
FormatException | 문자열을 숫자로 변환할 때 형식이 맞지 않을 때
DivideByZeroException | 0으로 나눌 때
ArgumentException | 메서드에 유효하지 않은 인수를 전달할 때
InvalidOperationException | 객체의 현재 상태에서 혀용되지 않는 연산을 수행할 때

- 모든 예외 객체는 Exception을 상속하므로 다음의 공통 속성을 가짐  

속성 | 설명
---|---
Message | 오류를 설명하는 문자열
StackTrace | 예외가 발생한 코드 위치(호출경로)를 문자열로 표시

### 다중 catch 블록  
하나의 try에 여러 catch를 연결하여, 예외 종류에 따라 다르게 처리 가능  

```csharp
try
{
	// 여러 종류의 예외가 발생할 수 있는 코드
}
catch (FormatException ex)
{
	// 형식 오류일 때의 처리
}
catch (DivideByZeroException ex)
{
	// 0으로 나눌 때의 처리
}
catch (Exception ex)
{
	// 위에서 잡히지 않은 나머지 모든 예외 처리
}
```

> catch블록의 순서가 중요. Exeption은 모든 예외의 부모이므로 최하단 catch문에 작성해야 한다.  
구체적인 예외를 먼저 작성하고 Exception 예외를 나중에 작성하는 것이 순서  

---

## throw 란?
예외는 시스템만 발생시키는 것이 아닌 개발자가 코드에서 의도적으로 예외 발생 가능  

### throw 기본 사용

```csharp
throw new ArgumentException("메세지");
```

throw는 new와 함꼐 예외객체를 생성하여 던짐.  
메서드의 매개변수가 유효하지 않거나 객체의 상태가 옳바르지 않을 때 사용  

---

## 예외 처리 vs 사전 검사  
같은 문제를 두 가지 방식으로 해결  

```csharp
// 방법 1. 사전 검사(if문 검사)
if(dictionary.ContainsKey(key))
{
	int value = dictionary[key];
}

// 방법 2. 예외 처리 (try/catch)
try
{
	int value = dictionary[key];
}
catch (KeyNotFoundException ex)
{
	Console.WriteLine("키가 존재하지 않습니다.");
}
```

- 예측 가능한 상황, 즉 data의 종류와 값이 정해진 상황은 if문으로 처리
- 예측이 어려운 상황, 즉 data의 종류와 값의 형식이 정해지지 않은 상황은 try/catch로 처리
- try/catch는 if문보다 성능 비용이 크다. "정상적인 흐름 제어"를 위해서는 if문을 사용하고 말 그대로 "예외적인 상황"이 발생할 때만 사용