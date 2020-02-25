using TMPro;

namespace Score
{
    public class ScoreManager
    {
        public int Score { get; private set; }
        private TextMeshProUGUI ScoreText => GameManager.Instance.ScoreText;

        public void IncreaseScore()
        {
            Score++;
            ScoreText.text = $"{Score}";
        }

        public void IncreaseScore(int amount)
        {
            Score += amount;
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

        public void ResetScore()
        {
            Score = 0;
            ScoreText.text = $"{Score}";
        }
    }
}
