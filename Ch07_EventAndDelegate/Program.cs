namespace Ch07_EventAndDelegate
{
    // ===이벤트 데이터 클래스===
    // EventArgs를 상속받아 이벤트와 함께 전달할 데이터 정의
    public class TodoCompletedEventArgs : EventArgs
    {
        public string Title { get; set; }
        public DateTime CompletedTime { get; set; }

        public TodoCompletedEventArgs(string title, DateTime completedTime)
        {
            Title = title;
            CompletedTime = completedTime;
        }
    }

    // TodoItem 클래스 (이벤트 발생자)
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        // --- 이벤트 선언 ---
        // event 키워드 : 외부에서 직접 delegate에 구독한 함수 호출 불가, += / -=만 가능
        // EventHandler<T> : 표준 이벤트 델리게이트, T는 전달할 데이터 타입
        public event EventHandler<TodoCompletedEventArgs> Completed; //Competed는 델리게이트

        public TodoItem(int id, string title)
        {
            Id = id;
            Title = title;
            IsCompleted = false;
        }

        public void Complete()
        {
            if(IsCompleted)
            {
                Console.WriteLine($"{Title}은 이미 완료한 항목입니다.");
                return;
            }

            IsCompleted = true;

            // --- 이벤트 발생 ---
            // Completed? : null 체크 (구독자가 없으면 null)
            // Invoke() : 등록된 모든 메소드 호출
            // this : 이벤트 발생 주체(sender)
            // new TodoCompletedEventArgs(...) : 전달할 데이터
            Completed?.Invoke(this, new TodoCompletedEventArgs(Title, DateTime.Now));
        }
    }

    // === Logger 클래스(이벤트 구독자 1)===
    public class Logger
    {
        // 이벤트 헨들러 메서드
       public void OnTodoCompleted(object sender, TodoCompletedEventArgs e)
        {
            Console.WriteLine($"[LOG] {e.CompletedTime:HH:mm:ss} - '{e.Title} 완료함");
        }
    }

    // === Statistics 클래스 (이벤트 구독자 2) ===
    public class Statistics
    {
        public int CompletedCount { get; set; }
        public void OnTodoCompleted(object sender,TodoCompletedEventArgs e)
        {
            CompletedCount++;
            Console.WriteLine($"[STATS] 총 완료 개수: {CompletedCount}개");
        }
    }

    // === Notification 클래스 (이벤트 구독자 3) ====
    public class Notification
    {
        public void OnTodoCompleted(object sender, TodoCompletedEventArgs e)
        {
            Console.WriteLine($"[NOTIFY] '{e.Title}' 항목을 완료");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // 객체 생성
            TodoItem todo1 = new TodoItem(1, "c# 공부");
            TodoItem todo2 = new TodoItem(2, "WPF 학습");

            Logger logger = new Logger();
            Statistics stats = new Statistics();
            Notification notify = new Notification();

            // 이벤트 구독
            // += 연산자로 이벤트에 메서드 등록
            // todo1 완료 시 3개의 메서드가 실행됨
            Console.WriteLine("===이벤트 구독 설정===");
            Console.WriteLine("todo1: Logger, Statistics, Notification 구독");
            Console.WriteLine("todo2: Logger 만 구독");
            Console.WriteLine();

            todo1.Completed += logger.OnTodoCompleted;
            todo1.Completed += stats.OnTodoCompleted;
            todo1.Completed += notify.OnTodoCompleted;

            // todo 2는 notfy만 구독
            todo2.Completed += notify.OnTodoCompleted;

            Console.WriteLine("=== todo1.Complete() 호출 ===");
            todo1.Complete();
            Console.WriteLine();

            Console.WriteLine("=== todo2.Complete() 호출 ===");
            todo2.Complete();
            Console.WriteLine();

            Console.WriteLine("=== todo1.Complete() 재호출 ===");
            todo1.Complete();
            Console.WriteLine();

            // --- 이벤트 구독 해제 ---
            Console.WriteLine("=== 이벤트 구독 해제 테스트 ===");
            TodoItem todo3 = new TodoItem(3, "운동");

            todo3.Completed += logger.OnTodoCompleted;
            todo3.Completed += notify.OnTodoCompleted;

            // 구독해제
            Console.WriteLine("Notification 구독 해제");
            todo3.Completed -= notify.OnTodoCompleted;

            Console.WriteLine("todo3.Complete() 호출 (looger만 실행됨)");
            todo3.Complete();
        }
    }
}
