using Assets.Scripts.Score;
using UnityEngine;

namespace Assets.Scripts.Objects.Items
{
    internal sealed class Collectable : ScrollableObject
    {
        [SerializeField] private GameObject m_Particles;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.transform.CompareTag("Player"))
                return;

            Instantiate(m_Particles, transform.position, Quaternion.identity);
            ScoreManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
