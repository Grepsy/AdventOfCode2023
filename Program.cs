using System.Diagnostics;
using static System.Console;

var day = DateTimeOffset.Now.AddHours(-6).Day;
var type = Type.GetType($"Day{day:00}")!;

WriteLine($"Day: {day}");
var sw = Stopwatch.StartNew();
var result1 = type.GetMethod("Part1")!.Invoke(null, null);
WriteLine($"Part 1: {result1} took {sw.ElapsedMilliseconds} ms");

sw.Restart();
var result2 = type.GetMethod("Part2")!.Invoke(null, null);
WriteLine($"Part 2: {result2} took {sw.ElapsedMilliseconds} ms");
