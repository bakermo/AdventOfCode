using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day5 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var rules = GetRules(input);
            var updates = GetUpdates(input);
            int result = 0;
            foreach (var update in updates)
            {
                if (IsUpdateValid(rules, update))
                {
                    int middleIndex = update.PageNumbers.Count / 2;
                    result += update.PageNumbers.ElementAt(middleIndex).Key;
                }
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var rules = GetRules(input);
            var updates = GetUpdates(input);

            var invalidUpdateQueue = new Queue<Update>();
            int result = 0;
            foreach (var update in updates)
            {
                if (!IsUpdateValid(rules, update))
                {
                    invalidUpdateQueue.Enqueue(update);
                }
            }

            while (invalidUpdateQueue.Count > 0)
            {
                var invalidUpdate = invalidUpdateQueue.Dequeue();

                foreach (var pageNumber in invalidUpdate.PageNumbers.Keys)
                {
                    foreach (var rule in rules)
                    {
                        if (pageNumber == rule.After)
                        {
                            if (invalidUpdate.PageNumbers.ContainsKey(rule.Before))
                            {
                                int beforeIndex = invalidUpdate.PageNumbers[rule.Before];
                                int pageNumberIndex = invalidUpdate.PageNumbers[pageNumber];
                                if (beforeIndex > pageNumberIndex)
                                {
                                    // the before index is actually after, so its backwards
                                    // so we swap them
                                    invalidUpdate.PageNumbers[rule.Before] = pageNumberIndex;
                                    invalidUpdate.PageNumbers[pageNumber] = beforeIndex;
                                }
                            }
                        }
                    }
                }

                if (IsUpdateValid(rules, invalidUpdate))
                {
                    var indexes = invalidUpdate.PageNumbers.Values.ToList();
                    indexes.Sort();
                    int middleIndex = indexes.Count / 2;
                    foreach (var pageNumber in invalidUpdate.PageNumbers.Keys)
                    {
                        if (invalidUpdate.PageNumbers[pageNumber] == indexes[middleIndex])
                        {
                            result += pageNumber;
                            break;
                        }
                    }
                }
                else
                {
                    invalidUpdateQueue.Enqueue(invalidUpdate);
                }
            }
            return result.ToString();
        }

        private List<Rule> GetRules(string[] input)
        {
            var rules = new List<Rule>();
            foreach (var line in input)
            {
                if (line.Contains("|"))
                {
                    var sides = line.Split("|");
                    rules.Add(new Rule
                    {
                        Before = int.Parse(sides[0]),
                        After = int.Parse(sides[1])
                    });
                }               
            }
            return rules;
        }

        private List<Update> GetUpdates(string[] input)
        {
            var updates = new List<Update>();
            foreach (var line in input)
            {
                if (line.Contains(","))
                {
                    string[] numbers = line.Split(",");
                    var update = new Update();
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        update.PageNumbers.Add(int.Parse(numbers[i]), i);
                    }
                    updates.Add(update);
                }
            }

            return updates;
        }

        private bool IsUpdateValid(List<Rule> rules, Update update)
        {
            foreach (var pageNumber in update.PageNumbers.Keys)
            {
                foreach (var rule in rules)
                {
                    if (pageNumber == rule.Before)
                    {
                        if (update.PageNumbers.ContainsKey(rule.After))
                        {
                            var afterIndex = update.PageNumbers[rule.After];
                            if (afterIndex < update.PageNumbers[pageNumber])
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }


    public class Rule
    {
        public int Before { get; set; }
        public int After { get; set; }
    }

    public class Update
    {
        // TODO: come back on another day and make a less ridiculous data structure
        public Dictionary<int, int> PageNumbers { get; set; } = new Dictionary<int, int>();
    }

}
