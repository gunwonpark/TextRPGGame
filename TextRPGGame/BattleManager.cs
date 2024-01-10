using System;
using System.Threading;

namespace TextRPGGame
{
	public class BattleManager :ConsoleText
	{
		Monster[] monsters;
		SpawnManager spawnManager;
		Player player;

		public BattleManager(Player _player)
		{
			spawnManager = new SpawnManager();
			monsters = spawnManager.GeneratorMonsters();
			player = _player;
		}


		public void BattleMenu()
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
				Console.Write(monsters[i].level+" ");
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
			DarkBlueText(player.level.ToString()+" ");
            Console.WriteLine();
            Console.Write("Class : ");
			DarkRedText(player.clas +" ");
			Console.WriteLine();
            Console.Write("HP ");
			RedText(player.currentHealth.ToString());
			DarkYellowText(" / ");
			DarkRedText(player.maxHealth.ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine();


            NextActionMessage();
            string select = Console.ReadLine();
            if (int.TryParse(select, out int num))
            {
                if (num == 1)
                {
                    Console.Clear();
                    BattleAttackMenu();
                }

            }
            else
            {
                Console.Clear();
                WrongInput();
                BattleMenu();
            }


        }


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

        void BattleDisplayResult_Player(Monster target)
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
                    GreenText("Level Up!!\n");
                    Console.Write("HP ");
                    DarkRedText(currentMaxHp.ToString());
                    YellowText(" -> ");
                    RedText(player.maxHealth.ToString());

                }
                else
                {
                    player.GainExperience(target.rewardExp);
                    Console.WriteLine("EXP");
                    DarkBlueText(currentExp.ToString());
                    YellowText(" -> ");
                    BlueText(player.currentExp.ToString());
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            RunningTimeText(3);
            Console.Clear();

        }

        void BattleDisplayResult_Mosters()
        {
            Console.WriteLine();
            Console.WriteLine();
            DarkMagentaText("Battle!!");
            Console.WriteLine();
            RedText("몬스터가 공격을 시작합니다.");
            Console.WriteLine();

            for (int i = 0; i< monsters.Length; i++)
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
                    Console.Write(monsters[i].name+"\n");
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

            if (!player.death)
            {
                Console.WriteLine();
                Console.WriteLine();
                GreenText("Please press Enter");
                Console.ReadLine();
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
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            NextActionMessage();
            Console.ReadLine();
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

