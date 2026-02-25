namespace CsharpBasicConsole
{

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public int DaysLeft { get; set; }
        public int Priority { get; set; }

        private string _priorityText;
        private string _urgencyText;
        private string _statusText;

        public TodoItem(int id, string title, bool isComplete, int daysLeft, int priority)
        {
            Id = id;
            Title = title;
            IsComplete = isComplete;
            DaysLeft = daysLeft;
            Priority = priority;

            _priorityText = GetPriorityText(priority);
            _urgencyText = GetUrgencyText(daysLeft);
            _statusText = GetStatusText(isComplete);

        }

        public string GetPriorityText(int priority)
        {
            if (priority == 1)
            {
                return "높음";
            }
            else if (priority == 2)
            {
                return "보통";
            }
            else if (priority == 3)
            {
                return "낮음";
            }
            else
            {
                return "처리안됨";
            }
        }

        public string GetUrgencyText(int daysLeft)
        {
            if (daysLeft > 7)
            {
                return "여유";
            }
            else if (daysLeft <= 7 && daysLeft > 3)
            {
                return "주의";
            }
            else
            {
                return "긴급";
            }
        }

        public string GetStatusText(bool isComplete)
        {
            return isComplete ? "완료" : "진행중";
        }

        public void PrintTodoItem()
        {

            if (_statusText == "완료")
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("완료한 항목 입니다.");
                Console.WriteLine("---------------------------");

                return;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine($"항목     :  {Title}");
            Console.WriteLine($"우선순위 :  {_priorityText}");
            Console.WriteLine($"긴급도   :  {_urgencyText}");
            Console.WriteLine($"상태     :  {_statusText}");
            Console.WriteLine("---------------------------");

        }

        public int CountCompleteTodo(bool[] isCompleteItems)
        {
            int count = 0;

            for (int i = 0; i < isCompleteItems.Length; i++)
            {
                if (isCompleteItems[i])
                {
                    count++;
                }
            }

            return count;

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


            Console.WriteLine("===== TODO 항목 =====");
            foreach(TodoItem todoItem in todoItems)
            {
                todoItem.PrintTodoItem();
            }

            Console.WriteLine("==========");

            

            Console.WriteLine($"전체: {todoItems.Length}개 | 완료: {completeCount}개");
            
        }
    }
}