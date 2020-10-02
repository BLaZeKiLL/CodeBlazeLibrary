using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBlaze.Library.Math {

    public struct Range  {

        public float Min { get; set; }
        public float Max { get; set; }
        public float range => Max - Min + 1;
        public float random => UnityEngine.Random.Range(Min, Max);

        public bool IsValid() => Min < Max;

    }
    
    public static class Random {

        public static float Ranges(params Range[] ranges) {
            var validRanges = new List<Range>();
            float count = ranges.Sum(r => {
                if (!r.IsValid()) return 0;

                validRanges.Add(r);

                return r.range;
            });
            
            float x = UnityEngine.Random.Range(0f, count);
            float sum = 0;
            
            foreach (var range in validRanges) {
                sum += range.range;

                if (x <= sum) return range.random;
            }
            
            throw new Exception("This should never happen");
        }

    }

}