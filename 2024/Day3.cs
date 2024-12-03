using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    internal class Day3 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var products = new List<int>();
            
            foreach (var line in input)
            {
                var matches = Regex.Matches(line, @"mul\((\d+),(\d+)\)");

                foreach (var match in matches)
                {
                    var m = (Match)match;
                    var x = int.Parse(m.Groups[1].Value);
                    var y = int.Parse(m.Groups[2].Value);
                    products.Add(x * y);
                }
            }
           
            return products.Sum().ToString();
        }

        public string Part2(string[] input)
        {
            var products = new List<int>();

            var instructions = new List<Instruction>();
            var dontInstructions = new List<Instruction>(); 
            var doInstructions = new List<Instruction>();
            string fullInput = string.Join("", input);

            var doMatches = Regex.Matches(fullInput, @"do\(\)");
            foreach (var doMatch in doMatches)
            {
                var instruction = new Instruction()
                {
                    IsDo = true,
                    IsDont = false,
                    Match = (Match)doMatch
                };
                instructions.Add(instruction);
            }
            var dontMatches = Regex.Matches(fullInput, @"don\'t\(\)");
            foreach (var dontMatch in dontMatches)
            {
                var instruction = new Instruction()
                {
                    IsDo = false,
                    IsDont = true,
                    Match = (Match)dontMatch
                };
                instructions.Add(instruction);
            }

            var mulMatches = Regex.Matches(fullInput, @"mul\((-?\d+),(-?\d+)\)");

            foreach (var mulMatch in mulMatches)
            {
                var instruction = new Instruction()
                {
                    IsDo = false,
                    IsDont = false,
                    Match = (Match)mulMatch
                };
                instructions.Add(instruction);
            }

            instructions = instructions.OrderBy(x => x.Match.Index).ToList();

            bool isDoing = true;
            foreach (var instruction in instructions)
            {
                if (instruction.IsDo)
                {
                    isDoing = true;
                }
                else if (instruction.IsDont)
                {
                    isDoing = false;
                }
                else if (isDoing)
                {
                    var x = int.Parse(instruction.Match.Groups[1].Value);
                    var y = int.Parse(instruction.Match.Groups[2].Value);
                    products.Add(x * y);
                }   
            }

            return products.Sum().ToString();
        }

        public class Instruction
        {
            public bool IsDo { get; set; }
            public bool IsDont { get; set; }
            public Match Match { get; set; }
        }
    }
}
