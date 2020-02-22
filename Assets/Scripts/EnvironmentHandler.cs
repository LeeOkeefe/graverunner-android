using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal sealed class EnvironmentHandler : MonoBehaviour
    {
        public float m_ParallaxSpeed = 0.15f;
        public float m_PrefabSpeed = 0.015f;

        private float m_CurrentTime;
        private float m_MaxTime = 5;

        public void SetEnvironmentSpeed(float parallax, float prefab)
        {
            var positive = parallax > m_ParallaxSpeed && prefab > m_PrefabSpeed;
            StartCoroutine(HandleSpeedChange(positive, parallax, prefab));
        }

        private IEnumerator HandleSpeedChange(bool positive, float parallax, float prefab)
        {
            m_CurrentTime = 0;

            if (positive)
            {
                while (m_CurrentTime < m_MaxTime)
                {
                    m_CurrentTime += Time.deltaTime;
                    LerpSpeed(parallax, prefab, 0);
                    yield return null;
                }
            }
            else
            {
                while (m_CurrentTime < m_MaxTime)
                {
                    m_CurrentTime += Time.deltaTime;
                    LerpSpeed(parallax, prefab, 2.5f);
                    yield return null;
                }
            }
        }

        private void LerpSpeed(float parallax, float prefab, float additionalTime)
        {
            m_ParallaxSpeed = Mathf.Lerp(m_ParallaxSpeed, parallax, m_CurrentTime / m_MaxTime + additionalTime);
            m_PrefabSpeed = Mathf.Lerp(m_PrefabSpeed, prefab, m_CurrentTime / m_MaxTime + additionalTime);
        }
    }
}
