namespace TextRPGGame
{
    class MainScene
    {
        static void Main(string[] args)
        {
            Utill.PrintStartLogo();
            GameManager.Instance.SetName();
            GameManager.Instance.GameStart();
        }
    }
}