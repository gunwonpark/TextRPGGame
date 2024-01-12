using System;
using TextRPGGame.Quest;
using TextRPGGame; 

public class QuestBoard
{

    List<Quest> quests = new List<Quest>();

    public QuestBoard()
    {
        Quest_0 quest_0 = new Quest_0();
        quests.Add(quest_0);
    }

    public void QuestBoardManu()
    {
        Console.WriteLine();
        Console.WriteLine();
        Utill.WriteRedText("Quest Board");
        Console.WriteLine();
        Console.WriteLine();

        for (int i = 0; i < quests.Count; i++)
        {
            Utill.WriteRedText((i + 1).ToString());
            Console.WriteLine($". {quests[i].questName}");
        }


    }

}



