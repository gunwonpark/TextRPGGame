using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    // Stage
    class Stage
    {
        public int Level { get; set; } = 1;
        
        private List<Monster> monsters;
        public Stage() {}
        public List<Monster> CreateMonster() 
        {
            if(Level == 1)
            {
                monsters = new List<Monster>
                {
                    new Monster("슬라임", 3, 15, 3),
                    new Monster("빨간슬라임", 5, 20, 5),
                    new Monster("거대슬라임", 10, 100, 7)
                };
                return monsters;
            }
            else if (Level == 2)
            {
                monsters = new List<Monster>
                {
                    new Monster("스켈레톤", 4, 17, 5),
                    new Monster("스켈레톤", 6, 24, 6),
                    new Monster("스켈레톤킹", 15, 150, 10)
                };
                return monsters;
            }
            else
            {
                monsters = new List<Monster>
                {
                   new Monster("Level99999",999999,9999,9999)
                };
                return monsters;
            }
        }
    }
}
