using UnityEngine;

namespace Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private float m_LerpTime = 1;
        [SerializeField] private float m_OffsetY = 2;

        private void Update()
        {
            var pos = transform.position;
            var targetPosition = new Vector3(pos.x, m_Player.position.y + m_OffsetY, pos.z);
            transform.position = Vector3.Lerp(pos, targetPosition, m_LerpTime * Time.deltaTime);
        }
    }
}
