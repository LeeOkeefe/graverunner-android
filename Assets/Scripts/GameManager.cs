using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public TextMeshProUGUI ScoreText;

        public void Awake()
        {
            if (Instance != null)
                Destroy(this);

            if (Instance == null)
                Instance = this;
        }
    }
}
