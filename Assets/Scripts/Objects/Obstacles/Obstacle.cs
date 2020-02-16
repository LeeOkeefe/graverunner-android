using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Objects.Obstacles
{
    internal sealed class Obstacle : ScrollableObject
    {
        [SerializeField] private GameObject m_Particles;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.transform.CompareTag("Player"))
                return;

            Instantiate(m_Particles, transform.position, Quaternion.identity);
            col.GetComponent<HealthObject>().Damage();
            Destroy(gameObject);
        }
    }
}
