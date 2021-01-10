using UnityEngine;

namespace CodeBlaze.Library.Extensions
{
    public static class Utils
    {
        /// <summary>
        /// Destroy all children of transform 
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }

            return transform;
        }

        /// <summary>
        /// Destroy all children of type T
        /// </summary>
        /// <param name="transform"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Transform Clear<T>(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<T>() != null)
                {
                    Object.Destroy(child.gameObject);
                }
            }

            return transform;
        }
    }
}