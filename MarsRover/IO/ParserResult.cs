using System.Collections.Generic;

namespace MarsRover.IO
{
    /// <summary>
    /// Represents the data parsed from a commands file.
    /// </summary>
    public class ParserResult
    {
        public int WorldWidth;
        public int WorldHeight;

        public List<Rover> Rovers;

        public ParserResult()
        {
            Rovers = new List<Rover>();
        }
    }
}
