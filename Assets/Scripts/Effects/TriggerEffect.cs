using UnityEngine;

namespace Effects
{
    internal sealed class TriggerEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip m_PowerUpAudioClip;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            GameManager.Instance.PlaySoundEffect(m_PowerUpAudioClip);
            var randomEffect = GameManager.Instance.EffectsManager.GetRandomEffect();
            randomEffect.Invoke();
            Destroy(gameObject);
        }
    }
}
