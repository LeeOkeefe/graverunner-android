using TMPro;

namespace Score
{
    public class ScoreManager
    {
        public float Score { get; private set; }
        public float Multiplier { get; private set; }
        private TextMeshProUGUI ScoreText => GameManager.Instance.ScoreText;

        public ScoreManager()
        {
            Score = 0;
            Multiplier = 1;
            ScoreText.text = $"{Score}";
        }

        public void IncreaseScore()
        {
            Score += (1 * Multiplier);
            ScoreText.text = $"{Score}";
        }

        public void IncreaseScore(int amount)
        {
            Score += (amount * Multiplier);
            ScoreText.text = $"{Score}";
        }

        public void DecreaseAmount()
        {
            Score--;
            ScoreText.text = $"{Score}";
        }

        public void DecreaseScore(int amount)
        {
            Score -= amount;
            ScoreText.text = $"{Score}";
        }

        public void HandleScoreMultiplier(float multiplier)
        {
            Multiplier = multiplier;
        }
    }
}
