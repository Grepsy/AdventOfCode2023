public static class Day11 {
    public static long Part1() => new Grid<char>(
        File.ReadAllLines("day11.txt")
            .SelectMany<string, string>(x => IsSpace(x) ? [x, x] : [x])
            .Transpose().Select(x => string.Concat(x))
            .SelectMany<string, string>(x => IsSpace(x) ? [x, x] : [x]))
        .Where(x => x != '.')
        .Subsets(2)
        .Sum(p => (p[0].Pos - p[1].Pos).Length);

    public static long Part2() {
        var lines = File.ReadAllLines("day11.txt");
        var h = lines.Index().Where(x => IsSpace(x.Value)).Select(x => x.Key);
        var v = lines.Transpose().Index().Where(x => IsSpace(x.Value)).Select(x => x.Key);

        return (from p in new Grid<char>(lines).Where(x => x != '.').Subsets(2)
                let dist = (p[0].Pos - p[1].Pos).Length
                let hx = h.Count(a => a.In(p[0].Y, p[1].Y)) * 999999L
                let vx = v.Count(a => a.In(p[0].X, p[1].X)) * 999999L
                select dist + hx + vx).Sum();
    }

    public static bool IsSpace(IEnumerable<char> x) => x.All(y => y == '.');
    public static bool In(this int x, int a, int b) => (x > a && x < b) || (x > b && x < a);
}