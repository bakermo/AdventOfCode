using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day24 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var switches = new Dictionary<string, int>();
            bool processingInitialStates = true;
            var switchQueue = new Queue<Switch>();
            foreach (var line in input)
            {
                if (processingInitialStates)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        processingInitialStates = false;
                        continue;
                    }

                    var parts = line.Split(": ");
                    switches.Add(parts[0], int.Parse(parts[1]));
                }
                else
                {
                    var parts = line.Split("->");
                    var inputs = parts[0].Split(" ");
                    var sw = new Switch()
                    {
                        Left = inputs[0].Trim(),
                        Operator = inputs[1].Trim(),
                        Right = inputs[2].Trim(),
                        Output = parts[1].Trim()
                    };
                    switchQueue.Enqueue(sw);
                }
            }

            //foreach (var sw in switches)
            //{
            //    Console.WriteLine($"{sw.Key}: {sw.Value}");
            //}

            while (switchQueue.Count > 0)
            {
                var sw = switchQueue.Dequeue();
                if (!switches.ContainsKey(sw.Left) || !switches.ContainsKey(sw.Right))
                {
                    switchQueue.Enqueue(sw);
                    continue;
                }

                if (!switches.ContainsKey(sw.Output))
                {
                    switches.Add(sw.Output, 0);
                }

                var left = switches[sw.Left];
                var right = switches[sw.Right];
                if (sw.Operator == "AND")
                {
                    if (left == 1 && right == 1)
                    {
                        switches[sw.Output] = 1;
                    }
                    else
                    {
                        switches[sw.Output] = 0;
                    }
                }
                else if (sw.Operator == "OR")
                {
                    if (left == 1 || right == 1)
                    {
                        switches[sw.Output] = 1;
                    }
                    else
                    {
                        switches[sw.Output] = 0;
                    }
                }
                else if (sw.Operator == "XOR")
                {
                    if (left != right)
                    {
                        switches[sw.Output] = 1;
                    }
                    else
                    {
                        switches[sw.Output] = 0;
                    }
                }
            }

            string binary = string.Empty;
            foreach (var sw in switches.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{sw.Key}: {sw.Value}");
                if (sw.Key.StartsWith("z"))
                {
                    binary = sw.Value.ToString() + binary;
                }
            }
            Console.WriteLine("binary: " + binary);
            var result = Convert.ToInt64(binary, 2);

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            return input[0].Length.ToString();
        }

        private class Switch
        {
            public string Left { get; set; } = string.Empty;
            public string Right { get; set; } = string.Empty;
            public string Operator { get; set; } = string.Empty;
            public string Output { get; set; } = string.Empty;
        }
    }
}
