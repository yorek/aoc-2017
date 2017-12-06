using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static int validSequences = 0;

        static void Main(string[] args)
        {      
            var part1sample = SplitData(GetSampleDataPart1());
            var part2sample = SplitData(GetSampleDataPart2());
            var input = SplitData(GetInputData());

            validSequences = 0;
            Parallel.ForEach(part1sample, l => Part1(l)); 
            Console.WriteLine($"Part 1, Sample => Valid Sequences: {validSequences}");

            validSequences = 0;
            Parallel.ForEach(input, l => Part1(l)); 
            Console.WriteLine($"Part 1, Input  => Valid Sequences: {validSequences}");

            validSequences = 0;
            Parallel.ForEach(part2sample, l => Part2(l)); 
            Console.WriteLine($"Part 2, Sample => Valid Sequences: {validSequences}");

            validSequences = 0;
            Parallel.ForEach(input, l => Part2(l)); 
            Console.WriteLine($"Part 2, Input  => Valid Sequences: {validSequences}");
        }

        static string GetSampleDataPart1()
        {
            string words = @"
            aa bb cc dd ee
            aa bb cc dd aa
            aa bb cc dd aaa
            ";

            return words;
        }

        static string GetSampleDataPart2()
        {
            string words = @"
            abcde fghij
            abcde xyz ecdab
            a ab abc abd abf abj
            iiii oiii ooii oooi oooo
            oiii ioii iioi iiio
            ";

            return words;
        }

        static string GetInputData()
        {
            using (StreamReader sr = new StreamReader("./input.txt")) {
                return sr.ReadToEnd();
            }
        }

        static List<List<string>> SplitData(string value)
        {
            List<List<string>> input = new List<List<string>>();

            string[] lines = value.Split(Environment.NewLine);            
            foreach(var l in lines.Where(l => l.Trim().Length > 0))
            {
                var wordList = new List<string>();                
                wordList.AddRange(l.Split(" ").Where(i => i.Trim().Length > 0));
                input.Add(wordList);
            }

            return input;
        }

        static void Part1(List<string> wordList)
        {
            bool isValid = true;

            foreach(var w in wordList)
            {
                var r = wordList.FindAll(s => s == w);
                if (r.Count() > 1) { 
                    isValid = false;
                    break;
                }
            }

            //Console.WriteLine($"{index}: {isValid}");
            if (isValid) Interlocked.Add(ref validSequences, 1);
        }

        static void Part2(List<string> wordList)
        {
            bool isValid = true;

            var newWordList = new List<string>();
            foreach(var w in wordList)
            {
                char[] ws = w.ToCharArray();
                Array.Sort(ws);
                newWordList.Add(new string(ws));
            }

            foreach(var nw in newWordList)
            {                
                var r = newWordList.FindAll(s => s == nw);
                if (r.Count() > 1) { 
                    isValid = false;
                    break;
                }
            }

            //Console.WriteLine($"{index}: {isValid}");
            if (isValid) Interlocked.Add(ref validSequences, 1);
        }
    }
}
