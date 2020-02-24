using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    internal sealed class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector3 m_InputDirection;
        private Vector3 m_TouchPosition;
        private PlayerMovement m_PlayerMovement;

        private void Start()
        {
            m_PlayerMovement = GameManager.Instance.PlayerMovement;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_TouchPosition = Input.mousePosition;
        }

        // Calculate swipe direction for PlayerMovement
        //
        public void OnPointerUp(PointerEventData eventData)
        {
            var deltaSwipe = m_TouchPosition - Input.mousePosition;

            if (Mathf.Abs(deltaSwipe.x) > 100)
            {
                m_InputDirection = deltaSwipe.x < 0 ? Vector3.right : Vector3.left;
            }
            else if (Mathf.Abs(deltaSwipe.y) > 100)
            {
                m_InputDirection = deltaSwipe.y < 0 ? Vector3.up : Vector3.down;
            }

            m_PlayerMovement.HandleSwipeGesture(m_InputDirection);
        }
    }
}