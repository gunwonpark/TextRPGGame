using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPGGame
{
    class Player
    {
        // 스텟
        int hp;
        int mp;
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public ClassType Class { get; set; }
        public int Attack { get; set; }
        [NonSerialized]
        public List<Monster> slainMonsters;
        public int Exp { get; set; }
        public bool IsDead
        {
            get
            {
                if (Hp <= 0)
                    return true;
                else
                    return false;
            }
        }
        public int FinalAttack
        {
            get
            {
                // 최종 타격
                int finalDamage;

                // 공격 오차율 10%
                int damageVariance = (int)Math.Ceiling(Attack * 0.1);

                // 공격 관련 확률 구하기
                Random random = new Random();
                int isCriticalHit = random.Next(0, 20);
                int isMiss = random.Next(0, 10);

                // 10%의 확률로 공격 회피
                if (isMiss < 1)
                {
                    finalDamage = 0;
                }
                // 90%의 확률로 공격
                else
                {
                    // 15%의 확률로 치명타(160% 데미지), 85%의 확률로 일반공격(10% 오차 데미지)
                    finalDamage = isCriticalHit < 3 ? (int)Math.Ceiling(Attack * 1.6) : new Random().Next(Attack - damageVariance, Attack + damageVariance + 1);
                }

                return finalDamage;
            }
        }
        public int Defense { get; set; }
        public int MaxHp { get; set; }
        public int Hp
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp < 0) hp = 0;
                if (value > MaxHp) hp = MaxHp;

            }
        }
        public int MaxMp { get; set; }
        public int Mp
        {
            get { return mp; }
            set
            {
                mp = value;
                if (mp < 0) mp = 0;
                if (value > MaxMp) mp = MaxMp;
            }
        }
        public int Gold { get; set; } = 1500;
        public int EquipmentAttack { get; set; }
        public int EquipmentDefense { get; set; }

        public List<Item> inventory;
        public Weapon equippedWeapon = null;
        public Shield equippedShield = null;
        public Player(string _name, ClassType _class)
        {
            Name = _name;
            Class = _class;
            slainMonsters = new List<Monster>();

            // 인벤토리 test용
            inventory = new List<Item>()
            {
                new Weapon("초보자 칼", "초보자에게 걸맞은 칼이다", 3, 200),
                new Shield("초보자 방패", "초보자에게 걸맞은 방패이다", 2, 200)
            };
        }
        public void InitializeClassStats()
        {

            switch (Class)
            {
                case ClassType.전사:
                    Attack = 8;
                    Defense = 10;
                    MaxHp = 120;
                    MaxMp = 50;
                    break;
                case ClassType.궁수:
                    Attack = 10;
                    Defense = 8;
                    MaxHp = 100;
                    MaxMp = 70;
                    break;
                case ClassType.마법사:
                    Attack = 12;
                    Defense = 6;
                    MaxHp = 90;
                    MaxMp = 120;
                    break;
                default:
                    Attack = 8;
                    Defense = 8;
                    MaxHp = 80;
                    MaxMp = 80;
                    break;
            }
            // 초기 HP, Mp를 최대치로 설정
            Hp = MaxHp;
            Mp = MaxMp;
        }


        public void Attacked(int damage)
        {
            Hp -= damage;
        }

        public void UseSkill(int skillMp)
        {
            Mp -= skillMp;
        }

        public void ShowStatus()
        {
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{Level:D2}\n");

            Console.Write($"{Name} ( {Class} )\n");

            Console.Write("공격력 : ");
            Utill.WriteRedText($"{Attack} {EquipmentAttack.FormatNumber()}\n");

            Console.Write($"방어력 : ");
            Utill.WriteRedText($"{Defense} {EquipmentDefense.FormatNumber()}\n");
            Console.Write($"체 력 : ");
            Utill.WriteRedText($"{Hp}\n");

            Console.Write($"마 나 : ");
            Utill.WriteRedText($"{Mp}\n");

            Console.Write($"Gold : ");
            Utill.WriteRedText($"{Gold}");
            Console.WriteLine($" G\n");
        }
        public void ShowInventroy()
        {

            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < inventory.Count; i++)
            {
                Utill.WriteRedText($"{i + 1} ");
                inventory[i].ItemInfo();
            }
        }
        public enum ClassType
        {
            None = 0,
            전사 = 1,
            궁수 = 2,
            마법사 = 3
        }

        public void GainExp(int exp)
        {
            Exp += exp;
            CheckLevelUp();
        }

        public bool CheckLevelUp()
        {
            int requiredExp = Level * (Level + 1) * 5;
            if (Exp >= requiredExp)
            {
                Level++;
                Exp = 0;
                Attack += 2; // 공격력 상승
                Defense += 1; // 방어력 상승
                MaxHp += 30;
                Hp = MaxHp;
                MaxHp += 15;
                Hp = MaxHp;
                LevelUpMessage();
                return true;
            }
            return false;
        }
        void LevelUpMessage()
        {
            Console.WriteLine("\n축하합니다 레벨업 하였습니다\n");
            Console.WriteLine($"레벨 : {Level - 1} -> {Level}");
            Console.WriteLine($"최대 체력 : {MaxHp - 30} -> {MaxHp}");
            Console.WriteLine($"최대 마나 : {MaxMp - 15} -> {MaxMp}");
            Console.WriteLine($"공격력 : {Attack - 2} -> {Attack}");
            Console.WriteLine($"방어력 : {Defense - 1} -> {Defense}\n");
        }
    }
}
