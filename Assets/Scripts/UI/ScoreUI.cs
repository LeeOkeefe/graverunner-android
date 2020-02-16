using System.Net.Mime;
using Assets.Scripts.Score;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal sealed class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            m_Text.text = $"{ScoreManager.Coins}";
        }

        public void UpdateScoreText()
        {
            m_Text.text = $"{ScoreManager.Coins}";
        }
    }
}
