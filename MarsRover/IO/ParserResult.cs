using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.IO
{
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
