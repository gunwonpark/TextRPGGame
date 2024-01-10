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
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public string Class { get; set; }
        public int Attack { get; set; }
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
                int damageVariance = (int)Math.Ceiling(Attack * 0.1);

                int finalDamage = new Random().Next(Attack - damageVariance, Attack + damageVariance);
                return finalDamage;
            }
        }
        public int Defense { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; } = 1500;
        public Player(string _name, string _class, int _attack, int _defense, int _hp)
        {
            Name = _name;
            Class = _class;
            Attack = _attack;
            Defense = _defense;
            MaxHp = _hp;
            Hp = _hp;
        }
        public void Attacked(int damage)
        {
            Hp -= damage;
        }
        public void ShowStatus()
        {
            Console.Write($"Lv. ");
            Utill.WriteRedText("{Level:D2}\n");

            Console.Write($"{Name} ( {Class} )\n");            

            Console.Write("공격력 : ");
            Utill.WriteRedText($"{Attack}");

            Console.Write($"방어력 : ");
            Utill.WriteRedText($"{Defense}");

            Console.Write($"체 력 : ");
            Utill.WriteRedText($"{Hp}\n");

            Console.Write($"Gold : ");
            Utill.WriteRedText($"{Gold}");
            Console.Write($" G\n");
        }
    }
}
