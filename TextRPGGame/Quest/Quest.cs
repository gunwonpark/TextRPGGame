using System;
namespace TextRPGGame.Quest
{
	public enum QuestState
	{
		REQUIRE_ACHIEVED,
        NOT_REQUIRE_ACHIEVED,
        PROGRESS,
		FINISH,
		CLEAR
	}

	public class Quest
	{
		
		public int id;
		public int requireLevel;
        public string questName = "";
		public string questInfo = "";
		public bool isQuestAccepted;
		public QuestState questState;
		public int rewardGold;


		public virtual void ShowQuestInfo() { }
		public virtual void Reset() { }
		public virtual void CheckCondition() { }



		public virtual void Clear()
		{
			questState = QuestState.CLEAR;
			ClearInfo();
		}
		public virtual void ClearInfo()
		{
			Console.Clear();
			Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("퀘스트를 완료했습니다!!");
            Console.WriteLine();
            Console.WriteLine();
			Console.Write(questName);
			Utill.WriteGreenText("퀘스트 보상!");
            Console.WriteLine();
			Console.WriteLine($"{rewardGold} G");
            Console.WriteLine();
            Console.WriteLine();
			Utill.WriteGreenText("PRESS THE ENTER");
			Console.ReadLine();

        }

		public void SetQuest(int id,string name,int gold)
		{
			this.id = id;
			questName = name;
			rewardGold = gold;
			questState = QuestState.NOT_REQUIRE_ACHIEVED;
		}

    }
}

