using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    public class Day14 : IDay<long, long, long, long>
    {
        private const int Day = 14;
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
            var data = File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return DoDay(data);
        }

        private static Dictionary<char, Dictionary<char, string>> GetRulesPt1(List<string> data)
        {
            var rules = new Dictionary<char, Dictionary<char, string>>();
            foreach (var rule in data)
            {
                var ruleParts = rule.Split(" -> ");
                if (ruleParts.Length > 0)
                {
                    if (!rules.ContainsKey(ruleParts[0][0]))
                    {
                        rules[ruleParts[0][0]] = new Dictionary<char, string>();
                    }

                    rules[ruleParts[0][0]][ruleParts[0][1]] = $"{ruleParts[0][0]}{ruleParts[1]}{ruleParts[0][1]}";
                }
            }

            return rules;
        }

        private static Dictionary<string, char> GetRulesPt2(List<string> data)
        {
            var rules = new Dictionary<string, char>();
            foreach (var rule in data)
            {
                var ruleParts = rule.Split(" -> ");
                if (ruleParts.Length > 0)
                {
                    rules[ruleParts[0]] = ruleParts[1][0];
                }
            }

            return rules;
        }


        private static long DoDay(List<string> data, bool isPt2 = false)
        {
            var rules = GetRulesPt1(data.Skip(2).ToList());
            var rulesPt2 = GetRulesPt2(data.Skip(2).ToList());
            var currentPolymer = data[0];
            var pairs = GetPairs(currentPolymer);
            var occurrences = new Dictionary<char, long>();
            foreach (var key in rulesPt2.Select(x => x.Key))
            {
                foreach (var letter in key)
                {
                    occurrences[letter] = 0;
                }
            }

            foreach (var letter in currentPolymer)
            {
                occurrences[letter]++;
            }

            foreach (var rule in rulesPt2)
            {
                if (!pairs.ContainsKey(rule.Key))
                {
                    pairs[rule.Key] = 0;
                }
            }

            var maxSteps = isPt2 ? 40 : 10;
            if (false)
            {
                return NaiveMethod(isPt2, maxSteps, currentPolymer.ToList(), rules);
            }
            else
            {
                for (int i = 0; i < maxSteps; i++)
                {
                    var current = pairs.Where(x => x.Value > 0).ToList();
                    foreach (var pair in current)
                    {
                        var letter = rulesPt2[pair.Key];

                        occurrences[letter] += pair.Value;
                        pairs[pair.Key] -= pair.Value;

                        pairs[$"{pair.Key[0]}{letter}"] += pair.Value;
                        pairs[$"{letter}{pair.Key[1]}"] += pair.Value;
                    }

                    //Console.WriteLine($"String is size {occurrences.Sum(x => x.Value)} on step {i + 1}");
                }
            }


            var sorted = occurrences.OrderByDescending(x => x.Value).ToList();
            return sorted.First().Value - sorted.Last().Value;
        }

        private static void AddOrIncrement(Dictionary<string, long> pairs, string value, long count)
        {
            pairs[value] += count;
        }

        private static Dictionary<string, long> GetPairs(string currentPolymer)
        {
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            for (int i = 0; i < currentPolymer.Length - 1; i++)
            {
                var key = $"{currentPolymer[i]}{currentPolymer[i + 1]}";
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                {
                    pairs[key] = 1;
                }
            }

            return pairs;
        }

        private static long NaiveMethod(bool isPt2, int maxSteps, List<char> currentPolymer,
            Dictionary<char, Dictionary<char, string>> rules)
        {
            for (int i = 0; i < maxSteps; i++)
            {
                var nextString = new List<char>();
                for (int j = 0; j < currentPolymer.Count - 1; j++)
                {
                    var match = rules[currentPolymer[j]][currentPolymer[j + 1]];
                    if (j == 0)
                    {
                        nextString.Add(match[0]);
                    }

                    nextString.Add(match[1]);
                    nextString.Add(match[2]);
                }

                currentPolymer = nextString;
                if (isPt2)
                {
                    Console.WriteLine(i);
                }
            }

            var occurrences = new Dictionary<char, long>();
            foreach (var c in currentPolymer)
            {
                if (occurrences.ContainsKey(c))
                    occurrences[c]++;
                else
                    occurrences[c] = 0;
            }

            var sorted = occurrences.OrderByDescending(x => x.Value).ToList();
            return sorted.First().Value - sorted.Last().Value;
        }
    }
}