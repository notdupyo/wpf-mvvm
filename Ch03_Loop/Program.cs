namespace Ch03_Loop
{
    /// <summary>
    /// 반목문
    /// 
    /// - for문
    /// for(초기화식; 조건식; 증감식) { 실행할 코드 }
    /// 
    /// - while문
    /// while(조건식) { 실행할 코드 }
    /// 
    /// - do-while 문
    /// : while과 비슷하지만 do 절에서 무조건 최소 한번 코드가 실행 됨
    /// do { 최소 한번 실행되는 코드 } while(조건식);
    /// 
    /// - foreach 문
    /// : 컬렉션(배열, 리스트 등)의 요소를 순서대로 하나씩 꺼내서 반복
    /// foreach(자료형 변수 in 컬렉션) { 각 요소에 대해 실행할 코드 }
    /// 
    /// - break와 continue
    /// : break 키워드는 반복을 즉시 중지하고 반복문에서 탈출
    /// : continue 키워드는 키워드 실행 즉시 반복을 건너 뛰고 다음 반복의 처음으로 진행
    /// 
    /// - for vs while vs foreach
    /// 반복 횟수가 명확할 때 => for
    /// 조건에 따라 반복할 때, 반복 횟수 명확하지 않음 => while 
    /// 컬렉션 요소를 순서대로 처리할 때 => foreach
    /// 최소 한번은 실행 후 반복될 때 => do-while
    /// 
    /// - 중첩반복문
    /// : 반복문 안에 반복문을 넣은 구조, 구구단처럼 2차원적인 반복이 필요할 때 사용
    /// : 반복의 중첩 수에 따라 n차 matrix구조로 표현 가능
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // for 문 기본 예제
            
            // 1부터 5까지 반복
            for(int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"반복 {i}번째");
            }

            // 5부터 1까지 역순 출력
            for(int i = 5; i > 0; i --)
            {
                Console.WriteLine($"역순 {i}번째");
            }

            // While 문 기본 예제
            int count = 0;
            while( count < 3)
            {
                Console.WriteLine($"while 반복 {count + 1}번쨰");
                count++;
            }

            // foreach 문 기본 예제
            string[] subjectArr = { "수학", "과학", "미술", "영어", "국어" };

            foreach(string subject in subjectArr)
            {
                Console.WriteLine($"과목: {subject}");
            }

            // break, continue 키워드
            for (int i = 0; i < 10; i++)
            {
                if (i == 5) continue;
                if (i == 8) break;

                Console.WriteLine($"{i}");
            }

        }
    }
}