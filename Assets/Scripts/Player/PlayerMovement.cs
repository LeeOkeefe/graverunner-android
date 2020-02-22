using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal sealed class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D m_Rb;

        [SerializeField]
        [Range(0, 1)]
        private float m_LerpTime = 0.25f;
        private float m_CurrentTime;
        private float m_MaxHeightLimit;
        private float m_MinHeightLimit;
        private float m_MinHorizontalMovement = 0;
        private float m_MaxHorizontalMovement = 3;

        [SerializeField] private EnvironmentHandler m_Environment;

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody2D>();
            var startHeight = transform.position.y;
            m_MaxHeightLimit = startHeight + Vector3.up.y + 2;
            m_MinHeightLimit = startHeight;
        }

        /// <summary>
        /// Lerp one unit in the given direction
        /// </summary>
        public IEnumerator SetMovement(Vector3 direction)
        {
            if (direction == Vector3.zero || m_CurrentTime > 0)
                yield break;

            if (direction == Vector3.right && transform.position.x >= m_MaxHorizontalMovement
                || direction == Vector3.left && transform.position.x <= m_MinHorizontalMovement)
                yield break;

            if (direction == Vector3.up && transform.position.y >= m_MaxHeightLimit)
            {
                GameManager.Instance.MaxRestrictionLine.Play();
                yield break;
            }

            if (direction == Vector3.down && transform.position.y <= m_MinHeightLimit)
            {
                GameManager.Instance.MinRestrictionLine.Play();
                yield break;
            }
                
            var startPos = transform.position;
            var endPos = transform.position + direction;

            while (m_CurrentTime < m_LerpTime)
            {
                m_CurrentTime += Time.deltaTime;
                m_Rb.MovePosition(Vector3.Lerp(startPos, endPos, m_CurrentTime / m_LerpTime));
                yield return null;
            }
            m_CurrentTime = 0;
            CalculateSpeed();
        }

        private void CalculateSpeed()
        {
            var charHeight = GetCharacterGridHeight();

            switch (charHeight)
            {
                case CharacterGridHeight.Low:
                    m_Environment.SetEnvironmentSpeed(0.225f, 0.0225f);
                    break;
                case CharacterGridHeight.Middle:
                    m_Environment.SetEnvironmentSpeed(0.25f, 0.025f);
                    break;
                case CharacterGridHeight.High:
                    m_Environment.SetEnvironmentSpeed(0.275f, 0.0275f);
                    break;
                default:
                    m_Environment.SetEnvironmentSpeed(0.2f, 0.02f);
                    break;
            }
        }

        private CharacterGridHeight GetCharacterGridHeight()
        {
            switch (transform.position.y)
            {
                case -1:
                    return CharacterGridHeight.Low;
                case 0:
                    return CharacterGridHeight.Middle;
                case 1:
                    return CharacterGridHeight.High;
                default:
                    return CharacterGridHeight.Bottom;
            }
        }
    }

    public enum CharacterGridHeight
    {
        Bottom,
        Low,
        Middle,
        High
    }
}
