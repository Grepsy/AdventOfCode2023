public static partial class Day12 {
    public static long Part1() =>
        (from parts in File.ReadAllLines("day12.txt").Select(x => x.Split(" "))
         let groups = parts[1].Split(',').Select(int.Parse)
         let wildIndices = Wilds().Matches(parts[0]).Select(x => x.Index)
         from powerset in wildIndices.Subsets()
         let candidate = parts[0].Select((x, i) => powerset.Contains(i) ? '#' : x).AsString()
         let candidateGroups = Springs().Matches(candidate).Select(x => x.Length)
         where candidateGroups.SequenceEqual(groups)
         select 1).Sum();

    public static string Match(string text, int[] sizes, int s = 0, int t = 0) {
        if (s >= sizes.Length || t > text.Length) {
            return text;
        }
        var r = new Regex("^[?#]+");

        var (sx, tx, score) = (sizes[s], text[t..], r.Match(text[t..]).Length) switch {
            (1, _, var length) when length > 0 => (length, Math.Max(length - 1, 1), Math.Min(length, 2)),
            //(2, ['?' or '#', '?' or '#', '?' or '#', ..]) => (1, 3, 0),
            //(3, ['#' or '?', '#' or '?', '#' or '?', ..]) => (1, 3, 0),
            //(4, ['#' or '?', '#' or '?', '#' or '?', '#' or '?', ..]) => (1, 4, 0),
            //(5, ['#' or '?', '#' or '?', '#' or '?', '#' or '?', '#' or '?', ..]) => (1, 5, 0),
            //(6, ['#' or '?', '#' or '?', '#' or '?', '#' or '?', '#' or '?', '#' or '?', ..]) => (1, 6, 0),
            (_, [_, ..], _) => (0, 1, 0),
        };

        $"{sizes[s]} {text[t..(t + tx)],6} {text[(t + tx)..],-20} -> tx: {tx} arr: {score}".Log();

        return Match(text, sizes, s + sx, t + tx);
    }

    public static long Part2() =>
        (from parts in File.ReadAllLines("day12.txt").Select(x => x.Split(" "))
         let groups = parts[1].Split(',').Select(int.Parse)
         let wildIndices = Wilds().Matches(parts[0]).Select(x => x.Index)
         from powerset in wildIndices.Subsets()
         let candidate = parts[0].Select((x, i) => powerset.Contains(i) ? '#' : x).AsString()
         let candidateGroups = Springs().Matches(candidate).Select(x => x.Length)
         where candidateGroups.SequenceEqual(groups)
         select 1).Sum();

    [GeneratedRegex(@"\?")]
    private static partial Regex Wilds();
    [GeneratedRegex(@"#+")]
    private static partial Regex Springs();
}