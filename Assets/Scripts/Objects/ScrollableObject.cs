using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    internal abstract class ScrollableObject : MonoBehaviour
    {
        [SerializeField][Range(0, 0.1f)] 
        private float m_Speed = 0.015f;

        private void Start()
        {
            StartCoroutine(HandleScrollingObject());
        }

        /// <summary>
        /// Scrolls game object down the screen, destroys when out of view
        /// </summary>
        protected IEnumerator HandleScrollingObject()
        {
            while (!HasScrolledOutOfView())
            {
                transform.Translate(Vector2.down * m_Speed);
                yield return null;
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Checks if the game object has scrolled out of the screen view
        /// </summary>
        private bool HasScrolledOutOfView()
        {
            return transform.position.y < GetBottomScreenCoordinates().y;
        }

        /// <summary>
        /// Calculates the coordinates of the bottom corners of the screen
        /// </summary>
        private static Vector2 GetBottomScreenCoordinates()
        {
            var bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
            var bottomRow = bottomRight + bottomLeft;

            return bottomRow / 2;
        }
    }
}
