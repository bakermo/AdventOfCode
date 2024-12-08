using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    internal class Day8 : IDaySolver
    {
        public string Part1(string[] input)
        {
            var grid = Helpers.GetGrid(input);
            grid.PrintGrid();
            var antennae = new List<Antennae>();    
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (Regex.Match(grid[row, col].ToString(), "^[a-zA-Z0-9]").Success)
                    {
                        antennae.Add(new Antennae { Frequency = grid[row, col], Position = new Position(row, col) });
                    }
                }
            }

            Console.WriteLine();
            var uniquePairs = new HashSet<Tuple<Antennae, Antennae>>();
            //var pairs = new List<Tuple<Antennae, Antennae>>();
            foreach (var antenna in antennae)
            {
                foreach (var other in antennae)
                {
                    if (ValidPair(antenna, other))
                    {
                        var pair = new Tuple<Antennae, Antennae>(antenna, other);
                        // don't double count the pair
                        if (!uniquePairs.Contains(new Tuple<Antennae, Antennae> (other, antenna)))
                        {
                            uniquePairs.Add(pair);
                        }
                    }
                }
            }

            var uniquePositions = new HashSet<Position>();
            foreach (var pair in uniquePairs)
            {
                var antennae1 = pair.Item1;
                var antennae2 = pair.Item2;
                Console.WriteLine($"pair: {antennae1.Frequency} ({antennae1.Position.Row}," +
                    $" {antennae1.Position.Column})  {antennae2.Frequency} ({antennae2.Position.Row}, {antennae2.Position.Column})");

                var run = antennae1.Position.Column - antennae2.Position.Column;
                var rise = antennae1.Position.Row - antennae2.Position.Row;

                var antiNode1Row = antennae1.Position.Row + rise;
                var antiNode1Col = antennae1.Position.Column + run;

                if (grid.IsInBounds(antiNode1Row, antiNode1Col))
                {
                    uniquePositions.Add(new Position(antiNode1Row, antiNode1Col));
                    if (grid[antiNode1Row, antiNode1Col] == '.')
                    {
                        grid[antiNode1Row, antiNode1Col] = '#';
                    }
                }

                var antiNode2Row = antennae2.Position.Row + (rise * -1);
                var antiNode2Col = antennae2.Position.Column + (run * -1);
                
                if (grid.IsInBounds(antiNode2Row, antiNode2Col))
                {
                    uniquePositions.Add(new Position(antiNode2Row, antiNode2Col));
                    if (grid[antiNode2Row, antiNode2Col] == '.')
                    {
                        grid[antiNode2Row, antiNode2Col] = '#';
                    }
                }

                //grid.PrintGrid();
            }

            grid.PrintGrid();

            return uniquePositions.Count.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = Helpers.GetGrid(input);
            grid.PrintGrid();
            var antennae = new List<Antennae>();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (Regex.Match(grid[row, col].ToString(), "^[a-zA-Z0-9]").Success)
                    {
                        antennae.Add(new Antennae { Frequency = grid[row, col], Position = new Position(row, col) });
                    }
                }
            }

            Console.WriteLine();
            var uniquePairs = new HashSet<Tuple<Antennae, Antennae>>();
            //var pairs = new List<Tuple<Antennae, Antennae>>();
            foreach (var antenna in antennae)
            {
                foreach (var other in antennae)
                {
                    if (ValidPair(antenna, other))
                    {
                        var pair = new Tuple<Antennae, Antennae>(antenna, other);
                        // don't double count the pair
                        if (!uniquePairs.Contains(new Tuple<Antennae, Antennae>(other, antenna)))
                        {
                            uniquePairs.Add(pair);      
                        }
                    }
                }
            }

            var uniquePositions = new HashSet<Position>();
            foreach (var pair in uniquePairs)
            {
                // we count the antennae's themselves now too as anti-nodes
                uniquePositions.Add(pair.Item1.Position);
                uniquePositions.Add(pair.Item2.Position);

                var antennae1 = pair.Item1;
                var antennae2 = pair.Item2;
                Console.WriteLine($"pair: {antennae1.Frequency} ({antennae1.Position.Row}," +
                    $" {antennae1.Position.Column})  {antennae2.Frequency} ({antennae2.Position.Row}, {antennae2.Position.Column})");


                var run = antennae1.Position.Column - antennae2.Position.Column;
                var rise = antennae1.Position.Row - antennae2.Position.Row;


                var antiNode1Row = antennae1.Position.Row + rise;
                var antiNode1Col = antennae1.Position.Column + run;


                while (grid.IsInBounds(antiNode1Row, antiNode1Col))
                {
                    uniquePositions.Add(new Position(antiNode1Row, antiNode1Col));
                    if (grid[antiNode1Row, antiNode1Col] == '.')
                    {
                        grid[antiNode1Row, antiNode1Col] = '#';
                    }

                    antiNode1Row += rise;
                    antiNode1Col += run;
                }

                var antiNode2Row = antennae2.Position.Row + (rise * -1);
                var antiNode2Col = antennae2.Position.Column + (run * -1);

                while (grid.IsInBounds(antiNode2Row, antiNode2Col))
                {
                    uniquePositions.Add(new Position(antiNode2Row, antiNode2Col));
                    if (grid[antiNode2Row, antiNode2Col] == '.')
                    {
                        grid[antiNode2Row, antiNode2Col] = '#';
                    }

                    antiNode2Row += (rise * -1);
                    antiNode2Col += (run * -1);
                }

                //grid.PrintGrid();
            }

            grid.PrintGrid();

            return uniquePositions.Count.ToString();
        }

        private double GetDistance(Position p1, Position p2)
        {
            var rise = Math.Abs(p1.Row - p2.Row);
            var run = Math.Abs(p1.Column - p2.Column);
            return rise / run;
        }

        private bool ValidPair(Antennae a1, Antennae a2)
        {
            var rise = Math.Abs(a1.Position.Row - a2.Position.Row);
            var run = Math.Abs(a1.Position.Column - a2.Position.Column);

            return a1.Frequency == a2.Frequency &&
                   a1.Position.Row != a2.Position.Row &&
                   a1.Position.Column != a2.Position.Column;// &&
                   //(rise > 1 || run > 1);
        }

    }

    internal class  Antennae 
    {
        public char Frequency { get; set; }
        public Position Position { get; set; }
    }

}
