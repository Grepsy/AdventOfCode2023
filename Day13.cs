public static class Day13 {
    public static long Part1() {
        var grids =
            from segment in File.ReadAllLines("day13.txt").Segment(string.IsNullOrWhiteSpace)
            let grid = segment.Where(x => x != "").Select(y => y.ToArray()).ToArray()
            select Mirror(grid);

        return grids.First();
    }

    public static int Mirror(char[][] grid) =>
        (from i in Enumerable.Range(0, grid.Length - 1)
         from y in Enumerable.Range(0, Math.Min(i, grid.Length - i - 2))
         from x in Enumerable.Range(0, grid[0].Length)
         where grid[i - y][x] == grid[i + y + 1][x]
         select 1).Sum();

    public static long Part2() => 0;
}