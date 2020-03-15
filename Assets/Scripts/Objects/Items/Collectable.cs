using UnityEngine;

namespace Objects.Items
{
    internal sealed class Collectable : MonoBehaviour
    {
        [SerializeField] private GameObject m_Particles;
        [SerializeField] private AudioClip m_CollectCoinAudioClip;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player"))
                return;
        
            GameManager.Instance.PlaySoundEffect(m_CollectCoinAudioClip);
            Instantiate(m_Particles, transform.position, Quaternion.identity);
            GameManager.Instance.ScoreManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
