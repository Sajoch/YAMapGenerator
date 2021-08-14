using System;
using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class GetRandomSelection<T> : ISelectionScheme<T> {
        Random random = new Random();

        public T SelectOne(IEnumerable<T> list) {
            var index = random.Next(list.Count());
            return list.ElementAtOrDefault(index);
        }
    }
}
