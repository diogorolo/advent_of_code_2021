using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day7 : IDay<long, long, long, long>
    {
        private const int Day = 7;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculateFuel(data);
        }

        public long Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculateFuel(data,true);
        }

        public long PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculateFuel(data, true);
        }

        public long PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").First().Split(",").Select(int.Parse).ToList();
            return CalculateFuel(data);
        }

        private long CalculateFuel(List<int> data, bool pt2 = false)
        {
            Dictionary<int, long> positions = new Dictionary<int, long>();

            foreach (var position in data)
            {
                if (positions.ContainsKey(position))
                {
                    positions[position]++;
                }
                else
                {
                    positions[position] = 1;
                }
            }

            var minValue = long.MaxValue;
            for(int i = 0 ; i < positions.Max(x => x.Key); i++)
            {
                var value = minValue;
                if (pt2)
                {
                    value = positions.Select(x => (Math.Abs(x.Key - i)*(Math.Abs(x.Key - i)+1)/2) * x.Value).Sum();;
                }
                else
                {
                    value = positions.Select(x => Math.Abs(x.Key - i) * x.Value).Sum();
                }

                if (value < minValue)
                {
                    minValue = value;
                }
            }

            return minValue;
        }

    }
}