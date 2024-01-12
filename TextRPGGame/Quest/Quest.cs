using System;
namespace TextRPGGame.Quest
{
	public enum QuestState
	{
		NONE,
		PROGRESS,
		FINISH,
		CLEAR
	}

	public class Quest
	{
		public int id;
		public string questName = "";
		public string questInfo = "";
		public bool isQuestAccepted;
		public QuestState questState;

		public int rewardGold;


		public virtual void Condition() { }
        public virtual void ShowProgress() { }


        public void Clear()
		{
			questState = QuestState.FINISH;
		}


		public void SetQuest(int id,string name,int gold)
		{
			this.id = id;
			questName = name;
			rewardGold = gold;
			questState = QuestState.NONE;
		}

    }
}

