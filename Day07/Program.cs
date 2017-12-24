using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day7
{
    public class TreeItem
    {
        public string Name;

        public int Weight;

        public int TotalWeight;

        public int Level;

        public TreeItem Parent = null;

        public List<TreeItem> Children = new List<TreeItem>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Load tree
            var root = GetInputData("./input.txt");

            // Part 1
            Console.WriteLine($"Root: {root} L: {root.Level} W: {root.TotalWeight}");         

            // Part 1
            TraverseWrite(root);
        }

        static void TraverseWrite(TreeItem item)
        {
            foreach(var c in item.Children)
            {            
                Console.WriteLine(new string('\t', c.Level) + $"Item: {c} L: {c.Level} W: {c.Weight} TW: {c.TotalWeight}");     
                TraverseWrite(c);
            }  
        }

        static TreeItem GetInputData(string file)
        {
            Dictionary<string, TreeItem> programs = new Dictionary<string, TreeItem>();

            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.EndOfStream == false)
                {
                    string row = sr.ReadLine();

                    string[] programInfo = row.Split("->");

                    string programName = programInfo[0].Split(" ")[0].Trim();
                    string programWeight = programInfo[0].Split(" ")[1].Trim(new char[] { '(', ')', ' ' });

                    TreeItem i = new TreeItem
                    {
                        Name = programName,
                        Weight = int.Parse(programWeight)
                    };

                    programs.Add(programName, i);
                    //Console.WriteLine($"{programName}: {programWeight}");                  
                }
            }

            TreeItem root = null;

            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.EndOfStream == false)
                {
                    string row = sr.ReadLine();

                    string[] programInfo = row.Split("->");

                    string programName = programInfo[0].Split(" ")[0].Trim();

                    if (programInfo.Count()> 1)
                    {
                        var relatedPrograms = programInfo[1].Split(",").Select(s => { return s.Trim(); });

                        foreach (var rp in relatedPrograms)
                        {
                            TreeItem p = programs[programName];
                            TreeItem c = programs[rp];
                            p.Children.Add(c);
                            c.Parent = p;
                        }
                    }
                }
            }

            // foreach (var p in programs)
            // {
            //     Console.WriteLine($"{p.Value.Name} -> P: {p.Value.Parent?.Name} C:{p.Value.Children.Count}");
            // }

            root = programs.Values.Where(i => i.Parent == null).First();

            TraverseTreeAndUpdateItems(root);

            return root;
        }

        static void TraverseTreeAndUpdateItems(TreeItem item, int level=0)
        {
            if (item.Parent != null) item.Level += level;

            item.TotalWeight = item.Weight;

            foreach(var c in item.Children)
            {                
                TraverseTreeAndUpdateItems(c, level+1);
            
                item.TotalWeight += c.TotalWeight;
            }        
        }
    }
}
