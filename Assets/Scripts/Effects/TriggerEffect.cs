using UnityEngine;

namespace Effects
{
    internal sealed class TriggerEffect : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            var randomEffect = GameManager.Instance.EffectsManager.GetRandomEffect();
            randomEffect.Invoke();
            Destroy(gameObject);
        }
    }
}
