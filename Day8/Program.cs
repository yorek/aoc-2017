using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    class Program
    {
        public enum OperationType
        {
            Inc,
            Dec
        }

        public enum ComparisonOperation
        {
            GreaterThan,
            LessThan,
            EqualTo,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo,
            NotEqual
        }

        public class Operation
        {
            public OperationType Type;
            public Action<string, int> Action;
        }

        public class Command
        {
            public string TargetRegister;

            public Operation Operation;

            public int Value;

            public string ConditionRegister;

            public ComparisonOperation Comparison;

            public int ComparisonValue;

            public void Execute()
            {
                Operation.Action(TargetRegister, Value);
            }
        }

        static Dictionary<string, int> registers = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            ReadCommands("./sample.txt");

            foreach(var r in registers)
            {
                Console.WriteLine($"{r.Key} = {r.Value}");
            }
        }

        static void ReadCommands(string file)
        {
            using(StreamReader sr = new StreamReader(file))
            {
                while(!sr.EndOfStream)
                {
                    string rawCommand = sr.ReadLine();

                    Command c = ParseRawCommand(rawCommand);

                    c.Execute();
                }
            }
        }

        static Command ParseRawCommand(string rawCommand)
        {
            var tokens = rawCommand.Split(' ');

            var c = new Command()
            {
                TargetRegister = tokens[0],
                Operation = DecodeOperationString(tokens[1]),
                Value = int.Parse(tokens[2]),
                ConditionRegister = tokens[4],
                Comparison = DecodeComparisonString(tokens[5]),
                ComparisonValue = int.Parse(tokens[6])
            };

            return c;
        }

        private static Operation DecodeOperationString(string v)
        {
            switch(v.ToLower())
            {
                case "inc": 
                    return new Operation()
                        {
                            Type = OperationType.Inc,
                            Action = (register, value) => { 
                                if (registers.ContainsKey(register))
                                    registers[register] += value; 
                                else    
                                    registers.Add(register, value);
                                }
                        };

                case "dec": 
                    return new Operation()
                        {
                            Type = OperationType.Inc,
                            Action = (register, value) => { 
                                if (registers.ContainsKey(register))
                                    registers[register] += value; 
                                else    
                                    registers.Add(register, value);
                                }
                        };

                default: throw new ApplicationException("Wrong operation type value");
            }
        }

        private static ComparisonOperation DecodeComparisonString(string v)
        {
            switch(v)
            {
                case ">": return ComparisonOperation.GreaterThan; 
                case "<": return ComparisonOperation.LessThan; 
                case ">=": return ComparisonOperation.GreaterThanOrEqualTo; 
                case "<=": return ComparisonOperation.LessThanOrEqualTo; 
                case "==": return ComparisonOperation.EqualTo;
                case "!=": return ComparisonOperation.NotEqual;
                default: throw new ApplicationException("Wrong comparison type value");
            }
                
        }
    }
}
