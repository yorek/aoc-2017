using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day7
{    
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            var input = GetInputData("./input.txt");

            // Find path lengths
            Dictionary<string, int> lengths = new Dictionary<string, int>();

            foreach(var i in input.Keys)
            {
                int l = Traverse(input, i);

                lengths.Add(i, l);
                //Console.WriteLine($"{i}: {l}");
            }

            // Get longest path
            var root = lengths.OrderByDescending(d => d.Value).First();

            Console.WriteLine($"{root.Key}: {root.Value}");
        }

        static int Traverse(Dictionary<string, List<string>> tree, string start)
        {            
            int l = 0;

            if (!tree.ContainsKey(start)) return l;

            foreach(string e in tree[start])
            {
                l += Traverse(tree, e);
            }

            return l+1;
        }

        static Dictionary<string, List<string>> GetInputData(string file)
        {            
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            using (StreamReader sr = new StreamReader(file)) 
            {
                while(sr.EndOfStream == false)
                {
                    string row  =  sr.ReadLine();

                    string[] v1 = row.Split("->");

                    string programName = v1[0].Split(" ")[0].Trim();

                    string[] relatedPrograms = new string[] {};
                    if (v1.Length > 1)
                    {
                       string[] v2 = v1[1].Split(",");
                       relatedPrograms = v2.Select(s => { return s.Trim(); }).ToArray();
                    }

                    //Console.WriteLine($"{programName} ==> {string.Join(",", relatedPrograms)}");
                    result.Add(programName, relatedPrograms.ToList());
                }
            }

            return result;
        }

    }
}
