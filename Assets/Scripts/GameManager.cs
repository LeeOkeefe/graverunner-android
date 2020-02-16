using Assets.Scripts.Player;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public PlayerMovement PlayerMovement;
        public TextMeshProUGUI ScoreText;
        public LivesUI LivesUI;
        public HealthObject HealthObject;

        // Ensure only one instance of the GameManager exists
        //
        public void Awake()
        {
            if (Instance != null)
                Destroy(this);

            if (Instance == null)
                Instance = this;
        }

        public void GameOver()
        {
            Debug.Log("GAME OVER.");
        }
    }
}
