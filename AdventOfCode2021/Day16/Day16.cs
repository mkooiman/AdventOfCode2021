using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day16
{
    internal static class Day16
    {
        private const string TestFile = "../../Day16/input_test.txt";
        private const string RealFile = "../../Day16/input.txt";

        private const string InputFile = TestFile;

        public static void Day16Pt1()
        {
            var input = File.ReadAllLines(InputFile);
            foreach (var hexString in input)
            {
                // Console.WriteLine(hexString);
                byte[] bytes = new byte[hexString.Length / 2];
                for (int i = 0; i < hexString.Length; i += 2)
                    bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                var bitStream = new BitStream(bytes);
                var packet = ReadPacket(bitStream);

                var sum = SumPacketVersions(packet);
                Console.WriteLine($"{sum}");
            }
            
        }
        
        public static void Day16Pt2()
        {
            var input = File.ReadAllLines(InputFile);
            foreach (var hexString in input)
            {
                // Console.WriteLine(hexString);
                byte[] bytes = new byte[hexString.Length / 2];
                for (int i = 0; i < hexString.Length; i += 2)
                    bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                var bitStream = new BitStream(bytes);
                var packet = ReadPacket(bitStream);
                
                Console.WriteLine($"{hexString} : {packet.Value()}");
            }
        }

        private static int SumPacketVersions(Packet packet)
        {
            int result = packet.Version;
            foreach (Packet p in packet.SubPackets)
            {
                result+= SumPacketVersions(p);
            }

            return result;
        }
        
        private static Packet ReadPacket(BitStream bitStream)
        {
            var packet = new Packet();
            packet.Version = bitStream.GetNext(3);
            packet.TypeId = bitStream.GetNext(3);
            if(packet.TypeId == 4)
            {
                bool hasNext;
                do
                {
                    hasNext = 1 == bitStream.GetNext(1);
                    int group = bitStream.GetNext(4);
                    packet.Groups.Add(group);
                } while (hasNext);
            }
            else
            {
                int len = bitStream.GetNext(1) == 0 ? 15 : 11;
                int packetLength = bitStream.GetNext(len);
                if (len == 11)
                {
                    for (int i = 0; i < packetLength; i++)
                    {
                        packet.SubPackets.Add(ReadPacket(bitStream));
                    }
                }else
                {
                    int currentOffset = bitStream.offset;
                    while (bitStream.offset - currentOffset < packetLength)
                    {
                        packet.SubPackets.Add(ReadPacket(bitStream));
                    }
                }
                
            }

            return packet;
        }
        
    }

    class Packet
    {
        public int Version { get; set; }
        public int TypeId { get; set; }
        public List<Packet> SubPackets { get; set; } = new List<Packet>();

        public List<int> Groups { get; set; } = new List<int>();

        public long Value()
        {
            switch (TypeId)
            {
                case 0:
                {
                    if (SubPackets.Count == 0)
                        return 0;
                    var res = 0l;
                    foreach (var subPacket in SubPackets)
                    {
                        // Console.Write($" {subPacket.Value() } +");
                        res += subPacket.Value();
                    }
                    // Console.WriteLine(
                        // $" = {res}");
                    return res;
                }
                case 1:
                {
                    if (SubPackets.Count == 0)
                        return 0;
                    var res = 1l;
                    foreach (var subPacket in SubPackets)
                    {
                        // Console.Write($"{subPacket.Value() } *");
                        res *= subPacket.Value();
                    }
                    // Console.WriteLine($" = {res}");
                    return res;
                }
                case 2:
                {
                    long val = SubPackets.Select(s => s.Value()).Min();
                    return val;
                }   
                case 3:
                {
                    long val = SubPackets.Select(s => s.Value()).Max();
                    return val;
                }
                case 4:
                    
                {
                    var result = 0;
                    foreach (var group in Groups)
                    {
                        result <<= 4;
                        result |= group;
                    }
                    return result;
                }

                case 5:
                {
                    // Console.WriteLine($"{SubPackets[0].Value()} > {SubPackets[1].Value()}?1:0");
                    return SubPackets[0].Value() > SubPackets[1].Value()?1:0;
                }
                case 6:
                {
                    // Console.WriteLine($"{SubPackets[0].Value()} < {SubPackets[1].Value()}?1:0");
                    return SubPackets[0].Value() < SubPackets[1].Value()?1:0;
                }
                case 7:
                {
                    // Console.WriteLine($"{SubPackets[0].Value()} == {SubPackets[1].Value()}?1:0");
                    return SubPackets[0].Value() == SubPackets[1].Value()?1:0;
                }
                default: return 0;
            }
        }
    }
    
    class BitStream
    {
        public byte[] Bytes { get; set; }
        public int offset { get; set; }
        

        public BitStream( byte[] bytes)
        {
            offset = 0;
            Bytes = bytes;
        }

        public int GetNext(int bitLength)
        {
            
            int byteIndex = offset / 8;
            int offsetInByte = offset % 8;
            int result = 0;
            for (int i = 0; i < bitLength; i++)
            {
                result <<= 1;
                result |= Bytes[byteIndex] >> (7 - offsetInByte) & 1;
                
                offset++;
                offsetInByte = offset % 8;
                if (offsetInByte == 0)
                {
                    byteIndex++;
                }
            }

            return result;
        }
    }
}