using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        public List<Skill> skills;
        public Potion potion;
        public QuestBoard questBoard;
        public Merchant merchant;
        public Stage stage;
        public GameManager()
        {
            potion = new Potion();
            player = new Player("", Player.ClassType.None);
            questBoard = new QuestBoard();
            stage = new Stage();
            merchant = new Merchant();
            skills = new List<Skill>
            {
                new Skill("알파 스트라이크", 10, 2, 1),
                new Skill("더블 스트라이크", 15, 1.5f, 2)
            };
        }

        public void GameStart()
        {
            Utill.PrintStartLogo();

            Console.Clear();
            Utill.WriteRedText("1. ");
            Console.WriteLine("새로 시작");
            Utill.WriteRedText("2. ");
            Console.WriteLine("이어 하기");
            // 원하는 행동 선택
            SetNextAction(0, 4);
            switch (action)
            {
                case 1:
                    SetName();
                    ChooseClass(player);
                    break;
                case 2:
                    player = DataManager.LoadPlayerData();
                    stage = DataManager.LoadStageData();
                    break;
            }

            MainScene();
        }

        // 메인 씬 활성화
        public void MainScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("00던전에 오신 것을 환영합니다");
                Console.WriteLine("이제 전투를 시작할 수 있습니다\n");
                Utill.WriteRedText("0. ");
                Console.WriteLine("게임 종료 및 저장");
                Utill.WriteRedText("1. ");
                Console.WriteLine("상태 보기");
                Utill.WriteRedText("2. ");
                Console.WriteLine($"전투 시작 ({stage.Level}층)");
                Utill.WriteRedText("3. ");
                Console.WriteLine("회복 아이템");

                Utill.WriteRedText("4. ");
                Console.WriteLine("퀘스트");

                Utill.WriteRedText("5. ");
                Console.WriteLine("상점");

                // 원하는 행동 선택
                SetNextAction(0, 5);

                switch (action)
                {
                    case 0:
                        SaveAndQuit();
                        return;
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
                    case 4:
                        Console.Clear();
                        questBoard.QuestBoardManu();
                        break;
                    case 5:
                        Console.Clear();
                        merchant.MerchantMenu();
                        break;
                }
            }
        }
        void SaveAndQuit()
        {
            // bin/Debug/net6.0폴더 안에 생성
            DataManager.SaveData();
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
            Console.WriteLine("당신의 직업을 선택하세요.");
            Console.WriteLine("\n[1] 전사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");
            Player.ClassType choice = Player.ClassType.None;
            while (true)
            {
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
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                }

                player.Class = choice;
                player.InitializeClassStats();
                return choice;
            }
        }

        void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 정보가 표시 됩니다.");

            player.ShowStatus();

            Utill.WriteRedText("0. ");
            Console.WriteLine("나가기");

            Utill.WriteRedText("1. ");
            Console.WriteLine("장비 관리");

            SetNextAction(0, 1);
            switch (action)
            {
                case 0:
                    break;
                case 1:
                    EquipmentManager();
                    break;
            }
        }
        void EquipmentManager()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n장착하고 싶은 아이템은 선택하세요\n");

                player.ShowInventroy();

                Console.WriteLine("0. 나가기");

                SetNextAction(0, player.inventory.Count);

                if (action == 0) break;

                // 아이템 장착
                EquipItem(action - 1);
            }
        }
        void EquipItem(int itemNumber)
        {
            IEquipable equipableItem = (IEquipable)player.inventory[itemNumber];

            if (equipableItem.IsEquiped)
                equipableItem.UnEquip(player);
            else
                equipableItem.Equip(player);
        }
        void StartBattle()
        {
            monsters = stage.CreateMonster();
            //BattleManager battleManager = new BattleManager();
            //battleManager.StartBattle();
            BattleManager.battleManager.SpawnMonster();
            BattleManager.battleManager.StartBattle();
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
