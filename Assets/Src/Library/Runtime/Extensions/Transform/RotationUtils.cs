using System.Collections;

using UnityEngine;

namespace CodeBlaze.Library.Extensions.Transforms {

    public static class RotationUtils {

        public static IEnumerator RelativeRotateOverTime(
            this Transform transform, 
            Quaternion newRotation,
            float duration) 
            => AbsoluteRotateOverTime(transform, transform.rotation * newRotation, duration);

        public static IEnumerator AbsoluteRotateOverTime(
            this Transform transform, 
            Quaternion newRotation, 
            float duration) {
            var rotation = transform.rotation;
            var elapsed = 0.0f;

            while (elapsed < duration) {
                rotation = Quaternion.Lerp(rotation, newRotation, elapsed / duration);
                transform.rotation = rotation;
                elapsed += Time.fixedDeltaTime;
                yield return null;
            }
            
            transform.rotation = newRotation;
        }

    }

}