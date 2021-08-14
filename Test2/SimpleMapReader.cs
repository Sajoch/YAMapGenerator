using YAMapGenerator;
using System;
using System.Collections.Generic;

namespace Test2 {
    public class SimpleMapReader {
        Dictionary<int, TileScheme> tiles = new Dictionary<int, TileScheme>();
        List<TileScheme> items = new List<TileScheme>();
        int width;
        int height;

        public SimpleMapReader(string input) {
            int y = 0;
            int x = 0;
            var rows = input.Split(';');
            height = rows.Length;
            foreach (var row in rows) {
                var line = row.Split(',');
                foreach (var name in line) {
                    var id = Int32.Parse(name);
                    var scheme = CreateOrGetTileScheme(id);
                    items.Add(scheme);
                    x++;
                }
                y++;
                width = x;
                x = 0;
            }
        }

        public Map CreateMap() {
            var map = new Map(width, height);
            map.Init(items);
            return map;
        }

        public IEnumerable<TileScheme> GetTileSchemes() {
            return tiles.Values;
        }

        TileScheme CreateOrGetTileScheme(int id) {
            if (tiles.TryGetValue(id, out TileScheme output))
                return output;
            var tile = new TileScheme() { Id = id };
            tiles.Add(id, tile);
            return tile;
        }
    }
}
