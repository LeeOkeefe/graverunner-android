using UnityEngine;

namespace Player
{
    internal sealed class HealthObject : MonoBehaviour
    {
        [SerializeField] [Range(1, 3)] private int m_LivesOnStart;
        [SerializeField] private GameObject m_DamageParticle;
        private SpriteRenderer m_SpriteRenderer;

        public HealthDefinition HealthDefinition;

        private bool m_IsInvincible;
        private Color m_InvincibilityColour;
        private Color m_NormalColour;

        private void Awake()
        {
            HealthDefinition = new HealthDefinition(m_LivesOnStart);
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            m_NormalColour = m_SpriteRenderer.color;
            m_InvincibilityColour = new Color(0.5f, 0.85f, 0.9f, 1);
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
            m_SpriteRenderer.color = m_IsInvincible ? m_InvincibilityColour : m_NormalColour;
        }
    }
}
