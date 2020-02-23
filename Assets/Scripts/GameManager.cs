using System.Collections.Generic;
using GridGeneration;
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
    public Animation MaxRestrictionLine;
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

        var gravegridgenerator = new GraveGridGenerator(8, 4);

        Grid = gravegridgenerator.GeneratePath();
        ScoreManager = new ScoreManager();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER.");
    }
}