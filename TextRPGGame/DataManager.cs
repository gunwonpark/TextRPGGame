using Newtonsoft.Json;
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
                string json = JsonConvert.SerializeObject(GameManager.Instance.player, Formatting.Indented, new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new Utill.ItemJsonConverter() }
                });
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
                    return JsonConvert.DeserializeObject<Player>(json, new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new Utill.ItemJsonConverter() }
                    });
                }
                else
                {
                    Console.WriteLine("저장된 파일 없음");
                    return new Player("???????", ClassType.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Load data: {ex.Message}");
                return new Player("???????", ClassType.None);
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
