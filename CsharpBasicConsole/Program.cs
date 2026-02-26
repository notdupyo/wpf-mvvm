namespace CsharpBasicConsole
{

    public class TodoItem
    {
        // 검증 로직이 필요할때 수동 get, set 구현 -> private 필드 필요함
        // 단순 읽기/쓰기 용 변수면 필드 자동 생성 프로퍼티 사용 -> private 필드 필요 없음
        private int _priority;

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public int DaysLeft { get; set; }
        public int Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (value > 3 || value < 1)
                {
                    _priority = 2;
                }
                else
                {
                    _priority = value;
                }
            }
        }


        // private string _priorityText = GetPriorityText();
        // : 초기화에서 함수 호출시 객체 생성전에 실행이 되어 호출 불가능, 오류임

        public TodoItem(int id, string title, bool isComplete, int daysLeft, int priority)
        {
            Id = id;
            Title = title;
            IsComplete = isComplete;
            DaysLeft = daysLeft;
            Priority = priority;
        }

        public string GetPriorityText()
        {
            if (Priority == 1)
            {
                return "높음";
            }
            else if (Priority == 2)
            {
                return "보통";
            }
            else if (Priority == 3)
            {
                return "낮음";
            }
            else
            {
                return "처리안됨";
            }
        }

        public string GetUrgencyText()
        {
            if (DaysLeft > 7)
            {
                return "여유";
            }
            else if (DaysLeft <= 7 && DaysLeft > 3)
            {
                return "주의";
            }
            else
            {
                return "긴급";
            }
        }

        public string GetStatusText()
        {
            return IsComplete ? "완료" : "진행중";
        }

        public void Print()
        {
            string priorityText = GetPriorityText();
            string urgencyText = GetUrgencyText();
            string statusText = GetStatusText();

            if (IsComplete)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("완료한 항목 입니다.");
                Console.WriteLine("---------------------------");

                return;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine($"항목     :  {Title}");
            Console.WriteLine($"우선순위 :  {priorityText}");
            Console.WriteLine($"긴급도   :  {urgencyText}");
            Console.WriteLine($"상태     :  {statusText}");
            Console.WriteLine("---------------------------");

        }


    }
    public class Program
    {

        public static void Main(string[] args)
        {
            // c#에서는 선언과 함께 초기화 하는게 정석 관행
            TodoItem todoItem1 = new TodoItem(1, "c#공부", false, 5, 1);
            TodoItem todoItem2 = new TodoItem(2, "wpf공부", true, 0, 3);
            TodoItem todoItem3 = new TodoItem(3, "mvvm공부", false, 10, 2);

            TodoItem[] todoItems = { todoItem1, todoItem2, todoItem3 };

            int completeCount = 0;


            Console.WriteLine("===== TODO 항목 =====");
            foreach(TodoItem todoItem in todoItems)
            {
                todoItem.Print();
            }
            Console.WriteLine("==========");

            completeCount = CountCompleteTodo(todoItems);

            Console.WriteLine($"전체: {todoItems.Length}개 | 완료: {completeCount}");
        }

        static int CountCompleteTodo(TodoItem[] items)
        {
            int count = 0;

            foreach (TodoItem todoItem in items)
            {
                if(todoItem.IsComplete)
                {
                    count++;
                }
            }

            return count;
        }

    }
}