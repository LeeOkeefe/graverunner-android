using TMPro;

namespace Score
{
    public class ScoreManager
    {
        public  int Coins { get; private set; }
        private TextMeshProUGUI ScoreText => GameManager.Instance.ScoreText;

        public void IncreaseScore()
        {
            Coins++;
            ScoreText.text = $"{Coins}";
        }

        public void IncreaseScore(int amount)
        {
            Coins += amount;
            ScoreText.text = $"{Coins}";
        }

        public void DecreaseAmount()
        {
            Coins--;
            ScoreText.text = $"{Coins}";
        }

        public void DecreaseScore(int amount)
        {
            Coins -= amount;
            ScoreText.text = $"{Coins}";
        }

        public void ResetScore()
        {
            Coins = 0;
            ScoreText.text = $"{Coins}";
        }
    }
}
