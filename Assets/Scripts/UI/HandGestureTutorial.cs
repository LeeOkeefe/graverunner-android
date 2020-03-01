using UnityEngine;

namespace UI
{
    internal sealed class HandGestureTutorial : MonoBehaviour
    {
        private void Start()
        {
            var completeTutorial = PlayerPrefs.GetInt("CompleteTutorial", 0);

            if (completeTutorial >= 1)
            {
                Destroy(gameObject);
            }
        }

        public void CompleteTutorial()
        {
            PlayerPrefs.SetInt("CompleteTutorial", 1);
            Destroy(gameObject);
        }
    }
}
