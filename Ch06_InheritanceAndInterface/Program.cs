using System.Globalization;

namespace Ch06_InheritanceAndInterface
{

    // 부모 클래스
    public class TodoItem
    {
        // 공통 속성
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        private int _priority;
        public int Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if(value < 1 || value > 3)
                {
                    _priority = 2;
                }
                else
                {
                    _priority = value;
                }
            }
        }

        // 생성자
        public TodoItem(int id, string title, int priority)
        {
            Id = id;
            Title = title;
            Priority = priority;
            IsComplete = false; // 기본값 false
        }

        // virtual 메서드
        // c#에서는 java와 달리 자식클래서스에서 재정의(override)할 메서드를 virtual 키워드로 만들어야 한다.
        public virtual string GetDisplayText()
        {
            string status = IsComplete ? "완료" : "진행중";
            return $"{status} [{GetPriorityText()}] {Title}"; // 아직 변수에 담지않고 문자열에 직접 함수를 호출하는게 좋은건진 모르겟음
        }

        public string GetPriorityText()
        {
            if (Priority == 1) return "높음";
            else if (Priority == 2) return "보통";
            else return "낮음";
        }

        public virtual void PrintDetail()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine($" ID: {Id}");
            Console.WriteLine($" 제목: {Title}");
            Console.WriteLine($" 우선순위: {GetPriorityText()}"); // 이것도 가독성이 안좋은데
            Console.WriteLine($" 완료: {(IsComplete ? "예" : "아니오")}"); // 이 방식도 가독성이 안좋지 않나?
            Console.WriteLine("---------------------");
        }
    }

    // 자식 클래스 1: 마감 있는 할 일
    // 콜론(:) 뒤에 부모 클래스 이름을 적어 상속 받음
    public class DeadlineTodoItem : TodoItem
    {
        // DeadlineTodoItem 고유 추가 속성
        public DateTime DeadLine { get; set; }

        // 생성자 - 부모 클래스에서 파라미터가 있는 생성자가 되어 있으므로 반드시 base 사용
        public DeadlineTodoItem(int id, string title, int priority, DateTime deadLine) : base(id, title, priority)
        {
            DeadLine = deadLine;
        }

        // override : 부모의 virtual 매서드를 재정의
        public override string GetDisplayText()
        {
            string status = IsComplete ? "o" : "x";
            // DateTime.Tosting("yyyy-MM-dd"); 날짜를 "2026-02-27" 형식 문자열로 변환
            return $"{status} [{GetPriorityText()}] {Title} (마감: {DeadLine:yyyy-MM-dd})";
        }

        public override void PrintDetail()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($" [마감 할일]");
            Console.WriteLine($"  ID: {Id}");
            Console.WriteLine($"  제목: {Title}");
            Console.WriteLine($"  우선순위: {GetPriorityText()}");
            Console.WriteLine($"  마감일: {DeadLine:yyyy-MM-dd}");
            Console.WriteLine($"  완료: {(IsComplete ? "예" : "아니오")}");

            int daysLeft = (DeadLine - DateTime.Now).Days;
            if( daysLeft < 0)
            {
                Console.WriteLine($" 마감 {-daysLeft}일 초과");
            }
            else
            {
                Console.WriteLine($" 남은 일수: {daysLeft}일");
            }
            Console.WriteLine("---------------------------------");
        }
    }

    // 자식 클래스2: 반복 할 일
    public class RepeatingTodoItem : TodoItem
    {
        // RepeatingTodoItem만의 추가 속성
        public int RepeatDays { get; set; }

        // id, title, priority는 부모에 정의 되어있으니 사용가능
        public RepeatingTodoItem(int id, string title, int priority, int repeatDays) : base(id, title, priority)
        {
            RepeatDays = repeatDays;
        }

        public override string GetDisplayText()
        {
            string status = IsComplete ? "o" : "x";
            return $"{status} [{GetPriorityText()}] {Title} ({RepeatDays}일마다 반복)";
        }

        public override void PrintDetail()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine($"  [반복 할일]");
            Console.WriteLine($"  ID: {Id}");
            Console.WriteLine($"  제목: {Title}");
            Console.WriteLine($"  우선순위: {GetPriorityText()}");
            Console.WriteLine($"  반복 주기: {RepeatDays}일마다");
            Console.WriteLine($"  완료: {(IsComplete ? "예" : "아니오")}");
            Console.WriteLine("---------------------------");
        }
    }

    // 인터페이스 정의
    // interface: 구현 없이 메서드 시그니처만 정의
    // 관례상 이름 앞에 I(대문자 i)를 붙임
    public interface IExportable
    {
        // 세미콜론으로 끝, 구현부 없음
        string ExportToText();
    }

    // 인터페이스 구현 클래스
    public class  ExportableTodoItem : TodoItem, IExportable
    {
        public ExportableTodoItem(int id, string title, int priority) : base(id, title, priority)
        {
            // 내용없음, 초기화할 자체 필드 없음
        }

        // 인터페이스에 정의되어 있음 메소드, 즉 반드시 구현해야 함
        public string ExportToText()
        {
            string status = IsComplete ? "완료" : "미완료";
            return $"{Id}, {Title}, {Priority}, {status}";
        }
    }


   public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("다형성 데모");

            // 부모 타입 배열에 여러 자식 타입 객체를 담을 수 있음
            TodoItem[] todoItems = new TodoItem[4];

            // 각각 다른 타입의 객체 생성
            todoItems[0] = new TodoItem(1, "c#학습", 2);
            todoItems[1] = new DeadlineTodoItem(2, "wpf학습", 1, new DateTime(2026, 3, 15));
            todoItems[2] = new RepeatingTodoItem(3, "운동하기", 2, 2);
            todoItems[3] = new DeadlineTodoItem(4, "일기쓰기", 1, new DateTime(2026, 2, 20));

            // 완료 처리 테스트
            todoItems[0].IsComplete = true;

            Console.WriteLine("할 일 목록");
            // 같은 메서드 호출 / 다른 결과 확인
            foreach(TodoItem item in todoItems)
            {
                // 각 객체에 구현된 내용 다른 GetDisplayText() 호출
                Console.WriteLine(item.GetDisplayText());
            }

            // 상세정보 출력
            Console.WriteLine("\n상세정보출력");
            foreach(TodoItem item in todoItems)
            {
                item.PrintDetail();
            }

            // 인터페이스 활용
            Console.WriteLine("인터페이스 내보내기");

            ExportableTodoItem exportableTodoItem = new ExportableTodoItem(5, "인터페이스 학습", 1);
            exportableTodoItem.IsComplete = true;

            // 인터페이스 타입 변수에 담기
            IExportable exportable = exportableTodoItem;

            Console.WriteLine("CSV 형식 내보내기:");
            Console.WriteLine(exportable.ExportToText());

            // 타입 확인
            Console.WriteLine("\n 타입확인");
            foreach(TodoItem item in todoItems)
            {
                // is 연산자 : 객체가 특정 타입인지 확인
                if (item is DeadlineTodoItem)
                {
                    Console.WriteLine($"'{item.Title}'은(는) 마감 할일입니다.");
                }
                else if (item is RepeatingTodoItem)
                {
                    Console.WriteLine($"'{item.Title}'은(는) 반복 할일입니다.");
                }
                else
                {
                    Console.WriteLine($"'{item.Title}'은(는) 일반 할일입니다.");
                }
            }

        }
    }
}
