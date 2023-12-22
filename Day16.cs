public static class Day16 {
    public static Grid<char> Grid = new(File.ReadAllLines("day16.txt"));

    public static long Part1() => Energized(Grid[-1, 0], (1, 0));

    public static long Part2() {
        var max = 0;
        for (var i = 0; i < 110; i++) {
            max = Math.Max(max, Energized(Grid[i, -1], (0, 1)));
            max = Math.Max(max, Energized(Grid[i, 110], (0, -1)));
            max = Math.Max(max, Energized(Grid[-1, i], (1, 0)));
            max = Math.Max(max, Energized(Grid[110, i], (-1, 0)));
        }

        return max;
    }

    public static int Energized(Grid<char> grid, Vec d) => Beam(grid, d, []).DistinctBy(x => x.p).Count();

    public static HashSet<(Vec d, Vec p)> Beam(Grid<char> grid, Vec d, HashSet<(Vec d, Vec p)> path) {
        var self = grid[d];
        if (self.InRange && !path.Contains((d, self.Pos))) {
            path.Add((d, self.Pos));
            path.UnionWith(self.Value switch {
                '/' => Beam(self, (-d.Y, -d.X), path),
                '\\' => Beam(self, (d.Y, d.X), path),
                '|' when d.X is not 0 => Beam(self, (0, -1), path).Union(Beam(self, (0, 1), path)),
                '-' when d.Y is not 0 => Beam(self, (-1, 0), path).Union(Beam(self, (1, 0), path)),
                _ => Beam(self, d, path),
            });
        }

        return path;
    }
}