using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day8.Machinery
{
    public class Machine
    {
        private class ComparisonOperation
        {
            public string Type;

            public Func<string, int, bool> Evaluate;
        }


        private class Operation
        {
            public string Type;
            public Func<string, int, int> Evaluate;
        }

        private class Command
        {
            public string TargetRegister;

            public Operation Operation;

            public int Value;

            public string ConditionRegister;

            public ComparisonOperation Comparison;

            public int ComparisonValue;

            public int? Execute()
            {
                int? result = null;
                if (Comparison.Evaluate(ConditionRegister, ComparisonValue))
                {
                    result = Operation.Evaluate(TargetRegister, Value);
                }            

                return result; 
            }
        }

        private List<Command> Commands = new List<Command>();

        public Dictionary<string, int> Registers = new Dictionary<string, int>();

        public int MaxValue = 0;

        public void AddCommand(string command)
        {
            var tokens = command.Split(' ');

            var c = new Command()
            {
                TargetRegister = tokens[0],
                Operation = DecodeOperationString(tokens[1]),
                Value = int.Parse(tokens[2]),
                ConditionRegister = tokens[4],
                Comparison = DecodeComparisonString(tokens[5]),
                ComparisonValue = int.Parse(tokens[6])
            };

            Commands.Add(c);
        }

        public void Run()
        {
            foreach (var c in Commands)
            {
                int? result = c.Execute();

                if (result.HasValue)
                {
                    if (result.Value > MaxValue) MaxValue = result.Value;
                }
            }
        }

        private Operation DecodeOperationString(string v)
        {
            switch (v.ToLower())
            {
                case "inc":
                    return new Operation()
                    {
                        Type = "+",
                        Evaluate = (register, value) =>
                        {
                            if (Registers.ContainsKey(register))
                                Registers[register] += value;
                            else
                                Registers.Add(register, value);
                            
                            return Registers[register];
                        }
                    };

                case "dec":
                    return new Operation()
                    {
                        Type = "-",
                        Evaluate = (register, value) =>
                        {
                            if (Registers.ContainsKey(register))
                                Registers[register] -= value;
                            else
                                Registers.Add(register, -value);

                            return Registers[register];
                        }
                    };

                default: throw new ApplicationException("Wrong operation type value");
            }
        }

        private ComparisonOperation DecodeComparisonString(string v)
        {
            switch (v)
            {
                case ">":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv > value;
                        }
                    };

                case ">=":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv >= value;
                        }
                    };

                case "<":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv < value;
                        }
                    };

                case "<=":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv <= value;
                        }
                    };

                case "==":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv == value;
                        }
                    };

                case "!=":
                    return new ComparisonOperation()
                    {
                        Type = v,
                        Evaluate = (register, value) =>
                        {
                            if (!Registers.ContainsKey(register)) Registers.Add(register, 0);
                            int rv = Registers[register];
                            return rv != value;
                        }
                    };

                default: throw new ApplicationException("Wrong comparison type value");
            }

        }
    }
}