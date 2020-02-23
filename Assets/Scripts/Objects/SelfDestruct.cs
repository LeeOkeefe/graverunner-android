using UnityEngine;

namespace Objects
{
    internal sealed class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float m_DestructionTimer = 5;

        private float m_Timer;

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_DestructionTimer)
            {
                Destroy(gameObject);
            }
        }
    }
}
