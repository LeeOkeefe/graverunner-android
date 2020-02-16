using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal sealed class LivesUI : MonoBehaviour
    {
        [SerializeField] private Image[] m_Images;

        private int m_NumberOfLives;

        private void Start()
        {
            UpdateLives();
        }

        /// <summary>
        /// Sets the number of hearts to the number of lives
        /// </summary>
        public void UpdateLives()
        {
            m_NumberOfLives = GameManager.Instance.HealthObject.HealthDefinition.Lives;

            foreach (var image in m_Images)
            {
                image.color = Color.clear;
            }

            for (var i = 0; i < m_NumberOfLives; i++)
            {
                m_Images[i].color = Color.white;
            }
        }
    }
}
