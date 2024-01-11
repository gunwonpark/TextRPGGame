using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPGGame
{
    // 게임의 전반적인 관리
    // 1. 게임 시작
    // 2. 행동 선택
    // 3. [메뉴] 상태보기, 전투시작

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
        #endregion // GameMAnager

        public Player player;
        public int action;
        public List<Monster> monsters = new List<Monster>();
        public List<Monster> battleMonsters = new List<Monster>();

        // 게임 초기화
        public GameManager()
        {
            // 플레이어 초기화
            player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);

            // 몬스터 초기화
            AddMonster(2, "미니언", 15, 5);
            AddMonster(3, "공허충", 10, 9);
            AddMonster(5, "대포미니언", 25, 8);
        }

        #region 몬스터 생성 / 삭제
        // 몬스터 생성
        void AddMonster(int inputLv, string inputName, int inputHp, int inputAtk)
        {
            Monster monster = new Monster(inputLv, inputName, inputHp, inputAtk);
            monsters.Add(monster);
        }

        // 전투 몬스터 랜덤 생성
        void RandomCreateMonster()
        {
            // 랜덤 숫자 생성
            Random random = new Random();

            // 몬스터 rand(1~4)마리 
            int rand = random.Next(1, 5);

            // 몬스터 랜덤 생성
            for (int i = 0; i < rand; i++)
            {
                int monstersIdx = random.Next(0, monsters.Count);

                battleMonsters.Add(monsters[monstersIdx].DeepCopy());
            }
        }

        // 전투에서 나갈 시 전투 몬스터 초기화
        private void InitBattleMonster()
        {
            for (int idx = battleMonsters.Count-1; idx >-1; idx--)
            {
                battleMonsters.RemoveAt(idx);
            }
        }
        #endregion // 몬스터 생성 / 삭제

        // 게임 시작 화면
        public void GameStart()
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("스파르타 던전에 오신 것을 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");

            // 원하는 행동 선택
            SetNextAction(1, 2);

            switch (action)
            {
                // 상태 보기
                case 1:
                    PrintPlayerStatus();
                    break;

                // 전투 시작
                case 2:
                    BattleStart();
                    break;
            }
        }

        #region 행동 선택
        // 원하는 행동 선택
        void SetNextAction(int minValue, int maxValue)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            Console.Write(">> ");

            // 입력 확인
            while (IsInValidAction(minValue, maxValue) == false)
            {
                ChangeColorRed("잘못된 입력입니다. 다시 선택해 주세요.");
                Console.Write(">> ");
            }

            Console.WriteLine("");
        }

        // 숫자 범위 체크
        bool IsInValidAction(int minValue, int maxValue)
        {
            // 입력한 값이 숫자인지 확인
            if (int.TryParse(Console.ReadLine(), out action) == false)
            {
                return false;
            }

            // 입력한 값이 유효한 값인지 확인
            if (action > maxValue || action < minValue)
            {
                return false;
            }

            return true;
        }
        #endregion //    행동 선택

        #region 글자 색 변환
        // RED
        void ChangeColorRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // DARKGRAY
        public void ChangeColorDarkGray(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion // 글자 색 변환

        #region [메뉴] 상태 보기
        // 상태 보기 출력
        private void PrintPlayerStatus()
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            // 플레이어 상태 보기
            player.ShowStatus();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            // 원하는 행동 선택
            SetNextAction(0, 0);

            switch (action)
            {
                // 게임 시작 화면
                case 0:
                    GameStart();
                    break;
            }
        }
        #endregion // [메뉴] 상태 보기

        #region [메뉴] 전투 시작
        // 전투 출력
        private void BattleStart()
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();

            // 몬스터 상태 보기
            RandomCreateMonster();
            for (int idx = 0; idx < battleMonsters.Count; idx++)
            {
                battleMonsters[idx].ShowStatus(false, idx);
            }

            // 플레이어 상태 보기
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.ShowStatusSimple();

            Console.WriteLine();
            Console.WriteLine("1. 공격");

            // 원하는 행동 선택
            SetNextAction(1, 1);

            switch (action)
            {
                // 공격
                case 1:
                    BattlekMonster();
                    break;
            }
        }

        // 게임 결과
        private void GameOver(bool isWin)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();

            // 이긴 결과창
            if (isWin)
            {
                Console.WriteLine("Victory");
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {battleMonsters.Count}마리를 잡았습니다.");

            }
            // 진 결과창
            else
            {
                Console.WriteLine("You Lose");
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{player.lv} {player.name}");
            Console.WriteLine($"HP {player.hp} -> {player.curHp}");
            player.hp = player.curHp;
            Console.WriteLine();
            Console.WriteLine("0. 다음");

            // 전투 몬스터 초기화
            InitBattleMonster();

            // 원하는 행동 선택
            SetNextAction(0, 0);

            switch (action)
            {
                // 게임 시작 화면
                case 0:
                    GameStart();
                    break;
            }
        }

        #region 공격
        // 몬스터 선택
        private void BattlekMonster()
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();

            // 몬스터 상태 출력
            for (int idx = 0; idx < battleMonsters.Count; idx++)
            {
                battleMonsters[idx].ShowStatus(true, idx);
            }

            // 플레이어 상태 보기
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.ShowStatusSimple();

            Console.WriteLine();
            Console.WriteLine("0. 도망가기");

            // 원하는 행동 선택
            SetNextAction(0, battleMonsters.Count);

            // 도망가기
            if (action == 0)
            {
                // 전투에서 나갈 시 전투 몬스터 초기화
                InitBattleMonster();

                // 플레이어 hp 업데이트
                player.curHp = player.updateHp;

                // 시작 화면
                GameStart();
            }
            // 몬스터 공격
            else
            {
                // 해당 몬스터가 살아있다면
                if (battleMonsters[action - 1].isDie == false)
                {
                    int attack = AttackMonster(action - 1);
                    AttackResult(action, attack);
                }
                // 해당 몬스터가 죽었다면
                else
                {
                    // 입력 확인
                    do
                    {
                        ChangeColorRed("잘못된 입력입니다. 다시 선택해 주세요.");
                        Console.Write(">> ");
                    } while (IsInValidAction(0, battleMonsters.Count) == false || battleMonsters[action - 1].isDie == true);

                    int attack = AttackMonster(action - 1);
                    AttackResult(action, attack);
                }
            }
        }

        // 플레이어 -> 몬스터 공격
        private int AttackMonster(int input)
        {
            int attack = player.AttackMonster();
            battleMonsters[input].curHp -= attack;

            // 몬스터가 죽었는지 확인
            if (battleMonsters[input].curHp <= 0)
            {
                battleMonsters[input].isDie = true;
            }

            return attack;
        }

        // 플레이어 공격 결과 표시
        private void AttackResult(int input, int atk)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"{player.name} 의 공격!");
            Console.WriteLine($"Lv.{battleMonsters[input - 1].lv} {battleMonsters[input - 1].name} 을(를) 맞췄습니다. [데미지 : {atk}]");
            Console.WriteLine();

            // 몬스터 공격 결과 보기
            Console.WriteLine($"Lv.{battleMonsters[input - 1].lv} {battleMonsters[input - 1].name}");
            Console.Write($"HP {battleMonsters[input - 1].hp} -> ");
            if (battleMonsters[input - 1].isDie == false)
            {
                Console.WriteLine($"{battleMonsters[input - 1].curHp}");
                battleMonsters[input - 1].hp = battleMonsters[input - 1].curHp;
            }
            else
            {
                Console.WriteLine("Dead");
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            // 원하는 행동 선택
            SetNextAction(0, 0);

            switch (action)
            {
                // 몬스터 차례
                case 0:
                    // 몬스터가 다 죽었는지 확인
                    bool isGameOver = true;
                    int monsterIdx = -1;
                    for (int idx = 0; idx < battleMonsters.Count; idx++)
                    {
                        if (battleMonsters[idx].isDie == false)
                        {
                            isGameOver = false;

                            if (monsterIdx == -1)
                            {
                                monsterIdx = idx;
                            }
                        }
                    }

                    // 몬스터가 한마리라도 살아있으면
                    if (isGameOver == false)
                    {
                        EnemyPhase(monsterIdx);
                    }
                    // 몬스터가 다 죽었으면
                    else
                    {
                        GameOver(true);
                    }
                    break;
            }
        }
        #endregion // 공격

        #region 방어
        // 몬스터 공격 차례
        private void EnemyPhase(int input)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"Lv.{battleMonsters[input].lv} {battleMonsters[input].name} 의 공격!");
            int atk = AttackPlayer(input);
            Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {atk}]");
            Console.WriteLine();

            // 플레이어 공격 결과 보기
            Console.WriteLine($"Lv.{player.lv} {player.name}");
            Console.Write($"HP {player.curHp} -> ");
            if (player.updateHp <= 0)
            {
                player.updateHp = 0;
            }
            Console.WriteLine($"{player.updateHp}");
            player.curHp = player.updateHp;

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            // 원하는 행동 선택
            SetNextAction(0, 0);

            // 풀레이어가 살아있으면
            if (player.curHp > 0)
            {
                // 다음으로 플레이어 공격 가능한 몬스터가 있는지 확인
                int monsterIdx = -1;
                for (int idx = input + 1; idx < battleMonsters.Count; idx++)
                {
                    if (battleMonsters[idx].isDie == false)
                    {
                        if (monsterIdx == -1)
                        {
                            monsterIdx = idx;
                        }
                    }
                }

                // 공격 가능한 몬스터가 있으면
                if (monsterIdx > -1)
                {
                    // 다음 몬스터의 공격
                    EnemyPhase(monsterIdx);
                }
                // 몬스터 차례가 끝났으면
                else
                {
                    // 플레이어 차례
                    BattlekMonster();
                }
            }
            else
            {
                // 플레이어가 죽었으면
                GameOver(false);
            }
        }

        // 몬스터 -> 플레이어 공격
        private int AttackPlayer(int input)
        {
            int attack = battleMonsters[input].AttackMonster();
            player.updateHp -= attack;

            return attack;
        }
        #endregion // 방어
        #endregion // [메뉴] 전투 시작
    }
}
