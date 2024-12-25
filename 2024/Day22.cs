using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day22 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var numbers = new List<long>();
            foreach (var line in input)
            {
                numbers.Add(long.Parse(line));
                //Console.WriteLine(line);
            }

            //for (int count = 0; count < 2000; count++)
            //{
            //    numbers = Evolve(numbers);
            //}
            var buyerPriceChanges = new Dictionary<int, List<int>>();

            for (int i = 0; i < numbers.Count; i++)
            {
                //Console.WriteLine(numbers[i]);
                if (!buyerPriceChanges.ContainsKey(i))
                {
                    buyerPriceChanges[i] = new List<int>();
                }

                buyerPriceChanges[i].Add((int)numbers[i]);
            }

            for (int count = 0; count < 2000; count++)
            {
                numbers = Evolve(numbers);

                for (int i = 0; i < numbers.Count; i++)
                {
                    buyerPriceChanges[i].Add((int)numbers[i]);
                }

            }

            //foreach (var number in numbers)
            //{
            //    Console.WriteLine(number);
            //}
            int sum = 0;
            foreach (var value in buyerPriceChanges.Values)
            {
                Console.WriteLine("last: " + value.Last());
                sum += value.Last();
            }
            return sum.ToString();
            //return numbers.Sum().ToString();
        }

        private List<long> Evolve(List<long> numbers)
        {
            var newNumbers = new List<long>();
            foreach (var number in numbers)
            {
                //var newNumber = number * 64;
                var newNumber = Mix(number, number * 64);
                newNumber = Prune(newNumber);

                //newNumber = newNumber / 32;
                newNumber = Mix(newNumber, newNumber / 32);
                newNumber = Prune(newNumber);

                //newNumber = newNumber * 2048;
                newNumber = Mix(newNumber, newNumber * 2048);
                newNumber = Prune(newNumber);

                newNumbers.Add(newNumber);
            }
            return newNumbers;
        }

        private long Mix(long secretNumber, long givenValue)
        {
            return secretNumber ^ givenValue;
        }
        
        private long Prune(long secretNumber)
        {
            return secretNumber % 16777216;
        }

        private int GetLastDigit(long value)
        {
            return (int)(value % 10);
        }

        public string Part2(string[] input)
        {
            var numbers = new List<long>();
            foreach (var line in input)
            {
                numbers.Add(long.Parse(line));
                //Console.WriteLine(line);
            }

            var buyerPriceChanges = new Dictionary<int, List<int>>();

            for (int i = 0; i < numbers.Count; i++)
            {
                //Console.WriteLine(numbers[i]);
                if (!buyerPriceChanges.ContainsKey(i))
                {
                    buyerPriceChanges[i] = new List<int>();
                }

                buyerPriceChanges[i].Add((int)numbers[i]);
            }

            for (int count = 0; count < 2000; count++)
            {
                numbers = Evolve(numbers);

                for (int i = 0; i < numbers.Count; i++)
                {
                    buyerPriceChanges[i].Add((int)numbers[i]);
                }

            }
            //for (int count = 0; count < 2000; count++)
            //{
            //    numbers = Evolve(numbers);

            //    for (int i = 0; i < numbers.Count; i++)
            //    {
            //        //Console.WriteLine(numbers[i]);
            //        if (!buyerPriceChanges.ContainsKey(i))
            //        {
            //            buyerPriceChanges[i] = new List<int>();
            //        }

            //        buyerPriceChanges[i].Add((int)numbers[i]);
            //    }

            //}

            //var sequences = new List<List<int>>();
            // 1598 is too low!
            //var sampleSequence = new List<int> { -2, 1, -1, 3 };
            var sampleSequence = new List<int> { 3, 1, 4, 1 };
            var sequenceToResultMap = new Dictionary<string, int>();
            //sequences.Add(sampleSequence);
            //var seenSequences = new HashSet<List<int>>();
            //seenSequences.Add(sampleSequence);

            //seenSequences.Add(sampleSequence);

           for (int bs = 0; bs < buyerPriceChanges.Count; bs++)
           {
                var buyerPriceChange = buyerPriceChanges[bs];
                var seenThisBuyer = new Dictionary<string, int>();
                var sequence = new List<int>();
                for (int i = 4; i < buyerPriceChange.Count; i++)
                {
                    var price = buyerPriceChange[i];
                    var lastDigit = price % 10;
                    sequence = new List<int>();
                    for (int j = 3; j >= 0; j--)
                    {
                        //Console.WriteLine("i: " + i + " j: " + j);
                        //var change = GetLastDigit(buyerPriceChange[i - (j + 1)]) - GetLastDigit(buyerPriceChange[i - j]);
                        var change = GetLastDigit(buyerPriceChange[i - j]) - GetLastDigit(buyerPriceChange[i - (j + 1)]);
                        sequence.Add(change);
                    }
                    if (SequencesEqual(sequence, sampleSequence))
                    {
                        Console.WriteLine("Found sequence at " + i);
                    }
                    var key = SequenceToString(sequence);
                    if (!seenThisBuyer.ContainsKey(key))
                    {
                        seenThisBuyer.Add(key, lastDigit);
                        if (!sequenceToResultMap.ContainsKey(key))
                        {
                            sequenceToResultMap.Add(key, lastDigit);
                        }
                        else
                        {
                            sequenceToResultMap[key] += lastDigit;
                        }
                    }
                    //if (seenThisBuyer.ContainsKey(key))
                    //{
                    //    //var current = sequenceToResultMap[sequence];
                    //    //if (lastDigit > current)
                    //    //{
                    //    //    sequenceToResultMap[sequence] = lastDigit;
                    //    //}
                    //    seenThisBuyer[key] += lastDigit;
                    //    break;
                    //}
                    //else
                    //{
                    //    seenThisBuyer.Add(key, lastDigit);
                    //    continue;
                    //}

                    //seenSequences.Add(sequence);
                }
           }

            var highestSum = sequenceToResultMap.Values.Max();
            var highestKey = sequenceToResultMap.FirstOrDefault(x => x.Value == highestSum).Key;
            Console.WriteLine(highestKey);
            return highestSum.ToString();
        }

        private string SequenceToString(List<int> sequence)
        {
            return string.Join("," , sequence);
        }

        private bool SequencesEqual(List<int> sequence1, List<int> sequence2)
        {
            if (sequence1.Count != sequence2.Count)
            {
                return false;
            }
            for (int i = 0; i < sequence1.Count; i++)
            {
                if (sequence1[i] != sequence2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
