using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day7
{
    internal sealed class Day7
    {
        public static void Day7Pt1()
        {
            var input = File.ReadAllLines("../../Day7/input.txt");
            var list = input[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            list.Sort();

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
            var input = File.ReadAllLines("../../Day7/input.txt");
            var list = input[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            list.Sort();

            var ans = list.Average();
            var steps = CalculateSteps2(list, (int)ans);
            Console.WriteLine($"Answer: {ans}\n" +
                              $"Steps: {steps}");
        }

        private static int CalculateSteps2(List<int> list, int start)
        {
            
            return list.Sum( x => Math.Abs( x-start));
        }
    }
}