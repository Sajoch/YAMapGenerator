using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class TileConnectionContainer {
        List<TileConnection> items = new List<TileConnection>();

        public int Count => items.Count;

        public void ReadFromMap(Map map) {
            foreach (var tile in map.Collection) {
                var connection = new TileConnection(tile);
                var found = items.Find(a => a.IsMatch(connection));
                if (found == null)
                    items.Add(connection);
            }
        }

        public IEnumerable<TileConnection> Get(TileScheme scheme) {
            return items.Where(a => a.Tile == scheme);
        }

        public new string ToString() {
            return $"TileConnectionContainer[{string.Join("\n", items.Select(a => a.ToString()))}]";
        }
    }
}
