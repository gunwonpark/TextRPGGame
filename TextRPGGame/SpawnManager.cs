using System;
namespace TextRPGGame
{
	public class SpawnManager
	{
		Random random = new Random();
        Monster[] monsters = { new Monster("미니언", 10, 5,"몽둥이질",10), new Monster("대포 미니언", 20, 10,"돌 던지기",15), new Monster("공허충", 7, 9,"침 뱉기",8) }; 

		public Monster[] GeneratorMonsters()
		{
			int randomNum = random.Next(1, 6);//spawn monster amount
			Monster[] newMonsters = new Monster[randomNum];

			for(int i = 0; i < newMonsters.Length; i++)
			{
				int ranIdx = new Random().Next(0,monsters.Length); //random monster level
				newMonsters[i] = monsters[ranIdx].Clone();
				newMonsters[i].SetMonsterState();
				
			}

			return newMonsters;
		}

		
	}
}

