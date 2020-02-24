using System.Collections.Generic;
using Player;
using Score;
using TMPro;
using UI;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement PlayerMovement;
    public TextMeshProUGUI ScoreText;
    public LivesUI LivesUI;
    public HealthObject HealthObject;
    public Animation MinRestrictionLine;

    public List<Vector2> Grid;
    public ScoreManager ScoreManager;

    // Ensure only one instance of the GameManager exists
    //
    public void Awake()
    {
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;

        ScoreManager = new ScoreManager();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER.");
    }
}