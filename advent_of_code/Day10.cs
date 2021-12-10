using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    public class Day10 : IDay<long, long, long, long>
    {
        private const int Day = 10;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        public long Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data);
        }

        public long Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data, true);
        }

        public long PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        private long DoDay(List<string> data, bool isPt2 = false)
        {
            var validExpression = new Regex(@"\(\)|\[\]|\{\}|<>", RegexOptions.Compiled);
            var corruptedExpression = new Regex(@"\)|\]|\}|>", RegexOptions.Compiled);
            var output = 0;
            var scores = new List<long>();
            foreach (var line in data)
            {
                var result = line;

                while (true)
                {
                    var currentResult = validExpression.Replace(result, "");
                    if (currentResult != result)
                    {
                        result = currentResult;
                    }
                    else
                    {
                        var corruptions = corruptedExpression.Matches(currentResult);
                        if (corruptions.Count > 0)
                        {
                            if (!isPt2)
                            {
                                output += corruptions.First().Value switch
                                {
                                    ")" => 3,
                                    "]" => 57,
                                    "}" => 1197,
                                    ">" => 25137,
                                    _ => throw new ArgumentOutOfRangeException()
                                };
                            }
                        }
                        else
                        {
                            if (isPt2)
                            {
                                long matchScore = 0;
                                foreach (var letter in currentResult.Reverse())
                                {
                                    matchScore *= 5;
                                    matchScore += letter switch
                                    {
                                        '(' => 1,
                                        '[' => 2,
                                        '{' => 3,
                                        '<' => 4,
                                        _ => throw new ArgumentOutOfRangeException()
                                    };
                                }
                                scores.Add(matchScore);
                            }
                        }

                        break;
                    }
                }
            }

            if (isPt2)
            {
                var sortedScores = scores.OrderBy(x => x);
                return sortedScores.Skip(scores.Count / 2).Take(1).First();
            }
            return output;
        }
    }
}