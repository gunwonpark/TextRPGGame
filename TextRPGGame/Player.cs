using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPGGame
{
    class Player
    {
        // 스텟
        int hp;
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public ClassType Class { get; set; }
        public int Attack { get; set; }
        public bool IsDead
        {
            get
            {
                if (Hp <= 0)
                    return true;
                else
                    return false;
            }
        }
        public int FinalAttack
        {
            get
            {
                int damageVariance = (int)Math.Ceiling(Attack * 0.1);

                int finalDamage = new Random().Next(Attack - damageVariance, Attack + damageVariance + 1);
                return finalDamage;
            }
        }
        public int Defense { get; set; }
        public int MaxHp { get; set; }
        public int Hp
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp < 0) hp = 0;
                if (value > MaxHp) hp = MaxHp;

            }
        }
        public int Gold { get; set; } = 1500;
        public Player(string _name, ClassType _class)
        {
            Name = _name;
            Class = _class;
            
            // 클래스에 기반한 기본 스탯 및 스킬 설정
            InitializeClassStats();
            InitializeClassSkills();
        }

        private void InitializeClassStats()
        {
            switch (Class)
            {
                case ClassType.전사: // 전사
                    Attack = 15;
                    Defense = 10;
                    MaxHp = 120;
                    break;
                case ClassType.궁수: // 궁수
                    Attack = 10;
                    Defense = 8;
                    MaxHp = 100;
                    break;
                case ClassType.마법사: // 마법사
                    Attack = 12;
                    Defense = 6;
                    MaxHp = 90;
                    break;    
            }
            // 초기 HP를 최대치로 설정
            Hp = MaxHp;
        }

        private void InitializeClassSkills() 
        {
            switch (Class)
            {
                case ClassType.전사:
                    // 전사 스킬 추가
                    break;
                case ClassType.궁수:
                    // 궁수 스킬 추가
                    break;
                case ClassType.마법사:
                    // 마법사스킬 추가
                    break;
            }
        }

        public void Attacked(int damage)
        {
            Hp -= damage;
        }
        public void ShowStatus()
        {
            if (Attack == 0 || Defense == 0 || MaxHp == 0)
            {
                InitializeClassStats();
            }
            Console.Write($"Lv. ");
            Utill.WriteRedText($"{Level:D2}\n");

            Console.Write($"{Name} ( {Class} )\n");            

            Console.Write("공격력 : ");
            Utill.WriteRedText($"{Attack}\n");

            Console.Write($"방어력 : ");
            Utill.WriteRedText($"{Defense}\n");

            Console.Write($"체 력 : ");
            Utill.WriteRedText($"{Hp}\n");

            Console.Write($"Gold : ");
            Utill.WriteRedText($"{Gold}");
            Console.WriteLine($" G\n");
        }
        public enum ClassType
        {
            None = 0,
            전사 = 1,
            궁수 = 2,
            마법사 = 3
        }
    }
}
