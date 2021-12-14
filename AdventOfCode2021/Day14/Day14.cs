using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day14
{
    internal static class Day14
    {
        private const string TestFile = "../../Day14/input_test.txt";
        private const string RealFile = "../../Day14/input.txt";

        private const string InputFile = RealFile;

        public static void Day14Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            var start = input[0];
            var dict = input
                .Skip(2)
                .Select(s => s.Split(" -> "))
                .ToDictionary(k => k[0], v => v[1][0]);

            var result = start;
            for (var i = 0; i < 1; i++)
            {
                result = Polymerize(result, dict);
            }
            
            var counts = CountCharacters(result);
            var min = Int64.MaxValue;
            var max = 0L;
            foreach (var keyValuePair in counts)
            {
                if (keyValuePair.Value > max)
                    max = keyValuePair.Value;
                if(keyValuePair.Value< min)
                    min = keyValuePair.Value;
            }

            Console.WriteLine(max - min);
        }
        
        public static void Day14Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            var start = input[0];
            var dict = input
                .Skip(2)
                .Select(s => s.Split(" -> "))
                .ToDictionary(k => k[0], v => v[1][0]);
            

            var counts = dict.ToDictionary(k => k.Key, (_) => 0L);
            var charCounts = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToDictionary(k => k, v => 0L);
            var cycles = 40;
            for( var i =1; i < start.Length; i++)
            {
                var tmp = start[i - 1] +""+ start[i];
                
                counts[tmp]++;
            }

            for (int i = 0; i < cycles; i++)
            {
                var newCount = dict.ToDictionary(k => k.Key, (_) => 0L);
                foreach (var keyValuePair in counts)
                {
                    var result = dict[keyValuePair.Key];

                    var one = keyValuePair.Key[0] + "" + result;
                    var two = "" + result + keyValuePair.Key[1];
                    newCount[one] += keyValuePair.Value;
                    newCount[two] += keyValuePair.Value;
                }
    
                counts = newCount;
            }
            
            foreach (var keyValuePair in counts)
            {
                
                charCounts[keyValuePair.Key[0]] += keyValuePair.Value;
                charCounts[keyValuePair.Key[1]] += keyValuePair.Value;
            }
            
            var min = long.MaxValue;
            var max = 0l;
            foreach (var keyValuePair in charCounts)
            {
                if (keyValuePair.Value > max)
                    max = keyValuePair.Value;
                if(keyValuePair.Value < min && keyValuePair.Value > 0)
                    min = keyValuePair.Value;
            }
            
            Console.WriteLine((max - min)/2 + 1 );
        }
        
        private static Dictionary<char, long> CountCharacters(string polymer)
        {
            
            var counts = new Dictionary<char, long>();
            foreach (var c in polymer)
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
                else
                {
                    counts.Add(c, 1l);
                }
            }

            return counts;
        }

        private static string Polymerize(string source, Dictionary<string,char> dict)
        {
            var sb = new StringBuilder();
            
            for( var i =1; i < source.Length; i++)
            {
                var tmp = source[i - 1] +""+ source[i];
                sb.Append(source[i - 1]);
                sb.Append(dict[tmp]);
            }
            sb.Append(source[source.Length - 1]);
            return sb.ToString();
        }

        private static Dictionary<string, long> PolymerizedCount(string source, Dictionary<string,char> dict, int cycles)
        {
            var counts = dict.ToDictionary(k => k.Key, (_) => 0l);
            for( var i =1; i < source.Length; i++)
            {
                var tmp = source[i - 1] +""+ source[i];
                counts[tmp] = 1;
            }
            
            for (var i = 0; i < cycles; i++)
            {
                var newCount = dict.ToDictionary(k => k.Key, (_) => 0l);
                foreach (var keyValuePair in counts)
                {
                    var result = dict[keyValuePair.Key];
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