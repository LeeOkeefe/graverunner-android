using UnityEngine;

namespace Extensions
{
    internal static class ColliderExtensions
    {
        /// <summary>
        /// Returns which side of the trigger was hit
        /// </summary>
        public static Vector3 GetColliderDirection(this Collider2D collider, Vector3 otherPosition)
        {
            var direction = collider.transform.position - otherPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? Vector3.right : Vector3.left;
            }

            return direction.y > 0 ? Vector3.up : Vector3.down;
        }

        /// <summary>
        /// Returns which side of the collider was hit
        /// </summary>
        public static Vector3 GetCollisionDirection(this Collision2D collider, Vector3 otherPosition)
        {
            var direction = collider.transform.position - otherPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? Vector3.right : Vector3.left;
            }

            return direction.y > 0 ? Vector3.up : Vector3.down;
        }
    }
}