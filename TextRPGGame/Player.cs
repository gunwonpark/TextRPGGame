using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    public class Player:ConsoleText
    {
        public string playerName;
        public int level;
        public int gold;
        public string clas;
        public int currentHealth;
        public int maxHealth;
        public float attackDamge;
        public float increaseInDamage;
        public int currentMana;
        public int maxMana;
        public int armor;
        public int increaseInArmor;
        public float currentExp;
        public float levelUpExp;
        public bool death;

        public Skill[] skills = new Skill[2];


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
            maxMana = 50;
            currentMana = maxMana;
            gold = 1500;
            currentExp = 0;
            levelUpExp = 20;

            skills[0] = new Skill();
            skills[0].SetSkillState("Arpha Strike", 10, 2,"공격력 * 2 로 하나의 적을 공격합니다.");
            skills[1] = new Skill();
            skills[1].SetSkillState("Double Strike", 15, 1.5f,"공격력 * 1.5 로 2명의 적을 랜덤하게 공격합니다.");
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
            maxMana += 5;
            currentMana = maxMana;
        }


        #region Skill Alpha Strike ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- -----

        public void SkillActive_AlphaStrike(Monster[] monsters)
        {
            Console.WriteLine();
            Console.WriteLine();
            DarkMagentaText("Battle!!");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < monsters.Length; i++)
            {
                DarkCyanText((i + 1).ToString());
                CyanText(" - ");
                if (monsters[i].death)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write("LV. ");
                Console.Write(monsters[i].level + " ");
                Console.Write(monsters[i].name + "\n");
                if (monsters[i].death)
                {
                    Console.Write("\tDead");
                }
                else
                {
                    Console.Write("\tHP ");
                    RedText(monsters[i].currentHealth.ToString());
                    YellowText(" / ");
                    DarkRedText(monsters[i].maxHealth.ToString());
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[ 내정보] ");
            Console.Write("LV. ");
            DarkBlueText(level.ToString() + " ");
            Console.WriteLine();
            Console.Write("Class : ");
            DarkRedText(clas + " ");
            Console.WriteLine();
            Console.Write("HP ");
            RedText(currentHealth.ToString());
            DarkYellowText(" / ");
            DarkRedText(maxHealth.ToString());
            Console.WriteLine();
            Console.Write("MP ");
            BlueText(currentMana.ToString());
            DarkYellowText(" / ");
            DarkBlueText(maxMana.ToString());
            Console.WriteLine();
            Console.Write("EXP ");
            BlueText(currentExp.ToString());
            DarkYellowText(" / ");
            DarkBlueText(levelUpExp.ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("공격할 몬스터를 정해주세요.");
            Console.WriteLine();
            Console.WriteLine("0. 공격 취소");
            Console.WriteLine();
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int num) && num-1 < monsters.Length)
            {
                if(num != 0)
                {
                    Console.Clear();
                    if (skills[0].mana >= currentMana)
                    {
                        Console.Clear();
                        RedText("마나가 부족 합니다!");
                        Console.WriteLine();
                        SkillActive_AlphaStrike(monsters);
                    }
                    else
                    {
                        if (monsters[num - 1].death)
                        {
                            Console.Clear();
                            RedText("선택한 몬스터는 이미 죽었습니다.");
                            SkillActive_AlphaStrike(monsters);
                        }
                        else
                        {
                         BattleDisplay_AlphaResult_Player(monsters[num - 1]);
                        }
                    }

                }
                else
                {
                    Console.Clear();
                    GameManager.Instance.battleManager.BattleSkillAttackMenu();
                }
            }
            else
            {
                Console.Clear();
                WrongInput();
                SkillActive_AlphaStrike(monsters);
            }

        }

        public void BattleDisplay_AlphaResult_Player(Monster target)
        {
            currentMana -= skills[0].mana;
            int monsterCurrentHp = target.currentHealth;
            int finalAttackDamage = (int)Math.Round(attackDamge * skills[0].increseDamage);
            target.TakeDamage(finalAttackDamage);

            Console.WriteLine();
            Console.WriteLine();
            DarkMagentaText("Battle!!");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(playerName + " 의 ");
            CyanText(skills[0].skillName);
            Console.Write(" 스킬 공격!\n");
            Console.Write("LV. ");
            Console.Write(target.level + " ");
            Console.Write(target.name);
            Console.Write(" 을(를) 맞췄습니다. [데미지 : ");
            MagentaText((monsterCurrentHp - target.currentHealth).ToString());
            Console.Write(" ]");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("LV. ");
            Console.Write(target.level + " ");
            Console.Write(target.name);
            Console.WriteLine();
            Console.Write("HP ");
            RedText(monsterCurrentHp.ToString());
            YellowText(" -> ");

            if (target.currentHealth <= 0)
            {
                Console.Write("Dead");
            }
            else
            {
                DarkRedText(target.currentHealth.ToString());
            }


            Console.WriteLine();
            Console.WriteLine();

            if (target.death)
            {
                float currentExp = this.currentExp;
                int currentMaxHp = this.maxHealth;

                Console.WriteLine("Reward");
                if (currentExp + target.rewardExp >= levelUpExp)
                {
                    GainExperience(target.rewardExp);
                    GreenText("Level Up!!\n");
                    YellowText("모든 체력과 마나가 회복되었습니다.");

                }
                else
                {
                    GainExperience(target.rewardExp);
                    Console.WriteLine("EXP");
                    DarkGreenText(currentExp.ToString());
                    YellowText(" -> ");
                    GreenText(this.currentExp.ToString());
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            RunningTimeText(3);
            Console.Clear();

        }

        #endregion


        //Recovery

        public void RecoveryHealth(int recoveryAmount)
        {
            currentHealth += recoveryAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        public void RecoveryMana(int recoveryAmount)
        {
            currentMana += recoveryAmount;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }

    }

    



    public class Skill
    {
        public string skillName = "";
        public int mana;
        public float increseDamage;
        public string skillInfo = "";


       public void SetSkillState(string name,int _mana,float _increseDamage,string info)
        {
            skillName = name;
            mana = _mana;
            increseDamage = _increseDamage;
            skillInfo = info;
        }
    }

   

    
}
