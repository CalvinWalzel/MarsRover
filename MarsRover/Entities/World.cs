using System;
using System.Collections.Generic;
using MarsRover.Enums;
using MarsRover.IO;

namespace MarsRover
{
    public interface IWorld
    {
        IList<string> Simulate();
        IList<string> PrintStatus();
    }

    /// <summary>
    /// A world object. Represents the world, takes care of the simulation and contains all Rovers.
    /// </summary>
    public class World : IWorld
    {
        private int worldWidth;
        private int worldHeight;

        private readonly List<Rover> rovers;
        private int simulationCount = 0;

        public World(ParserResult result)
        {
            worldWidth = result.WorldWidth;
            worldHeight = result.WorldHeight;

            rovers = new List<Rover>();
            
            for(int i = 0; i < result.Rovers.Count; i++)
            {
                var rover = result.Rovers[i];

                // Determine the highest amount of commands for the simulation loop.
                if (rover.Commands.Count > simulationCount)
                    simulationCount = rover.Commands.Count;

                rovers.Add(rover);
            }
        }

        public IList<string> Simulate()
        {
            for(int i = 0; i < simulationCount; i++)
            {
                for(int e = 0; e < rovers.Count; e++)
                {
                    var rover = rovers[e];

                    // If the rover has no commands anymore, skip it.
                    if (rover.Commands.Count == 0)
                        continue;

                    var command = rover.Commands.Dequeue();

                    // If the rover would move out of boundaries, we throw an exception.
                    if (rover.X > worldWidth || rover.X < 0 || rover.Y > worldHeight || rover.Y < 0)
                        throw new Exception(string.Format("Invalid command. Rover would move out of boundaries. Rover {0}", e));
                    
                    rover.Execute(command);
                }
            }
            return PrintStatus();
        }

        /// <summary>
        /// Returns the current status of all Rovers as a string array.
        /// </summary>
        public IList<string> PrintStatus()
        {
            List<string> result = new List<string>();

            foreach(var rover in rovers)
            {
                result.Add(rover.ToString());
            }

            return result;
        }
    }
}
