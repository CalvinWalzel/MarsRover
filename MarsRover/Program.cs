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

            // If the commands file is given as an argument, use it automatically. Else show a prompt.
            if (args.Length != 0)
            {
                commandFile = args[0];
            }
            else
            {
                Console.Write("No command file given. Please enter path to a command file: ");
                commandFile = Console.ReadLine();
            }

            try
            {
                // Parse all data from the commands file.
                IParser parser = new Parser();
                var data = parser.Read(commandFile);

                // Create a world from the data
                IWorld world = new World(data);

                // Simulate the world and return the result
                var result = world.Simulate();

                foreach (var line in result)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
