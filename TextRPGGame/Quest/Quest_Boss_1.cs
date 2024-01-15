using System;
namespace TextRPGGame.Quest
{
	public class Quest_Boss_1 : Quest
	{
        int clearCondition = 1;
        int current = 0;
        string target;

        public Quest_Boss_1()
        {
            SetQuest(0, "스켈레톤킹 처리하기", 2500);
            target = "스켈레톤킹";
            questInfo = "스켈레톤킹을 잡아주게..";
            requireLevel = 2;
        }
        public override void ShowQuestInfo()
        {
            base.ShowQuestInfo();
            Console.Write($" -  {target} ");
            if (questState == QuestState.CLEAR)
            {
                Console.Write(" - Clear -");
            }
            else
            {
                Utill.WriteRedText(clearCondition.ToString());
                Console.Write(" 마리 처치 ");
            }

            if (questState == QuestState.PROGRESS)
            {
                Console.Write($"{current} / {clearCondition}");
            }

        }


        public override void Reset()
        {
            base.Reset();
            current = 0;
        }

        public override void Clear()
        {
            base.Clear();
            GameManager.Instance.player.Gold += rewardGold;
            GameManager.Instance.player.slainMonsters.RemoveAll(monster => monster.Name == target);


        }
        public override void CheckCondition()
        {
            base.CheckCondition();

            if (GameManager.Instance.player.slainMonsters.Count > 0)
            {
                List<Monster> slainMonsters = GameManager.Instance.player.slainMonsters;
                int count = 0;

                foreach (Monster monster in slainMonsters)
                {
                    if (monster.Name.Contains(target))
                    {
                        count++;
                    }
                }
                current = Math.Max(current, count);

                if (current >= clearCondition)
                {
                    questState = QuestState.FINISH;
                }
            }

        }
    }
}

