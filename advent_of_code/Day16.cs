using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace advent_of_code
{
    public class Day16 : IDay<long, long, long, long>
    {
        private const int Day = 16;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data);
        }

        public long Part2()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest2()
        {
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest1()
        {
            var data = File.ReadLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        private static long DoDay(List<string> data, bool isPt2 = false)
        {
            var offset = 0;
            long versionSum = 0;
            var byteData = Convert.FromHexString(data[0]);
            while(offset + 11 < byteData.Length * 8)
            {
                var packet = new Packet(byteData,offset, isPt2);
                (offset, var version, _) = packet.Process();
                versionSum += version;
            }
            
            return versionSum;
        }

        class Packet
        {
            public Header Header { get; }
            public List<long> Data { get; }
            private byte[] data;
            private readonly int _offset;
            private readonly bool _isPt2;

            public Packet(byte[] data, int offset, bool isPt2)
            {
                this.data = data;
                _offset = offset;
                _isPt2 = isPt2;
                Header = new Header( data, offset);
            }

            public (int processBytes, long versionSum, long result) Process()
            {
                long versionSum = Header.Version;
                var curOffset = _offset + Header.HeaderLength;
                if (Header.Type == 4)
                {
                    curOffset += ProcessSingleData(data, curOffset, out var value);
                    return (curOffset, versionSum, 0);
                }

                if (Header.LengthType == 1)
                {
                    for (int i = 0; i < Header.DataLength; i++)
                    {
                        Packet subPacket = new Packet(data, curOffset, _isPt2);
                        (curOffset, var version, _) = subPacket.Process();
                        versionSum += version;
                    }
                }
                else
                {
                    var processedBytes = 0;
                    
                    while(processedBytes < Header.DataLength)
                    {
                        Packet subPacket = new Packet(data, curOffset, _isPt2);
                        (curOffset, var version, _) = subPacket.Process();
                        versionSum += version;
                        processedBytes = curOffset - Header.HeaderLength;
                    }

                    //curOffset += processedBytes;
                }
                
                return (curOffset, versionSum, 0);
            }

            int ProcessSingleData(byte[] array, int start, out long value)
            {
                var bits = new byte[64];
                int curBit = start;
                int curAddedBit = 0;
                bool isLast = false;
                while (true)
                {
                    var curByte = curBit / 8;
                    var curBitInByte = curBit % 8;
                    
                    var bit = (array[curByte] & (1 << 7 - curBitInByte)) >> (7 - curBitInByte);
                    var signalBit = (curBit - start) % 5 == 0;
                    if (isLast && signalBit)
                    {
                        break;
                    }
                    if ( signalBit && bit == 0)
                    {
                        isLast = true;
                    }

                    
                    curBit++;

                    if(signalBit)
                        continue;

                    var curAddedByte = curAddedBit / 8;
                    var curAddedBitInByte = curAddedBit % 8;
                    bits[curAddedByte] |= (byte)(bit << (7 - curAddedBitInByte));
                    curAddedBit++;
                }

                var neededData = bits[..((curAddedBit - 1) / 8 + 1)];

                value = CalculateValue(curAddedBit, neededData, 0);
                return curBit - start;
            }
        }
        private static long CalculateValue(int numBits, byte[] array, int offset)
        {
            long value = 0;
            for (int i = offset; i < numBits + offset; i++)
            {
                var curByte = i / 8;
                var curBit = i % 8;
                //Console.WriteLine($"{((array[curByte] & (1 << 7 - curBit)) >> (7 - curBit))} x 2 ^ {numBits + offset - i - 1}");
                value += (((array[curByte] & (1 << 7 - curBit)) >> (7 - curBit)) * (int)Math.Pow(2, numBits + offset - i - 1));
            }

            return value;
        }
        class Header
        {
            private readonly int _start;

            public Header(byte[] data, int start)
            {
                _start = start;
                Data = data;
            }
            public int Version => (byte) CalculateValue(3, Data, _start);

            public int Type => (byte)CalculateValue(3, Data, _start + 3);

            public int LengthType => Type != 4 ? (byte)(byte)CalculateValue(1, Data, _start + 6) : throw new Exception();
            public int DataLength => LengthType == 0 ?
                (byte)(CalculateValue(15, Data, _start + 7)) :
                (byte)(CalculateValue(11, Data, _start + 7));

            public int HeaderLength => 6 + (Type == 4 ? 0 : LengthType == 0 ? 16 : 12);
            public byte[] Data { get; private init; }
        }
    }
}