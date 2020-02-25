using TMPro;
using UnityEngine;

namespace UI
{
    internal sealed class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            m_Text.text = $"{GameManager.Instance.ScoreManager.Score}";
        }

        public void UpdateScoreText()
        {
            m_Text.text = $"{GameManager.Instance.ScoreManager.Score}";
        }
    }
}
