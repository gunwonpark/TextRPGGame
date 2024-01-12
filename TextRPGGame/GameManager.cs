using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        
        public Player _player;
        public int action;
        public List<Monster> monsters;
        private Random random;

        public GameManager()
        {
            _player = new Player("chad", "전사", 1, 10, 5, 100, 1500);
            random = new Random();

            monsters = new List<Monster>
            {
                new Monster("미니언", 2, 15, 5),
                new Monster("공허충", 3, 10, 9),
                new Monster("대포미니언", 5, 25, 8)
            };
        }

        public void GameStart()
        {
            Console.Clear();
            Console.WriteLine("00던전에 오신 것을 환영합니다");
            Console.WriteLine("이제 전투를 시작할 수 있습니다\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            // 원하는 행동 선택
            SetNextAction(1, 2);

            switch (action)
            {
                case 1:
                    Player.ShowStatus();
                    break;
                case 2:
                    StartBattle();
                    break;
            }
        }
        

        private void StartBattle()
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

