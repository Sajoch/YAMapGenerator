using System;
using System.Linq;
using YAMapGenerator;

namespace Test2
{
	class Program
	{
        //Trying to run test with random selector i get "testhost.exe' has exited with code -1073741819 (0xc0000005) 'Access violation'."
        static void Main(string[] args)
		{
            CheckValidTilesRandom("1,2,2,2,2,3;4,5,5,5,5,6;4,5,5,5,5,6;4,5,5,5,5,6;7,8,8,8,8,9");

        }

        static void CheckValidTilesRandom(string input) {
            var reader = new SimpleMapReader(input);
            var allTiles = reader.GetTileSchemes();
            var connections = new TileConnectionContainer();
            var basicMap = reader.CreateMap();
            connections.ReadFromMap(basicMap);
            var generator = new MapGenerator(connections, allTiles.ToList(), basicMap.Width, basicMap.Height, new GetRandomSelection<TileScheme>());

            var map = generator.Generate(5);

            Console.WriteLine(map.ToString());
            Console.WriteLine(connections.ToString());
        }
    }
}
