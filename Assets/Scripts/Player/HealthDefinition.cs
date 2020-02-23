using System;

namespace Player
{
    internal sealed class HealthDefinition
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public int Lives { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public HealthDefinition()
        {
            CurrentHealth = 100;
            MaxHealth = 100;
            Lives = 1;

            ClampHealth();
        }

        public HealthDefinition(int lives)
        {
            CurrentHealth = 100;
            MaxHealth = 100;
            Lives = lives;

            if (Lives > 3)
                throw new ArgumentException($"Maximum 3 lives. Lives was: {lives}");

            if (Lives < 0)
                throw new ArgumentException($"Cannot have negative lives. Lives was: {lives}");

            ClampHealth();
        }

        public void Damage(int amount)
        {
            CurrentHealth -= amount;
            ClampHealth();

            if (IsDead)
                Lives--;

            GameManager.Instance.LivesUI.UpdateLives();
        }

        public void Heal(int amount)
        {
            if (IsDead)
                return;

            CurrentHealth += amount;
            ClampHealth();
        }

        /// <summary>
        /// Clamps the health between 0 and <see cref="MaxHealth"/>
        /// </summary>
        private void ClampHealth()
        {
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }
    }
}
