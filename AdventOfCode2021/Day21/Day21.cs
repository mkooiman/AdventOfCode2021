using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day21
{
    internal sealed class Day21
    {
        private const string TestFile = "../../Day21/input_test.txt";
        private const string RealFile = "../../Day21/input.txt";

        private const string InputFile = RealFile;



        public static void Day21Pt1()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => s.Substring("Player 1 starting position: ".Length))
                .Select(int.Parse)
                .ToArray();
            var triangle = CalculateNumbers(1200);
            var points = new int[input.Length];
            int i = 0;
            input[0]--;
            input[1]--;
            while (points[0] < 1000 && points[1] < 1000)
            {
                int roll = triangle[i];
                int roll2 = triangle[i+1];
                var pos = (input[0] + roll) % 10;
                var pos2 = (input[1] + roll2) % 10;
                // if(pos == 0) pos = 9;
                // if(pos2 == 0) pos2 = 9;
                
                points[0] += pos+1;
                i++;
                if(points[0]>=1000) break;
                points[1] += pos2+1;
                input[0] = pos;
                input[1] = pos2;
                
                Console.WriteLine($"p1 roll { roll}, pos {pos}");
                Console.WriteLine($"p2 roll { roll2}, pos {pos2}");
                i++;
            }
            
            var res = points.Min() * (i*3);
            Console.WriteLine($"Part 1: {points[0]} {points[1]} {res}");
        }

        public static int[] CalculateNumbers(int count)
        {
            var result = new int[count];
            var baseNumber = 1;
            for (var i = 0; i < count; i++)
            {
                result[i] = 3*baseNumber + 3;
                baseNumber += 3;
            }

            return result;
        }
        
        public static void Day21Pt2()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => s.Substring("Player 1 starting position: ".Length))
                .Select(int.Parse)
                .ToArray();
            input[0]--;
            input[1]--;
            var multipliers = InitCounts(3, 3, 0,null);
            ulong p1Wins = 0;
            ulong p2Wins = 0;
            RollP1(0,0, input[0], input[1], 1, multipliers, ref p1Wins, ref p2Wins);
            Console.WriteLine($"\n" +
                              $"p1 wins {p1Wins} \n" +
                              $"p2 wins {p2Wins} ");

            if (p1Wins > p2Wins)
            {
                Console.WriteLine($"p1 wins in {p1Wins} universes");
            }
            else
            {
                Console.WriteLine($"p2 wins in {p2Wins} universes");
            }
     }

        private static int[] InitCounts(int faces, int dice, int count, int[] collection = null)
        {
            if (collection == null)
            {
                collection = new int[(dice + 1) * (1 + faces)];
            }

            if (dice == 1)
            {
                for (int i = 1; i < faces+1 ; i++)
                {
                    collection[count + i] += 1;
                }
            }
            else
            {
                for (int i = 1; i < faces+1; i++)
                {
                    InitCounts(faces, dice - 1, count + i, collection); 
                }
            }

            return collection;

        }
        
        private static void RollP1( int p1Score, int p2Score, int p1Start, int p2Start, ulong p1Games,int[] multipliers, ref ulong p1Wins, ref ulong p2Wins)
        {
            for (int x = 3; x < 10; x++)
            {
                var newStart = (p1Start + x) % 10;
                var newP1Score = p1Score + newStart + 1;
                if (newP1Score >= 21)
                {
                    p1Wins += p1Games*(ulong)multipliers[x];
                }

                else
                {
                    var p1Games1 = p1Games * (ulong)multipliers[x];
                    RollP2(newP1Score, p2Score, newStart, p2Start,p1Games1, multipliers,ref p1Wins,ref p2Wins);
                }
            }
            
        }
        
        private static void RollP2( int p1Score, int p2Score, int p1Start, int p2Start, ulong games,int[] multipliers, ref ulong p1Wins, ref ulong p2Wins)
        {
            for (int x = 3; x < 10; x++)
            {
                var newStart = (p2Start + x) % 10;
                var newP2Score = p2Score + newStart+1;
                if (newP2Score >= 21)
                {
                    p2Wins += games*(ulong)multipliers[x];
                }

                else
                {
                    var p2Games1 = games * (ulong)multipliers[x];
                    RollP1(p1Score, newP2Score, p1Start, newStart,p2Games1, multipliers,ref p1Wins,ref p2Wins);
                }
            }
        }
    }
}