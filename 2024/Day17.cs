using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day17 : IDaySolver
    {
        private int RegisterA { get; set; }
        private int RegisterB { get; set; }
        private int RegisterC { get; set; }
        public string Part1(string[] input)
        {
            foreach (var line in input)
            {
                Console.WriteLine(line);
            }

            RegisterA = int.Parse(input[0].Split(":").Last());
            RegisterB = int.Parse(input[1].Split(":").Last());
            RegisterC = int.Parse(input[2].Split(":").Last());
            Console.WriteLine(RegisterA);
            Console.WriteLine(RegisterB);
            Console.WriteLine(RegisterC);

            string program = input[4].Split(":").Last();
            int[] instructions = program.Split(',').Select(int.Parse).ToArray();

            Console.WriteLine("Instructions:");
            foreach (var instruction in instructions)
            {
                Console.WriteLine(instruction);
            }

            var outputs = new List<int>();
            int instructionPointer = 0;
            while (instructionPointer < instructions.Length)
            {
                var instruction = (OpCode)instructions[instructionPointer];
                // will this throw out of bounds?
                int operand = instructions[instructionPointer + 1];

                switch (instruction)
                {
                    case OpCode.adv:
                        RegisterA = Divide(operand);
                        break;
                    case OpCode.bxl:
                        RegisterB = RegisterB ^ operand;
                        break;
                    case OpCode.bst:
                        RegisterB = GetComboOperand(operand) % 8;
                        break;
                    case OpCode.jnz:
                        if (RegisterA != 0)
                        {
                            instructionPointer = operand;
                            continue;
                        }
                        break;
                    case OpCode.bxc:
                        RegisterB = RegisterB ^ RegisterC;
                        break;
                    case OpCode._out:
                        outputs.Add(GetComboOperand(operand) % 8);
                        break;
                    case OpCode.bdv:
                        RegisterB = Divide(operand);
                        break;
                    case OpCode.cdv:
                        RegisterC = Divide(operand);
                        break;

                }

                instructionPointer += 2;
            }


            return string.Join(",", outputs);

            //return input[0].Length.ToString();
        }

        private int Divide(int operand)
        {
            return RegisterA / (int)Math.Pow(2, GetComboOperand(operand));
        }

        private int Multiply(int operand)
        {
            return RegisterA * (int)Math.Pow(2, GetComboOperand(operand));
        }

        private int GetComboOperand(int comboOperator)
        {
            switch (comboOperator)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return comboOperator;
                case 4:
                    return RegisterA;
                case 5:
                    return RegisterB;
                case 6:
                    return RegisterC;
            }
            return 0;
        }

        public string Part2(string[] input)
        {
            foreach (var line in input)
            {
                Console.WriteLine(line);
            }

            RegisterA = int.Parse(input[0].Split(":").Last());
            RegisterB = int.Parse(input[1].Split(":").Last());
            RegisterC = int.Parse(input[2].Split(":").Last());
            Console.WriteLine(RegisterA);
            Console.WriteLine(RegisterB);
            Console.WriteLine(RegisterC);

            string program = input[4].Split(":").Last();
            int[] instructions = program.Split(',').Select(int.Parse).ToArray();

            Console.WriteLine("Instructions:");
            foreach (var instruction in instructions)
            {
                Console.WriteLine(instruction);
            }

            var outputs = new List<int>();
            int instructionPointer = instructions.Length - 2;
            while (instructionPointer > 0)
            {
                var instruction = (OpCode)instructions[instructionPointer];
                // will this throw out of bounds?
                int operand = instructions[instructionPointer + 1];

                switch (instruction)
                {
                    case OpCode.adv:
                        RegisterA = Multiply(operand);
                        break;
                    case OpCode.bxl:
                        RegisterB = operand;
                        break;
                    case OpCode.bst:
                        RegisterB = GetComboOperand(operand) % 8;
                        break;
                    case OpCode.jnz:
                        if (RegisterA != 0)
                        {
                            instructionPointer = operand;
                            continue;
                        }
                        break;
                    case OpCode.bxc:
                        RegisterB = RegisterC;
                        break;
                    case OpCode._out:
                        outputs.Add(GetComboOperand(operand) % 8);
                        break;
                    case OpCode.bdv:
                        RegisterB = Multiply(operand);
                        break;
                    case OpCode.cdv:
                        RegisterC = Multiply(operand);
                        break;

                }

                instructionPointer -= 2;
            }


            return RegisterA.ToString();

        }

        public enum OpCode
        {
            adv = 0,
            bxl = 1,
            bst = 2,
            jnz = 3,
            bxc = 4,
            _out = 5,
            bdv = 6,
            cdv = 7
        }
    }
}
