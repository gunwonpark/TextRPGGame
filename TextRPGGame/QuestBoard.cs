using System;
using TextRPGGame.Quest;
using TextRPGGame; 

public class QuestBoard
{

    List<Quest> quests;

    public QuestBoard()
    {
        quests = new List<Quest>();
        Quest_0 quest_0 = new Quest_0();
        Quest_1 quest_1 = new Quest_1();
        quests.Add(quest_0);
        quests.Add(quest_1);
    }

    public void QuestBoardManu()
    {
        QuestProgressCheck();
        Console.WriteLine();
        Console.WriteLine();
        Utill.WriteRedText("Quest Board");
        Console.WriteLine();
        Console.WriteLine();

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].requireLevel <= GameManager.Instance.player.Level && quests[i].questState == QuestState.NOT_REQUIRE_ACHIEVED)
            {
                quests[i].questState = QuestState.REQUIRE_ACHIEVED;
            }
            if (quests[i].questState == QuestState.CLEAR)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write(i + 1);
            Console.Write($". {quests[i].questName}");

            switch (quests[i].questState)
            {
                case QuestState.PROGRESS:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" (진행중) ");
                    Console.ResetColor();
                    break;
                case QuestState.FINISH:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" (보상 받기) ");
                    Console.ResetColor();
                    break;
                case QuestState.CLEAR:
                    Console.Write(" (완료) ");
                    break;
                case QuestState.REQUIRE_ACHIEVED:
                    Utill.WriteGreenText(" (수행 가능) ");
                    break;
                case QuestState.NOT_REQUIRE_ACHIEVED:
                    Utill.WriteRedText(" (수행 불가) ");
                    Console.Write($" 필요 수행 레벨 - ( {quests[i].requireLevel} )");
                    break;
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 퀘스트를 선택해 주세요.");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(">> ");
        Console.ResetColor();
        string action = Console.ReadLine();
        if(int.TryParse(action,out int num) && num-1 < quests.Count && num >=0)
        {
            if(num == 0)
            {
                Console.Clear();
            }
            else
            {
                Console.Clear();
                if (quests[num-1].requireLevel <= GameManager.Instance.player.Level)
                {
                    QuestBoard_QuestInfo(quests[num - 1]);
                }
                else
                {
                    Console.WriteLine("현재 수행할 수 없는 퀘스트 입니다.");
                    QuestBoardManu();
                }

            }
           
        }
        else
        {
            Console.Clear();
            Console.WriteLine("잘못된 입력 ");
            QuestBoardManu();
        }

    }



    void QuestBoard_QuestInfo(Quest quest)
    {
        Console.WriteLine();
        Console.WriteLine();
        Utill.WriteRedText("Quest ");
        if (quest.questState == QuestState.CLEAR)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.Write(quest.questName);
        Console.Write($" [필요 수행 레벨 - {quest.requireLevel} ]");
        switch (quest.questState)
        {
            case QuestState.PROGRESS:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" (진행중) ");
                Console.ResetColor();
                break;
            case QuestState.CLEAR:
                Console.Write(" (완료) ");
                break;
        }

        Console.WriteLine();
        Console.WriteLine();
        if(quest.questState != QuestState.CLEAR)
        {
            Console.WriteLine(quest.questInfo);
        }
        Console.WriteLine();
        quest.ShowQuestInfo();
        Console.WriteLine();
        Console.WriteLine("보상");
        Console.WriteLine(quest.rewardGold +"G");
        Console.WriteLine();
        switch (quest.questState)
        {
            case QuestState.REQUIRE_ACHIEVED :
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                break;
            case QuestState.PROGRESS:
                Console.WriteLine("1. 포기");
                Console.WriteLine("0. 나가기");
                break;
            case QuestState.FINISH:
                Console.WriteLine("0. 보상 받기");
                break;
            case QuestState.CLEAR:
                Console.WriteLine("0. 나가기");
                break;
        }
        
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해 주세요.");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(">> ");
        Console.ResetColor();
        string action = Console.ReadLine();
        switch (action)
        {
            case "1":
                if(quest.questState == QuestState.PROGRESS)
                {
                    quest.questState = QuestState.REQUIRE_ACHIEVED;
                    quest.Reset();
                    Console.Clear();
                    QuestBoard_QuestInfo(quest);
                }
                else if(quest.questState == QuestState.REQUIRE_ACHIEVED)
                {
                    Console.Clear();
                    //플레이어 퀘스트 리스트에 추가
                    quest.questState = QuestState.PROGRESS;
                    QuestBoard_QuestInfo(quest);
                }
                else
                {
                    Console.Clear();
                    Utill.WriteRedText("잘못된 입력");
                    QuestBoard_QuestInfo(quest);
                }
                break;
            case "2":
                if (quest.questState == QuestState.PROGRESS)
                {
                    Console.Clear();
                    QuestBoardManu();
                }
                else if(quest.questState == QuestState.REQUIRE_ACHIEVED)
                {
                    Console.Clear();
                    QuestBoardManu();
                }
                else
                {
                    Console.Clear();
                    Utill.WriteRedText("잘못된 입력");
                    QuestBoard_QuestInfo(quest);
                }
                break;
            case "0":
                if(quest.questState == QuestState.FINISH)
                {
                    quest.Clear();
                    //플레이어 보상 수령
                    Console.ResetColor();
                    Console.Clear();
                    QuestBoardManu();
                }
                else
                {
                    Console.Clear();
                    QuestBoardManu();
                }
               
                break;
            default:
                Console.Clear();
                Console.WriteLine("잘못된 입력");
                QuestBoard_QuestInfo(quest);
                break;
        }
    }


    void QuestProgressCheck()
    {
        foreach(Quest quest in quests)
        {
            if (quest.questState == QuestState.PROGRESS)
            {
             quest.CheckCondition();
            }
        }
    }
}



