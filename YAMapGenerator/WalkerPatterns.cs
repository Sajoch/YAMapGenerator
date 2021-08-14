using System;
using System.Collections.Generic;

namespace YAMapGenerator {
    public class WalkerPatterns {
        public static Position[] Simple { get; } = new Position[]{
            new Position(-1, -1),
            new Position(-1, 0),
            new Position(-1, 1),
            new Position(0, -1),
            new Position(0, 1),
            new Position(1, -1),
            new Position(1, 0),
            new Position(1, 1),
        };

        public static IEnumerable<Position> ForEach(int maxX, int maxY) {
            for (int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++)
                    yield return new Position(x, y);

        }

        public static IEnumerable<KeyValuePair<Position, T>> ForEachList<T>(int maxX, int maxY, IEnumerable<T> list) {
            var handle = list.GetEnumerator();
            handle.MoveNext();
            for (int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++) {
                    yield return new KeyValuePair<Position, T>(new Position(x, y), handle.Current);
                    handle.MoveNext();
                }
        }
    }
}
