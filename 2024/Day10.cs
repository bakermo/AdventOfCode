using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day10 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var grid = input.GetGrid();
            //grid.PrintGrid();

            var startPositions = grid.GetStartPositions('0');
            var trailScores = 0;

            foreach (var position in startPositions)
            {
                var foundTrailEnds = new HashSet<Position>();
                FindTrailScores(grid, position, foundTrailEnds);
                var trailScore = foundTrailEnds.Count;
                //Console.WriteLine("trailScore: {0}", trailScore);
                trailScores += trailScore;
            }

            

            return trailScores.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = input.GetGrid();
            //grid.PrintGrid();

            var startPositions = grid.GetStartPositions('0');
            var trailRatings = 0;
            foreach (var position in startPositions)
            {
                var foundTrailEnds = new Dictionary<Position, int>();
                FindTrailRatings(grid, position, foundTrailEnds);
                var trailRating = foundTrailEnds.Sum(x => x.Value);
                //Console.WriteLine("trailRating: {0}", trailRating);
                trailRatings += trailRating;
            }

            return trailRatings.ToString();
        }

        private void FindTrailScores(char[,] grid, Position startPosition,
            HashSet<Position> foundTrailEnds)
        {
            var value = int.Parse(grid[startPosition.Row, startPosition.Column].ToString());
            var testPositions = new List<Position>();
            testPositions.Add(startPosition.GetNextPosition(Direction.Up));
            testPositions.Add(startPosition.GetNextPosition(Direction.Down));
            testPositions.Add(startPosition.GetNextPosition(Direction.Left));
            testPositions.Add(startPosition.GetNextPosition(Direction.Right));

            foreach (var position in testPositions)
            {
                if (grid.IsInBounds(position.Row, position.Column))
                {
                    var isValid = int.TryParse(grid[position.Row, position.Column].ToString(), out int nextValue);
                    if (!isValid)
                    {
                        continue;
                    }
                    if (nextValue == value + 1)
                    {
                        if (nextValue == 9)
                        {
                            foundTrailEnds.Add(position);
                            //return;
                        }
                        else
                        {
                            FindTrailScores(grid, position, foundTrailEnds);
                        }
                    }
                }
            }
        }

        private void FindTrailRatings(char[,] grid, Position startPosition, Dictionary<Position, int> foundTrailEnds)
        {
            var value = int.Parse(grid[startPosition.Row, startPosition.Column].ToString());
            var testPositions = new List<Position>();
            testPositions.Add(startPosition.GetNextPosition(Direction.Up));
            testPositions.Add(startPosition.GetNextPosition(Direction.Down));
            testPositions.Add(startPosition.GetNextPosition(Direction.Left));
            testPositions.Add(startPosition.GetNextPosition(Direction.Right));

            foreach (var position in testPositions)
            {
                if (grid.IsInBounds(position.Row, position.Column))
                {
                    var isValid = int.TryParse(grid[position.Row, position.Column].ToString(), out int nextValue);
                    if (!isValid)
                    {
                        continue;
                    }
                    if (nextValue == value + 1)
                    {
                        if (nextValue == 9)
                        {
                            if (foundTrailEnds.ContainsKey(position))
                            {
                                foundTrailEnds[position]++;
                            }
                            else
                            {
                                foundTrailEnds.Add(position, 1);
                            }
                        }
                        else
                        {
                            FindTrailRatings(grid, position, foundTrailEnds);
                        }
                    }
                }
            }
        }
    }
}
