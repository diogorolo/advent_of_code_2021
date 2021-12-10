using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    public class Day8 : IDay<long, long, long, long>
    {
        private const int Day = 8;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateNumbers(data);
        }

        public long Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateNumbers(data, true);
        }

        public long PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test_2.txt").ToList();
            return CalculateNumbers(data, true);
        }

        public long PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateNumbers(data);
        }

        private long CalculateNumbers(List<string> data, bool pt2 = false)
        {
            if (!pt2)
            {
                return data
                    .Select(x => x.Split("|")[1])
                    .SelectMany(x => x.Trim().Split(" ")).Count(x => x.Length is 2 or 3 or 4 or 7);
            }

            var finalOutput = 0;
            foreach(var line in data)
            {
                var output = 0;
                var segments = line.Split("|").First().Trim().Split(" ");
                var one = segments.First(x => x.Length == 2);
                var four = segments.First(x => x.Length == 4);
                var seven = segments.First(x => x.Length == 3);
                var eight = segments.First(x => x.Length == 7);
                var regex = new Regex($"[{one}]");
                var topSegment = regex.Replace(seven, "");
                var threeHypothesis = segments.Where(x => x.Contains(topSegment) && x.Length is 5);
                foreach (var letter in one)
                {
                    threeHypothesis = threeHypothesis.Where( x=> x.Contains(letter)).ToList();
                }

                if (threeHypothesis.Count() != 1)
                {
                    throw new Exception("err");
                }

                var three = threeHypothesis.First();
                var bottomSegment = three.Replace(topSegment, "");
                foreach (var letter in one.Union(four))
                {
                    bottomSegment = bottomSegment.Replace(new string( new []{letter}), "");
                }
                
                var middleSegment = three.Replace(topSegment, "");
                middleSegment = middleSegment.Replace(bottomSegment, "");
                foreach (var letter in one)
                {
                    middleSegment = middleSegment.Replace(new string( new []{letter}), "");
                }
                
                var zero = segments.First(x => x.Length is 6 && !x.Contains(middleSegment));
                var nineHypothesis = segments.Where(x => x.Length is 6 && x != zero);
                foreach (var letter in one)
                {
                    nineHypothesis = nineHypothesis.Where( x=> x.Contains(letter)).ToList();
                }

                if (nineHypothesis.Count() != 1)
                {
                    throw new Exception("err");
                }

                var nine = nineHypothesis.First();
                var six = segments.First(x => x.Length is 6 && x != zero && x != nine);
                var regex2 = new Regex($"[{nine}]");
                var bottomLeftSegment = regex2.Replace(eight, "");
                var two = segments.First(x => x.Length is 5 && x.Contains(bottomLeftSegment));
                var five = segments.First(x => x.Length is 5 && x != two && x != three);
                var outputSegments = line.Split("|")[1].Trim().Split(" ");
                for (int i = 0; i < outputSegments.Length; i++)
                {
                    var curSegment = new String(outputSegments[i].OrderBy(x => x).ToArray());
                    if (curSegment == new string(zero.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 0;
                    }
                    else if (curSegment ==  new string(one.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 1;
                    }else if (curSegment == new string(two.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 2;
                    }else if (curSegment == new string(three.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 3;
                    }else if (curSegment == new string(four.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 4;
                    }else if (curSegment ==  new string(five.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 5 ;
                    }else if (curSegment == new string(six.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 6;
                    }else if (curSegment ==  new string(seven.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 7;
                    }else if (curSegment == new string(eight.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 8;
                    }else if (curSegment ==  new string(nine.OrderBy(x => x).ToArray()))
                    {
                        output += (int)Math.Pow(10,outputSegments.Length - i - 1) * 9;
                    }
                    else
                    {
                        throw new Exception("err");
                    }
                }

                finalOutput += output;
            }
            return finalOutput;
        }
    }
}