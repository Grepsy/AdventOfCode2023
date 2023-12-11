public static class Day10 {
    public static long Part1() {
        var grid = new Grid<char>(File.ReadAllLines("day10.txt"));
        var start = grid.Single(x => x.Value == 'S');
        start.Value = 'F';
        return (MoreEnumerable.TraverseDepthFirst(new Node(start), node => node.cell
            .CardinalNeighbors()
            .Where(x => !node.Parents().Any(y => y.cell.Pos == x.Pos) && Connects(node.cell, x)).Take(1)
            .Select(x => new Node(x, node))).Max(x => x.Depth) / 2) + 1;
    }

    public static long Part2() {
        var grid = new Grid<char>(File.ReadAllLines("day10.txt"));
        var start = grid.Single(x => x == 'S');
        start.Value = 'F';
        var path = MoreEnumerable.TraverseDepthFirst(new Node(start), node => node.cell
            .CardinalNeighbors()
            .Where(x => !node.Parents().Any(y => y.cell.Pos == x.Pos) && Connects(node.cell, x)).Take(1)
            .Select(x => new Node(x, node)));

        var nodes = path.Select(x => x.cell.Pos).ToArray();

        return path
            .Pairwise((a, b) => new[] { a.cell[(b.cell.Pos - a.cell.Pos).Norm.CW], b.cell[(b.cell.Pos - a.cell.Pos).Norm.CW] })
            .SelectMany(x => x)
            .Where(x => x == '.' || !nodes.Contains(x.Pos))
            .SelectMany(x => x.CardinalNeighbors().Append(x))
            .Where(x => x == '.' || !nodes.Contains(x.Pos))
            .SelectMany(x => x.CardinalNeighbors().Append(x))
            .Where(x => x == '.' || !nodes.Contains(x.Pos))
            .SelectMany(x => x.CardinalNeighbors().Append(x))
            .Where(x => x == '.' || !nodes.Contains(x.Pos))
            .SelectMany(x => x.CardinalNeighbors().Append(x))
            .Where(x => x == '.' || !nodes.Contains(x.Pos))
            .DistinctBy(x => x.Pos)
            .Count();
    }

    private static bool Connects(Grid<char> src, Grid<char> dst) => (src.Value, dst.Value, dst.Pos - src.Pos) is
        ('|', '|' or '7' or 'F', (0, -1)) or ('|', '|' or 'L' or 'J', (0, 1)) or
        ('-', '-' or '7' or 'J', (1, 0)) or ('-', '-' or 'L' or 'F', (-1, 0)) or
        ('F', '-' or '7' or 'J', (1, 0)) or ('F', '|' or 'L' or 'J', (0, 1)) or
        ('7', '|' or 'L' or 'J', (0, 1)) or ('7', '-' or 'F' or 'L', (-1, 0)) or
        ('J', '-' or 'F' or 'L', (-1, 0)) or ('J', '|' or 'F' or '7', (0, -1)) or
        ('L', '|' or 'F' or '7', (0, -1)) or ('L', '-' or 'J' or '7', (1, 0));

    private record Node(Grid<char> cell, Node? parent = null) {
        public int Depth = parent == null ? 0 : parent.Depth + 1;

        public IEnumerable<Node> Parents() {
            var n = this;
            while ((n = n.parent) != null)
                yield return n;
        }
    }

    private static void Print(Node[] path, Grid<char>[] inside) {
        path.ForEach(x => x.cell.Value = x.cell.Value switch {
            '|' => '║',
            '-' => '═',
            'F' => '╔',
            'L' => '╚',
            '7' => '╗',
            'J' => '╝',
        });
        inside.ForEach(x => x.Value = '█');
    }
}