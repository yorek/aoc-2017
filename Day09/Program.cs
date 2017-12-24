using System;
using System.IO;

namespace Day9
{
    class StreamParser
    {
        private string Stream;

        public int CleanedCharacters { get; private set; }
        
        public int Score { get; private set; }
        
        public StreamParser(string stream)
        {
            Stream = stream;
            CleanedCharacters = 0;
        }

        public int Parse()
        {
            int groups = 0;
            int score = 0;

            bool ignoreNext = false;
            bool garbage = false;

            foreach(char c in Stream) 
            {
                if (ignoreNext == true) {
                    ignoreNext = false;
                    continue;
                }

                if (c == '>') {
                    garbage = false;
                    continue;
                }

                if (garbage == true)
                {
                    if (c == '!') {
                        ignoreNext = true;
                    } else 
                    {
                        CleanedCharacters += 1;
                    }
                    continue;
                }

                if (c == '{') groups += 1;
                
                if (c == '}') {
                    score += groups;
                    groups -= 1;
                }

                if (c == '<') garbage = true;
            }
        
            Score = score;
            return score;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] samples1 = new string[] {
                "{}",
                "{{{}}}",
                "{{},{}}",
                "{{{},{},{{}}}}",
                "{<a>,<a>,<a>,<a>}",
                "{{<ab>},{<ab>},{<ab>},{<ab>}}",
                "{{<!!>},{<!!>},{<!!>},{<!!>}}",
                "{{<a!>},{<a!>},{<a!>},{<ab>}}"
            };

            Console.WriteLine("Part 1 Checks");
            foreach(var s in samples1)
            {
                var sp1 = new StreamParser(s);
                Console.WriteLine($"{s}: {sp1.Parse()}");
            }
            Console.WriteLine();

             string[] samples2 = new string[] {
                "<>",
                "<random characters>",
                "<<<<>",
                "<{!>}>",
                "<!!>",
                "<!!!>>",
                "<{o\"i!a,<{i<a>",
            };

            Console.WriteLine("Part 2 Checks");
            foreach(var s in samples2)
            {
                var sp2 = new StreamParser(s);
                sp2.Parse();
                Console.WriteLine($"{s}: {sp2.CleanedCharacters}");
            }
            Console.WriteLine();
            
            string input = File.ReadAllText("./input.txt");
            var sp = new StreamParser(input);
            sp.Parse();
            Console.WriteLine($"Score: {sp.Score}");       
            Console.WriteLine($"CleanedCharacters: {sp.CleanedCharacters}");       
        }
    }
}
