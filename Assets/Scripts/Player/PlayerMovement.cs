using System.Collections;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1)]
        private float m_LerpTime = 0.25f;
        private float m_CurrentTime;
        private float m_FurthestDistance;
        private float m_MinimumOffsetY = 3;
        private float m_MinHorizontalMovement = 0;
        private float m_MaxHorizontalMovement = 3;

        private Vector3 targetPos;

        private void Start()
        {
            var startHeight = transform.position.y;
            m_FurthestDistance = startHeight;
            targetPos = transform.position;
        }

        private void Update()
        {
            var myPos = transform.position;
            var newPos = new Vector3(myPos.x, myPos.y + (1 * Time.deltaTime), myPos.z);
            transform.Translate(newPos - myPos);
            if (newPos.y > m_FurthestDistance)
            {
                m_FurthestDistance = myPos.y;
            }
        }

        /// <summary>
        /// Lerp one unit in the given direction
        /// </summary>
        public IEnumerator HandleSwipeGesture(Vector3 direction)
        {
            print("HANDLING SWIPE GESTURE :D");
            print("DIRECTION IS: " + direction);
            var targetPos = transform.position + direction;

            print("TARGET IS " + targetPos);

            if (direction == Vector3.zero || m_CurrentTime > 0)
                yield break;

            if (targetPos.x > m_MaxHorizontalMovement || targetPos.x < m_MinHorizontalMovement)
            {
                print("TOO FAR LEFT OR RIGHT");
                yield break;
            }
            
            if (targetPos.y < m_FurthestDistance - m_MinimumOffsetY)
            {
                print("TOO FAR DOWN");

                GameManager.Instance.MinRestrictionLine.Play();
                yield break;
            }

            while (m_CurrentTime < m_LerpTime)
            {
                m_CurrentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPos, m_CurrentTime / m_LerpTime);
                yield return null;
            }
            m_CurrentTime = 0;
        }
    }
}
