using UnityEngine;

namespace Assets.Scripts.Extensions
{
    internal static class CameraExtensions
    {
        public static void Shake(this Camera camera)
        {
            camera.GetComponent<Animation>().Play();
        }
    }
}