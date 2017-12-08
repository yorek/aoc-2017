using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Day8.Machinery;

namespace Day8
{
    class Program
    {        
        static void Main(string[] args)
        {
            Machine m = new Machine();

            var commands = ReadCommands("./input.txt");

            foreach (var command in commands)
            {
                m.AddCommand(command);
            }

            m.Run();

            // Part 1
            foreach (var r in m.Registers.OrderByDescending(i => i.Value))
            {
                Console.WriteLine($"{r.Key} = {r.Value}");
            }

            // Part 2
            Console.WriteLine($"MaxValue: {m.MaxValue}");
        }

        static List<string> ReadCommands(string file)
        {
            var result = new List<string>();

            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    result.Add(line);
                }
            }

            return result;
        }
    }
}
