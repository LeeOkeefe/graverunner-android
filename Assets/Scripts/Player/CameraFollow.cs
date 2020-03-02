using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private float m_MovementSpeed = 5f;
        [SerializeField] private float m_OffsetY = 3;

        private void Update()
        {
            var pos = transform.position;
            var targetPosition = new Vector3(pos.x, m_Player.position.y + m_OffsetY, pos.z);

            var distance = Vector3.Distance(pos, targetPosition);

            m_MovementSpeed = distance <= 1 ? 5f : 15f;

            transform.position = Vector3.MoveTowards(pos, targetPosition, m_MovementSpeed * Time.deltaTime);
        }
    }
}
