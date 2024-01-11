using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Monster
    {
        // 플레이어 정보
        public int lv { get; set; }
        public string name { get; set; }
        public int hp { get; set; }
        public int curHp { get; set; }
        public int atk { get; set; }
        public bool isDie { get; set; }

        // 생성자
        public Monster()
        {

        }

        // 생성자
        public Monster(int inputLv, string inputName, int inputHp, int inputAtk, bool inputIsDie = false)
        {
            this.lv = inputLv;
            this.name = inputName;
            this.hp = inputHp;
            this.curHp = inputHp;
            this.atk = inputAtk;
            this.isDie = inputIsDie;
        }

        // 전투 몬스터 생성시 깊은 복사
        public Monster DeepCopy()
        {
            Monster copyMonster = new Monster();
            copyMonster.lv = this.lv;
            copyMonster.name = this.name;
            copyMonster.hp = this.hp;
            copyMonster.curHp = this.curHp;
            copyMonster.atk = this.atk;
            copyMonster.isDie = this.isDie;

            return copyMonster;
        }

        // 몬스터 상태 보기
        public void ShowStatus(bool withNum = false, int idx = 0)
        {
            // 몬스터 숫자 인덱스가 필요할 경우
            if (withNum)
            {
                // 몬스터가 죽어있으면
                if (isDie == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{idx + 1} ");
                    Console.ResetColor();
                }
                // 몬스터가 살아있으면
                else
                {
                    Console.Write($"{idx + 1} ");
                }
            }

            // 몬스터 상태 보기
            if (isDie == true)
            {
                // 몬스터가 죽어있으면
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv. {lv} {name} Dead");
                Console.ResetColor();
            }
            // 몬스터가 살아있으면
            else
            {
                Console.WriteLine($"Lv. {lv} {name} HP {hp}");
            }
        }

        // 플레이어 공격
        public int AttackMonster()
        {
            Random random = new Random();

            // 공격력 오차율 10%
            int errorRate = (int)Math.Ceiling(this.atk * 0.1f);
            int attack = random.Next(this.atk - errorRate, (this.atk + errorRate) + 1);

            return attack;
        }
    }
}
