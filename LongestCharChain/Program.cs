using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmeriosExam
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt");
            var charPositions = ParsePositions(input);
            Console.WriteLine(GetLargestChain(charPositions, input.Length));
        }

        private static IDictionary<string, List<(int, int)>> ParsePositions(string[] input)
        {
            var charPositions = new Dictionary<string, List<(int, int)>>();

            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                var row = line.Split(", ");
                for (int j = 0; j < row.Length; j++)
                {
                    var character = row[j];
                    if (!charPositions.ContainsKey(character))
                    { 
                        charPositions[character] = new List<(int, int)>();
                    }
                    charPositions[character].Add((i,j));
                }
            }

            return charPositions;
        }

        private static string GetLargestChain(IDictionary<string, List<(int, int)>> charPositions, int n)
        {
            string mostRepeatedChar = "";
            int maxConsecutives = 1;

            foreach (var character in charPositions.Keys)
            {
                var positions = charPositions[character];
                var invertedXPositions = positions.Select(x => (x.Item1, x.Item2 = n - x.Item2 - 1));

                var positionsGroupedByX = positions.GroupBy(x => x.Item1).Select(x => x.ToList().Select(x => x.Item2).ToList()).ToList();
                var positionsGroupedByY = positions.GroupBy(x => x.Item2).Select(x => x.ToList().Select(x => x.Item1).ToList()).ToList();
                var leftToRightDiagonals = positions.GroupBy(x => x.Item1 + x.Item2).Select(x => x.ToList()).ToList();
                var rightToLeftDiagonals = invertedXPositions.GroupBy(x => x.Item1 + x.Item2).Select(x => x.ToList()).ToList();

                var maxXConsecutives = GetMaxConsecutivesInLine(positionsGroupedByX);
                var maxYConsecutives = GetMaxConsecutivesInLine(positionsGroupedByY);
                var maxLToRConsecutives = GetMaxConsecutivesInDiagonal(leftToRightDiagonals);
                var maxRToLConsecutives = GetMaxConsecutivesInDiagonal(rightToLeftDiagonals);

                var currentCharMax = GetMax(maxXConsecutives, maxYConsecutives, maxLToRConsecutives, maxRToLConsecutives);
                if (currentCharMax > maxConsecutives)
                {
                    maxConsecutives = currentCharMax;
                    mostRepeatedChar = character;
                }
            }

            return string.Join(", ", Enumerable.Repeat(mostRepeatedChar, maxConsecutives));
        }

        private static int GetMaxConsecutivesInLine(List<List<int>> lines)
        {
            int consecutives = 1;
            int maxConsecutives = 1;

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Count - 1; i++)
                {
                    int distanceToNext = line[i + 1] - line[i];

                    consecutives = distanceToNext == 1 || distanceToNext == -1 ? consecutives + 1 : 1;

                    if (consecutives > maxConsecutives) maxConsecutives = consecutives;
                }
                consecutives = 1;
            }

            return maxConsecutives;
        }

        private static int GetMaxConsecutivesInDiagonal(List<List<(int, int)>> diagonals)
        {
            int consecutives = 1;
            int maxConsecutives = 1;

            foreach (var diagonal in diagonals)
            {
                for (int i = 0; i < diagonal.Count - 1; i++)
                {
                    int xDistanceToNext = diagonal[i + 1].Item1 - diagonal[i].Item1;
                    int yDistanceToNext = diagonal[i + 1].Item2 - diagonal[i].Item2;

                    if ((xDistanceToNext == 1 || xDistanceToNext == -1) && (yDistanceToNext == 1 || yDistanceToNext == -1))
                    {
                        consecutives++;
                    }
                    else 
                    {
                        consecutives = 1;
                    }

                    if (consecutives > maxConsecutives) maxConsecutives = consecutives;
                }
                consecutives = 1;
            }

            return maxConsecutives;
        }

        private static int GetMax(params int[] nums)
        {
            return nums.Max();
        }
    }
}
