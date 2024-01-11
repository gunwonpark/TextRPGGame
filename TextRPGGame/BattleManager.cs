using System;
using System.Threading;

namespace TextRPGGame
{
	public class BattleManager :ConsoleText
	{
		Monster[] monsters;
		SpawnManager spawnManager;
		Player player;

        int playerStartLevel;
        int playerStartHp;
        int playerStartMaxHp;
        int playerStartMana;
        int playerStartMaxMana;
        float playerStartExp;
        int playerStartGold;
        float playerStartAttackDamage;
        int playerStartArmor;


        public BattleManager(Player _player)
		{
			spawnManager = new SpawnManager();
			monsters = spawnManager.GeneratorMonsters();
			player = _player;
            Set_PlayerStartState();
		}


        void Set_PlayerStartState()
        {
            playerStartLevel = player.level;
            playerStartHp = player.currentHealth;
            playerStartMaxHp = player.maxHealth;
            playerStartMana = player.currentMana;
            playerStartMaxMana = player.maxMana;
            playerStartExp = player.currentExp;
            playerStartGold = player.gold;
            playerStartAttackDamage = player.attackDamge;
            playerStartArmor = player.armor;

        }



		public void BattleMenu()
		{
            if (CheckAliveMonsters() && !player.death)
            {
                Console.WriteLine();
                Console.WriteLine();
                DarkMagentaText("Battle!!");
                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < monsters.Length; i++)
                {
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
                Console.WriteLine("[ 내정보 ]");
                Console.Write("LV. ");
                DarkBlueText(player.level.ToString() + " ");
                Console.WriteLine();
                Console.Write("Class : ");
                DarkRedText(player.clas + " ");
                Console.WriteLine();
                Console.Write("HP ");
                RedText(player.currentHealth.ToString());
                DarkYellowText(" / ");
                DarkRedText(player.maxHealth.ToString());
                Console.WriteLine();
                Console.Write("MP ");
                BlueText(player.currentMana.ToString());
                DarkYellowText(" / ");
                DarkBlueText(player.maxMana.ToString());
                Console.WriteLine();
                Console.Write("EXP ");
                GreenText(player.currentExp.ToString());
                DarkYellowText(" / ");
                DarkGreenText(player.levelUpExp.ToString());
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. 일반 공격");
                Console.WriteLine("2. 스킬 공격");
                Console.WriteLine();
                Console.WriteLine();


                NextActionMessage();
                string select = Console.ReadLine();
                if (int.TryParse(select, out int num) && num != 0 && num <= 2)
                {
                    if (num == 1)
                    {
                        Console.Clear();
                        BattleAttackMenu();
                    }
                    if (num == 2)
                    {
                        Console.Clear();
                        BattleSkillAttackMenu();
                        Console.Clear();
                        BattleMenu();
                        //스킬
                    }

                }
                else
                {
                    Console.Clear();
                    WrongInput();
                    BattleMenu();
                }

            }
            else
            {
                if (player.death)
                {
                    Console.Clear();
                    BattleResultLose();
                }
                else
                {
                    //Monster all clear
                    Console.Clear();
                    BattleResultVictory();
                }
            }

        }

