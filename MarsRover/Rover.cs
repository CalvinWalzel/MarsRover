using MarsRover.Enums;
using System.Collections.Generic;

namespace MarsRover
{
    public interface IRover
    {
        void AddCommand(Command command);
    }

    public class Rover : IRover
    {
        public int X { get; }
        public int Y { get; }
        public Orientation Orientation { get; }
        public List<Command> Commands { get; }

        public Rover (int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Commands = new List<Command>();
        }

        public void AddCommand(Command command)
        {
            Commands.Add(command);
        }

        public void AddCommands(IList<Command> commands)
        {
            Commands.AddRange(commands);
        }
    }
}
