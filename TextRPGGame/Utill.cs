using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TextRPGGame
{
    public static class Utill
    {
        public static void WriteRedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{text}");
            Console.ResetColor();
        }
        public static void WriteOrangeText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red | ConsoleColor.Blue;
            Console.Write($"{text}");
            Console.ResetColor();
        }
        public static void WriteBlueText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{text}");
            Console.ResetColor();
        }
        public static void WriteDarkBlueText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"{text}");
            Console.ResetColor();
        }
        public static void WriteGreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{text}");
            Console.ResetColor();
        }

        public static string FormatNumber(this int number)
        {
            if (number == 0) return "";
            return number > 0 ? $"(+{number})" : $"({number})";
        }

        public class ItemJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Item);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject item = JObject.Load(reader);
                ItemType itemType = item["Type"].ToObject<ItemType>();
                switch (itemType)
                {
                    case ItemType.Weapon:
                        return item.ToObject<Weapon>();
                    case ItemType.Shield:
                        return item.ToObject<Shield>();
                }
                return null;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public static void PrintStartLogo()
        {
            Console.WriteLine("======================================================================");
            Console.WriteLine("  ___________________   _____  __________ ___________ _____    ");
            Console.WriteLine(" /   _____/\\______   \\ /  _  \\ \\______   \\__    ___//  _  \\   ");

            Console.WriteLine(" \\_____  \\  |     ___//  /_\\  \\ |       _/  |    |  /  /_\\  \\  ");

            Console.WriteLine(" /        \\ |    |   /    |    \\|    |   \\  |    | /    |    \\ ");

            Console.WriteLine("/_______  / |____|   \\____|__  /|____|_  /  |____| \\____|__  / ");

            Console.WriteLine("        \\/                   \\/        \\/                  \\/  ");

            Console.WriteLine("                                                               ");

            Console.WriteLine("________                     ____                              ");

            Console.WriteLine("\\______ \\   __ __   ____    / ___\\   ____   ____    ____       ");

            Console.WriteLine(" |    |  \\ |  |  \\ /    \\  / /_/  >_/ __ \\ /  _ \\  /    \\      ");

            Console.WriteLine(" |    `   \\|  |  /|   |  \\ \\___  / \\  ___/(  <_> )|   |  \\     ");

            Console.WriteLine("/_______  /|____/ |___|  //_____/   \\___  >\\____/ |___|  /     ");

            Console.WriteLine("        \\/             \\/               \\/             \\/      ");
            Console.WriteLine("======================================================================");
            Console.WriteLine("                         PRESS ANY KEY TO START                       ");
            Console.WriteLine("======================================================================");
            Console.ReadKey();
        }
    }
}
