using MarsRover;
using MarsRover.IO;
using MarsRover.Tests.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class WorldTest
    {
        [Test]
        public void ShouldSimulate()
        {
            IParser parser = new Parser();
            var data = parser.Read("Data/full.txt");

            IWorld world = new World(data);

            var result = world.Simulate();

            var expected = new List<string>
            {
                "1 3 N",
                "5 1 E"
            };

            AssExt.AreEqualByJson(result, expected);
        } 
    }
}
