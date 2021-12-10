using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day3 : IDay<int, int, int, int>
    {
        private const int Day = 3;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public int Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculatePower(data);
        }

        public int Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_2.txt").ToList();
            return CalculateLifeSupport(data);
        }

        public int PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculatePower(data);
        }

        public int PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateLifeSupport(data);
        }

        private int CalculateLifeSupport(List<string> data)
        {
            return Convert.ToInt32(GetStuff(new List<string>(data),false).First(),2) * Convert.ToInt32(GetStuff(new List<string>(data),true).First(),2);
        }

        private static IEnumerable<string> GetStuff(IEnumerable<string> data, bool invert)
        {
            var it = 0;
            do
            {
                var count = data.Count(x => x[it] == '1');
                var toggle = '1';
                if (count < data.Count() / 2.0)
                {
                    toggle = '0';
                }

                if (invert)
                {
                    toggle = toggle == '1' ? '0' : '1';
                }

                if (data.Count() > 1)
                    data = data.Where(x => x[it] == toggle).ToList();
                it++;
            } while (data.Count() > 1);

            return data;
        }

        private int CalculatePower(List<string> data)
        {
            var bitLength = data[0].Length;
            int[] occurences = new int[bitLength];
            for (int i = 0; i < data.Count; i++)
            {
                var value = Convert.ToInt32(data[i], 2);
                for (int j = 0; j < bitLength; j++)
                {
                    if ((value & (1 << j)) != 0)
                    {
                        occurences[bitLength - j - 1]++;
                    }
                }
            }

            var finalValue = 0;
            for (int j = 0; j < bitLength; j++)
            {
                if(occurences[j] > data.Count/2) {
                    finalValue |= 1 << (bitLength - j - 1);
                }
            }
            
            return finalValue * ( (~finalValue) & (( 1 << bitLength) - 1));
        }
    }
}