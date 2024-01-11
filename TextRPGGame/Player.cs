using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Player
    {
        Random rand = new Random();
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Gold { get; set; }
        public int MaxHp { get; set; }
        public int Atk { get; }
        public int Def { get; }
        public int CurrentHP { get; private set; }
        public bool IsDead => CurrentHP <= 0;

        // 스텟
        public Player(string name, string job, int level, int atk, int def, int maxHp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Gold = gold;
            MaxHp = maxHp;
            Atk = atk;
            Def = def;
            CurrentHP = maxHp;
            
        }
        // 포션
        public int potion = 5;
        public int mods = 0;

        // 플레이어 스테이터스 보여주기
        public static void ShowStatus()
        {
            Console.Clear();

            ShowHighLighterText("■상태 보기■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintWithHightlights("Lv.", GameManager._player.Level.ToString("00"));
            Console.WriteLine("\n{0} ({1})", GameManager._player.Name, GameManager._player.Job);

            PrintWithHightlights("공격력 :", GameManager._player.Atk.ToString());
            PrintWithHightlights("방어력 :", GameManager._player.Def.ToString());
            PrintWithHightlights("체력 :", GameManager._player.CurrentHP.ToString());
            PrintWithHightlights("골드 :", GameManager._player.Gold.ToString());
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
        public static void ShowHighLighterText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void PrintWithHightlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        public void TakeDamage(int damage)
        {
            // Decrease player's health when taking damage
            CurrentHP -= damage;

            // Ensure health doesn't go below 0
            if (CurrentHP < 0)
                CurrentHP = 0;
        }

        

        // Additional method to calculate actual damage (considering randomness or modifiers)
        private int CalculateActualDamage(int damage)
        {
            // Adjust this method based on your game's logic for calculating actual damage
            // For example, consider adding randomness or other factors
            return damage;
        }

    }
}
