using UnityEngine;

namespace Player
{
    internal sealed class HealthObject : MonoBehaviour
    {
        [SerializeField] [Range(1, 3)] private int m_LivesOnStart;
        [SerializeField] private GameObject m_DamageParticle;

        public HealthDefinition HealthDefinition;

        private bool m_IsInvincible;

        private void Awake()
        {
            HealthDefinition = new HealthDefinition(m_LivesOnStart);
        }

        public void Damage()
        {
            if (m_IsInvincible)
                return;

            Damage(HealthDefinition.MaxHealth);
        }

        public void Damage(int amount)
        {
            if (m_IsInvincible)
                return;

            HealthDefinition.Damage(amount);

            if (HealthDefinition.IsDead)
            {
                HandleDeathFeedback();
            }

            if (HealthDefinition.Lives <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }

        private void HandleDeathFeedback()
        {
            GetComponent<Animation>().Play();
            Instantiate(m_DamageParticle, transform.position, Quaternion.identity);
        }

        public void Heal(int amount)
        {
            HealthDefinition.Heal(amount);
        }

        public void SetInvincibility(bool isInvincible)
        {
            m_IsInvincible = isInvincible;
        }
    }
}
