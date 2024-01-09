using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    public class Monster
    {
        public string name;
        public int level;
        public int health;
        public float damage;
        public bool death;

        public Monster(string monsterName,int initHealth,float initDamage)
        {
            name = monsterName;
            level = 1;
            health = initHealth;
            damage = initDamage;
        }


        public void SetMonsterState()
        {
            int randomLevel = new Random().Next(1, 6);
            level = randomLevel;
            health += randomLevel * 5;
            damage += randomLevel * 2;

        }

        public Monster Clone()
        {
            Monster clone = new Monster(this.name,this.health,this.damage);
            return clone;
        }

    }
}
