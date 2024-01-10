using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Player
    {
        // 스텟
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Class { get; set; } = "전사";
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int MaxHp { get; set; } = 100;
        public int Hp { get; set; } = 100;
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
