using System;
using System.Collections.Generic;
using System.IO;

namespace Day23
{
    class Program
    {
        private static Dictionary<string, int> _registers = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            var file = File.ReadAllText("./input.txt");
            var instructions = file.Split(Environment.NewLine);

            int mul = 0;

            int ptr = 0;
            int ctr = 0;

            _registers.Add("a", 1);
            _registers.Add("b", 0);
            _registers.Add("c", 0);
            _registers.Add("d", 0);
            _registers.Add("e", 0);
            _registers.Add("f", 0);
            _registers.Add("g", 0);
            _registers.Add("h", 0);

            while (true)
            {
                var line = instructions[ptr].ToLower().Split(" ");
                var cmd = line[0].Trim();
                var par1 = line[1].Trim();
                var par2 = line[2].Trim();

                Console.SetCursorPosition(0, 0);
                foreach (var register in _registers)
                {
                    Console.WriteLine($"{register.Key}:\t{register.Value}".PadRight(30));
                }
                Console.WriteLine($"{ptr}\t{instructions[ptr].ToLower()}".PadRight(30));
                Console.WriteLine($"{ctr}".PadRight(30));                

                switch (cmd)
                {
                    case "set":
                        if (_registers.ContainsKey(par1))
                            _registers[par1] = GetValue(par2);
                        else
                            _registers.Add(par1, GetValue(par2));

                        ptr += 1; 
                        break;

                    case "sub":
                        if (_registers.ContainsKey(par1))
                            _registers[par1] -= GetValue(par2);
                        else
                            _registers.Add(par1, -GetValue(par2));

                        ptr += 1;
                        break;

                    case "mul":
                        mul += 1;
                        if (_registers.ContainsKey(par1))
                            _registers[par1] *= GetValue(par2);
                        else
                            _registers.Add(par1, 0);

                        ptr += 1;
                        break;

                    case "jnz":
                        int nz = GetValue(par1);
                        if (nz != 0)
                            ptr += GetValue(par2);
                        else                            
                            ptr += 1;                            
                        break;

                    default:
                        throw new ApplicationException($"Unknown command: {cmd}");                        
                }
                Console.ReadLine();
                ctr += 1;
                if (ptr < 0 || ptr >= instructions.Length) break;
            }

            Console.WriteLine($"MUL: {mul}");            
            Console.ReadLine();
        }

        public static int GetValue(string valueOrRegister)
        {
            int val = 0;
            if (_registers.ContainsKey(valueOrRegister))
                val = _registers[valueOrRegister];
            else
                val = int.Parse(valueOrRegister);

            return val;
        }
        
    }
}
