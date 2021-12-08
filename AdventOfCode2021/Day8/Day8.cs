using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day8
{
    internal static class Day8
    {
        private const string TestFile = "../../Day8/input_test.txt";
        private const string RealFile = "../../Day8/input.txt";

        private const string InputFile = RealFile;
        
        public static void Day8Pt1()
        {
            var validLengths = new[] { 2, 3, 4, 7 };
            var input = File.ReadAllLines(InputFile);
            
            var result = input
                .Select(x => x.Split(" | ")[1].Split(' '))
                .Sum(x => x.Count( y => validLengths.Contains(y.Length)));
            
            Console.WriteLine($"Result: {result}");

        }
        
        public static string[] Split(this string target, string separator)
        {
            return target.Split(new[] {separator}, StringSplitOptions.None);
        }
    }
}