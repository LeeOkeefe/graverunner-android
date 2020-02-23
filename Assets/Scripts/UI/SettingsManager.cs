using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    internal sealed class SettingsManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;

        public void OpenSettings()
        {
            m_CanvasGroup.ToggleGroup(true);
        }

        public void CloseSettings()
        {
            m_CanvasGroup.ToggleGroup(false);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
