using Extensions;
using Player;
using UnityEngine;

namespace Objects.Obstacles
{
    internal sealed class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameObject m_Particles;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.transform.CompareTag("Player"))
                return; 
            
            //TODO: Come back to Camera shake :D
            //StartCoroutine(Camera.main.Shake(0.5f, 0.5f));
            Instantiate(m_Particles, transform.position, Quaternion.identity);
            col.GetComponent<HealthObject>().Damage();
            Destroy(gameObject);
        }
    }
}
