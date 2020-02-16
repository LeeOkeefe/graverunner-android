using TMPro;

namespace Assets.Scripts.Score
{
    public static class ScoreManager
    {
        public static int Coins { get; private set; }
        private static TextMeshProUGUI ScoreText => GameManager.Instance.ScoreText;

        public static void IncreaseScore()
        {
            Coins++;
            ScoreText.text = $"{Coins}";
        }

        public static void IncreaseScore(int amount)
        {
            Coins += amount;
            ScoreText.text = $"{Coins}";
        }

        public static void DecreaseAmount()
        {
            Coins--;
            ScoreText.text = $"{Coins}";
        }

        public static void DecreaseScore(int amount)
        {
            Coins -= amount;
            ScoreText.text = $"{Coins}";
        }
    }
}
