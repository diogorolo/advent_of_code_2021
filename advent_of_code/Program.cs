// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using advent_of_code;

var day = new Day12();
var watch = new Stopwatch();
watch.Start();
Console.WriteLine(day.PartTest1());
Console.WriteLine($"Pt1 test took: {watch.ElapsedMilliseconds}");
watch.Restart();
Console.WriteLine(day.Part1());
Console.WriteLine($"Pt1 took: {watch.ElapsedMilliseconds}");
watch.Restart();
Console.WriteLine(day.PartTest2());
Console.WriteLine($"Pt2 test took: {watch.ElapsedMilliseconds}");
watch.Restart();
Console.WriteLine(day.Part2());
Console.WriteLine($"Pt2 took: {watch.ElapsedMilliseconds}");
