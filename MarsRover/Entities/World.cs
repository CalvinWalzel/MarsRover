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

                    if (rover.Commands.Count == 0)
                        continue;

                    var command = rover.Commands.Dequeue();

                    if (rover.X > worldWidth || rover.X < 0 || rover.Y > worldHeight || rover.Y < 0)
                        throw new Exception(string.Format("Invalid command. Rover would move out of boundaries. Rover {0}", e));
                    
                    rover.Execute(command);
                }
            }
            return PrintStatus();
        }

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
