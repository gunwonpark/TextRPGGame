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
            //mpPotionCount = 3;
            //mpAmount = 30;
        }


		public void PotionManu()
		{
			Console.WriteLine();
            Console.WriteLine();
            Utill.WriteOrangeText("회복\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"포션을 사용하면 체력을 {hpAmount} 회복할 수 있습니다. (남은 포션 : {hpPotionCount})");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.Write("HP ");
            Utill.WriteRedText(GameManager.Instance.player.Hp.ToString());
            Console.Write(" / ");
            Utill.WriteRedText(GameManager.Instance.player.MaxHp.ToString());
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("1. 사용하기");
            Console.WriteLine("2. 나가기");
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
            int len = monsters.Count;
            int rewoardAmount = 0;
            for (int i = 0; i < len; i++)
            {
                int percent = new Random().Next(0, 101);
                if(percent <= 60)
                {
                    rewoardAmount++;
                }
            }

            hpPotionCount += rewoardAmount;
            
            Console.Write($"Hp 회복 포션 x{rewoardAmount} 획득\n");
        }
    }
}

