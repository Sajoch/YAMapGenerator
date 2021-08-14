using System;
using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class Map {
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Tile> Collection => nodes.Values;
        Dictionary<Position, Tile> nodes = new Dictionary<Position, Tile>();

        public Map(int maxX, int maxY) {
            Width = maxX;
            Height = maxY;

            var list = WalkerPatterns.ForEach(Width, Height);
            foreach (var pos in list) {
                nodes.Add(pos, new Tile(pos.X, pos.Y));
            }
            foreach (var node in nodes.Values) {
                var neighbour = FindNeighbours(node);
                node.SetNeighbours(neighbour.ToList());
            }
        }

        public void Init(List<TileScheme> tiles) {
            var list = WalkerPatterns.ForEachList(Width, Height, tiles);
            foreach (var pair in list) {
                nodes[pair.Key].SetScheme(pair.Value);
            }
        }

        public Tile FindByPosition(int x, int y) {
            var position = new Position(x, y);
            return nodes[position];
        }

        public Tile FindByPosition(Position position) {
            return nodes[position];
        }

        public bool IsValid() {
            return !nodes.Values.Any(a => a.Scheme == null);
        }

        public new string ToString() {
            return string.Join(";", ListRows().ToList());
        }

        IEnumerable<string> ListRows() {
            for (int y = 0; y < Height; y++) {
                yield return string.Join(",", ListRow(y).ToList());
            }
        }

        IEnumerable<int> ListRow(int y) {
            for (int x = 0; x < Width; x++) {
                yield return nodes[new Position(x, y)].Scheme?.Id ?? 0;
            }
        }

        IEnumerable<Tile> FindNeighbours(Tile node) {
            foreach (var offset in WalkerPatterns.Simple) {
                var pos = node.Position + offset;
                if (nodes.TryGetValue(pos, out Tile neighbour)) {
                    yield return neighbour;
                } else {
                    yield return null;
                }
            }
        }
    }
}
