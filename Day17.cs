public static class Day17 {
    public static Grid<int> Grid = new(File.ReadAllLines("day17.txt").Select(x => x.Select(y => int.Parse(y.ToString()))));

    public static long Part1() {
        var path = PathFinding.Find(Grid, Grid.Size - (1, 1), Cost);
        //path.ToArray().Dump();
        return path.Sum(x => Grid[x].Value);
    }

    public static long Part2() => 0;

    public static double Cost(Vec next, Vec[] path) {
        // TODO: calc relative movement
        if (path.Length > 2 && (path[^3..].All(x => x.X == next.X) || path[^3..].All(x => x.Y == next.Y))) {
            return double.PositiveInfinity;
        }

        return Grid[next].Value;
    }

    public static class PathFinding {
        public static IEnumerable<Vec> Find<T>(Grid<T> grid, Vec goal, Func<Vec, Vec[], double> costFn) {
            var start = grid.Pos;
            var open = new List<Vec> { start };
            var visited = new Dictionary<Vec, Vec>();
            var gScore = new Dictionary<Vec, double> { { start, 0 } };
            var fScore = new Dictionary<Vec, double> { { start, costFn(start, []) } };

            while (open.Count > 0) {
                var current = open.OrderBy(x => fScore[x]).First();

                if (current == goal) {
                    return ReconstructPath(visited, current);
                }

                open.Remove(current);

                foreach (var neighbor in grid[current].CardinalNeighbors()) {
                    var cost = costFn(neighbor.Pos, ReconstructPath(visited, current));
                    var tentativeScore = gScore[current] + cost;

                    if (tentativeScore < gScore.GetValueOrDefault(neighbor.Pos, double.MaxValue)) {
                        visited[neighbor.Pos] = current;
                        gScore[neighbor.Pos] = tentativeScore;
                        fScore[neighbor.Pos] = tentativeScore + cost;

                        if (!open.Contains(neighbor.Pos)) {
                            open.Add(neighbor.Pos);
                        }
                    }
                }
            }

            return Enumerable.Empty<Vec>();
        }

        public static Vec[] ReconstructPath(Dictionary<Vec, Vec> visited, Vec current) {
            var path = new List<Vec>();

            while (visited.ContainsKey(current)) {
                path.Add(current);
                current = visited[current];
            }

            path.Reverse();
            return path.ToArray();
        }
    }

}