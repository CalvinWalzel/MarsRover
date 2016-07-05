using MarsRover.Enums;
using System.Collections.Generic;

namespace MarsRover
{
    public interface IRover
    {
        void AddCommands(IList<Command> commands);
        void Execute(Command command);
    }

    public class Rover : IRover
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Orientation Orientation { get; private set; }
        public Queue<Command> Commands { get; }

        public Rover (int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Commands = new Queue<Command>();
        }

        /// <summary>
        /// Add a batch of commands to the queue.
        /// </summary>
        public void AddCommands(IList<Command> commands)
        {
            foreach(var command in commands)
            {
                Commands.Enqueue(command);
            }
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        public void Execute(Command command)
        {
            switch(command)
            {
                case Command.MoveForward:

                    if (Orientation == Orientation.North)
                        Y++;
                    else if (Orientation == Orientation.East)
                        X++;
                    else if (Orientation == Orientation.South)
                        Y--;
                    else
                        X--;

                    break;
                case Command.TurnLeft:

                    if ((int)Orientation == 0)
                        Orientation = (Orientation)3;
                    else
                        Orientation = (Orientation)((int)Orientation - 1);

                    break;
                case Command.TurnRight:

                    if ((int)Orientation == 3)
                        Orientation = (Orientation)0;
                    else
                        Orientation = (Orientation)((int)Orientation + 1);

                    break;
            }
        }

        /// <summary>
        /// Prints the status of the Rover as a string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X, Y, Orientation.ToFriendlyString());
        }
    }
}
