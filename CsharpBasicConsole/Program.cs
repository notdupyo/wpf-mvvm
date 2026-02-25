namespace CsharpBasicConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Ch01, Ch02 과제 
            // c#에서는 선언과 함께 초기화 하는게 정석 관행
            int todoId = 1;
            string todoTitle = "c# 공부하기";
            bool isComplete = false;
            int daysLeft = 5;
            int priority = 1; // 1:높음, 2:보통, 3:낮음
            string priorityText;
            string urgencyText;
            string statusText;

            if(priority == 1)
            {
                priorityText = "높음";
            }
            else if(priority == 2)
            {
                priorityText = "보통";
            }
            else if(priority == 3)
            {
                priorityText = "낮음";
            }
            else
            {
                priorityText = "처리안됨";
            }

            if(daysLeft > 7)
            {
                urgencyText = "여유";
            }
            else if(daysLeft <= 7 && daysLeft > 3)
            {
                urgencyText = "주의";
            }
            else if(daysLeft <= 3)
            {
                urgencyText = "긴급";
            }
            else
            {
                urgencyText = "처리안됨";
            }

            statusText = isComplete ? "완료" : "진행중";

            if (isComplete)
            {
                Console.WriteLine("완료된 항목입니다.");
                return;
            }

            System.Console.WriteLine("========= TODO 항목 ========");
            Console.WriteLine($"번호     :  {todoId}");
            Console.WriteLine($"제목     :  {todoTitle}");
            Console.WriteLine($"우선순위 :  {priorityText}");
            Console.WriteLine($"남은일   :  {daysLeft}일");
            Console.WriteLine($"긴급도   :  {urgencyText}");
            Console.WriteLine($"상태     :  {statusText}");
            Console.WriteLine("========================");


            // Ch03 과제 구현
            string[] todoTitleArr = { "c# 공부", "wpf 공부", "mvvm 공부" };
            bool[] isCompleteArr = { false, true, false };
            int[] daysLeftArr = { 5, 0, 10 };
            int completeCount = 0;


            System.Console.WriteLine("========= TODO 항목 ========");
            for (int i = 0; i < todoTitleArr.Length; i++)
            {

                if (daysLeftArr[i] > 7)
                {
                    urgencyText = "여유";
                }
                else if (daysLeftArr[i] <= 7 && daysLeftArr[i] > 3)
                {
                    urgencyText = "주의";
                }
                else if (daysLeftArr[i] <= 3)
                {
                    urgencyText = "긴급";
                }
                else
                {
                    urgencyText = "처리안됨";
                }
                

               
                Console.WriteLine($"할일     : [{i+1}] {todoTitleArr[i]}");
                if (isCompleteArr[i])
                {
                    statusText = "완료";
                    completeCount++;
                    Console.WriteLine($"완료된 항목입니다.");
                }
                else
                {
                    statusText = "진행중";
                    Console.WriteLine($"긴급도   :  {urgencyText}");
                }
                Console.WriteLine($"상태     :  {statusText}");
                Console.WriteLine("-----------------------");

            }
            Console.WriteLine($"전체: {todoTitleArr.Length}개 | 완료: {completeCount}개");
            Console.WriteLine("========================");
        }
    }
}