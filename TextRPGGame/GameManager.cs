using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPGGame
{
    // 게임의 전반적인 관리
    // 1. 게임 시작
    // 2. 행동 선택
    // 3.
    class GameManager
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
        public List<Monster> monsters;
        public int action;
        public Potion potion;
        private Random random;

        public GameManager()
        {
            random = new Random();
            potion = new Potion();
            player = new Player("", Player.ClassType.None, 10, 5, 100);

            monsters = new List<Monster>
            {
                new Monster("미니언", 2, 10, 5),
                new Monster("공허충", 3, 10, 5),
                new Monster("대포미니언", 5, 10, 5)
            };

        }

        public void GameStart()
        {
            Utill.PrintStartLogo();
            SetName();
            ChooseClass(player);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("00던전에 오신 것을 환영합니다");
                Console.WriteLine("이제 전투를 시작할 수 있습니다\n");
                Utill.WriteRedText("1. ");
                Console.WriteLine("상태 보기");
                Utill.WriteRedText("2. ");
                Console.WriteLine("전투 시작");
                Utill.WriteRedText("3. ");
                Console.WriteLine("회복 아이템");
                // 원하는 행동 선택
                SetNextAction(1, 3);

                switch (action)
                {
                    case 1:
                        ShowStatus();
                        break;
                    case 2:
                        StartBattle();
                        break;
                    case 3:
                        Console.Clear();
                        potion.PotionManu();
                        break;
                }
            }
        }
        
        public void SetName() // 캐릭터 이름 추가 기능
        {
            Console.Clear();
            Console.WriteLine("스파르타 게임");
            Console.Write("이름 : ");
            player.Name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("당신의 이름은 " + player.Name + "입니다.");
            Console.ReadKey();
        }
        static Player.ClassType ChooseClass(Player player)
        {
            Console.Clear();
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("[1] 전사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");

            Player.ClassType choice = Player.ClassType.None;
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    choice = Player.ClassType.전사;
                    break;
                case "2":
                    choice = Player.ClassType.궁수;
                    break;
                case "3":
                    choice = Player.ClassType.마법사;
                    break;
            }
            player.Class = choice;
            return choice;
        }

        void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 정보가 표시 됩니다.");

            player.ShowStatus();

            Utill.WriteRedText("0. ");
            Console.WriteLine("나가기");
            SetNextAction(0, 0);
        }

        void StartBattle()
        {
            BattleManager battleManager = new BattleManager();
            battleManager.StartBattle();
        }

        
        

        #region 행동 선택
        public void SetNextAction(int minValue, int maxValue)
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
