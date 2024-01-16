using System;
namespace TextRPGGame.Quest
{
	public class Quest_Boss_0:Quest
	{
        int clearCondition = 1;
        int current = 0;
        

        public Quest_Boss_0()
		{
            SetQuest(0, "변종 거대슬라임 처리하기", 2000);
            target = "거대슬라임";
            questInfo = "일반 슬라임 보다 몸집이 약 5~6배는 더 커서 애를 먹고있네..\n자네가 마을을 위해 힘을 좀 써줬으면 좋겠는데..";
            requireLevel = 1;
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
            Console.WriteLine();
            Console.WriteLine("보상");
            Console.WriteLine(rewardGold + "G");

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

