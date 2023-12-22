public static class Day13 {
    public static long Part1() => Find(0);
    public static long Part2() => Find(1);

    public static long Find(int smudges) =>
        (from segment in File.ReadAllLines("day13.txt").Segment(string.IsNullOrEmpty)
         let grid1 = segment.Where(x => x != "").Select(y => y.ToArray()).ToArray()
         let grid2 = grid1.Transpose().Select(x => x.ToArray()).ToArray()
         let h = from i in Enumerable.Range(0, grid1.Length - 1)
                 select (i: i + 1, miss: Mirror(grid1, i))
         let v = from i in Enumerable.Range(0, grid2.Length - 1)
                 select (i: i + 1, miss: Mirror(grid2, i))
         let hr = h.FirstOrDefault(x => x.miss == smudges).i
         let vr = v.FirstOrDefault(x => x.miss == smudges).i
         select (hr * 100) + vr).Sum();

    public static int Mirror(char[][] grid, int axis) {
        int y = 0, miss = 0;
        while (axis - y >= 0 && axis + y < grid.Length - 1) {
            for (var x = 0; x < grid[0].Length; x++) {
                miss += grid[axis - y][x] != grid[axis + y + 1][x] ? 1 : 0;
            }
            y++;
        }

        return miss;
    }
}