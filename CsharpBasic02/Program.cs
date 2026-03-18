namespace CsharpBasic02
{
    class Program
    {
        static void Main(string[] args)
        {
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
