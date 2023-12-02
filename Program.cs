using static System.Console;

var day = DateTimeOffset.Now.Day;
var type = Type.GetType($"Day{day}")!;

var result1 = type.GetMethod("Part1")!.Invoke(null, null);
var result2 = type.GetMethod("Part2")!.Invoke(null, null);

WriteLine(
   $"""
    Day: {day}
    Part 1: {result1}
    Part 2: {result2}
    """);
