namespace Ch05_ClassAndObject
{
    /// <summary>
    /// 클래스와 객체
    /// 
    /// </summary>


    // BacnkAccount 클래스
    public class BankAccount
    {
        // 접근 제한자
        // private : 같은 클래스 안에서만 접근 가능
        // protected : 같은 클래스 안에서만 접근 가능 + 상속받은 클래스에서 접근 가능
        // public : 어디서든 접근 가능
        // internal : 같은 프로젝트 안에서만 접근 가능(클래스간 접근 가능)

        private decimal _balance; // private변수명은 _로 시작하는것이 관례

        public string OwnerName { get; set; } // 프로퍼티는 대문자 시작이 관례
        public string AccountNumber { get; set; }

        public decimal Balance
        { 
            // 읽기 전용 프로퍼티가 됨, set 구문 없음
            get
            {
                return _balance;
            }
        }

        // 위에서 _balance를 private로 정의하고, Balance는 public으로 정의함
        // 이 의도는 외부에서 set을 금지하여 다른 클래스에서 값이 변경 되는 것을 방지
        // 내부에서 _balance에 값을 대입하여 Balance 프로퍼티의 값을 설정하는 방식

        // Constuctor (생성자)
        // 생성자를 정의하지 않으면 parameter가 없는 기본 생성자가 자동으로 생성됨
        public BankAccount(string ownerName, string accountNumber, decimal initalBalance)
        {
            OwnerName = ownerName;
            AccountNumber = accountNumber;
            _balance = initalBalance;
        }

        public void Deposit(decimal amount)
        {
            // 방어코드 패턴, 초기에 예외를 잡아서 return시킴
            // 함수 내부 로직을 끝까지 수행하지 않고 종료하므로 성능에 이점이 있음
            if(amount <=0 )
            {
                Console.WriteLine("입금액은 0보다 커야 합니다.");
                return;
            }

            _balance += amount;
            // amount:N0 => 서식지정자, 출력하는 문자열에 서식을 지정함
            Console.WriteLine($"{amount:N0}원 입금 완료. 잔액: {_balance:N0}원");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("출금액은 0보다 커야 합니다.");
                return;
            }
            if (amount > _balance)
            {
                Console.WriteLine("잔액이 부족합니다.");
                return;
            }
            _balance -= amount;
            Console.WriteLine($"{amount:N0}원 출금 완료. 잔액: {_balance:N0}원");
        }

        public void PrintInfo()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine($"예금주   : {OwnerName}");
            Console.WriteLine($"계좌번호 : {AccountNumber}");
            Console.WriteLine($"잔액     : {Balance:N0}원");
            Console.WriteLine("---------------------------");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 객체 생성 - 생성자에 초기값 전달
            BankAccount account1 = new BankAccount("홍길동", "110-1234-5678", 100000);
            BankAccount account2 = new BankAccount("김철수", "110-9987-5432", 50000);

            // 계좌 정보 출력
            account1.PrintInfo();
            account2.PrintInfo();

            // 입출금
            account1.Deposit(30000);
            account1.Withdraw(200000000);
            account1.Withdraw(50000);

            Console.WriteLine();
            account1.PrintInfo();
        }
    }
}
