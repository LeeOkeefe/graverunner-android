using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Effects
{
    internal sealed class EffectsManager : MonoBehaviour
    {
        private Camera m_Camera;
        private List<Action> m_Effects;
        private List<Effect> m_ActiveEffects;

        [SerializeField] private Image m_FlipCameraIcon;
        [SerializeField] private Image m_SpeedBoostIcon;
        [SerializeField] private Image m_ReversedControlsIcon;
        [SerializeField] private Image m_InvincibilityIcon;

        [SerializeField] private EffectsUI m_EffectsUI;

        private Random m_Random;

        private void Awake()
        {
            m_Camera = Camera.main;
        }

        private void Start()
        {
            m_Random = new Random();
            m_ActiveEffects = new List<Effect>();

            m_Effects = new List<Action>
            {
                () => FlipCamera(2),
                () => ReverseControls(3),
                () => SpeedBoost(1.5f, 5),
                () => TemporaryInvincibility(5)
            };
        }

        private void Update()
        {
            var remainingEffects = new List<Effect>();
            foreach (var effect in m_ActiveEffects)
            {
                if (Time.time >= effect.ExpiryTime)
                {
                    effect.UndoAction();
                    continue;
                }

                remainingEffects.Add(effect);
            }

            m_ActiveEffects = remainingEffects;
        }

        public Action GetRandomEffect()
        {
            var randomValue = m_Random.Next(m_Effects.Count);
            return m_Effects[randomValue];
        }

        /// <summary>
        /// Flips the camera upside down for the duration
        /// </summary>
        public void FlipCamera(float duration)
        {
            m_Camera.transform.eulerAngles = new Vector3(0, 0, 180);
            m_EffectsUI.UpdateSlot(m_FlipCameraIcon, true);

            var effect = new Effect(duration, () =>
            {
                m_Camera.transform.eulerAngles = new Vector3(0, 0, 0);
                m_EffectsUI.UpdateSlot(m_FlipCameraIcon, false);
            });

            m_ActiveEffects.Add(effect);
        }

        /// <summary>
        /// Increases the player movement speed for the duration
        /// </summary>
        public void SpeedBoost(float duration, float speed)
        {
            var playerMovement = GameManager.Instance.PlayerMovement;
            playerMovement.ModifyMoveSpeed(speed);
            m_EffectsUI.UpdateSlot(m_SpeedBoostIcon, true);

            var effect = new Effect(duration, () =>
            {
                playerMovement.ModifyMoveSpeed(-speed);
                m_EffectsUI.UpdateSlot(m_SpeedBoostIcon, false);
            });

            m_ActiveEffects.Add(effect);
        }

        /// <summary>
        /// Reverses the input controls for the duration
        /// </summary>
        public void ReverseControls(float duration)
        {
            var playerMovement = GameManager.Instance.PlayerMovement;
            playerMovement.ReverseInput(true);
            m_EffectsUI.UpdateSlot(m_ReversedControlsIcon, true);

            var effect = new Effect(duration, () =>
            {
                playerMovement.ReverseInput(false);
                m_EffectsUI.UpdateSlot(m_ReversedControlsIcon, false);
            });

            m_ActiveEffects.Add(effect);
        }

        /// <summary>
        /// Invincibility for the player health object for the set duration
        /// </summary>
        public void TemporaryInvincibility(float duration)
        {
            var healthObject = GameManager.Instance.HealthObject;
            healthObject.SetInvincibility(true);
            m_EffectsUI.UpdateSlot(m_InvincibilityIcon, true);

            var effect = new Effect(duration, () =>
            {
                healthObject.SetInvincibility(false);
                m_EffectsUI.UpdateSlot(m_InvincibilityIcon, false);
            });

            m_ActiveEffects.Add(effect);
        }
    }
}
