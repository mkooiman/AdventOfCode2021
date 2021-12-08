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
        
        public static void Day8Pt2()
        {
            var input = File.ReadAllLines(InputFile);


            var result = input
                .Select(x =>
                    x.Split(" | ")
                        .Select(x =>x.Split(' ').Select(x => x.Sort()).ToArray()
                        )
                        .ToArray());
            var total = 0;
            foreach (var strings in result)
            {
                var mapping = GetMapping(strings[0]);
                var value = strings[1]
                    .Aggregate(0, (current, s) => current * 10 + Array.IndexOf(mapping, s.Sort()));

                total += value;
            }
            Console.WriteLine($"Result: {total}");

        }

        
        
        private static string[] GetMapping(string[] input)
        {
            var mapping = new[]
            {
                null,
                input.Single(x => x.Length == 2),
                null,
                null,
                input.Single(x => x.Length == 4),
                null,
                null,
                input.Single(x => x.Length == 3),
                input.Single(x => x.Length == 7),
                null,
            };
            
            var fivelen = input
                .Where(x => x.Length == 5)
                .ToList();
            
            if(fivelen.Count!=3)
                throw new ArgumentException("There should be exactly 3 strings of length 5");
            
            var sixlen = input
                .Where(x => x.Length == 6)
                .ToList();
            
            if(sixlen.Count!= 3)
                throw new ArgumentException("There should be exactly 3 six length digits");
            
            mapping[3] = fivelen
                .First(x => x.IndexOf(mapping[1][0]) != -1 && x.IndexOf(mapping[1][1]) != -1);
            fivelen.Remove(mapping[3]);
            
            mapping[6] = sixlen
                .First(x => x.IndexOf(mapping[1][0])==-1 || x.IndexOf(mapping[1][1]) == -1);
            sixlen.Remove(mapping[6]);
            
            var tmp = mapping[6].Aggregate(mapping[8], (current, c) => current.Replace("" + c, ""));

            mapping[2] = fivelen.Single(x => x.Contains(tmp));
            fivelen.Remove(mapping[2]);
            mapping[5] = fivelen[0];
            
            if (mapping[4].Any(c => !sixlen[0].Contains(c)))
            {
                mapping[0] = sixlen[0];
                mapping[9] = sixlen[1];
            }else
            {
                mapping[0] = sixlen[1];
                mapping[9] = sixlen[0];
            }

            return mapping;
        }

        private static string[] Split(this string target, string separator)
        {
            return target.Split(new[] {separator}, StringSplitOptions.None);
        }
        
        private static string Sort( this string target)
        {
            var result = target.ToCharArray();
            Array.Sort(result);
            return new string(result);
        }
    }
}