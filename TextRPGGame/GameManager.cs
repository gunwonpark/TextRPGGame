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
        public static Random rand = new Random();
        private List<Monster> _enemyMonsters = new List<Monster>();

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
        
        public static Player _player;
        public int action;

        public GameManager()
        {
            GameDataSetting();
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
            Console.Clear();
            Console.WriteLine("전투가 시작됐습니다");

            //  1에서 4까지의 몬스터가 무작위로 나타남
            int numberOfMonsters = rand.Next(1, 5);

            Console.WriteLine($"이 전투에서 {numberOfMonsters} 마리의 몬스터가 나타납니다:\n");

            // 무작위 몬스터 생성 및 표시
            for (int i = 0; i < numberOfMonsters; i++)
            {
                Monster randomMonster = GenerateRandomMonster();
                Console.WriteLine($"Lv.{randomMonster.Level} {randomMonster.Name} HP {randomMonster.HP}");
                // 몬스터를 적 몬스터 목록에 추가
                _enemyMonsters.Add(randomMonster);
            }

            Console.WriteLine("\n[내 정보]");
            Console.WriteLine($"{_player.Name} ({_player.Job})");
            Console.WriteLine($"HP {_player.CurrentHP}/{_player.MaxHp}\n");

            Console.WriteLine("1. 공격\n");

            SetNextAction(1, 3);

            switch (action)
            {
                case 1:
                    AttackMonster();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        private void AttackMonster()
        {
            Console.Clear();
            Console.WriteLine("전투가 시작됐습니다\n");

            for (int i = 0; i < _enemyMonsters.Count; i++)
            {
                Monster monster = _enemyMonsters[i];
                Console.WriteLine($"{i + 1} Lv.{monster.Level} {monster.Name} HP {monster.HP}");
            }

            Console.WriteLine("\n[내 정보]");
            Console.WriteLine($"{_player.Name} ({_player.Job})");
            Console.WriteLine($"HP {_player.CurrentHP}/{_player.MaxHp}\n");

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요:\n>>");

            int targetMonsterIndex;
            while (!int.TryParse(Console.ReadLine(), out targetMonsterIndex) || targetMonsterIndex < 1 || targetMonsterIndex > 3)
            {
                Console.WriteLine("잘못된 입력입니다. 몬스터를 다시 선택하세요:");
            }

            if (targetMonsterIndex <= _enemyMonsters.Count)
            {
                Monster targetMonster = _enemyMonsters[targetMonsterIndex - 1];

                if (!targetMonster.IsDead)
                {
                    int attackPower = (int)(_player.Atk * (rand.Next(9, 12) / 10.0));
                    int damageDealt = targetMonster.TakeDamage(attackPower);

                    Console.WriteLine($"{targetMonster.Name}에게 {damageDealt}의 데미지를 입혔습니다.");

                    if (targetMonster.IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{targetMonster.Name}가 사망했습니다. HP: Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{targetMonster.Name}의 현재 HP: {targetMonster.HP}");
                    }
                    PlayerAttack(targetMonster);
                }
                else
                {
                    Console.WriteLine($"이미 죽은 {targetMonster.Name}를 공격할 수 없습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        private void PlayerAttack(Monster targetMonster)
        {
            int attackPower = (int)(_player.Atk * (rand.Next(9, 12) / 10.0));
            int damageDealt = targetMonster.TakeDamage(attackPower);
            Console.Clear();
            Console.WriteLine($" {damageDealt}만큼의 데미지를 입혔습니다.");

            if (targetMonster.IsDead)
            {
                Console.WriteLine($"몬스터가 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"몬스터의 남은 HP: {targetMonster.HP}");
            }

            Console.WriteLine($"{_player.Name}\nHP {_player.CurrentHP} -> {_player.CurrentHP}\n");

            Console.ReadKey();

            MonsterCounterattacks();
        }

        private void MonsterCounterattacks()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");

            foreach (var monster in _enemyMonsters.Where(monster => !monster.IsDead))
            {
                // 몬스터의 반격을 시뮬레이션합니다.
                int playerDamage = rand.Next(4, 8);
                _player.TakeDamage(playerDamage);

                Console.WriteLine($"Lv.{monster.Level }{monster.Name}의 공격!\n{_player.Name}을(를) 맞췄습니다. [피해: {_player.CurrentHP - playerDamage}]\n");

                Console.WriteLine($"Lv.{_player.Level} {_player.Name}\nHP {_player.CurrentHP} -> {_player.MaxHp - _player.CurrentHP}\n");
                Console.WriteLine("0. 다음\n");
                
            }
            
            SetNextAction(0, 0);


        }

        private Monster GenerateRandomMonster()
        {
            string[] monsterTypes = { "Lv2 미니언", "Lv3 공허충", "Lv5 대포 미니언", "Lv3 칼날부리" };
            string randomMonsterType = monsterTypes[rand.Next(monsterTypes.Length)];

            switch (randomMonsterType)
            {
                case "Lv2 Minion":
                    return new Monster("미니언", 2, 15, 5);
                case "Lv3 Void Insect":
                    return new Monster("공허충t", 3, 10, 9);
                case "Lv5 Cannon Minion":
                    return new Monster("대포 미니언", 5, 25, 8);
                case "Lv4 칼날부리":
                    return new Monster("칼날부리", 4, 20, 7);
                default:
                    // 무작위 유형에 문제가 있으면 기본적으로 Lv2 하수인으로 설정
                    return new Monster("미니언", 2, 15, 5);
            }
        }
        

        private void ShowBattleResults(Monster targetMonster, int damageDealt)
        {
            Console.WriteLine($"Battle!!\n{_player.Name}'s attack!");
            Console.WriteLine($"당신은 {targetMonster.Name}을 공격했습니다. [피해: 10]\n");

            // Display updated monster status
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{targetMonster.Name}\nHP: {targetMonster.HP} -> {(targetMonster.IsDead ? "Dead" : $"{targetMonster.HP - damageDealt}")}");
            Console.ResetColor();

            Console.WriteLine("\n0. 계속");
            SetNextAction(0, 0);

            Console.Clear();

            // 몬스터가 모두 죽으면
            if (_enemyMonsters.All(monster => monster.IsDead))
            {
                Console.WriteLine("\nBattle!! - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine("던전의 모든 몬스터를 처치했습니다.\n");

                // 결과 
                Console.WriteLine($"{_player.Name}\nHP {_player.MaxHp} -> {_player.CurrentHP}\n");

                Console.WriteLine("0. 계속");
                SetNextAction(0, 0);
            }
            else if (_player.IsDead)
            {
                Console.WriteLine("\nBattle!! - Result\n");
                Console.WriteLine("You Lose\n");
                Console.WriteLine("던전에서 전투에 패배했습니다.\n");

               
                Console.WriteLine($"{_player.Name}\nHP {_player.CurrentHP} -> 0\n");

                Console.WriteLine("0. 계속");
                SetNextAction(0, 0);
            }
        }

        private Monster SelectAliveMonster()
        {
            // Filter out dead monsters
            var aliveMonsters = _enemyMonsters.Where(monster => !monster.IsDead).ToList();

            // Randomly select a living monster
            Monster targetMonster = aliveMonsters[GameManager.rand.Next(aliveMonsters.Count)];

            return targetMonster;
        }

        private int SimulatePlayerAttack(Monster targetMonster)
        {
            // 플레이어 공격 및 데미지 계산 로직으로 대체하세요
            int attackPower = (int)(_player.Atk * (GameManager.rand.Next(9, 12) / 10.0));

            // 몬스터에게 데미지를 입힙니다
            int damageDealt = targetMonster.TakeDamage(attackPower);

            // 공격에 관한 정보를 표시합니다
            Console.WriteLine($"{_player.Name}의 공격!\n당신은 {targetMonster.Name}를 맞췄습니다. [데미지: {attackPower}]");

            // 몬스터가 죽었다면 'Dead' 표시
            if (targetMonster.IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{targetMonster.Name}\nHP: Dead");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{targetMonster.Name}\nHP: {targetMonster.HP - damageDealt}");
            }

            // 입힌 데미지를 반환합니다
            return damageDealt;
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
        public static void GameDataSetting()
        {
            _player = new Player("chad", "전사", 1, 10, 5, 100, 1500);
        }
    }
}

