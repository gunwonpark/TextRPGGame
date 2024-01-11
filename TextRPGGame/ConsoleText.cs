using System;
using static System.Collections.Specialized.BitVector32;

namespace TextRPGGame
{
	public class ConsoleText
	{
        public void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력 입니다.");
            Console.ResetColor();
        }

        public void DarkRedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str);
            Console.ResetColor();
        }
        public void RedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ResetColor();
        }

        public void YellowText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkYellowText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(str);
            Console.ResetColor();
        }

        public void DarkBlueText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(str);
            Console.ResetColor();
        }
        public void BlueText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(str);
            Console.ResetColor();
        }
        public void GreenText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkGreenText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkMagentaText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(str);
            Console.ResetColor();
        }
        public void MagentaText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(str);
            Console.ResetColor();
        }
        public void CyanText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(str);
            Console.ResetColor();
        }

        public void DarkCyanText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkGrayText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(str);
            Console.ResetColor();
        }

        public void NextActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">> ");
        }



        public void FindTextChangeColorGreen(string mainText,string findText)
        {
            int stringIdx = mainText.ToString().IndexOf(findText);
            int findTextLen = findText.Length;
            if(stringIdx != -1)
            {
              foreach(char c in mainText)
                {
                    if(findTextLen > 0)
                    {
                        GreenText(c.ToString());
                        findTextLen--;
                    }
                    else
                    {
                    Console.Write(c);
                    }
                }
            }
            else
            {
                Console.Write(mainText);
            }
        }

        public void TextAnimation(string str,int animationSpeed)
        {
            foreach(char c in str)
            {
                Console.Write(c);
                Thread.Sleep(animationSpeed);
            }
            Console.Clear();
        }


        public int SetNextAction(int minValue, int maxValue)
        {
            NextActionMessage();
            int action = IsInValidAction(minValue, maxValue);

            while (action == -1)
            {
                Console.WriteLine("잘못된 입력입니다 다시 선택해 주세요");
            }

            return action;
            
            
        }


        int IsInValidAction(int minValue, int maxValue)
        {
            if (!int.TryParse(Console.ReadLine(), out int action)) return -1;

            if (action > maxValue || action < minValue) return -1;

            return action;
        }


        public void RunningTimeText(int num)
        {
            for (int i = num; i >=0; i--)
            {
                int row = Console.CursorTop;
                int col = Console.CursorLeft;
                if (col != 0)
                {
                    Console.SetCursorPosition(col - 1, row);
                }
                GreenText(i.ToString());
                Thread.Sleep(1000);
            }

        }
    }
}

