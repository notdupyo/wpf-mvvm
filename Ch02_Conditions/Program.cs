namespace Ch02_Conditions
{
    /// <summary>
    /// Ch02_조건문(if/switch)
    /// 
    /// - 조건연산자의 종류
    /// == (같다), != (같지 않다), > (크다), < (작다), >= (크거나 같다), <= (작거나 같다)
    /// 
    /// - 논리연산자의 종류
    /// && (AND), || (OR), ! (NOT)
    /// 
    /// - if/else/else-if문 용법
    /// if(조건식) { ... }
    /// else {...}
    /// else if(조건식) { ... }
    /// 
    /// - switch문 용법
    /// switch(변수)
    /// {
    ///   case 값n:
    ///      변수의 값이 값n일때 실행
    ///      break;
    ///   default:
    ///      어떠한 case의 값에도 해당 안될때 실행
    ///      break;
    /// }
    /// 
    /// - 단축평가
    /// : 논리 연산자가 결과를 이미 확정할 수 있으면 나머지 조건을 아예 평가하지 않음
    /// ex) if(age >= 19 && HasValidId()) -> age가 19미만이면 HasValidId()는 호출조차 하지 않음
    /// , 나중에 WPF에서 객체가 null인지 먼저 확인하고 속성에 접근하는 패턴을 자주 쓰기 때문에 이 개념을 알아둬야 함 
    ///
    /// 
    /// 
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // if 문 기본 예제
            int score = 85;

            if(score >= 90)
            {
                Console.WriteLine("A학점");
            }
            else if(score >= 80)
            {
                Console.WriteLine("B학점");
            }
            else if(score >= 70)
            {
                Console.WriteLine("C학점");
            }
            else
            {
                Console.WriteLine("F학점");
            }

            // 삼항 연산자 예제
            string result = (score >= 60) ? "합격" : "불합격";
            Console.WriteLine(result);

            // 논리 연산자 예제
            int age = 25;
            bool hasTicket = true;

            if(age >= 18 && hasTicket)
            {
                Console.WriteLine("입장 가능");
            }
            else
            {
                Console.WriteLine("입장 불가능");
            }

            // switch문 기본 예제
            int dayOfWeek = 3;

            switch (dayOfWeek)
            {
                case 1:
                    Console.WriteLine($"월요일 {dayOfWeek}");
                    break;
                case 2:
                    Console.WriteLine($"화요일 {dayOfWeek}");
                    break;
                case 3:
                    Console.WriteLine($"수요일 {dayOfWeek}");
                    break;
                default:
                    Console.WriteLine("디폴트");
                    break;
            }

            // switch 식 예제
            int energy = 2;
            string condition = energy switch
            {
                0 => "나쁨",
                1 => "평범",
                2 => "좋음",
                _ => "디폴트"
            };
        }
    }
}