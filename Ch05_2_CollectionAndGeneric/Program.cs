using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace Ch05_2_CollectionAndGeneric
{


    internal class Program
    {
        List<Item> allItem = new List<Item>();
        Dictionary<string, int> itemCountByCategory = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            // 1. List<T> 기본 사용

            List<string> allStudentName = new List<string>();

            // Add(): 리스트 끝에 항목 추가
            allStudentName.Add("김영희");
            allStudentName.Add("김철수");
            allStudentName.Add("이순신");
            allStudentName.Add("곽부용");

            // Count: 현재 항목 수 반환하는 속성
            Console.WriteLine($"학생 수 : {allStudentName.Count}");

            // [index]: 배열처럼 인덱스로 접근(0 시작)
            Console.WriteLine($"첫 번째 학생 : {allStudentName[0]}");

            // Insert(Index, Item): 지정 인덱스에 항목 삽입
            // 기존 항목들은 뒤로 밀림
            allStudentName.Insert(2, "최추가");

            // Contains(item): 해당 값이 리스트에 존재하는지 확인, bool값 반환
            bool hasSchoi = allStudentName.Contains("최추가");
            Console.WriteLine($"최수가 존재 여부: {hasSchoi}");

            // IndexOf(item): 해당 값의 인덱스 반환, 없으면 -1
            int index = allStudentName.IndexOf("이순신");
            Console.WriteLine($"이순신 인덱스 번호: {index}");

            // Remove(item): 해당 값을 리스트에서 제거
            // 제거 성공 여부에 따라 true/false 반환
            allStudentName.Remove("곽부용");

            // RemoveAt(index): 해당 인덱스의 값을 리스트에서 제거
            allStudentName.RemoveAt(0); // 김영희 제거

            // foreach로 순회
            Console.WriteLine("===== 학생 명단 =====");
            Console.WriteLine("foreach문 순회");
            foreach (string name in allStudentName)
            {
                Console.WriteLine($"foreach : {name}");
            }

            // for문으로 순회
            Console.WriteLine("for문 순회");
            for (int i = 0; i < allStudentName.Count; i++)
            {
                Console.WriteLine($"for : {allStudentName[i]}");
            }

            // 2. List에 객체 저장

            // Student 클래스 객첼르 저장하는 리스트
            List<Student> allStudent = new List<Student>();
            allStudent.Add(new Student("김철수", 3, 95));
            allStudent.Add(new Student("이영희", 2, 80));
            allStudent.Add(new Student("박민수", 1, 75));

            Console.WriteLine("===== 학생 객체 목록 =====");
            foreach (Student student in allStudent)
            {
                Console.WriteLine($"이름: {student.name}, 번호: {student.studentNum}, 점수: {student.score}");
            }

            // 3. Dictonary<TKey, TValue> 기본 사용

            // string 키로 int 값을 저장하는 Dictionary
            // key: 학생이름, value: 점수
            Dictionary<string, int> scoreByStudentName = new Dictionary<string, int>();

            // Add(key, value): 키-값 쌍 추가
            scoreByStudentName.Add("김철수", 90);
            scoreByStudentName.Add("이영희", 85);
            scoreByStudentName.Add("박남길", 70);

            // [key]로 값에 접근
            Console.WriteLine($"Dictionary[김철수] 점수: {scoreByStudentName["김철수"]}");

            // [key]=value: 키가 이미 존재하면 값 덮어쓰기, 없으면 새로 추가
            scoreByStudentName["김철수"] = 60; // 기존 값 변경
            scoreByStudentName["홍길동"] = 77; // 새 값 추가

            // Contains(key): 해당 키 존재 유무 확인
            if (scoreByStudentName.ContainsKey("홍길동"))
            {
                Console.WriteLine($"홍길동 점수: {scoreByStudentName["홍길동"]}");
            }

            // TryGetValue(key, out value): ContainsKey + 값 가져오기 한 번에 처리
            // 키가 존재하면 out 변수에 값을 넣고 true 반환, 없으면 false 반환
            if (scoreByStudentName.TryGetValue("김철수", out int score))
            {
                Console.WriteLine($"TryGetValue 김철수 점수 : {score}");
            }

            // Reove(key): 해당 키의 항목 제거
            scoreByStudentName.Remove("김철수");

            // 4. Dictionary 순회

            // foreach 순회
            // Dictionary를 foreach로 순회하면 각 항목이 KeyValuePair<TKey, TValue> 타입
            // KeyValuePair: .Key와 .Value 두 속성을 가진 구조체
            Console.WriteLine("==== Dictionary foreach 순회 ====");
            foreach (KeyValuePair<string, int> pair in scoreByStudentName)
            {
                Console.WriteLine($"key: {pair.Key}, value: {pair.Value}");
            }

            // Keys: 모든 키의 컬렉션 반환
            Console.WriteLine("==== Dictonary Keys 속성 ====");
            foreach (string key in scoreByStudentName.Keys)
            {
                Console.WriteLine($"name(key): {key}");
            }

            // Values: 모든 값의 컬렉션 반환
            Console.WriteLine("==== Dictionary Values 속성 ====");
            foreach (int value in scoreByStudentName.Values)
            {
                Console.WriteLine($"score(value): {value}");
            }


            // 5. Dictionary에 객체를 값으로 저장
            Dictionary<int, Student> studentByNum = new Dictionary<int, Student>();
            studentByNum.Add(1, new Student("김철수", 3, 100));
            studentByNum.Add(2, new Student("김철수", 2, 90));
            studentByNum.Add(3, new Student("김철수", 1, 75));

            // key 번호로 객체 조회
            if (studentByNum.TryGetValue(1, out Student findStudent))
            {
                Console.WriteLine($"{findStudent.studentNum}번, {findStudent.name}, {findStudent.score} 점");
            }

            // Count: 모든 항목의 수
            Console.WriteLine($"모든 항목 수 :{studentByNum.Count}");

            // Clear(); 모든 항목 제거
            studentByNum.Clear();
            Console.WriteLine($"초기화 후 항목 수: {studentByNum.Count}");


            // Collection, Generic 과제 코드 수행
            Program program = new Program();
            Item item1 = new Item("롱소드", "무기", 150);
            Item item2 = new Item("가죽 갑옷", "방어구", 300);
            Item item3 = new Item("체력 물약", "소모품", 10);

            program.AddItem(item1);
            program.AddItem(item1);
            program.AddItem(item1);
            program.AddItem(item2);
            program.AddItem(item3);
            program.AddItem(item3);

            program.RemoveItem(item1);

            program.Display();

        }

        public void AddItem(Item item)
        {
            
            allItem.Add(item);

            if (!itemCountByCategory.ContainsKey(item.Category))
            {
                itemCountByCategory[item.Category] = 1;
            }
            else
            {
                itemCountByCategory[item.Category]++;
            }
        }

        public void RemoveItem(Item item)
        {
            allItem.Remove(item);

            if (!itemCountByCategory.ContainsKey(item.Category))
            {
                return;
            }
            else
            {
                itemCountByCategory[item.Category]--;
            }
        }

        public void Display()
        {
            //IEnumerable<Item> aa = allItem.Distinct(); 여기서 ditinct가 먹힌 이유는 같은 객체(item1 ...) 을 중복으로 add했기 때문이다.
            // 즉 list의 중복 제거에 좋은 방법은 아니다.

            foreach(Item item in allItem)
            {
                Console.WriteLine($"[{item.Category}] {item.Name} {item.Price}G");
            }

            Console.WriteLine("카테고리별 수량: ");
            foreach(KeyValuePair<string, int> keyValuePair in itemCountByCategory)
            {
                Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}개");
            }
        }
    }
    public class Student
    {
        public string name;
        public int studentNum;
        public int score;

        public Student(string name, int studentNum, int score)
        {
            this.name = name;
            this.studentNum = studentNum;
            this.score = score;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public Item(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }

}

