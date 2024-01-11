using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TextRPGGame
{
    internal class BattleManager
    {
        Player player;
        List<Monster> monsters = new List<Monster>();
        public BattleManager()
        {
            player = GameManager.Instance.player;
            SpawnMonster();
        }
        public void StartBattle()
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!!\n");
            Console.WriteLine();

            // 몬스터 정보 표시
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].ShowStatus();
            }

            // 플레이어 정보 표시
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{player.Level}");
            Console.WriteLine($" {player.Name} ({player.Class})");
            Console.Write($"HP ");
            Utill.WriteRedText($"{player.MaxHp}");
            Utill.WriteRedText($"/{player.Hp}\n\n");

            Utill.WriteRedText("1. ");
            Console.WriteLine("공격");

            GameManager.Instance.SetNextAction(1, 1);

            switch (GameManager.Instance.action)
            {
                case 1:
                    Battle();
                    break;
            }
        }
        void Battle()
        {
            while (true)
            {
                Console.Clear();
                Utill.WriteOrangeText("Battle!!\n");
                Console.WriteLine();

                int deadMonsterCount = 0;

                // 몬스터 정보 표시
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (monsters[i].IsDead)
                    {
                        deadMonsterCount++;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.Write($"{i + 1} ");
                    monsters[i].ShowStatus();
                }

                // 모든 몬스터가 죽었으면 게임종료
                if (deadMonsterCount == monsters.Count)
                {
                    Victory(deadMonsterCount);
                    return;
                }
                // 내 체력이 0이되면 게임종료
                if (player.IsDead)
                {
                    Lose();
                    return;
                }
                // 플레이어 정보 표시
                Console.WriteLine("\n[내정보]");
                Console.Write($"Lv. ");
                Utill.WriteRedText($"{player.Level}");
                Console.WriteLine($" {player.Name} ({player.Class})");
                Console.Write($"HP ");
                Utill.WriteRedText($"{player.MaxHp}");
                Utill.WriteRedText($"/{player.Hp}\n\n");

                Utill.WriteRedText("0. ");
                Console.WriteLine("취소");

                GameManager.Instance.SetNextAction(0, monsters.Count);

                if (GameManager.Instance.action == 0)
                {
                    StartBattle();
                    return;
                }

                // 몬스터 선택
                Monster selectedMonster = monsters[GameManager.Instance.action - 1];
                while (selectedMonster.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터 입니다");
                    GameManager.Instance.SetNextAction(0, monsters.Count);
                    selectedMonster = monsters[GameManager.Instance.action - 1];
                }
                int damage = player.FinalAttack;
                selectedMonster.Attacked(damage);

                PlayerAttackResultMessage(selectedMonster, damage);
            }
        }
        void Victory(int deadMonsterCount)
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!! - Result\n");
            Console.WriteLine();

            Utill.WriteBlueText("Victory\n");

            Console.Write("던전에서 몬스터 ");
            Utill.WriteRedText($"{deadMonsterCount}");
            Console.WriteLine("마리를 잡았습니다.\n");

            // 플레이어 정보 표시
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{player.Level}");
            Console.WriteLine($" {player.Name} ({player.Class})");
            Console.Write($"HP ");
            Utill.WriteRedText($"{player.MaxHp}");
            Utill.WriteRedText($"/{player.Hp}\n\n");

            Utill.WriteRedText("0. ");
            Console.WriteLine("다음");

            GameManager.Instance.SetNextAction(0, 0);
        }
        void Lose()
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!! - Result\n");
            Console.WriteLine();

            Utill.WriteRedText("You Lose\n");

            // 플레이어 정보 표시
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{player.Level}");
            Console.WriteLine($" {player.Name} ({player.Class})");
            Console.Write($"HP ");
            Utill.WriteRedText($"{player.MaxHp}");
            Utill.WriteRedText($"/{player.Hp}\n\n");

            Utill.WriteRedText("0. ");
            Console.WriteLine("다음");

            GameManager.Instance.SetNextAction(0, 0);
        }

        void PlayerAttackResultMessage(Monster monster, int damage)
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!!\n");
            Console.WriteLine();

            // 플레이어 공격 표시
            Console.WriteLine($"{player.Name}의 공격!");
            Console.Write("Lv.");
            Utill.WriteRedText($"{monster.Level} ");
            Console.Write($"{monster.Name}을(를) 맞췄습니다. [데미지 : ");
            Utill.WriteRedText($"{damage}");
            Console.WriteLine("]\n");

            // 공격 결과 표시
            Console.Write("Lv.");
            Utill.WriteRedText($"{monster.Level} ");
            Console.WriteLine($"{monster.Name}");
            Console.Write("Hp ");
            Utill.WriteRedText($"{monster.Hp + damage} ");
            Console.WriteLine($"-> {(monster.IsDead ? "Dead" : monster.Hp)}\n");

            Utill.WriteRedText("0. ");
            Console.WriteLine("다음");

            GameManager.Instance.SetNextAction(0, 0);

            switch (GameManager.Instance.action)
            {
                case 0:
                    MonsterAttack();
                    break;
                default:
                    break;
            }
        }

        void MonsterAttack()
        {
            foreach (Monster monster in monsters)
            {
                if (monster.IsDead == false)
                {
                    player.Attacked(monster.Attack);
                    MonsterAttackResultMessage(monster, monster.Attack);
                    if (player.IsDead)
                        return;
                }
            }
        }

        void MonsterAttackResultMessage(Monster monster, int damage)
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!!\n");
            Console.WriteLine();

            // 플레이어 공격 표시
            Console.WriteLine($"{monster.Name}의 공격!");
            Console.Write("Lv.");
            Utill.WriteRedText($"{player.Level} ");
            Console.Write($"{player.Name}을(를) 맞췄습니다. [데미지 : ");
            Utill.WriteRedText($"{damage}");
            Console.WriteLine("]\n");

            // 공격 결과 표시
            Console.Write("Lv.");
            Utill.WriteRedText($"{player.Level} ");
            Console.WriteLine($"{player.Name}");
            Console.Write("Hp ");
            Utill.WriteRedText($"{player.Hp + damage} ");
            Console.WriteLine($"-> {(player.IsDead ? "Dead" : player.Hp)}\n");

            Utill.WriteRedText("0. ");
            Console.WriteLine("다음");

            GameManager.Instance.SetNextAction(0, 0);
        }
        void SpawnMonster()
        {
            int monsterCount = new Random().Next(1, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                int monsterIndex = new Random().Next(GameManager.Instance.monsters.Count);
                monsters.Add(GameManager.Instance.monsters[monsterIndex].DeepCopy());
            }
        }
    }
}
