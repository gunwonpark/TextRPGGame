using System;
namespace TextRPGGame
{
	public class BattleManager
	{
		Monster[] monsters;
		SpawnManager spawnManager;

		public BattleManager()
		{
			spawnManager = new SpawnManager();
			monsters = spawnManager.GeneratorMonsters();
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
				Console.Write("LV. ");
				Console.Write(monsters[i].level+" ");
				Console.Write(monsters[i].name + "\n");
				Console.Write("\tHP :");
				DarkRedText(monsters[i].health.ToString());
                Console.WriteLine("");
                Console.WriteLine("");

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("내정보");
			Console.WriteLine("LV.  ");
            Console.WriteLine("HP ");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            
            NextActionMessage();
            string select = Console.ReadLine();


        }




        public void DarkMagentaText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkRedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str);
            Console.ResetColor();
        }

        public void NextActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
        }


    }
}

