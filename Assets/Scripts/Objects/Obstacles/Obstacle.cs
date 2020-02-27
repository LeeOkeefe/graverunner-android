using Extensions;
using Player;
using UnityEngine;

namespace Objects.Obstacles
{
    internal sealed class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameObject m_Particles;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            Camera.main.Shake();

            Instantiate(m_Particles, transform.position, Quaternion.identity);
            other.GetComponent<HealthObject>().Damage();
            other.GetComponent<PlayerMovement>().Rebound();
            Destroy(gameObject);
        }
    }
}
