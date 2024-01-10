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
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public Player(string _name, string _class, int _attack, int _defense, int _hp)
        {
            Name = _name;
            Class = _class;
            Attack = _attack;
            Defense = _defense;
            Hp = _hp;
        }
        public void ShowStatus()
        {
            Console.WriteLine($"Lv. {Level:D2}\n");
            Console.WriteLine($"{Name} ( {Class} )");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G\n");
        }
    }
}
