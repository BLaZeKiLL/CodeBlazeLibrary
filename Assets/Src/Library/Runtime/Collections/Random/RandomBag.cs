using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Random {

    public class RandomBag<T> {

        private List<T> _list;
        private List<T> _bag;

        public RandomBag(IEnumerable<T> elements) {
            _list = new List<T>(elements);
            _bag = new List<T>(_list);
        }

        public T GetItem() {
            if (_bag.Count == 0) {
                _bag.AddRange(_list);
            }
            
            var index = UnityEngine.Random.Range(0, _bag.Count);
            var obj = _bag[index];
            
            _bag.RemoveAt(index);

            return obj;
        }

    }

}