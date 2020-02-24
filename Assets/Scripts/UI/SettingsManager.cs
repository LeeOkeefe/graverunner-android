using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    internal sealed class SettingsManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;

        public void ToggleSettings()
        {
            TogglePause();
            var condition = m_CanvasGroup.interactable == false;
            m_CanvasGroup.ToggleGroup(condition);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
            TogglePause();
        }

        public void ExitGame()
        {
            SceneManager.LoadScene("Menu");
            TogglePause();
        }

        private void TogglePause()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}
