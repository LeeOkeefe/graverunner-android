﻿using System.Collections.Generic;
using Assets.Scripts.GridGeneration;
using Assets.Scripts.Player;
using Assets.Scripts.Score;
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
        public Animation MaxRestrictionLine;
        public Animation MinRestrictionLine;
        public float BackgroundRepeatSpeed = 0.215f;
        public float ObjectFallingSpeed = 0.015f;

        public List<Vector2> Grid;

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
        }

        private void OnEnable()
        {
            ScoreManager.ResetScore();
        }

        public void GameOver()
        {
            Debug.Log("GAME OVER.");
        }
    }
}
