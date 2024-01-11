using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    // 게임의 전반적인 관리
    // 1. 게임 시작
    // 2. 행동 선택
    // 3.
    class GameManager : ConsoleText
    {
        #region GameManager
        // 싱글톤 방식으로 하나의 GameManager만 구현한다.
        // GameManager은 곳곳에서 불러올 수가 있다.
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }
        #endregion

        public Player player;
        public BattleManager battleManager;
        
        public int action;


        public GameManager()
        {
            player = new Player();
            player.Init();
            
        }

        public void GameStart()
        {
            Console.WriteLine("00던전에 오신 것을 환영합니다");
            Console.WriteLine("이제 전투를 시작할 수 있습니다\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            // 원하는 행동 선택
            SetNextAction(1, 2);

            switch (action)
            {
                case 1:
                    Console.Clear();
                    StateMenu();
                    break;
                case 2:
                    Console.Clear();
                    battleManager = new BattleManager(player);
                    battleManager.BattleMenu();
                    break;
            
            }



        }
        #region State
        void StateMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상태 보기");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
            string level = "";


            Console.Write("Name\t: ");
            DarkMagentaText(player.playerName);
            Console.WriteLine();
            if (player.level < 10)
            {
                level = "0" + player.level;
            }
            else
            {
                level = player.level.ToString();
            }

            Console.Write("LV. ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(level);
            Console.ResetColor();

            Console.Write("Class\t:");
            Console.Write("{0}\n", player.clas);

            Console.Write("Damage\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.attackDamge);
            Console.ResetColor();
            if (player.increaseInDamage != 0)
            {
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(player.increaseInDamage);
                Console.ResetColor();
                Console.Write(")\n");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Armor\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.armor);
            Console.ResetColor();
            if (player.increaseInArmor != 0)
            {
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(player.increaseInArmor);
                Console.ResetColor();
                Console.Write(")\n");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Health\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.currentHealth);
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(player.maxHealth);
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Mana\t: ");
            DarkBlueText(player.currentMana.ToString());
            Console.Write(" / ");
            BlueText(player.maxMana.ToString());
            Console.WriteLine();

            Console.Write("Gold\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.gold);
            Console.ResetColor();
            Console.Write(" G\n");

            Console.Write("경험치\t: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(player.currentExp);
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(player.levelUpExp);
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기 ");
            NextActionMessage();
            string select = Console.ReadLine();

            if (select != "0")
            {
                Console.Clear();
                WrongInput();
                StateMenu();

            }
            else
            {
                Console.Clear();
                Console.ResetColor();
                GameStart();
            }
        }
        #endregion


        #region 행동 선택
        void SetNextAction(int minValue, int maxValue)
        {
            Console.WriteLine("\n원하는 행동을 입력해 주세요");
            while (IsInValidAction(minValue, maxValue) == false) { Console.WriteLine("잘못된 입력입니다 다시 선택해 주세요"); }
            Console.WriteLine();
        }


        bool IsInValidAction(int minValue, int maxValue)
        {
            if (int.TryParse(Console.ReadLine(), out action) == false) return false;

            if (action > maxValue || action < minValue) return false;

            return true;
        }
        #endregion
    }
}
