using MarsRover.Enums;
using MarsRover.IO;
using MarsRover.Tests.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void ShouldParseCommandsFile()
        {
            IParser parser = new Parser();

            var result = parser.Read("commands.txt");

            var expected = new ParserResult
            {
                WorldWidth = 25,
                WorldHeight = 25,
                Rovers = new List<Rover>
                {
                    new Rover(1, 2, Orientation.North),
                    new Rover(3, 3, Orientation.East)
                }
            };

            expected.Rovers[0].AddCommands(new List<Command>
            {
                Command.TurnLeft,
                Command.MoveForward,
                Command.TurnRight
            });

            expected.Rovers[1].AddCommands(new List<Command>
            {
                Command.MoveForward,
                Command.TurnLeft,
                Command.TurnRight,
            });

            AssExt.AreEqualByJson(result, expected);
        }
    }
}