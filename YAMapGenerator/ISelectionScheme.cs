using System.Collections.Generic;

namespace YAMapGenerator {
    public interface ISelectionScheme<T> {
        T SelectOne(IEnumerable<T> collection);
    }
}
