using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day2
{
    internal static class Day2
    {
        public static void Day2Pt1()
        {
            var input = File.ReadAllLines("../../Day2/input.txt");
            
            var result = input
                .Select(i => i.Split(' '))
                .GroupBy(
                    i => Enum.Parse(typeof(Direction), i[0]),
                    i => int.Parse(i[1]))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum());
            
            Console.WriteLine($"Up: {result[Direction.up]}\n" +
                              $"Down: {result[Direction.down]}\n" +
                              $"Forward: {result[Direction.forward]}\n" +
                              $"Vertical: {result[Direction.down] - result[Direction.up]}\n"+
            $"Result: {(result[Direction.down] - result[Direction.up]) * result[Direction.forward]}");
        }
        
        public static void Day2Pt2(string[] args)
        {
            var input = File.ReadAllLines("../../Day2/input.txt");

            var result = input
                .Select(i => i.Split(' '))
                .Select(i => new Model
                {
                    Direction = (Direction)Enum.Parse(typeof(Direction), i[0]),
                    Movement = int.Parse(i[1]),
                })
                .Aggregate(new Position(), (pos, model) =>
                {
                    switch (model.Direction)
                    {
                        case Direction.up:
                            pos.Aim -= model.Movement;
                            break;

                        case Direction.down:
                            pos.Aim += model.Movement;
                            break;

                        case Direction.forward:
                            pos.Forward += model.Movement;
                            pos.Depth += (model.Movement * pos.Aim);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    return pos;
                });

            Console.WriteLine($"Forward: {result.Forward}\n" +
                              $"Depth: {result.Depth}\n" +
                              $"Result: {result.Depth * result.Forward}");
        }

        private record Position
        {
            public int Aim { get; set; }
            public int Depth { get; set; }
            public int Forward { get; set; }
        }

        private record Model
        {
            public Direction Direction { get; set; }
            public int Movement { get; set; }
        }

        private enum Direction
        {
            forward,
            up,
            down
        }
    }
}