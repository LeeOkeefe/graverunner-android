using UnityEngine;

namespace Objects
{
    internal sealed class DestroyOffScreen : MonoBehaviour
    {
        [SerializeField] private Camera m_MainCamera;
        [SerializeField] private Vector3 m_Top;
        private void Awake()
        {
            m_MainCamera = Camera.main;
            m_Top = GetComponent<SpriteRenderer>().bounds.max;
        }

        private void Update()
        {
            var pos = m_MainCamera.WorldToViewportPoint(m_Top);

            if (m_MainCamera.transform.rotation.eulerAngles.z < 179f && pos.y < -0.2f)
            {
                Destroy(gameObject);
            }

            else if (m_MainCamera.transform.rotation.eulerAngles.z > 179f && pos.y > 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
