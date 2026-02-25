namespace Ch04_Method
{
    /// <summary>
    /// Method의 구조
    /// 접근제한자 반환타입 메서드이름 (매개변수)
    /// {
    ///     실행할 코드
    ///     return 반환값; => 메소드 반환타입에 따라 결정 void라면 생략가능
    /// }
    /// 
    /// Overloading (오버로딩)
    /// : 같은 이름의 메서드를 매개변수가 다르게 여러 개 정의 하는 것
    /// int Add(int a, int b) {}
    /// int Add(int a, int b, int c) {}
    /// double Add(double a, double b) {}
    /// 이런 식으로 메서드 이름은 같지만 메서드 정의 가능
    /// 
    /// 값 전달 vs 참조 전달
    /// : 메서드에 값을 전달하는 두 가지 방식
    /// - Call by Value (값 전달)
    /// static void AddOne(int number) { number++ } => 복사본을 변경
    /// 이 경우 int x =5 ; AddOne(x); Console.Wirte(x); 해보면 5 출력, x 원본 값이 바뀌지 않음
    /// 
    /// - Call by Reference => ref 키워드 사용, 메모리 주소를 전달 함
    /// static void AddOne(ref int number) { number ++ } => 원본을 변경
    /// 이 경우 int x = 5; AddOne(x); Console.Write(x); 해보면 6 출력, x 원본 값이 바뀜
    /// * ref는 코드 흐름을 파악하기 어렵게 만들기 때문에 꼭 필요한 경우에만 사용
    /// 
    /// Optional Parameter(선택적 매개변수)
    /// : Parameter에 기본값을 지정하면 호출할 때 해당 인수를 생략 가능
    /// static void PrintTodo(stirng title, bool isComplete = false, int priority = 2)
    /// {
    ///     Console.Write($"제목: {title} ... );
    /// }
    /// 일때 다음과 같이 호출 가능
    /// PrintTodo("타이틀"); => isComplete, priority 인수 생략
    /// PrintTodo("타이틀", true) => 역시 가능 priority 인수만 생략
    /// 단 title은 기본값이 정해져있지 않아 생략 불가능
    /// 
    /// Named Argument (명명된 인수)
    /// : 인수를 전달할 때 매개변수 이름을 명시하면 순서와 무관하게 전달 가능
    /// PrintTodo(priority: 2, title: "타이틀", isComplete: true); => 이런식으로 인수의 순서 변경 가능
    /// wpf에서 자주 마주치는 패턴
    /// 
    /// * static 으로 선언한 메서드에선 static 메서드만 직접 호출 가능
    /// * Optional Parameter (선택적 매개변수)는 모든 필수 매개 변수 다음에 와야 한다. 
    ///   static void PrintTodo(string title, bool isComplete = false, int daysLeft) <= 이건 오류
    ///   static void PrintTodo(string title, int daysLeft, bool isComplete = false) <= 이건 정상
    /// </summary>
    public class Program
    {
       

        // 반환값 없는 메서드
        static void PrintTodo(string title, bool isComplete, int daysLeft)
        {
            string statusText = isComplete ? "완료" : "진행중";
            string urgencyText;

            if(isComplete)
            {
                Console.WriteLine($"[{statusText}] {title}");
                return;
            }

            if(daysLeft > 7)
            {
                urgencyText = "여유";
            }
            else if(daysLeft <= 7 && daysLeft > 3)
            {
                urgencyText = "주의";
            }
            else
            {
                urgencyText = "긴급";
            }

            Console.WriteLine($"제목: {title} | 긴급도: {urgencyText} | 상태: {statusText}");
        }

        // 반환값 있는 메서드
        static int CountCompleted(bool[] isCompleteItems)
        {
            int count = 0;

            foreach(bool isComplete in isCompleteItems)
            {
                if(isComplete)
                {
                    count++;
                }
            }

            return count;
        }

        // Overloading 예제
        static int Add(int a, int b)
        {
            return a + b;
        }

        static double Add(double a, double b)
        {
            return a + b;
        }

        public static void Main(string[] args)
        {
            string[] todoTitleItems = { "c#공부", "wpf공부", "mvvm공부" };
            bool[] isCompletesItems = { false, true, false };
            int[] daysLeftItems = { 5, 0, 10 };

            Console.WriteLine("===== TODO 목록 =====");

            for (int i = 0; i < todoTitleItems.Length; i++)
            {
                PrintTodo(todoTitleItems[i], isCompletesItems[i], daysLeftItems[i]);
                Console.WriteLine("-----------------");
            }

            int completedCount = CountCompleted(isCompletesItems);
            Console.WriteLine($"전체: {todoTitleItems.Length}개 | 완료: {completedCount}개");
            Console.WriteLine("======================");

            // 오버로딩 호출
            Console.WriteLine(Add(3, 5)); // int 형 메서드
            Console.WriteLine(Add(3.5, 6.7)); // double 형 메서드
        }
    }
}