using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInputData();
            //Part1(input);
            Part2(input);
        }

        static List<int> GetSampleData()
        {
            return new List<int> { 0, 2, 7, 0 };
        }

        static List<int> GetInputData()
        {
            return new List<int> { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };
        }

        static void Part1(List<int> input)
        {
            List<string> seen = new List<string>();
            int r = 1;
            while(true)
            {
                int m = input.Max(v => v);
                int pm = input.IndexOf(m);

                Console.WriteLine($"{m}@{pm}");

                // spread the values
                input[pm] = 0;
                int p = pm;
                for(int c=0; c<m; c++)            
                {
                    p += 1;
                    if (p >= input.Count()) p = 0;
                    input[p] += 1;
                };

                string state = string.Join('|', input);
                Console.WriteLine(state);
                if (seen.Contains(state)) break;
                seen.Add(state);
                r += 1;
            }
            Console.WriteLine($"Redistribution Cycles: {r}");
        }

        static void Part2(List<int> input)
        {
            Dictionary<string, int> seen = new Dictionary<string, int>();
            int r = 1;
            string state = string.Empty;
            while(true)
            {
                int m = input.Max(v => v);
                int pm = input.IndexOf(m);

                Console.WriteLine($"{m}@{pm}");

                // spread the values
                input[pm] = 0;
                int p = pm;
                for(int c=0; c<m; c++)            
                {
                    p += 1;
                    if (p >= input.Count()) p = 0;
                    input[p] += 1;
                };

                state = string.Join('|', input);
                Console.WriteLine(state);
                if (seen.ContainsKey(state)) break;
                seen.Add(state, r);
                r += 1;
            }
            Console.WriteLine($"Loop size: {r - seen[state]}");
        }
    }
}
