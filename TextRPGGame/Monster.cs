using System;
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
        public string Name { get; }
        public int Level { get; }
        public int HP { get; set; }
        public int Atk { get; }

        public bool IsDead => HP <= 0;

        public Monster(string name, int level, int hp, int atk)
        {
            Name = name;
            Level = level;
            HP = hp;
            Atk = atk;
        }

        public int TakeDamage(int damage)
        {
            if (!IsDead)
            {
                int actualDamage = Math.Max(1, (int)Math.Ceiling(damage * (rand.Next(9, 12) / 10.0)));
                HP -= actualDamage;
                if (HP < 0)
                {
                    HP = 0;
                }
                return damage;
            }
            return 0;
        }
    }
}