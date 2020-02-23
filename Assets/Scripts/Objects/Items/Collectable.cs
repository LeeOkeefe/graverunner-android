using UnityEngine;

namespace Objects.Items
{
    internal sealed class Collectable : MonoBehaviour
    {
        [SerializeField] private GameObject m_Particles;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.transform.CompareTag("Player"))
                return;

            Instantiate(m_Particles, transform.position, Quaternion.identity);
            GameManager.Instance.ScoreManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
