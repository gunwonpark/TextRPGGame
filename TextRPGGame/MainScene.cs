using System.Numerics;

namespace TextRPGGame
{
    class MainScene
    {
        static void Main(string[] args)
        {
            Utill.PrintStartLogo();
            GameManager.Instance.SetName();
            GameManager.Instance.ChooseClass();
            GameManager.Instance.GameStart();
        }
    }
}