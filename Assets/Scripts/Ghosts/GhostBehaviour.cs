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
        private Animator m_Anim;
        private bool m_IsMoving = true;

        [SerializeField] private AudioClip m_GhostAudioClip;

        private static readonly int Death = Animator.StringToHash("Death");

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Collider = GetComponent<Collider2D>();
            m_Anim = GetComponent<Animator>();
        }

        private void Start()
        {
            var pos = transform.position;
            m_TargetPos = pos.x > 1 ? new Vector3(m_MaxHorizontalMovement, pos.y, pos.z) : new Vector3(m_MinHorizontalMovement, pos.y, pos.z);
        }

        private void Update()
        {
            if (!m_IsMoving)
                return;

            transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_MoveSpeed * Time.deltaTime);
            ChangeTargetPosition();
        }

        private void ChangeTargetPosition()
        {
            if (transform.position.x >= m_MaxHorizontalMovement)
            {
                ChangeTargetPositionX(m_MinHorizontalMovement);
            }

            if (transform.position.x <= m_MinHorizontalMovement)
            {
                ChangeTargetPositionX(m_MaxHorizontalMovement);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ghost"))
            {
                ChangeTargetPositionX(m_Collider.GetColliderDirection(other.transform.position) == Vector3.right
                    ? m_MaxHorizontalMovement
                    : m_MinHorizontalMovement);
            }

            if (other.CompareTag("Player"))
            {
                var hitDirection = other.GetColliderDirection(transform.position);
                var health = other.GetComponent<HealthObject>();
                var movement = other.GetComponent<PlayerMovement>();

                health.Damage();
                movement.Rebound(hitDirection);
                Camera.main.Shake();
                DeactivateGhostOnHit();
            }
        }

        /// <summary>
        /// Change the target direction of the X-axis,
        /// flip the sprite depending on the direction
        /// </summary>
        private void ChangeTargetPositionX(float x)
        {
            m_TargetPos = new Vector3(x, m_TargetPos.y, m_TargetPos.z);
            m_SpriteRenderer.flipX = !(m_TargetPos.x < 2);
        }

        private void DeactivateGhostOnHit()
        {
            m_IsMoving = false;
            m_Anim.SetTrigger(Death);
        }

        // Public method used as an Animation Event to destroy
        // the gameObject once the death animation has finished
        //
        public void DestroyGhostAnimEvent()
        {
            Destroy(gameObject);
        }
    }
}
