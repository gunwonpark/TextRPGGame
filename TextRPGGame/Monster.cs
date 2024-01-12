using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TextRPGGame
{
    class Monster
    {
        public static Random rand = new Random();
        int hp;
        public string Name { get; }
        public int Level { get; }
        public int HP
        {
            get { return hp; }
            set
            { 
                hp = value; 
                if (hp < 0) hp = 0;
                if (value > MaxHp) hp = MaxHp;
            }
        }
        public int Attack { get; }
        public int MaxHp { get; set; }

        public bool IsDead
        {
            get
            {
                if (HP <= 0)
                    return true;
                else
                    return false;
            }
        }

        public Monster(string name, int level, int _hp, int atk)
        {
            Name = name;
            Level = level;
            hp = _hp;
            MaxHp = _hp;
            Attack = atk;
        }
        public Monster DeepCopy()
        {
            // 현재 몬스터의 속성을 사용하여 새로운 몬스터 객체를 생성합니다.
            Monster newMonster = new Monster(Name, Level, MaxHp, Attack);
            // 새로운 몬스터 객체를 반환합니다.
            return newMonster;
        }

        public void ShowStatus()
        {
            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{Level} {Name}  Dead");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
                Console.Write($"Lv.");
                Utill.WriteRedText($"{Level} ");
                Console.Write($"{Name} ");
                Console.Write("Hp ");
                Utill.WriteRedText($"{HP}");
                Console.WriteLine();
            }
        }

        public void Attacked(int damage)
        {
            HP -= damage;
        }
    }
}