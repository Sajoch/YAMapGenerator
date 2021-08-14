using System;
using System.Collections.Generic;
using System.Linq;

namespace YAMapGenerator {
    public class MapGenerator {
        private readonly TileConnectionContainer connections;
        private readonly List<TileScheme> tiles;
        private readonly ISelectionScheme<TileScheme> selection;
        private readonly TileBlacklist<TileScheme> blacklist = new TileBlacklist<TileScheme>();
        List<Tile> emptyNodes = new List<Tile>();
        Map map;

        public MapGenerator(TileConnectionContainer connections, List<TileScheme> tiles, int width, int height, ISelectionScheme<TileScheme> selection) {
            this.connections = connections;
            this.tiles = tiles;
            this.selection = selection;
            map = new Map(width, height);
        }

        public Map Generate(int limit) {
            int attempt = 0;
            while (!map.IsValid() && attempt < limit) {
                EvaluateGeneration();
                attempt++;
            }
            return map;
        }

        void EvaluateGeneration() {
            emptyNodes.Clear();
            var list = WalkerPatterns.ForEach(map.Width, map.Height);
            foreach (var pos in list) {
                var node = map.FindByPosition(pos);
                GenerateForNode(node);
            }
        }

        void GenerateForNode(Tile node) {
            var availableTiles = GetAvailableTiles(node);
            var tile = selection.SelectOne(availableTiles);
            if (tile == null) {
                RegenerateTheWeakest(node);
                GenerateForNode(node);
                return;
            }
            node.SetScheme(tile);
        }

        void RegenerateTheWeakest(Tile node) {
            var best = GetTilesWithScore(node)
                .OrderByDescending(a => a.Key)
                .Select(a => a.Value)
                .FirstOrDefault();
            RegenerateNotMatching(node, best);
        }

        IEnumerable<TileScheme> GetAvailableTiles(Tile node) {
            return tiles
                .Where(tile => Check(tile, node))
                .Where(tile => !blacklist.IsBlocked(node.Position, tile));
        }

        IEnumerable<KeyValuePair<int, TileConnection>> GetTilesWithScore(Tile node) {
            foreach (var tile in tiles) {
                var availableConnections = connections.Get(tile);
                foreach (var connection in availableConnections) {
                    var score = connection.GetScore(node);
                    yield return new KeyValuePair<int, TileConnection>(score, connection);
                }
            }
        }

        void RegenerateNotMatching(Tile baseNode, TileConnection connection) {
            var zipped = baseNode.Neighbours.Zip(connection.Pattern);
            foreach (var (node, match) in zipped) {
                if (node != null && node.Scheme != null && node.Scheme != match) {
                    blacklist.Add(node.Position, node.Scheme);
                    node.SetScheme(null);
                    Console.WriteLine($"Discard for {node.Position.ToString()}");
                    GenerateForNode(node);
                }
            }
        }

        bool Check(TileScheme scheme, Tile node) {
            var availableConnections = connections.Get(scheme);
            foreach (var connection in availableConnections) {
                if (connection.Check(node))
                    return true;
            }
            return false;
        }
    }
}
