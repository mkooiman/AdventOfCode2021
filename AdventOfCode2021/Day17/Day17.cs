using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day17
{
    internal sealed class Day17
    {
        private const string TestFile = "../../Day17/input_test.txt";
        private const string RealFile = "../../Day17/input.txt";

        private const string InputFile = RealFile;

        public static void Day17Pt1()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => Regex.Replace(s, "[^0-9-\\.,]", ""))
                .Select(s => s.Replace("..","."))
                .Select(s => s.Split(new[]{",","."}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .Select( s => new Area{X = s[0], X2 = s[1], Y = s[2], Y2 = s[3]})
                .SingleOrDefault();
            
            var x = CalculateXSpeeds(input).FirstOrDefault();
            
            var highestY = 0;
            var ySpeed = 0;
            
            for( int y =Math.Abs(input.Y-1) ;y>0;y--)
            {
                if (IsHit(input, x, y, out var maxY, out var steps))
                {
                    if (highestY < maxY)
                    {
                        highestY = maxY;
                        ySpeed = y;
                    }
                    Console.WriteLine(maxY+ ", " + steps);
                }
            }
            Console.WriteLine("max: " + ySpeed + ", " + highestY);
                
        }

        public static void Day17Pt2()
        {
            var input = File.ReadAllLines(InputFile)
                .Select(s => Regex.Replace(s, "[^0-9-\\.,]", ""))
                .Select(s => s.Replace("..","."))
                .Select(s => s.Split(new[]{",","."}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .Select( s => new Area{X = s[0], X2 = s[1], Y = s[2], Y2 = s[3]})
                .SingleOrDefault();

            var x = CalculateXSpeeds(input);
            Console.WriteLine(CountYSpeedsForX(input, x));
        }

        private static int CountYSpeedsForX(Area a, List<int> xSpeeds)
        {
            var count = 0;
            {
            foreach (var xSpeed in xSpeeds)
            {
                for( int i = a.Y; i<=Math.Abs(a.Y); i++)
                    if (IsHit(a, xSpeed, i, out var maxY, out var steps))
                    {
                        Console.WriteLine(xSpeed+ ", " + i);
                        count++;
                    }    
                }
            }

            return count;
        }

        private static List<int> CalculateXSpeeds(Area area)
        {
            List<int> validSpeeds = new();
            int xSpeed = 1;
            while (xSpeed <= area.X2)
            {
                var speed = xSpeed;
                var xPos = 0;
                do
                {
                    xPos+=speed;
                    if (xPos >= area.X && xPos <= area.X2)
                    {
                        validSpeeds.Add(xSpeed);
                        break;
                    }

                } while (speed-- >= 0);
                xSpeed++;
            }

            return validSpeeds;
        }
        
        public static bool IsHit(Area area, int velX, int velY, out int maxY, out int steps)
        {
            int xPos = 0;
            int yPos = 0;

            maxY = yPos;
            steps = 0;
            while (xPos <= area.X2 && yPos >= area.Y)
            {
                if (area.contains(xPos, yPos)) return true;
                xPos += velX;
                yPos += velY;
                // Console.WriteLine(xPos + ", " + yPos);
                if(maxY< yPos) maxY = yPos;
                velY --;
                velX -= Math.Sign(velX);
                steps++;
            }

            return (area.contains(xPos, yPos));
        }
        
        
    }

    class Area
    {
        public int X { get; set; }
        public int X2 { get; set; }
        public int Y { get; set; }
        public int Y2 { get; set; }
        
        
        public bool contains( int x, int y)
        {
            return x >= X && x <= X2 && y >= Y && y <= Y2;
        }
    }
}