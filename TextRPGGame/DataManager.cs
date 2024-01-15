﻿using Newtonsoft.Json;
using System.Numerics;
using System.Text.Json.Serialization;
using static TextRPGGame.Player;


namespace TextRPGGame
{
    class DataManager
    {
        public static string playerDataPath = "./PlayerData.json";
        public static string stageDataPath = "./StageData.json";

        public static void SaveData()
        {
            SavePlayerData();
            SaveStageData();

        }
        public static void SavePlayerData()
        {
            Console.WriteLine("player데이터 저장중");
            try
            {
                string json = JsonConvert.SerializeObject(GameManager.Instance.player, Formatting.Indented);
                File.WriteAllText($"{playerDataPath}", json);
                Console.WriteLine("Data saved successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
        public static Player LoadPlayerData()
        {
            try
            {
                if (File.Exists($"{playerDataPath}"))
                {
                    string json = File.ReadAllText($"{playerDataPath}");
                    Console.WriteLine("데이터 복구중");
                    return JsonConvert.DeserializeObject<Player>(json);
                }
                else
                {
                    Console.WriteLine("저장된 파일 없음");
                    return new Player("???????", ClassType.전사, 10, 5, 100, 50);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Load data: {ex.Message}");
                return new Player("???????", ClassType.전사, 10, 5, 100, 50);
            }
        }

        public static void SaveStageData()
        {
            Console.WriteLine("stage 데이터 저장중");
            try
            {
                string json = JsonConvert.SerializeObject(GameManager.Instance.stage, Formatting.Indented);
                File.WriteAllText($"{stageDataPath}", json);
                Console.WriteLine("Data saved successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
        public static Stage LoadStageData()
        {
            try
            {
                if (File.Exists($"{stageDataPath}"))
                {
                    string json = File.ReadAllText($"{stageDataPath}");
                    Console.WriteLine("데이터 복구중");
                    return JsonConvert.DeserializeObject<Stage>(json);
                }
                else
                {
                    Console.WriteLine("저장된 파일 없음");
                    return new Stage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Load data: {ex.Message}");
                return new Stage();
            }
        }
    }
}
