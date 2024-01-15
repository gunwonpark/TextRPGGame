using System;
namespace TextRPGGame.Quest
{
	public class Quest_Boss_1 : Quest
	{
        int clearCondition = 1;
        int current = 0;
        

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
        public override void CheckCondition(string name)
        {
            base.CheckCondition();
            if (name.Contains(target))
            {
                current++;
            }

            if (current >= clearCondition)
            {
                questState = QuestState.FINISH;
            }
        }
        
    }
}

