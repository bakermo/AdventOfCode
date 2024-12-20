 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day14 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var robots = new List<Robot>();
            foreach (var line in input)
            {
                // p = 9,5 v = -3,-3
                var position = Regex.Match(line, "p=(-?\\d+),(-?\\d+)").Groups;
                var velocity = Regex.Match(line, "v=(-?\\d+),(-?\\d+)").Groups;

                var robot = new Robot()
                {
                    Col = int.Parse(position[1].ToString()),
                    Row = int.Parse(position[2].ToString()),
                    ColVelocity = int.Parse(velocity[1].ToString()),
                    RowVelocity = int.Parse(velocity[2].ToString())
                };
                robots.Add(robot);
            }

            int rows = 103;// 7;
            int columns = 101;//  11;
            var grid = new char[rows, columns];
            ClearGrid(grid);
            var robotPositions = PlaceRobots(grid, robots);

            for (int i = 0; i < 100; i++)
            {
                ClearGrid(grid);
                MoveBots(robots, grid.GetLength(0), grid.GetLength(1));
                robotPositions = PlaceRobots(grid, robots);

                //grid.PrintGrid();
                //Console.ReadLine();
            }

            int middleRow = grid.GetLength(0) / 2;
            int middleColumn = grid.GetLength(1) / 2;
            int quadrant1 = 0;
            int quadrant2 = 0;
            int quadrant3 = 0;
            int quadrant4 = 0;

            foreach (var position in robotPositions)
            {
                if (position.Key.Row < middleRow && position.Key.Column < middleColumn)
                {
                    quadrant1 += position.Value;
                }
                else if (position.Key.Row < middleRow && position.Key.Column > middleColumn)
                {
                    quadrant2 += position.Value;
                }
                else if (position.Key.Row > middleRow && position.Key.Column < middleColumn)
                {
                    quadrant3 += position.Value;
                }
                else if (position.Key.Row > middleRow && position.Key.Column > middleColumn)
                {
                    quadrant4 += position.Value;
                }
            }

            grid.PrintGrid();
            var safetyFactor = quadrant1 * quadrant2 * quadrant3 * quadrant4;
            return safetyFactor.ToString();
            
        }

        private void ClearGrid(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    grid[row, col] = '.';
                }
            }
        }

        private void MoveBots(List<Robot> robots, int rows, int columns)
        {
            foreach (var robot in robots)
            {
                robot.Row = Wrap(robot.Row + robot.RowVelocity, rows);
                robot.Col = Wrap(robot.Col + robot.ColVelocity, columns);
                //Console.WriteLine("new row: " + robot.Row);
                //Console.WriteLine("new col: " + robot.Col);
            }
        }

        private int Wrap(int value, int max)
        {
            return (value % max + max) % max;
        }

        private Dictionary<Position, int> PlaceRobots(char[,] grid, List<Robot> robots)
        {
            var robotPositions = new Dictionary<Position, int>();
            foreach (var robot in robots)
            {
                var position = new Position(robot.Row, robot.Col);
                if (robotPositions.ContainsKey(position))
                {
                    robotPositions[position]++;
                }
                else
                {
                    robotPositions.Add(position, 1);
                }

                grid[robot.Row, robot.Col] = 'X';
            }

            return robotPositions;
        }

        public string Part2(string[] input)
        {
            var robots = new List<Robot>();
            foreach (var line in input)
            {
                // p = 9,5 v = -3,-3
                var position = Regex.Match(line, "p=(-?\\d+),(-?\\d+)").Groups;
                var velocity = Regex.Match(line, "v=(-?\\d+),(-?\\d+)").Groups;

                var robot = new Robot()
                {
                    Col = int.Parse(position[1].ToString()),
                    Row = int.Parse(position[2].ToString()),
                    ColVelocity = int.Parse(velocity[1].ToString()),
                    RowVelocity = int.Parse(velocity[2].ToString())
                };
                robots.Add(robot);
            }

            int rows = 103;// 7;
            int columns = 101;//  11;
            var grid = new char[rows, columns];
            ClearGrid(grid);
            var robotPositions = PlaceRobots(grid, robots);

            int seconds = 0;
            while (true)
            {
                seconds++;
                ClearGrid(grid);
                MoveBots(robots, grid.GetLength(0), grid.GetLength(1));
                robotPositions = PlaceRobots(grid, robots);
            
                int middleRow = grid.GetLength(0) / 2;
                int middleColumn = grid.GetLength(1) / 2;
                int quadrant1 = 0;
                int quadrant2 = 0;
                int quadrant3 = 0;
                int quadrant4 = 0;

                foreach (var position in robotPositions)
                {
                    if (position.Key.Row < middleRow && position.Key.Column < middleColumn)
                    {
                        quadrant1 += position.Value;
                    }
                    else if (position.Key.Row < middleRow && position.Key.Column > middleColumn)
                    {
                        quadrant2 += position.Value;
                    }
                    else if (position.Key.Row > middleRow && position.Key.Column < middleColumn)
                    {
                        quadrant3 += position.Value;
                    }
                    else if (position.Key.Row > middleRow && position.Key.Column > middleColumn)
                    {
                        quadrant4 += position.Value;
                    }
                }

                //grid.PrintGrid();
                
                var safetyFactor = quadrant1 * quadrant2 * quadrant3 * quadrant4;
                if (safetyFactor < 50000000)
                //if (safetyFactor < 200000000)
                {
                    grid.PrintGrid();
                    // clustered images seem to have very low safety scores,
                    // most are in the mid 200 mils
                    Console.WriteLine("seconds: " + seconds);

                    Console.WriteLine("safety score:" + safetyFactor);
                    Console.ReadLine();
                    //if (safetyFactor > 250000000)
                    //{

                    //}
                }
            }
        }

        private class Robot
        {
            public int Col { get; set; }
            public int Row { get; set; }
            public int ColVelocity { get; set; }
            public int RowVelocity { get; set; }
        }
    }
}
