using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day2 : IDay<int, int, int, int>
    {
        private const int Day = 2;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public int Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return GetDisplacement(data);
        }

        public int Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_2.txt").ToList();
            return GetDisplacement(data, true);
        }

        public int PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return GetDisplacement(data);
        }

        private static int GetDisplacement(List<string> data, bool useAim = false)
        {
            int x = 0, y = 0, aim = 0;
            foreach (var command in data)
            {
                var commandSplit = command.Split(" ");
                var direction = commandSplit[0];
                var value = int.Parse(commandSplit[1]);
                switch (direction.ToLower())
                {
                    case "forward":
                        x += value;
                        if (useAim)
                        {
                            y += aim * value;
                        }
                        break;
                    case "down":
                        if (useAim)
                        {
                            aim += value;
                        }
                        else
                        {
                            y += value;
                        }
                        break;
                    case "up":
                        if (useAim)
                        {
                            aim -= value;
                        }
                        else
                        {
                            y -= value;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("direction.ToLower()");
                }
            }

            return x * y;
        }

        public int PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return GetDisplacement(data, true);
        }
    }
}