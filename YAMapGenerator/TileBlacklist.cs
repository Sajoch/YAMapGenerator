using System.Collections.Generic;

namespace YAMapGenerator {
    public class TileBlacklist<T> {
        Dictionary<Position, List<T>> items = new Dictionary<Position, List<T>>();

        public void Add(Position position, T scheme) {
            if (items.TryGetValue(position, out List<T> list)) {
                list.Add(scheme);
            } else {
                items.Add(position, new List<T>() { scheme });
            }
        }

        public bool IsBlocked(Position position, T scheme) {
            if (!items.TryGetValue(position, out List<T> list))
                return false;

            return list.Contains(scheme);
        }
    }
}
