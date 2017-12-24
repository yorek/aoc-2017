using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] jumps1 = GetInputData();
            int stepsPart1 = Resolve(jumps1, offset => { return 1; });
            Console.WriteLine($"Part 1 -> Jumps: {stepsPart1}");

            int[] jumps2 = GetInputData();
            int stepsPart2 = Resolve(jumps2, offset => { return offset >= 3 ? -1 : 1; });
            Console.WriteLine($"Part 2 -> Jumps: {stepsPart2}");
        }

        static int Resolve(int[] jumps, Func<int, int> offsetEvaluator)
        {
            int offset = 0;
            int steps = 1;
            while(true)
            {
                // Get current value in the jump list                
                int value = jumps[offset];

                // Increase it by 1
                int newValue = value + offsetEvaluator(value);
            
                // Updated the value in the current position with the new value
                jumps[offset] = newValue;

                // Move using the read value
                offset = offset + value;

                if ((offset >= jumps.Length) || offset < 0) break;

                steps += 1;
            }

            //Console.WriteLine(string.Join(" ", jumps));
            return steps;
        }       

        static int[] GetSampleData()
        {
            return new int[] { 0, 3, 0, 1, -3 };
        }

        static int[] GetInputData()
        {            
            using (StreamReader sr = new StreamReader("./input.txt")) 
            {
                string inputData =  sr.ReadToEnd();

                string[] values = inputData.Split(Environment.NewLine);
                return values.Select(v => { return int.Parse(v); }).ToArray();
            }
        }
    }
}
