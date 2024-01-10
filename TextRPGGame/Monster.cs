using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Monster
    {
        // 스텟
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public Monster(string _name, int _level, int _hp, int _atk)
        {
            Name = _name;
            Level = _level;
            HP = _hp;
            ATK = _atk;
        }
        public void ShowStatus()
        {
            Console.WriteLine($"Lv.{Level} {Name}  HP {HP}");
        }
    }
}
