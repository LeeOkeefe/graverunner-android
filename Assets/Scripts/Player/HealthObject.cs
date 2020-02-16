﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    internal sealed class HealthObject : MonoBehaviour
    {
        [SerializeField] [Range(1, 3)] private int m_LivesOnStart;
        [SerializeField] private GameObject m_DamageParticle;

        public HealthDefinition HealthDefinition;

        private void Awake()
        {
            HealthDefinition = new HealthDefinition(m_LivesOnStart);
        }

        public void Damage()
        {
            Damage(HealthDefinition.MaxHealth);
        }

        public void Damage(int amount)
        {
            HealthDefinition.Damage(amount);

            if (HealthDefinition.IsDead)
            {
                Instantiate(m_DamageParticle, transform.position, Quaternion.identity);
            }

            if (HealthDefinition.Lives <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }

        public void Heal(int amount)
        {
            HealthDefinition.Heal(amount);
        }
    }
}