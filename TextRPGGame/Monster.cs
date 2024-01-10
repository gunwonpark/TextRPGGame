using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Monster
    {
        // 스텟
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int ATK { get; set; }
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
        public Monster(string _name, int _level, int _hp, int _atk)
        {
            Name = _name;
            Level = _level;
            Hp = _hp;
            ATK = _atk;
        }

        public void ShowStatus()
        {
            if (IsDead)
            {
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
