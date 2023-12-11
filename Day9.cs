public static class Day9 {
    public static long Part1() => Calculate((a, b) => b + a[^1]);
    public static long Part2() => Calculate((a, b) => a[0] - b);

    public static long Calculate(Func<long[], long, long> f) =>
        (from line in File.ReadAllLines("day9.txt")
         let history = History(line.Split(" ").Select(long.Parse).ToArray())
         select history.AggregateRight(0L, f)).Sum();

    public static IEnumerable<long[]> History(long[] list) {
        List<long[]> history = [list];
        while (history[^1].Any(x => x != 0)) {
            history.Add(history[^1].Pairwise((a, b) => b - a).ToArray());
        }

        return history;
    }
}