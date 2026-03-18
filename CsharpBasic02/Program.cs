namespace CsharpBasic02
{
    class Program
    {
        static void Main(string[] args)
        {

            // 예외처리 과제
            List<Character> lst = new List<Character>();
            try
            {
                Warrior warrior1 = new Warrior("홍길동", "전사", 15, 150, 10);
                lst.Add(warrior1);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"오류: {ex.Message}");
            }

            try
            {
                Warrior warrior2 = new Warrior("이순신", "전사", 50, 500, 50);
                lst.Add(warrior2);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"오류: {ex.Message}");
                
            }

            try
            {
                Mage mage1 = new Mage("", "마법사", 15, 150, 10);
                lst.Add(mage1);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"오류: {ex.Message}"); 
            }

            try
            {
                Mage mage2 = new Mage("홍길동", "전사", -4, 150, 10);
                lst.Add(mage2);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"오류: {ex.Message}");
            }

            try
            {
                Warrior warrior3 = new Warrior("아이유", "전사", 19, 0, 10);
                lst.Add(warrior3); 
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"오류: {ex.Message}");
            }

            foreach(Character character in lst)
            {
                Console.WriteLine($"{character.Name}, {character.CharacterClass}, {character.Level}, {character.Hp}, {character.Mp}");
            }

            // 람다식, LINQ 과제
            lst.Add(new Warrior("가나다", "전사", 2, 20, 3));
            lst.Add(new Warrior("라마바", "전사", 20, 200, 30));
            lst.Add(new Warrior("고노도", "전사", 12, 120, 6));
            lst.Add(new Mage("신한", "마법사", 1, 10, 30));
            lst.Add(new Mage("해리포터", "마법사", 33, 330, 660));
            lst.Add(new Mage("전우치", "마법사", 50, 500, 1000));

            // 레벨이 10 이상인 목록
            List<Character> overLevel10List = lst.Where(character => character.Level >= 10).ToList();
            Console.WriteLine("레벨 10 이상 목록: ");
            foreach(Character character in overLevel10List)
            {
                Console.WriteLine($"[{character.CharacterClass}] {character.Name} LV.{character.Level} HP:{character.Hp} MP:{character.Mp}");
            }

            // 직업이 전사만 목록
            List<Character> warriorList = lst.Where(character => character.CharacterClass == "전사").ToList();
            Console.WriteLine("전사 목록:");
            foreach (Character character in warriorList)
            {
                Console.WriteLine($"[{character.CharacterClass}] {character.Name} LV.{character.Level} HP:{character.Hp} MP:{character.Mp}");
            }

            // 레벨 내림차순 정렬
            List<Character> sortDscByLevel = lst.OrderByDescending(character => character.Level).ToList();
            Console.WriteLine("레벨 내림차순 DSC: ");
            foreach (Character character in sortDscByLevel)
            {
                Console.WriteLine($"[{character.CharacterClass}] {character.Name} LV.{character.Level} HP:{character.Hp} MP:{character.Mp}");
            }

            // 이름만 추출
            List<string> nameList = lst.Select(chracter => chracter.Name).ToList();
            Console.WriteLine("이름만 출력: ");
            foreach (string name in nameList)
            {
                Console.WriteLine($"{name}");
            }

            // 전체 캐릭터 평균레벨
            double avgLevel = lst.Average(character => character.Level);
            Console.WriteLine($"평균레벨: {avgLevel}");

            // hp가 가장 높은 캐릭터
            Character topHpCharacter = lst.OrderByDescending(character => character.Hp).FirstOrDefault();
            Console.WriteLine($"최고 HP 캐릭터 {topHpCharacter.Name} - hp: {topHpCharacter.Hp}");

            // 레벨 20이상인 마법사 존재 여부
            bool hasOverLevel20Mage = lst
                .Where(character => character.CharacterClass == "마법사")
                .Any(character => character.Level >= 20);
            Console.WriteLine($"레벨 20이상 마법사 여부 : {hasOverLevel20Mage}");

        }
    }

    public class Character
    {
        public string Name { get; set; }
        public string CharacterClass { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }

        public Character(string name, string characterClass, int level, int hp, int mp)
        {
            // 여기에 throw 유효성 검사 추가

            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("이름엔 빈 값이 들어올 수 없습니다.");
            }
            
            if(level < 1 || level > 99)
            {
                throw new ArgumentException("레벨은 1~99 사이의 정수입니다.");
            }

            if(hp <= 0)
            {
                throw new ArgumentException("Hp는 0 보다 커야 합니다.");
            }

            Name = name;
            CharacterClass = characterClass;
            Level = level;
            Hp = hp;
            Mp = mp;
        }
    }

    public class Warrior : Character
    {
        public Warrior(string name, string characterClass, int level, int hp, int mp)
            : base(name, characterClass, level, hp, mp)
        {
        }
    }

    public class Mage : Character
    {
        public Mage(string name, string characterClass, int level, int hp, int mp)
            : base(name, characterClass, level, hp, mp)
        {
        }
    }
}
