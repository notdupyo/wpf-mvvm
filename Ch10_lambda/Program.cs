namespace Ch10_lambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 람다식 기본 문법
            Console.WriteLine("==== 람다식 기본 ====");

            // 람다식으로 메서드 내용을 바로 작성
            Action<string> printMsg1 = (msg) => { Console.WriteLine(msg); };
            printMsg1("람다식 기본 문법");

            // 매개변수 1개일 때 ( ) 괄호 생략 가능
            Action<string> printMsg2 = msg => { Console.WriteLine(msg); };
            printMsg2("매개변수 1개, 괄호 생략");

            // 본문이 한 줄이면 { } 중괄호 생략 가능
            Action<string> printMsh3 = msg => Console.WriteLine(msg);
            printMsh3("본문 한 줄, 중괄호 생략");

            // 반환값을 다루는 람다식
            Func<int, int, int> add = (x, y) => x + y;
            int result = add(4, 7);
            Console.WriteLine($"반환값을 다루는 람다식 x + y: {result}");

            // 본문이 여러 줄일 때의 람다식
            // { } 중괄호와 return 모두 필요
            Func<string, int, string> printInfo = (name, score) =>
            {
                string grade = "";
                if (score >= 90)
                {
                    grade = "A";
                }
                if (score >= 80)
                {
                    grade = "B";
                }
                if (score >= 70)
                {
                    grade = "C";
                }

                string result = $"학생이름: {name}, 학생 점수:{score}, 과목 학점: {grade}";

                return result;
            };
            Console.WriteLine(printInfo("홍길동", 88));

            // 2. 람다식 응용- List<T> 컬렉션이 제공하는 메서드에 람다식 전달
            Console.WriteLine("===== 람다식 응용 =====");

            List<Student> allStudent = new List<Student>();
            allStudent.Add(new Student("김철수", 3, 95));
            allStudent.Add(new Student("이영희", 2, 72));
            allStudent.Add(new Student("박민수", 1, 88));
            allStudent.Add(new Student("정수진", 3, 65));
            allStudent.Add(new Student("최준호", 2, 91));

            // FindAll(조건) : 조건을 만족하는 몬든 항목을 새 list로 반환
            // 람다식으로 "어떤 조건으로 필터링할지"를 전달
            List<Student> allHighScore = allStudent.FindAll(student => student.Score >= 90);

            Console.WriteLine("90점 이상 학생 목록");
            foreach (Student student in allHighScore)
            {
                Console.WriteLine($"{student.Name}-{student.Score}점");
            }

            // Find(조건): 조건을 만족하는 첫 번째 항목 하나만! 반환, 없으면 null
            Student firstGrade3 = allStudent.Find(student => student.Grade == 3);
            Console.WriteLine($"첫 번째 3학년 : {firstGrade3.Name}");

            // Exists(조건): 조건을 만족한 항목이 하나라도 있으면 true
            bool hasFailer = allStudent.Exists(student => student.Score < 70);
            Console.WriteLine($"70점 미만 존재 여부: {hasFailer}");

            // RemoveAll(조건): 조건을 만족하는 모든 항목 제거 후 제거한 항목의 수 반환
            // 원본 리스트를 변경함! 원본의 변경을 원치않는 경우 다른 방법 또는 별도 리스트로 관리
            List<Student> tempList = new List<Student>(allStudent); // 복사본 생성
            int removeCount = tempList.RemoveAll(tempList => tempList.Score < 70);
            Console.WriteLine($"70점 미만 항목 제거 수 {removeCount}, 남은 항목 수 {tempList.Count}");

            // 3. LINQ - 필터/정렬/변환을 제공하는 확장 메서드
            Console.WriteLine("===== LINQ =====");

            // Where(): 조건을 만족하는 항목만 필터링
            // ToList(): LINQ 결과를 List<T>로 변환, 이 과정이 있어야 필터한 결과 List를 사용가능
            List<Student> allGrade2 = allStudent
                .Where(student => student.Grade == 2)
                .ToList();

            Console.WriteLine("Where결과 grade==2 필터링");
            foreach (Student student in allGrade2)
            {
                Console.WriteLine($"{student.Name} - 학년{student.Grade}");
            }

            // OrderBy(기준) / OrderByDescending(기준)
            // 내부에서 각 항목을 람다식에 넣어 정렬 기준값을 얻음
            List<Student> sortByScore = allStudent
                .OrderByDescending(student => student.Score)
                .ToList();

            Console.WriteLine("OrderByDescending결과 score 기준 필터링");
            foreach (Student student in sortByScore)
            {
                Console.WriteLine($"{student.Name} - 점수{student.Score}");
            }

            // Select(조건) : 각 항목을 다른 형태로 변환
            // 마치 테이블 컬럼을 원하는 것만 select해서 가져오듯이
            List<string> nameList = allStudent
                .Select(student => student.Name)
                .ToList();

            Console.WriteLine("Select 결과 - 이름만 선택");
            foreach (string name in nameList)
            {
                Console.WriteLine($"이름: {name}");
            }

            // Count(조건): 조건을 만족하는 항목 수
            int highCount = allStudent.Count(student => student.Score >= 80);
            Console.WriteLine($"Count결과 - 80점 이상 : {highCount}");

            // Any(조건): 조건을 만족하는 항목이 하나라도 있는지 확인
            bool hasGrade1 = allStudent.Any(student => student.Grade == 1);
            Console.WriteLine($"Any결과 - 1학년 존재 유무: {hasGrade1}");

            // FirstOrDefault(조건): 조건을 만족하는 첫 항목, 없으면 null
            Student topStudent = allStudent
                .OrderByDescending(student => student.Score)
                .FirstOrDefault();
            Console.WriteLine($"최고점수 학생 :{topStudent.Name} - {topStudent.Score}점");

            // 4. LINQ의 메서드 체이닝
            // LINQ 메서드를 연결하여 다양한 조건으로 필터링이 가능하다.

            // Where -> OrderBy -> Select -> ToList 메소드 체이닝 예제
            List<string> grade3Names = allStudent
                .Where(student => student.Grade == 3)
                .OrderByDescending(student => student.Score)
                .Select(student => student.Name)
                .ToList();

            Console.WriteLine("3학년 점수순:");
            foreach (string name in grade3Names)
            {
                Console.WriteLine($"{name}");
            }

            // 5. 내부 동작 확인 - 외부 메소드의 파라미터로 전달되는 람다식의 파라미터 전달 원리
            // 지금까지 student => student.Score >= 90 이라 쓰면
            // 자동으로 student 파라미터에 List에 있는 값이 할당 됨
            // 하지만 student 파라미터에 값을 전달하는 코드는 보이지 않음
            // FindAll/Where 같이 람다식을 파라미터로 받는 메서드의 내부 원리를 직접 구현하여 확인

            Console.WriteLine("===== 내부 동작 확인 (직접 구현) =====");

            // FilterStudents는 FindAll과 동일한 동작을 하는 메서드를 직접 구현한 것
            /* 구현한 메소드 내용
            static List<Student> FilterStudents(List<Student> students, Func<Student, bool> condition)
            {
                // condition: 외부에서 전달받은 람다식
                List<Student> result = new List<Student>();

                foreach (Student student in students)
                {
                    bool matched = condition(student);

                    if (matched)
                    {
                        result.Add(student);
                    }
                }
                return result;
            }
            */

            List<Student> highScoreStudents = FilterStudents(allStudent, student => student.Score >= 90);
            // FilterStudents(allStudent, student => student.Score >= 90); 는
            // 결과적으로 allStudent.FilterStudents(student => student.Score >= 90;); 과 같음
            Console.WriteLine("FilterStudents - 90점 이상:");
            foreach (Student student in highScoreStudents)
            {
                Console.WriteLine($"  {student.Name} - {student.Score}점");
            }


        }

        static List<Student> FilterStudents(List<Student> students, Func<Student, bool> condition)
        {
            // condition: 외부에서 전달받은 람다식
            List<Student> result = new List<Student>();

            foreach (Student student in students)
            {
                bool matched = condition(student);

                if (matched)
                {
                    result.Add(student);
                }
            }
            return result;
        }

    }
    
    internal class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public int Score { get; set; }

        public Student(string name, int grade, int score)
        {
            Name = name;
            Grade = grade;
            Score = score;
        }
    }
}
