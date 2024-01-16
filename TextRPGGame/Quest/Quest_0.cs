using System;
namespace TextRPGGame.Quest
{
	public class Quest_0 : Quest
	{
		int clearCondition = 5;
		int current = 0;
        Weapon rewardItem = new Weapon("슬라임 채찍", "슬라임으로 만든 채찍,말랑말랑 하다.", 3, 750);
        public Quest_0()
		{
			SetQuest(0, "마을을 위협하는 슬라임 처치", 1000);
			target = "슬라임";
			questInfo = "이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!";
            requireLevel = 1;
            //Check_Requirement();
        }

      

        public override void ShowQuestInfo()
        {
            base.ShowQuestInfo();
			Console.Write($" -  {target} ");
            if(questState == QuestState.CLEAR)
            {
                Console.Write(" - Clear -");
            }
            else
            {
                Utill.WriteRedText(clearCondition.ToString());
                Console.Write(" 마리 처치 ");
            }
			
			if(questState == QuestState.PROGRESS)
			{
				Console.Write($"{current} / {clearCondition}");
			}
            Console.WriteLine();
            Console.WriteLine("보상");
            Console.WriteLine(rewardGold + "G");
            Console.WriteLine(rewardItem.Name);
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
            GameManager.Instance.player.inventory.Add(rewardItem);
            GameManager.Instance.player.slainMonsters.RemoveAll(monster => monster.Name == target);

        }


        public override void CheckCondition(string name)
        {
            base.CheckCondition();
            if (name.Contains(target))
            {
                current++;
            }

            if(current >= clearCondition)
            {
                questState = QuestState.FINISH;
            }
        }

    }

	
}

