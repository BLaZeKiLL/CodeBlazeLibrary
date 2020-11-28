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
    }
}