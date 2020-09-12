using System;
using System.Collections;
using System.Threading;

using UniRx;

using UnityEngine;

namespace CodeBlaze.Extensions.UniRx {

    public static class RotationUtils {
        
        public static IEnumerator RelativeRotateOverTime(
            this Transform transform, 
            Quaternion newRotation,
            float duration, 
            IObserver<Unit> observer, 
            CancellationToken token) 
            => AbsoluteRotateOverTime(transform, transform.rotation * newRotation, duration, observer, token);

        public static IEnumerator AbsoluteRotateOverTime(
            this Transform transform, 
            Quaternion newRotation, 
            float duration, 
            IObserver<Unit> observer, 
            CancellationToken token) {
            
            var rotation = transform.rotation;
            var elapsed = 0.0f;
            
            if (token.IsCancellationRequested) yield break;
            
            while (elapsed < duration) {
                rotation = Quaternion.Lerp(rotation, newRotation, elapsed / duration);
                transform.rotation = rotation;
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = newRotation;
            observer.OnNext(new Unit());
            observer.OnCompleted();
        }

    }

}