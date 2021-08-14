using System.Collections.Generic;

namespace YAMapGenerator {
    public class Tile {
        public Position Position { get; set; }
        public TileScheme Scheme { get; set; }
        public List<Tile> Neighbours { get; private set; }

        public Tile(int x, int y) {
            Position = new Position(x, y);
        }

        public void SetScheme(TileScheme scheme) {
            Scheme = scheme;
        }

        public void SetNeighbours(List<Tile> tiles) {
            Neighbours = tiles;
        }
    }
}
