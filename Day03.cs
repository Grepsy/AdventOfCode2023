﻿public static class Day03 {
    public static IEnumerable<string> Lines = File.ReadAllLines("day03.txt");
    public static MatchCollection[] Numbers = Lines.Select(x => Regex.Matches(x, @"(\d+)")).ToArray();

    public static int Part1() =>
        (from cell in new Grid<char>(Lines)
         where !char.IsDigit(cell) && cell != '.'
         from n in cell.AdjacentNeighbors()
         where char.IsDigit(n)
         from match in Numbers[n.Y]
         where n.X >= match.Index && n.X < match.Index + match.Length
         select match).Distinct().Select(Parse.Int).Sum();

    public static int Part2() =>
        (from cell in new Grid<char>(Lines)
         where cell == '*'
         let matches = from n in cell.AdjacentNeighbors()
                       where char.IsDigit(n)
                       from match in Numbers[n.Y]
                       where n.X >= match.Index && n.X < match.Index + match.Length
                       select match.Int()
         let ratios = matches.Distinct().ToArray()
         where ratios.Length == 2
         select ratios[0] * ratios[1]).Sum();
}