namespace CsharpBasicConsole
{
    public class Program
    {
        // ch04 과제, 내부 로직 함수로 분리
        static string GetPriorityText(int priority)
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

        static string GetUrgencyText(int daysLeft)
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

        static string GetStatusText(bool isComplete)
        {
            return isComplete ? "완료" : "진행중";
        }

        static void PrintTodoItem(string title, string priorityText, string urgencyText, string statusText)
        {

            if(statusText == "완료")
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("완료한 항목 입니다.");
                Console.WriteLine("---------------------------");

                return;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine($"항목     :  {title}");
            Console.WriteLine($"우선순위 :  {priorityText}");
            Console.WriteLine($"긴급도   :  {urgencyText}");
            Console.WriteLine($"상태     :  {statusText}");
            Console.WriteLine("---------------------------");

        }

        static int CountCompleteTodo(bool[] isCompleteItems)
        {
            int count = 0;

            for(int i = 0; i < isCompleteItems.Length; i++)
            {
                if (isCompleteItems[i])
                {
                    count++;
                }
            }

            return count;

        }

        public static void Main(string[] args)
        {
            // c#에서는 선언과 함께 초기화 하는게 정석 관행

            string[] todoItems = { "c#공부", "wpf공부", "mvvm공부" };
            int[] daysLeftItems = { 5, 0, 10 };
            bool[] isCompleteItems = { false, true, false };
            int[] priorityItems = { 1, 3, 2 }; 

            int completeCount = CountCompleteTodo(isCompleteItems);

            Console.WriteLine("===== TODO 항목 =====");

            for (int i = 0; i < todoItems.Length; i++)
            {
                string title = todoItems[i];
                string priorityText = GetPriorityText(priorityItems[i]);
                string urgencyText = GetUrgencyText(daysLeftItems[i]);
                string statusText = GetStatusText(isCompleteItems[i]);

                PrintTodoItem(title, priorityText, urgencyText, statusText);
            }

            Console.WriteLine("==========");

            

            Console.WriteLine($"전체: {todoItems.Length}개 | 완료: {completeCount}개");
            
        }
    }
}