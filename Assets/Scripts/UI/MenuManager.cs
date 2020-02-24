using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    internal sealed class MenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
