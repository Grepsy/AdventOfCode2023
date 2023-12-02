public static class Day2 {
    public static int Part1() =>
        (from kv in File.ReadAllLines("day2.txt").Index()
         let subsets = kv.Value.Split(";")
         let invalid =
             from subset in subsets
             from match in Regex.Matches(subset, @"(\d+) (\w+)")
             let color = match.Groups[2].Value
             let count = int.Parse(match.Groups[1].Value)
             select (color == "red" && count > 12) ||
                    (color == "green" && count > 13) ||
                    (color == "blue" && count > 14)
         where !invalid.Any(x => x)
         select kv.Key + 1).Sum();

    public static int Part2() =>
        (from line in File.ReadAllLines("day2.txt")
         let maxs = from match in Regex.Matches(line, @"(\d+) (\w+)")
                    group match by match.Groups[2].Value into g
                    select g.Max(x => int.Parse(x.Groups[1].Value))
         select maxs.Aggregate((a, b) => a * b)).Sum();
}