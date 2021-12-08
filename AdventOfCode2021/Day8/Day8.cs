using System;
using System.Collections.Generic;
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
                        .Select(x =>x.Split(' '))
                        .ToArray());
            var total = 0;
            foreach (var strings in result)
            {
                var mapping = GetMapping(strings[0]);
                var value = 0;
                foreach (var s in strings[1])
                {
                    value =  ( value * 10 ) + Array.IndexOf(mapping,s.Sort());
                }

                total += value;
            }
            Console.WriteLine($"Result: {total}");

        }

        
        
        private static string[] GetMapping(string[] input)
        {
            var one = input.SingleOrDefault(x => x.Length == 2);
            var seven = input.SingleOrDefault(x => x.Length == 3);
            var four = input.SingleOrDefault(x => x.Length == 4);
            var eight = input.SingleOrDefault(x => x.Length == 7);
            
            var fivelen = input.Where(x => x.Length == 5).ToList();
            if(fivelen.Count()!=3)
                throw new ArgumentException("There should be exactly 3 strings of length 5");
            
            var sixlen = input.Where(x => x.Length == 6).ToList();
            
            if(sixlen.Count!= 3)
                throw new ArgumentException("There should be exactly 3 six length digits");
            
            var three = fivelen
                .FirstOrDefault(x => x.IndexOf(one[0]) != -1 && x.IndexOf(one[1]) != -1);
            fivelen.Remove(three);
            var six = sixlen
                .FirstOrDefault(x => x.IndexOf(one[0])==-1 || x.IndexOf(one[1]) == -1);
            sixlen.Remove(six);
            
            var tmp = six.Aggregate("abcdefg", (current, c) => current.Replace("" + c, ""));

            var two = fivelen.SingleOrDefault(x => x.Contains(tmp));
            fivelen.Remove(two);
            var five = fivelen[0];
            string zero = null;
            string nine = null;
            if (four.Any(c => !sixlen[0].Contains(c)))
            {
                zero = sixlen[0];
                nine = sixlen[1];
            }

            if (zero == null)
            {
                nine = sixlen[0];
                zero = sixlen[1];
            }

            return new []{ zero.Sort(), one.Sort(), two.Sort(), three.Sort(), four.Sort(), five.Sort(), 
                six.Sort(), seven.Sort(), eight.Sort(), nine.Sort()};
        }

        public static string[] Split(this string target, string separator)
        {
            return target.Split(new[] {separator}, StringSplitOptions.None);
        }
        
        public static string Sort( this string target)
        {
            var result = target.ToCharArray();
            Array.Sort(result);
            return new string(result);
        }
    }
}