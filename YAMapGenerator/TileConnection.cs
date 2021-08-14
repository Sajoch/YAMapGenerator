using System;
using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class TileConnection {
        public TileScheme Tile { get; private set; }
        public List<TileScheme> Pattern { get; private set; }

        public TileConnection(Tile tile) {
            Tile = tile.Scheme;
            Pattern = tile.Neighbours.Select(a => a?.Scheme).ToList();
        }

        public bool IsMatch(TileConnection other) {
            if (Tile != other.Tile)
                return false;

            return Pattern.SequenceEqual(other.Pattern);
        }

        public bool Check(Tile tile) {
            var zipped = tile.Neighbours.Zip(Pattern);
            foreach (var (node, match) in zipped) {
                if (node != null && match == null)
                    return false;
                if (node != null && node.Scheme != null && node.Scheme != match)
                    return false;
            }
            return true;
        }

        public new string ToString() {
            return $"TileConnection[Id={Tile.Id}; List={string.Join(',', Pattern.Select(a => a?.Id))}]";
        }
    }
}