        #region 기본 공격--------
        void BattleAttackMenu()
		{
            if (CheckAliveMonsters() && !player.death)
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
                DarkBlueText(player.level.ToString() + " ");
                Console.WriteLine();
                Console.Write("Class : ");
                DarkRedText(player.clas + " ");
                Console.WriteLine();
                Console.Write("HP ");
                RedText(player.currentHealth.ToString());
                DarkYellowText(" / ");
                DarkRedText(player.maxHealth.ToString());
                Console.WriteLine();
                Console.Write("MP ");
                BlueText(player.currentMana.ToString());
                DarkYellowText(" / ");
                DarkBlueText(player.maxMana.ToString());
                Console.WriteLine();
                Console.Write("EXP ");
                BlueText(player.currentExp.ToString());
                DarkYellowText(" / ");
                DarkBlueText(player.levelUpExp.ToString());
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("공격할 몬스터를 정해주세요.");
                Console.WriteLine();
                Console.WriteLine("0. 공격 취소");
                Console.WriteLine();
                Console.WriteLine();

                NextActionMessage();
                string select = Console.ReadLine();
                if (int.TryParse(select, out int num))
                {
                    if (num > monsters.Length)// 잘못 입력
                    {
                        Console.Clear();
                        WrongInput();
                        BattleAttackMenu();
                    }
                    else if (num == 0)
                    {
                        Console.Clear();
                        BattleMenu();
                    }
                    else
                    {
                        if (monsters[num-1].currentHealth <= 0) //몬스터를 모두 죽였을때
                        {
                            Console.Clear();
                            RedText("선택한 몬스터는 이미 죽었습니다.");
                            BattleAttackMenu();
                        }
                        else // 공격 실행
                        {

                            Console.Clear();
                            BattleDisplayResult_Player(monsters[num - 1]);
                            BattleDisplayResult_Mosters();
                            Console.Clear();
                            
                            BattleAttackMenu();
                        }

                    }
                }
                else // 잘못 입력
                {
                    Console.Clear();
                    WrongInput();
                    BattleAttackMenu();
                }
            }
            else
            {
                if (player.death)
                {
                    Console.Clear();
                    BattleResultLose();
                }
                else
                {
                    //Monster all clear
                    Console.Clear();
                    BattleResultVictory();
                }
            }
            
        }//
        #endregion

