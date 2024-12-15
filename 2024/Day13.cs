using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day13 : IDaySolver
    {
        public string Part1(string[] input)
        {
            double cost = 0;
            var seen = new HashSet<(long, long)>();
            for (int i = 0; i < input.Length; i+=4)
            {
                var buttonAX = GetValue(input[i], @"X\+(\d+)");
                var buttonAY = GetValue(input[i], @"Y\+(\d+)");

                var buttonBX = GetValue(input[i + 1], @"X\+(\d+)");
                var buttonBY = GetValue(input[i + 1], @"Y\+(\d+)");

                var targetX = GetValue(input[i + 2], @"X=(\d+)");
                var targetY = GetValue(input[i + 2], @"Y=(\d+)");
                Console.WriteLine("buttonAX: " + buttonAX + " buttonAY: " + buttonAY);
                Console.WriteLine("buttonBX: " + buttonBX + " buttonBY: " + buttonBY);
                Console.WriteLine("targetX: " + targetX + " targetY: " + targetY);
                if (seen.Contains((targetX, targetY)))
                {
                    Console.WriteLine("FOUND ONE!!!!");// continue;
                }

                seen.Add((targetX, targetY));

                var determinant = GetDeterminant(buttonAX, buttonBX, buttonAY, buttonBY);
                //Console.WriteLine("base determinant: " + determinant);
                if (determinant != 0)
                {
                    var determinantX = GetDeterminant(targetX, buttonBX, targetY, buttonBY);
                    var determinantY = GetDeterminant(buttonAX, targetX, buttonAY, targetY);
                    var a = (double)(targetX * buttonBY - buttonBX * targetY) / (double)determinant;
                    var b = (double)(buttonAX * targetY - targetX * buttonAY) / (double)determinant;
                    //Console.WriteLine("a: " + a);
                    //Console.WriteLine("b: " + b);

                    if (a >= 0 && a <= 100 && b >= 0 && b <= 100)
                    {
                        if (a % 1 == 0 && b % 1 == 0)
                        {
                            var thisCost = (3 * a) + b;
                            Console.WriteLine("thisCost: " + thisCost);
                            cost += thisCost;
                        }
                        
                    }
                }

            } 
            return cost.ToString();
        }

        private long GetValue(string input, string regex)
        {
            return long.Parse(Regex.Match(input, regex).Groups[1].Value);
        }

        private long GetDeterminant(long a, long b, long c, long d)
        {

            //
            // 
            // A = | a b |
            //     | c d |
            //
            return a * d - b * c;
        }

        public string Part2(string[] input)
        {
            double cost = 0;
            var seen = new HashSet<(long, long)>();
            for (int i = 0; i < input.Length; i += 4)
            {
                var buttonAX = GetValue(input[i], @"X\+(\d+)");
                var buttonAY = GetValue(input[i], @"Y\+(\d+)");

                var buttonBX = GetValue(input[i + 1], @"X\+(\d+)");
                var buttonBY = GetValue(input[i + 1], @"Y\+(\d+)");

                var targetX = GetValue(input[i + 2], @"X=(\d+)") + 10000000000000;
                var targetY = GetValue(input[i + 2], @"Y=(\d+)") + 10000000000000;
                Console.WriteLine("buttonAX: " + buttonAX + " buttonAY: " + buttonAY);
                Console.WriteLine("buttonBX: " + buttonBX + " buttonBY: " + buttonBY);
                Console.WriteLine("targetX: " + targetX + " targetY: " + targetY);
                if (seen.Contains((targetX, targetY)))
                {
                    Console.WriteLine("FOUND ONE!!!!");// continue;
                }

                seen.Add((targetX, targetY));

                var determinant = GetDeterminant(buttonAX, buttonBX, buttonAY, buttonBY);
                //Console.WriteLine("base determinant: " + determinant);
                if (determinant != 0)
                {
                    var determinantX = GetDeterminant(targetX, buttonBX, targetY, buttonBY);
                    var determinantY = GetDeterminant(buttonAX, targetX, buttonAY, targetY);
                    var a = (double)(targetX * buttonBY - buttonBX * targetY) / (double)determinant;
                    var b = (double)(buttonAX * targetY - targetX * buttonAY) / (double)determinant;
                    //Console.WriteLine("a: " + a);
                    //Console.WriteLine("b: " + b);

                    if (a >= 0 && b >= 0)
                    {
                        if (a % 1 == 0 && b % 1 == 0)
                        {
                            var thisCost = (3 * a) + b;
                            Console.WriteLine("thisCost: " + thisCost);
                            cost += thisCost;
                        }

                    }
                }

            }
            return cost.ToString();
        }
    }
}
