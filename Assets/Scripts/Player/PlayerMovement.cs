using System.Collections;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerMovement : MonoBehaviour
    {
        private float m_CurrentTime;
        private float m_FurthestDistance;
        private float m_MinimumOffsetY = 3;
        private float m_MinHorizontalMovement = 0;
        private float m_MaxHorizontalMovement = 3;

        private Vector3 m_TargetPos;
        [SerializeField] private float m_LerpSpeed = 6;
        [SerializeField] private float m_MoveSpeed = 1;

        private void Start()
        {
            var startHeight = transform.position.y;
            m_FurthestDistance = startHeight;
            m_TargetPos = transform.position;
        }

        private void Update()
        {
            var myPos = transform.position;
            var newPos = new Vector3(myPos.x, myPos.y + m_MoveSpeed * Time.deltaTime, myPos.z);

            m_TargetPos = new Vector3(m_TargetPos.x, m_TargetPos.y + m_MoveSpeed * Time.deltaTime, m_TargetPos.z);
            transform.Translate(newPos - myPos);
            
            if (newPos.y > m_FurthestDistance)
            {
                m_FurthestDistance = myPos.y;
            }

            if (transform.position != m_TargetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_LerpSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Change target position based on direction of swipe
        /// </summary>
        public void HandleSwipeGesture(Vector3 direction)
        {
            var targetPos = m_TargetPos + direction;

            if (direction == Vector3.zero || m_CurrentTime > 0)
                return;

            if (targetPos.x > m_MaxHorizontalMovement || targetPos.x < m_MinHorizontalMovement)
                return;
            
            if (targetPos.y < m_FurthestDistance - m_MinimumOffsetY)
            {
                GameManager.Instance.MinRestrictionLine.Play();
                return;
            }

            m_TargetPos = targetPos;
        }
    }
}
