using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class MapGenerator {
        private readonly TileConnectionContainer connections;
        private readonly List<TileScheme> tiles;

        public MapGenerator(TileConnectionContainer connections, List<TileScheme> tiles) {
            this.connections = connections;
            this.tiles = tiles;
        }

        public Map Generate(int maxx, int maxy) {
            var map = new Map(maxx, maxy);
            EvaluateGeneration(map);
            return map;
        }

        void EvaluateGeneration(Map map) {
            var list = WalkerPatterns.ForEach(map.Width, map.Height);
            foreach (var pos in list) {
                var node = map.FindByPosition(pos);
                var availableTiles = GetAvailableTiles(node);
                var randomTile = availableTiles.FirstOrDefault();
                if (randomTile != null)
                    node.SetScheme(randomTile);
            }
        }

        IEnumerable<TileScheme> GetAvailableTiles(Tile node) {
            return tiles.Where(tile => connections.Check(tile, node));
        }
    }
}
