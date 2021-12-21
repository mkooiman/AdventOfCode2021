using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day19
{
    internal static class Day19
    {
        private const string TestFile = "../../Day19/input_test.txt";
        private const string RealFile = "../../Day19/input.txt";

        private const string InputFile = TestFile;
        
        public static void Day19Pt1()
        {
            var scanners = Parse(InputFile);

            foreach(var scanner in scanners){
                foreach (var scanner1 in scanners)
                {
                    if (scanner == scanner1) continue;
                    
                    foreach(var beacon in scanner.Beacons)
                    {
                        var matchingCount = 0;
                        foreach (var other in scanner1.Beacons)
                        {
                            var count = CountCommonDistances(beacon, other);
                            if (count != 0)
                                matchingCount++;
                        }

                        if (matchingCount >= 12)
                        {
                            Console.WriteLine(matchingCount);
                        }
                    }
                }
            }
        }

        private static int CountCommonDistances(Beacon beacon, Beacon other)
        {
            return beacon
                .BeaconDistances
                .Count(bd =>
                {
                    var matching = other.BeaconDistances.Where(bd2 => bd.Value.Equals(bd2.Value)).ToList();
                    
                    return matching.Any();
                });
        }

        private static List<Scanner> Parse(string file)
        {
            var input = File.ReadAllLines(file);
            Scanner scanner=null;
            List<Scanner> scanners = new();
            foreach (var line in input)
            {
                if(line.StartsWith("--- scanner ")){
                    if (scanner != null)
                    {
                        CalculateDistancesBetweenBeacons(scanner);
                        scanners.Add(scanner);
                    }
                    scanner = new Scanner();
                    var scannerNumber = Regex.Replace(line, "[^0-9]*","");
                    scanner.Id = int.Parse(scannerNumber);
                }

                else if (!string.IsNullOrWhiteSpace(line))
                {
                    // Console.WriteLine(line);
                    var coords = line.Split(',').Select(int.Parse).ToArray();
                    scanner.Beacons
                        .Add(
                            new Beacon()
                            {
                                // DistanceToScanner = new int[]
                            });
                }
            }
            scanners.Add(scanner);
            return scanners;
        }

        private static void CalculateDistancesBetweenBeacons(Scanner scanner)
        {
            foreach (var scannerBeacon in scanner.Beacons)
            {
                foreach (var beacon in scanner.Beacons)
                {
                    if (scannerBeacon == beacon) continue;
                    
                    // scannerBeacon.BeaconDistances.Add(beacon, Math.Abs( scannerBeacon.DistanceToScanner-beacon.DistanceToScanner));
                    
                }
            }
        }
    }

    internal class Scanner
    {
        public int Id { get; set; }
        public List<Beacon> Beacons { get; set; } = new();
    }
    
    internal class Beacon
    {
        public int Id { get; set; }
        public int[] DistanceToScanner { get; set; }

        public Dictionary<Beacon, int[]> BeaconDistances { get; set; } = new();

        
    }
}