using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    public class Monster
    {
        // 스텟
        int hp;
        public string Name { get; set; }
        public int Level { get; set; }
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
        public int Attack { get; set; }
        public bool IsDead
        {
            get
            {
                if (Hp <= 0)
                {
                    GameManager.Instance.questBoard.Check_MonsterQuest(Name);
                    return true;
                }
                else
                    return false;
            }
        }
        public Monster(string _name, int _level, int _hp, int _attack)
        {
            Name = _name;
            Level = _level;
            MaxHp = _hp;
            hp = _hp;
            Attack = _attack;
        }

        public Monster DeepCopy()
        {
            Monster newMonster = new Monster(Name, Level, MaxHp, Attack);
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
                Utill.WriteRedText($"{Hp}");
                Console.WriteLine();
            }
        }

        public void Attacked(int damage)
        {
            Hp -= damage;
        }


    }
}
