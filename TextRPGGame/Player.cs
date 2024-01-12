using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; } =1;
        public int Gold { get; set; }
        public int MaxHp { get; set; }
        int hp;
        public int HP
        {
            get { return hp; } // 현재 체력 값을 가져온다
            set
            {
                hp = value; // 속성에 새로운 값으로 설정
                //만약 체력이 0보다 작으면 0으로 고정
                if (hp < 0) hp = 0;
                //만약 체력이 최대 체력보다 초과하면 최대 체력으로 고정
                if (value > MaxHp) hp = MaxHp;

            }
        }
        public int Attack { get; }
        public int Defense { get; }
        public int FinalAttack
        {
            get // 몬스터의 최종 공격력 반환
            {
                int damageVariance = (int)Math.Ceiling(Attack * 0.1); //공격력의 10%범위 내의 편차를 저장
                // finalDamage에는 편차를 적용한 최종 공격력 계산.
                int finalDamage = new Random().Next(Attack - damageVariance, Attack + damageVariance + 1); 
                return finalDamage;
            }
        }
        public void Attacked(int damage) //받은 데미지만큰 체력 감소
        {
            HP -= damage;
        }
        public bool IsDead
        {
            get
            {
                if (HP <= 0)
                    return true;
                else
                    return false;
            }
        }

        // 스텟
        public Player(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Gold = gold;
            MaxHp = hp;
            Attack = atk;
            Defense = def;
            HP = hp;
            
        }
        
        // 플레이어 스테이터스 보여주기
        public static void ShowStatus()
        {
            Console.Clear();

            Utill.ShowHighLighterText("■상태 보기■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            Utill.PrintWithHightlights("Lv.", GameManager.Instance._player.Level.ToString("00"));
            Console.WriteLine("\n{0} ({1})", GameManager.Instance._player.Name, GameManager.Instance._player.Job);

            Utill.PrintWithHightlights("공격력 :", GameManager.Instance._player.Attack.ToString());
            Utill.PrintWithHightlights("방어력 :", GameManager.Instance._player.Defense.ToString());
            Utill.PrintWithHightlights("체력 :", GameManager.Instance._player.HP.ToString());
            Utill.PrintWithHightlights("골드 :", GameManager.Instance._player.Gold.ToString());
            Console.WriteLine("\n0. 뒤로가기");
            Console.WriteLine("");
            GameManager.Instance.SetNextAction(0, 0);
            switch (GameManager.Instance.action)
            {
                case 0:
                    GameManager.Instance.GameStart();
                    break;
            }
        }
    }
}
