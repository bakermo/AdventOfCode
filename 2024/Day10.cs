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
            grid.PrintGrid();

            var startPositions = GetStartPositions(grid);
            foreach (var position in startPositions)
            {
                Console.WriteLine("Row: {0}, Col: {1}", position.Row, position.Column);
            }

            var count = 0;

            foreach (var position in startPositions)
            {
                var foundNines = new Dictionary<Position, HashSet<Position>>();
                Traverse(grid, position, position, foundNines);
                foreach (var startingPosition in foundNines)
                {
                    count += startingPosition.Value.Count;
                }
            }

            

            return count.ToString();
        }

        public string Part2(string[] input)
        {
            return input[0].Length.ToString();
        }

        private List<Position> GetStartPositions(char[,] grid)
        {
            var positions = new List<Position>();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    var value = int.TryParse(grid[row, col].ToString(), out int result);
                    if (value && result == 0)
                    {
                        positions.Add(new Position(row, col));
                    }
                }
            }

            return positions;
        }

        private void Traverse(char[,] grid, Position startPosition, Position originalStartingPosition, 
            Dictionary<Position, HashSet<Position>> foundNines)
        {
            var value = int.Parse(grid[startPosition.Row, startPosition.Column].ToString());
            var testPositions = new List<Position>(value);
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
                            Console.WriteLine("Row: {0}, Col: {1}", position.Row, position.Column);
                            if (foundNines.ContainsKey(originalStartingPosition))
                            {
                                foundNines[originalStartingPosition].Add(position);
                            }
                            else
                            {
                                var positionsFound = new HashSet<Position>();
                                positionsFound.Add(position);
                                foundNines.Add(originalStartingPosition, positionsFound);
                            }
                            //return;
                        }
                        else
                        {
                            Traverse(grid, position, originalStartingPosition, foundNines);
                        }

                    }
                }
            }
        }
    }
}
