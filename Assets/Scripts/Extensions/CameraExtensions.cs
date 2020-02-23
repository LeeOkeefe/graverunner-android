using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    internal static class CameraExtensions
    {
        public static IEnumerator Shake(this Camera camera, float magnitude, float duration)
        {
            var cameraTransform = camera.transform;
            var orignalPosition = cameraTransform.position;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-1f, 1f) * magnitude;

                cameraTransform.position = new Vector3(x, y, -1f);
                elapsed += Time.deltaTime;
                yield return 0;
            }

            cameraTransform.position = orignalPosition;
        }
    }
}