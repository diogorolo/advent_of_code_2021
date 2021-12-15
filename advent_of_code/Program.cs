// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using advent_of_code;
var day = new Day15();
var watch = new Stopwatch();
string elapsedTime = "";
long res = 0;
watch.Start();
res = day.PartTest1();
watch.Stop();
Console.WriteLine(res);
var ts = watch.Elapsed;
elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
Console.WriteLine($"Pt1 test took: {elapsedTime}");

watch.Start();
res = day.Part1();
watch.Stop();
Console.WriteLine(res);
ts = watch.Elapsed;
elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
Console.WriteLine($"Pt1 took: {elapsedTime}");

watch.Start();
res = day.PartTest2();
watch.Stop();
Console.WriteLine(res);
ts = watch.Elapsed;
elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
Console.WriteLine($"Pt2 test took: {elapsedTime}");

watch.Start();
res = day.Part2();
watch.Stop();
Console.WriteLine(res);
ts = watch.Elapsed;
elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
Console.WriteLine($"Pt2 took: {elapsedTime}");
