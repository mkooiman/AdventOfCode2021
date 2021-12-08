using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day7
{
    internal sealed class Day7
    {
        private const string TestFile = "../../Day7/input_test.txt";
        private const string RealFile = "../../Day7/input.txt";

        private const string InputFile = RealFile;
        
        public static void Day7Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            var list = input[0]
                .Split(',')
                .Select(int.Parse)
                .OrderBy(i => i)
                .ToList();

            var ans = list[list.Count / 2];
            var steps = CalculateSteps(list, ans);
            Console.WriteLine($"Answer: {ans}\n" +
                              $"Steps: {steps}");
        }

        private static int CalculateSteps(List<int> list, int start)
        {
            return list.Sum( x => Math.Abs( x-start));
        }
        
        public static void Day7Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var list = input[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            // TODO: There seems to be some sort of rounding issue
            // so we calculate with both rounding up and down
            // and take the best answer
            var ansDouble = list.Average();
            
            var ansInt = (int)Math.Floor(ansDouble);
            var ansInt2 = (int)Math.Ceiling(ansDouble);
            
            var steps = CalculateSteps2(list, ansInt);
            var steps2 = CalculateSteps2(list, ansInt2);
            
            if(steps< steps2)
                Console.WriteLine($"Average: {ansDouble}\n" +
                                  $"Answer: {ansInt}\n" +
                                  $"Steps: {steps}");
            else
            {
                Console.WriteLine($"Average: {ansDouble}\n" +
                                  $"Answer: {ansInt2}\n" +
                                  $"Steps: {steps2}");
            }
        }

        private static int CalculateSteps2(IEnumerable<int> list, int start)
        {
            return list.Sum( x =>
            {
                var steps = Math.Abs(x - start);
                return steps * (steps + 1) / 2;
            });
        }
    }
}