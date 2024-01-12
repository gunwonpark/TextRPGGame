using System;
namespace TextRPGGame.Quest
{
	public class Quest_0 : Quest
	{
		int clearCondition;
		int current;
		string target;
		List<Monster> slainMonsters = new List<Monster>();


        public Quest_0()
		{
			SetQuest(0,"마을을 위협하는 미니언 처치",1000);
            //slainMonsters = GameManager.Instance.player.slainMonsters;
			target = "미니언";
			questInfo = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!";
        }

        public override void Condition()
        {
            base.Condition();
			CheckCondition();
        }

        public override void ShowProgress()
        {
            base.ShowProgress();
			Console.Write($" -  {target}");
			Console.Write(clearCondition);
			Console.Write("마리 처치 ");
			if(questState == QuestState.PROGRESS)
			{
				Console.WriteLine($"{current} / {clearCondition}");
			}
			
        }

        void CheckCondition()
		{
			int count = 0;

			foreach(Monster monster in slainMonsters)
			{
				if(monster.Name == target)
				{
					count++;
				}
			}
			current = Math.Max(current, count);

			if(current >= clearCondition)
			{
				Clear();
			}
		}
    }

	
}

