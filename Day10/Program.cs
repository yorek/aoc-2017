using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        public class KnotHash
        {
            public List<int> Values { get; }
            public List<int> Lengths { get; }

            public KnotHash(string inputValues, string inputLenghts)
            {
                this.Lengths = new List<int>();
                this.Values = new List<int>();

                this.Lengths.AddRange(
                    Array.ConvertAll(
                        inputLenghts.Split(',', StringSplitOptions.RemoveEmptyEntries), 
                        i => int.Parse(i)
                    )
                );

                this.Values.AddRange(
                    Array.ConvertAll(
                        inputValues.Split(',', StringSplitOptions.RemoveEmptyEntries), 
                        i => int.Parse(i)
                    )
                );
            }

            public int Execute(int rounds)
            {
                int s = 0;
                int p = 0;

                for(int r=0; r<rounds; r++)
                {
                    foreach(int l in Lengths)
                    {
                        // Get sublist and reverse it
                        var a = GetSublist(p, l);                    
                        Array.Reverse(a);

                        // Replace sublist
                        ReplaceSublist(p, a);

                        // new position
                        p += l + s;

                        while (p >= Values.Count) 
                        {
                            p -= Values.Count;
                        }

                        // increase skip size
                        s += 1;

                        //Console.WriteLine(string.Join(',', Values.ToArray()));
                    }
                }
                return Values[0] * Values[1];
            }

            public string CalculateHash()
            {
                Execute(64);
                StringBuilder sb = new StringBuilder();

                for(int b=0; b<16; b++)
                {
                    int s = (b * 16);
                    
                    var block = Values.GetRange(s, 16);
                    int h = 0;
                    foreach(var v in block)
                    {
                        h = h ^ v;
                    }

                    sb.Append(h.ToString("x2"));
                }

                return sb.ToString();
            }           

            private int[] GetSublist(int startFrom, int Length)
            {
                List<int> result = new List<int>();
                int e = 0;
                
                while( e < Length )
                {
                    int p = startFrom + e;

                    while (p >= Values.Count) 
                    {
                        p -= Values.Count;
                    }

                    result.Add(Values[p]);
                    e++;
                }

                return result.ToArray();
            }

            private void ReplaceSublist(int startFrom, int[] sublist)
            {
                int e = 0;
                foreach(int i in sublist)                        
                {
                    int p = e + startFrom;
                    
                    while (p >= Values.Count) 
                    {
                        p -= Values.Count;
                    }

                    Values.RemoveAt(p);
                    Values.Insert(p, sublist[e]);
                    e += 1;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("SAMPLE");
            var khs = new KnotHash("0,1,2,3,4", "3,4,1,5");
            Console.WriteLine($"Final value: {khs.Execute(1)}");
            Console.WriteLine();
            
            Console.WriteLine("CHALLENGE - PART 1");
            string inputValues = string.Join(',', Enumerable.Range(0, 256).ToArray());
            string inputLengths = "106,118,236,1,130,0,235,254,59,205,2,87,129,25,255,118";
            var khc = new KnotHash(inputValues, inputLengths);
            Console.WriteLine($"Final value: {khc.Execute(1)}");
            Console.WriteLine();

            Console.WriteLine("CHALLENGE - PART 2");     
            //inputLengths = "";       
            string inputLengths2 = string.Join(',', Encoding.ASCII.GetBytes(inputLengths)) + ",17,31,73,47,23";
            var khc2 = new KnotHash(inputValues, inputLengths2);
            Console.WriteLine($"Final value: {khc2.CalculateHash()}");
            Console.WriteLine();
            
        }
    }
}
