using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    public class Player
    {
        public string playerName;
        public int level;
        public int gold;
        public string clas;
        public int currentHealth;
        public int maxHealth;
        public float attackDamge;
        public float increaseInDamage;
        public int armor;
        public int increaseInArmor;
        public float currentExp;
        public float levelUpExp;
        public bool death;




        public Player()
        {

        }



        public void Init()
        {
            playerName = "이름";
            level = 1;
            clas = "(전사)";
            attackDamge = 10;
            armor = 5;
            maxHealth = 100;
            currentHealth = maxHealth;
            gold = 1500;
            currentExp = 0;
            levelUpExp = 20;

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


        public void GainExperience(float exp)
        {
            currentExp += exp;

            if(currentExp >= levelUpExp)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            level++;
            attackDamge += 0.5f;
            armor++;
            maxHealth += 10;
            currentHealth = maxHealth;
            currentExp = currentExp - levelUpExp;
            levelUpExp = levelUpExp * 1.5f;
        }

    }
}
