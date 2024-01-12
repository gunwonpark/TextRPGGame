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
