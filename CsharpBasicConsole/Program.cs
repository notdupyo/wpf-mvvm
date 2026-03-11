namespace CsharpBasicConsole
{
    public class CharacterEventArgs : EventArgs
    {
        public string CharacterName { get; set; }
        public int Amount { get; set; }
        public int CurrentHp { get; set; }

        public CharacterEventArgs(string characterName, int amount, int currentHp)
        {
            CharacterName = characterName;
            Amount = amount;
            CurrentHp = currentHp;
        }
    }

    public class BattleLog
    {
        
        public void OnCharacterDamaged(CharacterEventArgs eventArgs)
        {
            Console.WriteLine($"{eventArgs.CharacterName}이 {eventArgs.Amount} 데미지를 받음 (현재 HP: {eventArgs.CurrentHp})");
        }

        public void OnCharacterDied(CharacterEventArgs eventArgs)
        {
            Console.WriteLine($"{eventArgs.CharacterName}이 사망했습니다.");
        }
    }

    public class Character
    {
        public string Name;
        public string CharacterClass;
        public int Level;
        public int Hp;
        public int Mp;

        // 이벤트 추가
        // Action 반환값 없음
        // Func 반환값 있음
        // EventHandler 이벤트 처리 전용 
        public event Action<CharacterEventArgs> Damaged;
        public event Action<CharacterEventArgs> Died;
       

        public Character(string name, string characterClass, int level, int hp, int mp)
        {
            Name = name;
            CharacterClass = characterClass;
            Level = level;
            Hp = hp;
            Mp = mp;

        }

        public void TakeDamage(int amount)
        {
            this.Hp = this.Hp - amount;

            
            Damaged?.Invoke(new CharacterEventArgs(this.Name, amount, this.Hp));

            if (this.Hp <= 0)
            {
                Died?.Invoke(new CharacterEventArgs(this.Name, amount, this.Hp));
                return;
            }
        }

        public virtual string GetStatus()
        {
            return $"HP: {Hp} | Mp: {Mp}";
        }

        public void PrintInfo()
        {
            Console.WriteLine("=====캐릭터정보=====");
            Console.WriteLine($"[{CharacterClass}] {Name} (Lv.{Level})");
            Console.WriteLine(GetStatus());

        }
    }

    public class Warrior : Character, ISkillable
    {
        private int _rage;
        public int Rage
        {
            get
            {
                return _rage;
            }
            set
            {
                if(value < 0 || value > 100)
                {
                    Console.WriteLine("0~100 사이값 입력");

                }
                else
                { // set 문에서 return 하지않음
                    _rage = value;
                }
                
            }
        }
        public Warrior(string name, string characterClass, int level, int hp, int mp, int rage = 0) : base(name, characterClass, level, hp, mp)
        {
            Rage = rage;
        }

        public override string GetStatus()
        {
            string statusResult = base.GetStatus();

            return statusResult + $"| Rage: {Rage}";
        }

        public void UseSkill()
        {
            Console.WriteLine("Smash! You give tone of demage enemy");
        }
    }

    public class Mage : Character, ISkillable
    {
        public string Element;

        public Mage(string name, string characterClass, int level, int hp, int mp, string element) : base(name, characterClass, level, hp, mp)
        {
            Element = element;
        }

        public override string GetStatus()
        {
            return base.GetStatus() + $"| Element: {Element}";
        }

        public void UseSkill()
        {
            Console.WriteLine($"{Element} Magic! You atack enemy");
        }
    }

    public class Trap : ISkillable
    {
        public void UseSkill()
        {
            Console.WriteLine("트랩 스킬이 발동했습니다.");
        }
    }

    public interface ISkillable
    {
        public void UseSkill();
    }

    public class Program
    {
        public static void Main(string[] argv)
        {
            Trap trap = new Trap();

            Character[] characters = new Character[2];

            Warrior warrior = new Warrior("아서", "전사", 10, 150, 30, 80);
            Mage mage = new Mage("멀린", "마법사", 12, 80, 200, "불");

            characters[0] = warrior;
            characters[1] = mage;

            foreach(Character character in characters)
            {
                character.PrintInfo();
            }

            // 인터페이스를 타입으로 선언하여 공통점이 없는 클래스간에
            // "공통 기능"으로 묶어서 기능을 사용한다.
            // Character 객체와 Trap객체는 공통점이 없지만 같은 기능을 공유해야 한다고 가정
            // Warrior, Mage, Trap 객체는 모두 ISkillable 인터페이스를 구현하여 인터페이스 타입의 배열에 할당 가능
            ISkillable[] skillables = new ISkillable[3];
            skillables[0] = warrior;
            skillables[1] = mage;
            skillables[2] = trap;

            Console.WriteLine("=====스킬사용=====");
            foreach(ISkillable skillable in skillables)
            {
                skillable.UseSkill();
            }

            BattleLog battleLog = new BattleLog();
            warrior.Damaged += battleLog.OnCharacterDamaged;
            warrior.Died += battleLog.OnCharacterDied;
            mage.Damaged += battleLog.OnCharacterDamaged;
            mage.Died += battleLog.OnCharacterDied;

            Console.WriteLine("====전투시작====");
            warrior.TakeDamage(30);
            mage.TakeDamage(50);
            warrior.TakeDamage(100);
            mage.TakeDamage(50);

        }
    }


}