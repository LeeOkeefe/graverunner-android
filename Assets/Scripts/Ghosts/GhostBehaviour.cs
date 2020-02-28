using Extensions;
using Player;
using UnityEngine;

namespace Ghosts
{
    internal sealed class GhostBehaviour : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;
        private float m_MinHorizontalMovement = 0;
        private float m_MaxHorizontalMovement = 3;

        [SerializeField] private float m_MoveSpeed = 1f;

        private Vector3 m_TargetPos;
        private Collider2D m_Collider;

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            var pos = transform.position;
            m_TargetPos = pos.x > 1 ? new Vector3(m_MaxHorizontalMovement, pos.y, pos.z) : new Vector3(m_MinHorizontalMovement, pos.y, pos.z);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_MoveSpeed * Time.deltaTime);
            GetTargetPosition();
        }

        /// <summary>
        /// Sets the target position to the min or max,
        /// and flips the ghost sprite depending on the direction
        /// </summary>
        private void GetTargetPosition()
        {
            if (transform.position.x >= m_MaxHorizontalMovement)
            {
                m_TargetPos = new Vector3(m_MinHorizontalMovement, m_TargetPos.y, m_TargetPos.z);
                m_SpriteRenderer.flipX = false;
            }
            if (transform.position.x <= m_MinHorizontalMovement)
            {
                m_TargetPos = new Vector3(m_MaxHorizontalMovement, m_TargetPos.y, m_TargetPos.z);
                m_SpriteRenderer.flipX = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ghost"))
            {
                var pos = transform.position;
                if (m_Collider.GetColliderDirection(other.transform.position) == Vector3.right)
                {
                    m_TargetPos = new Vector3(m_MaxHorizontalMovement, pos.y, pos.z);
                    m_SpriteRenderer.flipX = true;
                }
                else
                {
                    m_TargetPos = new Vector3(m_MinHorizontalMovement, pos.y, pos.z);
                    m_SpriteRenderer.flipX = false;
                }
            }

            if (!other.CompareTag("Player"))
                return;

            other.GetComponent<HealthObject>().Damage();

            var colliderDirection = other.GetColliderDirection(transform.position);
            other.GetComponent<PlayerMovement>().Rebound(colliderDirection);
        }
    }
}
