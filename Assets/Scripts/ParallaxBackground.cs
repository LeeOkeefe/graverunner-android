using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float m_Speed = 0.25f;

        private Renderer m_Renderer;

        private void Start()
        {
            m_Renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            m_Renderer.material.mainTextureOffset = new Vector2(0, Time.time * m_Speed);
        }
    }
}
