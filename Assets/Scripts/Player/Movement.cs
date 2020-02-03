using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    internal sealed class Movement : MonoBehaviour
    {
        private Rigidbody2D m_Rb;

        [SerializeField] private Slider m_Slider;
        [SerializeField] private float m_SpeedX;

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody2D>();
            m_Slider.value = 0.5f;
        }

        private void Update()
        {
            Controls();
        }

        private void Controls()
        {
            if (m_Slider.value <= 0)
            {
                m_Rb.AddForce(new Vector2(-m_SpeedX, 0.015f));
                m_Slider.value = 0.5f;
            }
            else if (m_Slider.value >= 1)
            {
                m_Rb.AddForce(new Vector2(m_SpeedX, 0.015f));
                m_Slider.value = 0.5f;
            }
            else
            {
                transform.Translate(Vector2.up * 0.015f);
            }
        }
    }
}
