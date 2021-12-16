using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day16
{
    internal sealed class Day16
    {
        private const string TestFile = "../../Day16/input_test.txt";
        private const string RealFile = "../../Day16/input.txt";

        private const string InputFile = TestFile;

        public static void Day16Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            foreach (var s in input)
            {
                SolvePt1(s);
            }
        }
        
        
        public static void Day16Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            foreach (var s in input)
            {
                SolvePt2(s);
            }
        }
        
        private static void SolvePt1(string input)
        {
            
            var bytes = new byte[input.Length / 2];
            for (var i = 0; i < input.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            var bs = new BitStream(bytes);
            var packet = ReadPacket(bs);
            Console.WriteLine(SumPacketVersions(packet));
        }
        
        private static int SumPacketVersions(Packet2 packet)
        {
            int result = packet.Version;
            foreach (Packet2 p in packet.Children)
            {
                result+= SumPacketVersions(p);
            }

            return result;
        }
        
        private static void SolvePt2(string input)
        {
            
            var bytes = new byte[input.Length / 2];
            for (var i = 0; i < input.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            var bs = new BitStream(bytes);
            var packet = ReadPacket(bs);
            Console.WriteLine(input + " : " +packet.GetValue());
        }

        private static Packet2 ReadPacket(BitStream bs)
        {
            var packet = new Packet2();
            packet.Version = bs.GetNext(3);
            packet.TypeId = bs.GetNext(3);
            if (packet.TypeId == 4)
            {
                ParseLiteral(packet, bs);
            }
            else
            {
                ParseOperator(packet, bs);
            }

            return packet;
        }

        private static void ParseLiteral(Packet2 p, BitStream bc)
        {
            var value = 0L;
            bool hasNext;
            do
            {
                hasNext = bc.GetNext(1) == 1;
                value <<= 4;
                value |= (uint)bc.GetNext(4);
            } while (hasNext);

            p.Value = value;
        }
        
        private static void ParseOperator(Packet2 p, BitStream bs)
        {
            var countType = bs.GetNext(1);
            if (countType == 0)
            {
                ParseSubPacketsLengthDelimited(p, bs);
            }
            else
            {
                ParseSubPacketsWithCount(p, bs);
            }
        }

        private static void ParseSubPacketsLengthDelimited(Packet2 p, BitStream bs)
        {
            var count = bs.GetNext(15);
            var start = bs.Offset;
            while (bs.Offset - start != count)
            {
                p.Children.Add(ReadPacket(bs));
            }
        }

        private static void ParseSubPacketsWithCount(Packet2 p, BitStream bs)
        {
            var count = bs.GetNext(11);
            for (var i = 0; i < count; i++)
            {
                p.Children.Add(ReadPacket(bs));
            }
        }
        
    }

    internal class Packet2
    {
        public int Version { get; set; }
        public int TypeId { get; set; }
        public long Value { get; set; }

        public List<Packet2> Children { get; } = new ();

        public long GetValue()
        {
            return TypeId switch
            {
                0 => Children.Select(c => c.GetValue()).Sum(),
                1 => (Children.Count == 0) ? 0 : Children.Select(c => c.GetValue()).Aggregate(1l, (l, l1) => l * l1),
                2 => Children.Select(c => c.GetValue()).Min(),
                3 => Children.Select(c => c.GetValue()).Max(),
                4 => Value,
                5 => Children[0].GetValue() > Children[1].GetValue() ? 1 : 0,
                6 => Children[0].GetValue() < Children[1].GetValue() ? 1 : 0,
                7 => Children[0].GetValue() == Children[1].GetValue() ? 1 : 0,
                _ => throw new ArgumentException("error")
            };
        }
    }

    internal class BitStream
    {
        private byte[] _bytes { get; }
        public int Offset { get; private set; }
        

        public BitStream( byte[] bytes)
        {
            Offset = 0;
            _bytes = bytes;
        }

        public int GetNext(int bitLength)
        {
            
            var byteIndex = Offset / 8;
            var offsetInByte = Offset % 8;
            var result = 0;
            for (var i = 0; i < bitLength; i++)
            {
                result <<= 1;
                result |= _bytes[byteIndex] >> (7 - offsetInByte) & 1;
                
                Offset++;
                offsetInByte = Offset % 8;
                if (offsetInByte == 0)
                {
                    byteIndex++;
                }
            }

            return result;
        }
    }
}