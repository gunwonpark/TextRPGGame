using System;
namespace TextRPGGame
{
	public class Potion
	{
		int hpPotionCount;
		int hpAmount;
        int mpPotionCount;
		int mpAmount;

		public Potion()
		{
			SetInit();
        }

		void SetInit()
		{
            hpPotionCount = 3;
            hpAmount = 30;
            mpPotionCount = 3;
            mpAmount = 20;
        }


		public void PotionManu()
		{
			Console.WriteLine();
            Console.WriteLine();
            Utill.WriteOrangeText("회복\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"포션을 사용하면 체력 / 마나를 회복할 수 있습니다.");
            Console.WriteLine($" (HP 포션 : {hpPotionCount}) ");
            Console.WriteLine($" (MP 포션 : {mpPotionCount}) ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.Write("HP ");
            Utill.WriteRedText(GameManager.Instance.player.Hp.ToString());
            Console.Write(" / ");
            Utill.WriteRedText(GameManager.Instance.player.MaxHp.ToString());
            Console.WriteLine();
            Console.Write("MP ");
            Utill.WriteDarkBlueText(GameManager.Instance.player.Mp.ToString());
            Console.Write(" / ");
            Utill.WriteBlueText(GameManager.Instance.player.MaxMp.ToString());
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("1. HP 포션 사용하기");
            Console.WriteLine("2. MP 포션 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">> ");
            Console.ResetColor();
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    if (hpPotionCount > 0)
                    {
                        if(GameManager.Instance.player.Hp == GameManager.Instance.player.MaxHp)
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체력이 꽉 차있습니다");
                            PotionManu();
                        }
                        else
                        {
                            hpPotionCount--;
                            GameManager.Instance.player.Hp += hpAmount;
                            Console.Clear();
                            Console.WriteLine("회복을 완료했습니다.");
                            PotionManu();
                        }
                        
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("포션이 부족합니다.");
                        PotionManu();
                    }
                    break;
                case "2":
                    if (mpPotionCount > 0)
                    {
                        if (GameManager.Instance.player.Mp == GameManager.Instance.player.MaxMp)
                        {
                            Console.Clear();
                            Console.WriteLine("이미 마나가 꽉 차있습니다");
                            PotionManu();
                        }
                        else
                        {
                            mpPotionCount--;
                            GameManager.Instance.player.Mp += mpAmount;
                            Console.Clear();
                            Console.WriteLine("회복을 완료했습니다.");
                            PotionManu();
                        }

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("포션이 부족합니다.");
                        PotionManu();
                    }
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    PotionManu();
                    break;
            }

        }


      public void BattleRewardPotion(List<Monster> monsters)
        {
            Random random = new Random();
            int len = monsters.Count;
            int rewoardHpAmount = 0;
            int rewoardMpAmount = 0;
            for (int i = 0; i < len; i++)
            {
                int hPpercent = random.Next(0, 101);
                int mPpercent = random.Next(0, 101);
                if (hPpercent <= 60)
                {
                    int amount = random.Next(1, 3);
                    rewoardHpAmount+=amount;
                }
                if (mPpercent <= 40)
                {
                    int amount = random.Next(1, 3);
                    rewoardMpAmount += amount;
                }
            }

            hpPotionCount += rewoardHpAmount;
            mpPotionCount += rewoardMpAmount;

            if(rewoardHpAmount > 0)
            {
                Console.Write($"Hp 포션 x{rewoardHpAmount} 획득\n");
            }
            if(rewoardMpAmount > 0)
            {
                Console.Write($"Mp 포션 x{rewoardMpAmount} 획득\n");
            }
        }
    }
}

