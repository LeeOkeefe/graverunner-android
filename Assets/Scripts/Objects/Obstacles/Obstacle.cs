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

            var pos = transform.position;
            Camera.main.Shake();

            Instantiate(m_Particles, pos, Quaternion.identity);
            other.GetComponent<HealthObject>().Damage();

            var colliderDirection = other.GetColliderDirection(pos);
            other.GetComponent<PlayerMovement>().Rebound(colliderDirection);
            Destroy(gameObject);
        }
    }
}
