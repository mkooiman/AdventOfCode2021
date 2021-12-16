using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day06
{
    internal static class Day6
    {
        
        public static void Day6Pt1()
        {
            var input = File.ReadAllLines("../../Day06/input.txt");
            var list = input[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();
            
            for(var i=0;i<80;i++)
            {
                Console.WriteLine($"{i}");
                Iter(list);
            }
            
            // print array
            foreach (var t in list)
            {
                Console.Write($"{t},");
            }
            Console.WriteLine();
            Console.WriteLine($"{list.LongCount()}");
        }


        public static void Day6Pt2()
        {
            
            var sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllLines("../../Day06/input.txt");
            var list = input[0].Split(',').Select(int.Parse).ToList();
            var fishy = new decimal[9];
            foreach (var fish in list)
            {
                fishy[fish]++;
            }
            var sw1 = new Stopwatch();
            sw1.Start();
            for (var generation = 0; generation < 256; generation++)
            {
                var first = fishy[0];
                for (var i = 0; i < fishy.Length-1; i++)
                {
                    fishy[i] = fishy[i + 1];
                }

                fishy[6] += first;
                fishy[8] = first;
            }
            sw.Stop();
            sw1.Stop();
            Console.WriteLine($"{fishy.Sum( )}  total: {sw.ElapsedMilliseconds}ms solve:{sw1.ElapsedMilliseconds}ms");
        }

        private static void Iter(List<int> arr)
        {
            var toCreate = 0;
            for(var i =0;i<arr.Count;i++)
            {
                arr[i]--;
                
                if (arr[i] >= 0)
                    continue;
                
                toCreate++;
                arr[i] = 6;
            }
            if(toCreate>0)
            {
                arr.AddRange(Enumerable.Repeat(8, toCreate));
            }
        } 
        
    }

}