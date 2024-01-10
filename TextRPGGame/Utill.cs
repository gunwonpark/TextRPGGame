using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    public class Utill
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
    }
}
