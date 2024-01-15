﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    internal class SkillManager
    {
        Player player;
        //BattleManager battleManager;
        public List<Monster> monsters = new List<Monster>();
        public List<Skill> skills = new List<Skill>();

        public SkillManager()
        {
            // GameManager에서 초기화한 플레이어 복사
            player = GameManager.Instance.player;

            //battleManager = new BattleManager();
            // battleManager에서 생성한 전투할 몬스터 리스트 복사
            monsters = BattleManager.battleManager.monsters;
            // GameManager에서 초기화한 스킬 리스트 복사
            skills = GameManager.Instance.skills;
        }

        // 스킬창 활성화
        public void StartSkill()
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!!\n");
            Console.WriteLine();

            //int deadMonsterCount = 0;

            // 몬스터 정보 표시
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (monsters[i].IsDead)
                {
                    //deadMonsterCount++;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                monsters[i].ShowStatus();
            }

            //// 모든 몬스터가 죽었으면 게임종료
            //if (deadMonsterCount == monsters.Count)
            //{
            //    BattleManager.battleManager.Victory(deadMonsterCount);
            //    // 높은 던전으로 이동
            //    GameManager.Instance.stage.Level++;
            //    return;
            //}
            //// 내 체력이 0이되면 게임종료
            //if (player.IsDead)
            //{
            //    BattleManager.battleManager.Lose();
            //    return;
            //}

            // 플레이어 정보 표시
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{player.Level}");
            Console.WriteLine($" {player.Name} ({player.Class})");
            Console.Write($"HP ");
            Utill.WriteRedText($"{player.MaxHp}");
            Utill.WriteRedText($"/{player.Hp}\n");
            Console.Write($"MP ");
            Utill.WriteRedText($"{player.MaxMp}");
            Utill.WriteRedText($"/{player.Mp}\n\n");

            // 스킬 정보 표시
            for(int i = 0; i < skills.Count; i++)
            {
                Utill.WriteRedText($"{i + 1}. ");
                skills[i].ShowSkills();
            }
            Utill.WriteRedText("0. ");
            Console.WriteLine("취소");

            GameManager.Instance.SetNextAction(0, skills.Count);

            switch (GameManager.Instance.action)
            {
                // 0. 취소
                case 0:
                    BattleManager.battleManager.StartBattle();
                    break;
                // 스킬 선택시
                default:
                    // MP가 부족할 경우
                    if (player.Mp < skills[GameManager.Instance.action - 1].Mp)
                    {
                        Utill.WriteRedText("MP가 부족합니다.");
                        Console.WriteLine();
                        Utill.WriteRedText("0. ");
                        Console.WriteLine("취소");
                        GameManager.Instance.SetNextAction(0, 0);
                        StartSkill();
                    }

                    SkillAttack(GameManager.Instance.action - 1);
                    break;
            }
        }

        // 스킬 공격
        private void SkillAttack(int skill)
        {
            // 스킬 공격 대상이 1명일 때
            if (skills[skill].AttackNum == 1)
            {
                AttackedMonster(skill);
            }
            // 스킬 공격 대상이 2명 이상일 때
            else
            {
                // 몬스터 선택
                // 선택될 몬스터 배열 선언
                Monster[] selectedMonster = new Monster[skills[skill].AttackNum];

                // 선택될 몬스터 랜덤 선정
                for (int i = 0; i < selectedMonster.Length; i++)
                {
                    Random random = new Random();
                    int num = random.Next(0, monsters.Count);

                    // 랜덤으로 선택된 몬스터가 죽었는지 확인
                    while (monsters[num].IsDead)
                    {
                        num = random.Next(0, monsters.Count);
                    }
                    selectedMonster[i] = monsters[num];
                }
                int damage = skills[skill].Attack;

                // !!!!!..퀘스트 부분인거 같은데 이 부분 필요한지 확인..!!!!!
                for (int i = 0; i < selectedMonster.Length; i++)
                {
                    if (selectedMonster[i].Hp - damage <= 0 && !selectedMonster[i].IsDead)
                    {
                        GameManager.Instance.player.slainMonsters.Add(selectedMonster[i]);
                    }
                }

                for (int i = 0; i < selectedMonster.Length; i++)
                {
                    selectedMonster[i].Attacked(damage);

                    PrintResultMessage(selectedMonster[i], damage);
                }

                BattleManager.battleManager.MonsterAttack();
            }
        }

        // 스킬 공격 대상 2명 이상일 때 공격 결과
        void PrintResultMessage(Monster monster, int damage)
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
            Utill.WriteRedText($"{(monster.IsDead ? "Dead" : monster.Hp + damage)} ");
            Console.WriteLine($"-> {(monster.IsDead ? "Dead" : monster.Hp)}\n");

            // 몬스터가 죽었으면 경험치 획득
            if (monster.IsDead)
            {
                player.GainExp(100);
            }

            Utill.WriteRedText("0. ");
            Console.WriteLine("다음");

            GameManager.Instance.SetNextAction(0, 0);
        }

        // 스킬 공격 대상 1명일 때 공격
        void AttackedMonster(int skill)
        {
            Console.Clear();
            Utill.WriteOrangeText("Battle!!\n");
            Console.WriteLine();


            // 몬스터 정보 표시
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"{i + 1} ");
                monsters[i].ShowStatus();
            }

            // 플레이어 정보 표시
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{player.Level}");
            Console.WriteLine($" {player.Name} ({player.Class})");
            Console.Write($"HP ");
            Utill.WriteRedText($"{player.MaxHp}");
            Utill.WriteRedText($"/{player.Hp}\n");
            Console.Write($"MP ");
            Utill.WriteRedText($"{player.MaxMp}");
            Utill.WriteRedText($"/{player.Mp}\n\n");

            Utill.WriteRedText("0. ");
            Console.WriteLine("취소");

            GameManager.Instance.SetNextAction(0, monsters.Count);

            if (GameManager.Instance.action == 0)
            {
                StartSkill();
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
            int damage = skills[skill].Attack;

            // !!!!!..퀘스트 부분인거 같은데 이 부분 필요한지 확인..!!!!!
            if (selectedMonster.Hp - damage <= 0 && !selectedMonster.IsDead)
            {
                GameManager.Instance.player.slainMonsters.Add(selectedMonster);
            }

            selectedMonster.Attacked(damage);

            BattleManager.battleManager.PlayerAttackResultMessage(selectedMonster, damage);
        }
    }
}
