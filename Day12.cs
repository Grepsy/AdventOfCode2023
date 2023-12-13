public static class Day12 {
    public static long Part1() {
        var a = from line in File.ReadAllLines("day12.txt").Select(x => x.Split(" "))
                let text = line[0]
                let sizes = line[1].Split(',').Select(int.Parse).ToArray()
                let log = $"{text} {string.Join(",", sizes)}".Log("\n")
                let match = Match(text, sizes)
                select match;

        return 0;
    }

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

    public static long Part2() => 0;
}