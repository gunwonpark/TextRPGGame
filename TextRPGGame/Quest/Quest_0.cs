﻿using System;
namespace TextRPGGame.Quest
{
	public class Quest_0 : Quest
	{
		int clearCondition = 2;
		int current = 0;
		string target;

		public Quest_0()
		{
			SetQuest(0, "마을을 위협하는 슬라임 처치", 1000);
			target = "슬라임";
			questInfo = "이봐! 마을 근처에 슬라임들 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!";
        }

        public override void Condition()
        {
            base.Condition();
			CheckCondition();
        }

        public override void ShowQuestInfo()
        {
            base.ShowQuestInfo();
			Console.Write($" -  {target} ");
			Utill.WriteRedText(clearCondition.ToString());
			Console.Write(" 마리 처치 ");
			if(questState == QuestState.PROGRESS)
			{
				Console.WriteLine($"{current} / {clearCondition}");
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
            GameManager.Instance.player.slainMonsters.RemoveAll(monster => monster.Name == target);


        }

        void CheckCondition()
		{
			if(GameManager.Instance.player.slainMonsters.Count > 0)
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

