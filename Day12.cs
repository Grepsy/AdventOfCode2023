public static partial class Day12 {
    public static long Part1() =>
        (from parts in File.ReadAllLines("day12.txt").Select(x => x.Split(" "))
         let groups = parts[1].Split(',').Select(int.Parse)
         let wildIndices = Wild().Matches(parts[0]).Select(x => x.Index)
         from powerset in wildIndices.Subsets()
         let candidate = parts[0].Select((x, i) => powerset.Contains(i) ? '#' : x).AsString()
         let candidateGroups = Spring().Matches(candidate).Select(x => x.Length)
         where candidateGroups.SequenceEqual(groups)
         select 1).Sum();

    public static long Part2() =>
        (from parts in File.ReadAllLines("day12.txt").Select(x => x.Split(" "))
         let text = string.Join('?', Enumerable.Repeat(parts[0], 5)).Log()
         let groups = parts[1].Split(',').Select(int.Parse).Repeat(5).ToArray()
         let wildIndices = Wild().Matches(text).Select(x => x.Index)
         from powerset in wildIndices.Subsets()
         let candidateGroups = text.ReplaceIndices(powerset, '#').GroupSizes('#')
         where groups.SequenceEqual(candidateGroups)
         select 1).Sum();

    [GeneratedRegex(@"\?")]
    private static partial Regex Wild();
    [GeneratedRegex(@"#+")]
    private static partial Regex Spring();
}