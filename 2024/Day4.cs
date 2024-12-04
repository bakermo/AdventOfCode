using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day4 : IDaySolver
    {
        public string Part1(string[] input)
        {
            int matchCount = 0;
            var rows = input.ToList();
       
            foreach (var row in rows)
            {
                matchCount += Regex.Matches(row, @"XMAS").Count;
                var reversed = Helpers.Reverse(row);
                matchCount += Regex.Matches(reversed, @"XMAS").Count;

            }

            var columns = Helpers.GetColumns(input);

            foreach (var column in columns)
            {
                matchCount += Regex.Matches(column, @"XMAS").Count;
                var reversed = Helpers.Reverse(column);
                matchCount += Regex.Matches(reversed, @"XMAS").Count;
            }

            var grid = Helpers.GetGrid(input);
            var foursDiagonal = Helpers.GetDiagonals(grid, 4);
            foreach (var diagonal in foursDiagonal)
            {
                if (diagonal == "XMAS")
                {
                    matchCount++;
                }

                var resversed = Helpers.Reverse(diagonal);
                if (resversed == "XMAS")
                {
                    matchCount++;
                }
            }

            return matchCount.ToString();
        }

        public string Part2(string[] input)
        {
            var grid = Helpers.GetGrid(input);
            var crosses = Helpers.GetCenteredCrosses(grid, 3);

            int xmasCount = 0;
            foreach (var cross in crosses)
            {
                var topToBottom = cross.TopToBottom;
                var topToBottomReversed = Helpers.Reverse(topToBottom);
                var bottomToTop = cross.BottomToTop;
                var bottomToTopReversed = Helpers.Reverse(bottomToTop);

                if ((topToBottom == "MAS" || topToBottomReversed == "MAS") && (bottomToTop == "MAS" || bottomToTopReversed == "MAS"))
                {
                    xmasCount++;
                }
            }
            return xmasCount.ToString();
        }
    }
}
