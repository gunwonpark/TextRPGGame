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
        public int maxHealth;
        public int currentHealth;
        public float damage;
        public bool death;
        public string attackSkill;
        public float rewardExp;


        public Monster(string monsterName,int initHealth,float initDamage, string _attackSkill,float _rewardExp)
        {
            name = monsterName;
            level = 1;
            maxHealth = initHealth;
            currentHealth = maxHealth;
            damage = initDamage;
            attackSkill = _attackSkill;
            rewardExp = _rewardExp;
        }


        public void SetMonsterState()
        {
            int randomLevel = new Random().Next(1, 6);
            level = randomLevel;
            maxHealth += randomLevel * 5;
            damage += randomLevel * 2;


            currentHealth = maxHealth;
            rewardExp += randomLevel*2;
        }




        public Monster Clone()
        {
            Monster clone = new Monster(this.name,this.maxHealth,this.damage,this.attackSkill,this.rewardExp);
            return clone;
        }


        public void TakeDamage(float damage)
        {
            float damageVariation = damage / 10;
            int finalAttackPower = new Random().Next((int)Math.Round(damage - damageVariation), (int)Math.Round(damage + damageVariation));

            currentHealth -= finalAttackPower;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                death = true;
            }
        }

    }
}
