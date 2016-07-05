using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRover.IO
{
    public interface IParser {
        ParserResult Read(string path);
    }

    /// <summary>
    /// Parses data from a commands file and serializes these into objects (a ParserResult).
    /// </summary>
    public class Parser : IParser
    {
        private ParserResult result;

        public Parser()
        {
            result = new ParserResult();
        }

        public ParserResult Read(string path)
        {
            StreamReader file;

            int counter = 1;
            string line;
            Rover rover = null;

            using (file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    // If line is empty, null or whitespace, skip it. After that, trim all leading and trailing whitespaces.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    line = line.Trim();

                    // If the separator parameter is null, white-space characters are assumed to be the delimiters.
                    var lineProperties = line.Split(null);

                    if (counter == 1)
                    {
                        CreateWorld(lineProperties);
                        counter++;
                        continue;
                    }

                    // Even line is a rover declaration.
                    if((counter % 2) == 0)
                    {
                        rover = CreateRover(lineProperties);
                    }

                    // Uneven line is a series of commands.
                    if((counter % 2) == 1)
                    {
                        var commands = CreateRoverCommands(lineProperties[0]);
                        rover.AddCommands(commands);

                        result.Rovers.Add(rover);
                    }

                    counter++;
                }
            }

            return result;
        }

        private void CreateWorld(string[] line)
        {
            result.WorldWidth = Convert.ToInt32(line[0]);
            result.WorldHeight = Convert.ToInt32(line[1]);
        }

        private Rover CreateRover(string[] line)
        {
            if (line.Length != 3)
                throw new Exception("Invalid declaration for rover " + result.Rovers.Count + 1);

            Orientation orientation;

            int x = Convert.ToInt32(line[0]);
            int y = Convert.ToInt32(line[1]);

            // Check if position is outside of world boundaries.
            if (x > result.WorldWidth)
                throw new Exception("X coordinate is outside of world for rover " + result.Rovers.Count + 1);
            if (y > result.WorldHeight)
                throw new Exception("Y coordinate is outside of world for rover " + result.Rovers.Count + 1);

            // Determine orientation.
            switch (line[2])
            {
                case "N":
                    orientation = Orientation.North;
                    break;
                case "E":
                    orientation = Orientation.East;
                    break;
                case "S":
                    orientation = Orientation.South;
                    break;
                case "W":
                    orientation = Orientation.West;
                    break;
                default:
                    throw new Exception("Invalid orientation given for rover " + result.Rovers.Count + 1);
            }

            return new Rover(x, y, orientation);
        }

        private IList<Command> CreateRoverCommands(string line)
        {
            var commands = new List<Command>();

            // If no commands, throw an exception.
            if (line == null)
                throw new Exception("Invalid or no commands for rover " + result.Rovers.Count + 1);

            // Determine command.
            foreach (char character in line)
            {
                Command command;

                switch(character)
                {
                    case 'L':
                        command = Command.TurnLeft;
                        break;
                    case 'R':
                        command = Command.TurnRight;
                        break;
                    case 'M':
                        command = Command.MoveForward;
                        break;
                    default:
                        throw new Exception("Invalid command given for rover " + result.Rovers.Count + 1 + ": " + character);
                }

                commands.Add(command);
            }

            return commands;
        }
    }
}
