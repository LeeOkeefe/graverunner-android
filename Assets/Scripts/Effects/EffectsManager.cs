using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Effects
{
    internal sealed class EffectsManager : MonoBehaviour
    {
        private Camera m_Camera;
        private List<Effect> m_Effects;

        private void Awake()
        {
            m_Camera = Camera.main;
        }

        private void Start()
        {
            m_Effects = new List<Effect>();
        }

        private void Update()
        {
            var remainingEffects = new List<Effect>();
            foreach (var effect in m_Effects)
            {
                if (Time.time >= effect.ExpiryTime)
                {
                    effect.UndoAction();
                    continue;
                }

                remainingEffects.Add(effect);
            }

            m_Effects = remainingEffects;

            if (Input.GetKeyDown(KeyCode.A))
            {
                ReverseControls(5);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                FlipCamera(1);
            }
        }

        public void FlipCamera(float duration)
        {
            var effect = new Effect(duration, () =>
            {
                m_Camera.transform.eulerAngles = new Vector3(0, 0, 0);
            });

            m_Camera.transform.eulerAngles = new Vector3(0, 0, 180);
            m_Effects.Add(effect);
        }

        public void SpeedBoost(float duration, float speed)
        {
            var playerMovement = GameManager.Instance.PlayerMovement;
            playerMovement.ModifySpeed(speed);

            var effect = new Effect(duration, () =>
            {
                playerMovement.ModifySpeed(-speed);
            });
            
            m_Effects.Add(effect);
        }

        public void ReverseControls(float duration)
        {
            var playerMovement = GameManager.Instance.PlayerMovement;
            playerMovement.ReverseInput(true);

            var effect = new Effect(duration, () =>
            { 
                playerMovement.ReverseInput(false);
            });

            m_Effects.Add(effect);
        }
    }
}
