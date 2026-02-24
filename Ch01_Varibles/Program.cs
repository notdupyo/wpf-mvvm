namespace Ch01_Variables
{
    public class Program
    {
        public static void Main()
        {
            string name = "홍길동";
            int age = 20;
            double tall = 175.5;  // 정교한 실수, 실수형 타입은 float도 있음
            string hobby = "독서";
            const string GOAL = "혁명"; // 상수, 대문자 시작이 관례, 하지만 전체 대문자로 많이들 사용

            // c#의 보간법 [$""], 보간법 사용 권장
            Console.WriteLine("=====자기소개=====");
            Console.WriteLine($"이름:  {name}");
            Console.WriteLine($"나이:  {age}");
            Console.WriteLine($"키  :  {tall}");
            Console.WriteLine($"취미:  {hobby}");
            Console.WriteLine($"목표:  {GOAL}");
            Console.WriteLine("==================");
        }
    }
}