using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day11 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var line = input[0];
            var numbers = line.Split(' ').Select(long.Parse).ToList();
            var linkedList = new LinkedList<long>(numbers);

            //foreach (var item in linkedList)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();

            int blinks = 0;
            while (blinks < 25)
            {
                blinks++;
                Console.WriteLine("blink: " + blinks);
                //foreach (var item in linkedList)
                //{
                //    Console.Write(item + " ");
                //}
                //Console.WriteLine();
                var node = linkedList.First;
                while (node != null)
                {

                    if (node.Value == 0)
                    {
                        node.Value = 1;
                    }
                    else if (node.Value.ToString().Length % 2 == 0)
                    {
                        var newValue = node.Value.ToString();
                        var left = newValue.Substring(0, newValue.Length / 2);
                        var right = newValue.Substring(newValue.Length / 2);

                        linkedList.AddBefore(node, long.Parse(left));
                        linkedList.AddBefore(node, long.Parse(right));
                        var oldNode = node;
                        node = node.Previous;
                        linkedList.Remove(oldNode);

                    }
                    else
                    {
                        node.Value *= 2024;
                    }
                    node = node.Next;
                }

                //foreach (var item in linkedList)
                //{
                //    Console.Write(item + " ");
                //}
                //Console.WriteLine();

            }
           
            return linkedList.Count.ToString();
        }

        public string Part2(string[] input)
        {
            var line = input[0];
            var numbers = line.Split(' ').Select(ulong.Parse).ToList();
            var seenNumbers = new Dictionary<ulong, ulong>();
            foreach (var number in numbers)
            {
                seenNumbers[number] = 1;
            }

            int blinks = 0;
            while (blinks < 75)
            {
                blinks++;

                var seenNumbersThisBlink = new Dictionary<ulong, ulong>();

                foreach (var number in seenNumbers.Keys)
                {
                    // we figure out how many we accumulated up until this blink,
                    // so we pull it from the accumulating cache
                    var countNumber = GetCountFromCache(seenNumbers, number);
                    if (number == 0)
                    {
                        // all the zeros we have just become ones
                        AddToCache(seenNumbersThisBlink, 1, countNumber);

                        //AddToCache(seenNumbers, 1);
                    }
                    else if (number.ToString().Length % 2 == 0)
                    {
                        var newValue = number.ToString();
                        var leftStr = newValue.Substring(0, newValue.Length / 2);
                        var rightStr = newValue.Substring(newValue.Length / 2);
                        var left = ulong.Parse(leftStr);
                        var right = ulong.Parse(rightStr);

                        //var leftCount = GetCountFromCache(seenNumbers, left);
                        //var rightCount = GetCountFromCache(seenNumbers, right);
                        AddToCache(seenNumbersThisBlink, left, countNumber);
                        AddToCache(seenNumbersThisBlink, right, countNumber);
                    }
                    else
                    {
                        AddToCache(seenNumbersThisBlink, number * 2024, countNumber);
                    }

                    // whatever the number was, it has mutated
                    // so there shouldn't be any in the original cache
                    //seenNumbers[number] = 0;
                }
                // wow actually you know what I don't even need to set it to 0
                // because deep clone here will just do that for me
                seenNumbers = new Dictionary<ulong, ulong>(seenNumbersThisBlink);
            }

            BigInteger wtf = 0;
            foreach (var number in seenNumbers.Keys)
            {
                wtf += seenNumbers[number];
            }
            return wtf.ToString();
        }

        private void AddToCache(Dictionary<ulong, ulong> seenNumbers, ulong number, ulong count)
        {
            if (!seenNumbers.ContainsKey(number))
            {
                seenNumbers.Add(number, count);
            }
            else
            {
                seenNumbers[number] += count;
            }
        }

        private ulong GetCountFromCache(Dictionary<ulong, ulong> seenNumbers, ulong number)
        {
            if (seenNumbers.ContainsKey(number))
            {
                return seenNumbers[number];
            }

            seenNumbers.Add(number, 0);
            return 0;
        }
    }
/*
0
1
2024
20 24
2 0 2 4
4048 1 4048 8096
4 0 4 8 20 24 4 0 4 8 8 0 9 6


1
1
1
2
4
4
6
12


3080
4633
7065
10840
16077
24616
37684
57111
86533
130429
199946


we just need to know when 2024 gives you an even number of digits
becasue everything else just splits right


0 to 4 even digits(4)
5 to 49 odd digits(5)
50 to 495 odd digits(7)

its just going to to to 2024 until it reaches even and then it will split
*/
}
