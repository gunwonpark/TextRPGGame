using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Player
    {
        // 플레이어 정보
        public int lv { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        public int curHp { get; set; }
        public int updateHp { get; set; }
        public int gold { get; set; }

        // 생성자
        public Player(int inputLv, string inputName, string inputJob, int inputAtk, int inputDef, int inputHp, int inputGold)
        {
            this.lv = inputLv;
            this.name = inputName;
            this.job = inputJob;
            this.atk = inputAtk;
            this.def = inputDef;
            this.hp = inputHp;
            this.curHp = inputHp;
            this.updateHp = inputHp;
            this.gold = inputGold;
        }

        // 플레이어 상태 보기
        public void ShowStatus()
        {
            Console.WriteLine($"Lv. {lv.ToString("00")}");
            Console.WriteLine($"{name} ( {job} )");
            Console.WriteLine($"공격력 \t: {atk}");
            Console.WriteLine($"방어력 \t: {def}");
            Console.WriteLine($"체  력 \t: {curHp}");
            Console.WriteLine($"Gold \t: {gold} G");
        }

        // 전투 중 플레이어 상태 보기
        public void ShowStatusSimple()
        {
            Console.WriteLine($"Lv. {lv} {name} ({job})");
            Console.WriteLine($"HP {curHp}/{hp}");
        }

        // 몬스터 공격
        public int AttackMonster()
        {
            Random random= new Random();

            // 공격력 오차율 10%
            int errorRate = (int)Math.Ceiling(this.atk * 0.1f);
            int attack = random.Next(this.atk - errorRate, (this.atk + errorRate) + 1);

            return attack;
        }
    }
}
