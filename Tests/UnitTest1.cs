using YAMapGenerator;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests {
    public class TileConnectionContainerTests {
        [TestCase("1,2,3;4,5,6;7,8,9", 9, 9)]
        [TestCase("1,2,2,3;4,5,5,6;7,8,8,9", 9, 12)]
        [TestCase("1,2,2,2,3;4,5,5,5,6;7,8,8,8,9", 9, 15)]
        [TestCase("1,2,2,2,2,3;4,5,5,5,5,6;7,8,8,8,8,9", 9, 15)]
        [TestCase("1,2,2,2,2,3;4,5,5,5,5,6;4,5,5,5,5,6;4,5,5,5,5,6;7,8,8,8,8,9", 9, 25)]
        public void ShouldHaveValidConnectionAmount(string input, int expectedNodes, int expectedConnections) {
            var reader = new SimpleMapReader(input);
            var map = reader.CreateMap();
            var allTiles = reader.GetTileSchemes();
            var connections = new TileConnectionContainer();

            connections.ReadFromMap(map);
            Console.WriteLine(map.ToString());

            Assert.That(allTiles.Count(), Is.EqualTo(expectedNodes));
            Assert.That(connections.Count, Is.EqualTo(expectedConnections));
        }

        [TestCase("1,2,3;4,5,6;7,8,9")]
        [TestCase("1,2,2,3;4,5,5,6;7,8,8,9")]
        [TestCase("1,2,2,2,2,3;4,5,5,5,5,6;4,5,5,5,5,6;4,5,5,5,5,6;7,8,8,8,8,9")]
        public void CheckValidTiles(string input) {
            var reader = new SimpleMapReader(input);
            var allTiles = reader.GetTileSchemes();
            var connections = new TileConnectionContainer();
            var basicMap = reader.CreateMap();
            connections.ReadFromMap(basicMap);
            var generator = new MapGenerator(connections, allTiles.ToList());

            var map = generator.Generate(basicMap.Width, basicMap.Height);

            Console.WriteLine(map.ToString());
            Console.WriteLine(connections.ToString());
            Assert.That(map.IsValid(), Is.True);
        }
    }
}
