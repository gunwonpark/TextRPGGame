using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    internal class Skill
    {
        public string SkillName { get; set; }
        public int Mp { get; set; }
        public float AttackPower { get; set; }
        public int AttackNum { get; set; }

        public int Attack
        {
            get
            {
                // 공격력이 소수점이면 올림처리
                int damage = (int)Math.Ceiling(GameManager.Instance.player.Attack * AttackPower);
                GameManager.Instance.player.UseSkill(Mp);
                return damage;
            }
        }

        public Skill()
        {

        }
        public Skill(string _skillName, int _mp, float _attackPower, int _attackNum)
        {
            SkillName = _skillName;
            Mp = _mp;
            AttackPower = _attackPower;
            AttackNum = _attackNum;
        }

        public void ShowSkills()
        {
            Console.Write($"{SkillName} - MP ");
            Utill.WriteRedText($"{Mp}\n");
            Console.Write("공격력 * ");
            Utill.WriteRedText($"{AttackPower} ");
            Console.Write("로 ");
            Utill.WriteRedText($"{AttackNum}");
            Console.Write("번 적을 "); // "명의 적을 "으로 했었으나 남은 몬스터가 1명일 때 스킬을 쓰는 것이 아까워서 수정함.
            if (AttackNum > 1)
            {
                Console.Write("랜덤으로 ");
            }
            Console.WriteLine("공격합니다.");
        }
    }
}
