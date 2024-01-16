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
		public Action action;
		public string target = "";
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
        public virtual void CheckCondition(string name) { }



        public virtual void Clear()
		{
			questState = QuestState.CLEAR;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("퀘스트를 완료했습니다!!");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(questName);
            Console.WriteLine();
            Utill.WriteGreenText("퀘스트 보상!");
            ClearInfo();
            Console.WriteLine();
            Console.WriteLine();
            Utill.WriteGreenText("PRESS THE ENTER");
            Console.ReadLine();
        }
		public virtual void ClearInfo()
		{
            Console.WriteLine();
			Console.WriteLine($"{rewardGold} G");
        
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

