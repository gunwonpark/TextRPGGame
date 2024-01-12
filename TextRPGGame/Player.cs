﻿using System;
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
        public List<Monster> slainMonsters;

        public int Exp { get; set; }
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
        public Player(string _name, ClassType _class, int _attack, int _defense, int _hp)
        {
            Name = _name;
            Class = _class;
            Attack = _attack;
            Defense = _defense;
            MaxHp = _hp;
            hp = _hp;
            slainMonsters = new List<Monster>();
        }


        public void Attacked(int damage)
        {
            Hp -= damage;
        }
        public void ShowStatus()
        {
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

        public void GainExp(int exp)
        {
            Exp += exp;
            CheckLevelUp();
        }

        public bool CheckLevelUp()
        {
            int requiredExp = Level * (Level + 1) * 5; 
            if (Exp >= requiredExp)
            {
                Level++;
                Exp = 0;
                Attack += 2; // 공격력 상승
                Defense += 1; // 방어력 상승
                MaxHp += 30;
                Hp = MaxHp;
                LevelUpMessage();
                return true;
            }
            return false;
        }
        void LevelUpMessage()
        {
            Console.WriteLine("\n축하합니다 레벨업 하였습니다\n");
            Console.WriteLine($"레벨 : {Level - 1} -> {Level}");
            Console.WriteLine($"최대 체력 : {MaxHp - 30} -> {MaxHp}");
            Console.WriteLine($"공격력 : {Attack - 2} -> {Attack}");
            Console.WriteLine($"방어력 : {Defense - 1} -> {Defense}\n");
        }
    }
}