        public void BattleSkillAttackMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            DarkMagentaText("Battle!!");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < monsters.Length; i++)
            {
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
            Console.WriteLine("[ 내정보 ]");
            Console.Write("LV. ");
            DarkBlueText(player.level.ToString() + " ");
            Console.WriteLine();
            Console.Write("Class : ");
            DarkRedText(player.clas + " ");
            Console.WriteLine();
            Console.Write("HP ");
            RedText(player.currentHealth.ToString());
            DarkYellowText(" / ");
            DarkRedText(player.maxHealth.ToString());
            Console.WriteLine();
            Console.Write("MP ");
            BlueText(player.currentMana.ToString());
            DarkYellowText(" / ");
            DarkBlueText(player.maxMana.ToString());
            Console.WriteLine();
            Console.Write("EXP ");
            BlueText(player.currentExp.ToString());
            DarkYellowText(" / ");
            DarkBlueText(player.levelUpExp.ToString());
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < player.skills.Length; i++)
            {
                Console.Write(i + 1+". ");
                Console.Write(player.skills[i].skillName);
                YellowText(" - ");
                DarkBlueText("MP ");
                BlueText(player.skills[i].mana.ToString());
                Console.WriteLine();
                Console.WriteLine("\t"+player.skills[i].skillInfo);
            }
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int num) && num < player.skills.Length)
            {
                if(num == 0)
                {
                    Console.Clear();
                    BattleMenu();
                }
                else if(num == 1)//arpha
                {
                    Console.Clear();
                    player.SkillActive_AlphaStrike(monsters);
                    BattleDisplayResult_Mosters();
                    Console.Clear();
                    BattleMenu();


                }
                else if (num == 2)//double
                {
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                WrongInput();
                BattleMenu();
            }



        }//Skill Attack

        //------------------------------------------------------------------------------Result

        public void BattleDisplayResult_Player(Monster target)
        {
            int monsterCurrentHp = target.currentHealth;
            target.TakeDamage(player.attackDamge);

            Console.WriteLine();
            Console.WriteLine();
            DarkMagentaText("Battle!!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(player.playerName+" 의 공격!");
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
            
            if(target.currentHealth <= 0)
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
                float currentExp = player.currentExp;
                int currentMaxHp = player.maxHealth;

                Console.WriteLine("Reward");
                if(player.currentExp + target.rewardExp >= player.levelUpExp)
                {
                    player.GainExperience(target.rewardExp);
                    Console.WriteLine();
                    GreenText("Level Up!!\n");
                    YellowText("모든 체력과 마나가 회복되었습니다.");

                }
                else
                {
                    player.GainExperience(target.rewardExp);
                    Console.WriteLine("EXP");
                    DarkGreenText(currentExp.ToString());
                    YellowText(" -> ");
                    GreenText(player.currentExp.ToString());
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            RunningTimeText(3);
            Console.Clear();

        }

        void BattleDisplayResult_Mosters()
        {
            if (CheckAliveMonsters())
            {
                Console.WriteLine();
                Console.WriteLine();
                DarkMagentaText("Battle!!");
                Console.WriteLine();
                RedText("몬스터가 공격을 시작합니다.");
                Console.WriteLine();

                for (int i = 0; i < monsters.Length; i++)
                {
                    if (!monsters[i].death && !player.death)
                    {
                        int playerCurrentHp = player.currentHealth;
                        player.TakeDamage(monsters[i].damage);

                        Console.WriteLine();
                        YellowText("' ");
                        Console.Write(monsters[i].name + " 의 공격!");
                        YellowText("' \n");
                        Console.Write("LV. ");
                        Console.Write(monsters[i].level + " ");
                        Console.Write(monsters[i].name + "\n");
                        Console.Write($"\t{monsters[i].attackSkill} ! [데미지 : ");
                        MagentaText((playerCurrentHp - player.currentHealth).ToString());
                        Console.Write(" ]");

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine(player.playerName);
                        Console.Write("\t HP ");
                        DarkRedText(playerCurrentHp.ToString());
                        YellowText(" -> ");
                        RedText(player.currentHealth.ToString());
                        Console.WriteLine();
                        Console.WriteLine();
                        GreenText("Please press Enter");
                        Console.ReadLine();

                        YellowText("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    }
                }

            }     
        }

        void BattleResultVictory()
        {
            Console.WriteLine();
            Console.WriteLine();
            MagentaText("Battle Result! \n");
            Console.WriteLine();
            Console.WriteLine();
            GreenText("Victory!!\n");
            Console.WriteLine();
            Console.WriteLine($"던전에서 {monsters.Length}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine();

            if(player.level != playerStartLevel)
            {
                Console.Write("LV. ");
                Console.Write(playerStartLevel);
                YellowText(" -> ");
                DarkGreenText(player.level.ToString());
                Console.WriteLine();
                Console.WriteLine();

                Console.Write("최대 체력. ");
                Console.Write(playerStartMaxHp);
                YellowText(" -> ");
                DarkRedText(player.maxHealth.ToString());
                Console.WriteLine();

                Console.Write("최대 마나 ");
                Console.Write(playerStartMana);
                YellowText(" -> ");
                DarkBlueText(player.maxMana.ToString());
                Console.WriteLine();

                Console.Write("Damage ");
                Console.Write(playerStartAttackDamage);
                YellowText(" -> ");
                DarkRedText(player.attackDamge.ToString());
                Console.WriteLine();

                Console.Write("Armor ");
                Console.Write(playerStartArmor);
                YellowText(" -> ");
                DarkBlueText(player.armor.ToString());
                Console.WriteLine();
            }
            else
            {
                Console.Write("HP. ");
                Console.Write(playerStartHp);
                YellowText(" -> ");
                DarkRedText(player.currentHealth.ToString());
                Console.WriteLine();

                Console.Write("MP. ");
                Console.Write(playerStartMana);
                YellowText(" -> ");
                DarkBlueText(player.currentMana.ToString());
                Console.WriteLine();

                Console.Write("EXP. ");
                Console.Write(playerStartExp);
                YellowText(" -> ");
                DarkGreenText(player.currentExp.ToString());
                Console.WriteLine();
            }
            

 
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            NextActionMessage();
            Console.ReadLine();

            Console.Clear();
            Console.ResetColor();

            GameManager.Instance.GameStart();
        }

        void BattleResultLose()
        {
            Console.WriteLine();
            Console.WriteLine();
            MagentaText("Battle Result! \n");
            Console.WriteLine();
            Console.WriteLine();
            RedText("You Lose..\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
           
        }
        //------------------------------------------------------------------------------Result

        bool CheckAliveMonsters()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                if (!monsters[i].death)
                {
                    return true;
                }
            }
            return false;
        }

        
    }

}

