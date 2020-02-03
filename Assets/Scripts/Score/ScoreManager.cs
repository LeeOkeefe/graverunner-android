using TMPro;

namespace Assets.Scripts.Score
{
    public static class ScoreManager
    {
        public static int Amount { get; private set; }
        private static TextMeshProUGUI ScoreText => GameManager.Instance.ScoreText;

        public static void IncreaseScore()
        {
            Amount++;
            ScoreText.text = $"Score: {Amount}";
        }

        public static void IncreaseScore(int amount)
        {
            Amount += amount;
            ScoreText.text = $"Score: {Amount}";
        }

        public static void DecreaseAmount()
        {
            Amount--;
            ScoreText.text = $"Score: {Amount}";
        }

        public static void DecreaseScore(int amount)
        {
            Amount -= amount;
            ScoreText.text = $"Score: {Amount}";
        }
    }
}
