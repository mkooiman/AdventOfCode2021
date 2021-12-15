using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day14
{
    internal static class Day14
    {
        private const string TestFile = "../../Day14/input_test.txt";
        private const string RealFile = "../../Day14/input.txt";

        private const string InputFile = RealFile;

        public static void Day14Pt1()
        {
            SolveWithCycles(10);
        }

        public static void Day14Pt2()
        {
            SolveWithCycles(40);
        }
        
        private static void SolveWithCycles(int cycles){
            var input = File.ReadAllLines(InputFile);
            var start = input[0];
            var dict = input
                .Skip(2)
                .Select(s => s.Split(" -> "))
                .ToDictionary(k => k[0], v => v[1][0]);
            
            var charCounts = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToDictionary(k => k, v => 0L);
            
            var counts = GetCounts(start, cycles, dict);
            
            foreach (var keyValuePair in counts)
            {
                charCounts[keyValuePair.Key[0]] += keyValuePair.Value;
                charCounts[keyValuePair.Key[1]] += keyValuePair.Value;
            }
            
            var min = long.MaxValue;
            var max = 0L;
            foreach (var keyValuePair in charCounts)
            {
                if (keyValuePair.Value > max)
                    max = keyValuePair.Value;
                if(keyValuePair.Value < min && keyValuePair.Value > 0)
                    min = keyValuePair.Value;
            }
            
            Console.WriteLine((max - min)/2 + 1 );
        }

        private static Dictionary<string, long> GetCounts(string start, int cycles, Dictionary<string,char> map)
        {
            
            var counts = map.ToDictionary(k => k.Key, (_) => 0L);
            for( var i =1; i < start.Length; i++)
            {
                var tmp = start[i - 1] +""+ start[i];
                counts[tmp]++;
            }
            for (var i = 0; i < cycles; i++)
            {
                var newCount = map.ToDictionary(k => k.Key, (_) => 0L);
                foreach (var keyValuePair in counts)
                {
                    var result = map[keyValuePair.Key];

                    var one = keyValuePair.Key[0] + "" + result;
                    var two = "" + result + keyValuePair.Key[1];
                    newCount[one] += keyValuePair.Value;
                    newCount[two] += keyValuePair.Value;
                }
                counts = newCount;
            }
            return counts;  
        }

        private static string[] Split(this string target, string separator)
        {
            return target.Split(new[] {separator}, StringSplitOptions.None);
        }
    }
}