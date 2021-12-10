using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace advent_of_code
{
    public class Day4 : IDay<int, int, int, int>
    {
        private const int Day = 4;
        private const string RootPath = @"C:\Users\gol\RiderProjects\advent_of_code\advent_of_code\data";

        class BingoBoard
        {
            private readonly List<List<int>> _rows = new List<List<int>>(5);
            private readonly List<List<int>> _columns = new List<List<int>>(5);

            public BingoBoard(int[] input)
            {
                for (int i = 0; i < Math.Sqrt(input.Length); i++)
                {
                    _rows.Add(new List<int>(5));
                    _columns.Add(new List<int>(5));
                }
                for (int i = 0; i < input.Length; i++)
                {
                    _rows[i%5].Add(input[i]);
                    _columns[i/5].Add(input[i]);
                }
            }

            public bool AddNumber(int number)
            {
                var bingo = false;
                foreach (var column in _columns)
                {
                    column.Remove(number);
                    if (column.Count == 0){
                        bingo = true;
                        break;
                    }
                }
                foreach (var row in _rows)
                {
                    row.Remove(number);
                    if (row.Count == 0){
                        bingo = true;
                        break;
                    }
                }

                return bingo;
            }

            public int GetUnusedNumbers()
            {
                return _columns.Sum(column => column.Sum());
            }
        }
        
        public int Part1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateBingo(data[0],data.Skip(2));
        }

        public int Part2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_1.txt").ToList();
            return CalculateBingo(data[0],data.Skip(2),true);
        }

        public int PartTest1()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateBingo(data[0],data.Skip(2));
        }

        private int CalculateBingo(string s, IEnumerable<string> data, bool pt2 = false)
        {
            var enumerable = data as string[] ?? data.ToArray();
            var input = s.Split(",").Select(int.Parse);
            var boards = new List<BingoBoard>();
            var currentBoard = new List<int>();
            for (int i = 0; i < enumerable.Count(); i++)
            {
                if (enumerable[i] == "")
                {
                    boards.Add(new BingoBoard(currentBoard.ToArray()));
                    currentBoard = new List<int>();
                }
                else
                {
                    currentBoard.AddRange(enumerable[i].Split(" ").Where(x => int.TryParse(x,out _)).Select(int.Parse).ToArray());
                }
            }
            boards.Add(new BingoBoard(currentBoard.ToArray()));

            foreach (var number in input)
            {
                foreach (var board in boards.ToList())
                {
                    if (board.AddNumber(number))
                    {
                        if (pt2)
                        {
                            boards.Remove(board);
                            if(boards.Count == 0)
                                return number * board.GetUnusedNumbers();
                        }
                        else
                        {
                            return number * board.GetUnusedNumbers();
                        }

                    }
                }
            }
            

            return 0;
        }


        public int PartTest2()
        {
            var data = System.IO.File.ReadAllLines($"{RootPath}/day_{Day}_test.txt").ToList();
            return CalculateBingo(data[0],data.Skip(2),true);
        }
        
        
    }
}