using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class GetFirstSelection<T> : ISelectionScheme<T> {
        public T SelectOne(IEnumerable<T> list) {
            return list.FirstOrDefault();
        }
    }
}
