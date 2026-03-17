namespace Ch06_2_Exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 기본 try / catch / finally 사용

            Console.WriteLine("기본 예외처리 사용");

            try
            {
                Console.WriteLine("0으로 나눗셈");
                int a = 10;
                int b = 0;
                int result = a / b; // DivideByZeroExceprion 예외 발생

                Console.WriteLine($"0으로 나눈 결과: {result}"); // 이 줄은 실행되지 않음
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"0으로 나눗셈 예외 발생 : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("예외처리와 무관한 finally 블록 실행");
            }

            // try / catch 이후의 코드는 정상적으로 실행됨
            Console.WriteLine("프로그램 중단 없이 계속 실행");


            // 2. 다중 catch 블록
            Console.WriteLine("==== 다중 catch 블록 ====");

            string[] allInput = { "100", "abc", "0" };

            for (int i = 0; i < allInput.Length; i++)
            {
                try
                {
                    // int.Parse(): 문자열을 int로 변환, 실패시 FormatException 예외 발생
                    // TryParse아 달리 변환 실패 시 예외 알림
                    int number = int.Parse(allInput[i]);
                    int divideResult = 1000 / number; // number가 0이면 DivideByZeroException 예외 발생

                    Console.WriteLine($"1000 / {number} = {divideResult}");
                }
                catch (FormatException ex)
                {
                    // "abc"를 int로 변환 시 수행
                    Console.WriteLine($"형식 오류 : {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    // 0으로 나눌 때 수행
                    Console.WriteLine($"0으로 나눔 오류 : {ex.Message}");
                }
            }

            // 3. throw 예외 직접 발생
            Console.WriteLine("==== throw ====");

            // 정상 호출
            Student student1 = new Student("김철수", 85);
            Console.WriteLine($"{student1.Name}: {student1.Score}점");

            // 잘못된 값으로 객체 생성 시도
            try
            {
                // 여기서 예외 발생 throw는 Student class 내부에서 발생
                // 하지만 class 내부에는 try-catch 문이 없다.
                // 이 경우 .NET 런타임은 Call Stack을 역으로 올라가 호출자를 탐색한다.
                // 따라서 Student class 에서 발생한 throw 예외는 Main으로 전달되어 try-catch를 찾는다.
                // 만약 예외 발생 후 Call Stack 어디에서도 try-catch를 찾지 못할 때 프로그램이 중단 된다.
                Student student2 = new Student("이영희", -10);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"생성실패: {ex.Message}");
            }

            try
            {
                Student student3 = new Student("", 90);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"생성 실패: {ex.Message}");
            }

            // 4. 예외처리 vs 사전 검사 비교
            Console.WriteLine("===== 예외처리 try/catch vs 사전 검사 if");

            Dictionary<string, int> allScores = new Dictionary<string, int>();
            allScores.Add("김철수", 90);
            allScores.Add("이영희", 85);

            // 방식 1. 사전 검사(if검사) : 예측 가능한 상황
            string searchKey = "박민수";
            if (allScores.ContainsKey(searchKey))
            {
                Console.WriteLine($"사전 검사: {allScores[searchKey]}점");
            }
            else
            {
                Console.WriteLine($"사전 검사: {searchKey}는 등록되지 않았습니다.");
            }

            // 방식 2. 예외 처리(try/catch) : 예측 불가능한 상황(데이터의 값 형식, 종류, 범위 등등의 변경을 예측할 수 없는 상황)
            try
            {
                Console.WriteLine($"try/catch: {allScores[searchKey]}점");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"try/catch: 키를 찾을 수 없습니다. {ex.Message}");
            }

            // 5. 실전 패턴 - 사용자 입력 처리
            Console.WriteLine("==== 실전패턴 - 사용자 입력 처리 ====");

            // 점수 입력을 받아 학생 목록에 추가
            List<Student> allStudent = new List<Student>();
            string[] allName = { "김철수", "이영희", "", "박민수" };
            string[] allScore = { "95", "abc", "80", "-5" };

            for (int i = 0; i < allName.Length; i++)
            {
                try
                {
                    // 변환 실패 시 FormatException 발생
                    int parsedScore = int.Parse(allScore[i]);

                    // Student 생성자에 유효하지 않은 값 전달 시 ArgumentException 발생
                    Student student = new Student(allName[i], parsedScore);

                    allStudent.Add(student);
                    Console.WriteLine($"등록 성공: {student.Name} - {student.Score}점");
                }
                catch (FormatException)
                {
                    // ex 변수를 사용하지 않을 경우 변수명 생략 가능
                    Console.WriteLine($"등록 실패: 점수 '{allScore[i]}'는 숫자가 아님");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"등록 실패: {ex.Message}");
                }
            }
            Console.WriteLine($"최종 등록 학생 수 : {allStudent.Count}");
        }
    }

    internal class Student
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public Student(string v1, int v2)
        {
            // throw: 유효하지 않은 값이 들어오면 예외를 직접 발생
            // string.IsNullOrWhteSpace(): null 이거나 공백만 포함하면 true 반환
            if(string.IsNullOrWhiteSpace(v1))
            {
                // ArgumentException: 메서드에 유효하지 않은 인수 전달 시 사용
                throw new ArgumentException("이름은 빈 값일 수 없습니다.");
            }

            if (v2 < 0 || v2 > 100)
            {
                throw new ArgumentException($"점수는 0~100 사이여야 한다. 입력값 :{v2}");
            }

            Name = v1;
            Score = v2;
        }
    }
}
