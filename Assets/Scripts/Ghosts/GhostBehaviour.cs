using System;
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

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            var pos = transform.position;
            m_TargetPos = new Vector3(3, pos.y, pos.z);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_MoveSpeed * Time.deltaTime);
            GetTargetPosition();
        }

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
            if (!other.CompareTag("Player"))
                return;

            other.GetComponent<HealthObject>().Damage();
            other.GetComponent<PlayerMovement>().Rebound();
        }
    }
}
