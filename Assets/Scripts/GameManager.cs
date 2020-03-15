using System.Collections.Generic;
using Ads;
using Effects;
using Player;
using Score;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement PlayerMovement;
    public TextMeshProUGUI ScoreText;
    public LivesUI LivesUI;
    public HealthObject HealthObject;
    public Animation MinRestrictionLine;
    public EffectsManager EffectsManager;
    public List<Vector2> Grid;
    public ScoreManager ScoreManager;

    private AudioSource m_AudioSource;

    // Ensure only one instance of the GameManager exists
    //
    public void Awake()
    {
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        ScoreManager = new ScoreManager();
        m_AudioSource = GetComponent<AudioSource>();

        Screen.orientation = ScreenOrientation.Portrait;
        AdsManager.Initialize();
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        m_AudioSource.PlayOneShot(audioClip);
    }
    
    public void GameOver()
    {
        AdsManager.ShowAd();

        PlayerPrefs.SetFloat("CurrentScore", ScoreManager.Score);
        var currentScore = PlayerPrefs.GetFloat("CurrentScore");
        var bestScore = PlayerPrefs.GetFloat("BestScore", 0);

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", currentScore);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("GameOver");
    }
}