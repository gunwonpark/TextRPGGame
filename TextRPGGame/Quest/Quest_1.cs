using System;
namespace TextRPGGame.Quest
{
	public class Quest_1 :Quest
	{
        int targetLevel;
		public Quest_1()
		{
            SetQuest(1, "더욱 더 강해지기!",1000);
            questInfo = "자네! 던전에서 살아남고 싶다면 계속해서 강해지도록 하게!\n 내가 자네의 성장을 도와주겠네!";
            requireLevel = 1;
            targetLevel = 3;
		}

      
        public override void CheckCondition()
        {
            base.CheckCondition();
            if(GameManager.Instance.player.Level >= targetLevel)
            {
                questState = QuestState.FINISH;
            }
        }

        public override void ShowQuestInfo()
        {
            base.ShowQuestInfo();
            Console.Write(" Level ");
            Utill.WriteRedText(targetLevel.ToString());
            Console.Write(" 달성하기 ");
            Console.WriteLine($"{GameManager.Instance.player.Level} / {targetLevel}");

        }
        public override void Clear()
        {
            base.Clear();
            Reset();

        }
        public override void Reset()
        {
            if (questState != QuestState.REQUIRE_ACHIEVED)
            {
                GameManager.Instance.player.Gold += rewardGold;
                questState = QuestState.PROGRESS;
                rewardGold += 500;
                requireLevel = targetLevel;
                targetLevel += 3;
            }
        }
    }
}

