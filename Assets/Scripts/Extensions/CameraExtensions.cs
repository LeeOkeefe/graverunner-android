using UnityEngine;

namespace Extensions
{
    internal static class CameraExtensions
    {
        public static void Shake(this Camera camera)
        {
            camera.GetComponentInParent<Animation>().Play();
        }
    }
}