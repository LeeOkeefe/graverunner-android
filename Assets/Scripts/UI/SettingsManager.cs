using Extensions;
using UnityEditor;
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

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void TogglePause()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}
