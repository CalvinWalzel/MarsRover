using MarsRover.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandFile = @"";

            if (args.Length != 0)
            {
                commandFile = args[0];
            }
            else
            {
                Console.WriteLine("No command file given. Please enter path to a command file:");
                commandFile = Console.ReadLine();
            }
            IParser parser = new Parser();
            var data = parser.Read(commandFile);

            IWorld world = new World(data);

            var result = world.Simulate();

            foreach(var line in result)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
