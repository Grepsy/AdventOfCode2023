public static class Day08 {
    public static int Part1() {
        var lines = File.ReadLines("day08.txt").ToArray();
        var lr = lines[0].Select(x => x == 'L' ? 0 : 1).ToArray();
        var nodes = (from line in lines[2..]
                     let vals = new[] { line[7..10], line[12..15] }
                     select (key: line[0..3], vals)).ToDictionary(x => x.key, x => (x.key, x.vals));

        var cur = nodes["AAA"];
        var i = 0;
        while (cur.key != "ZZZ") {
            cur = nodes[cur.vals[lr[i++ % lr.Length]]];
        }

        return i;
    }

    public static long Part2() {
        var lines = File.ReadLines("day08.txt").ToArray();
        var lr = lines[0].Select(x => x == 'L' ? 0 : 1).ToArray();
        var nodes = (from line in lines[2..]
                     let vals = new[] { line[7..10], line[12..15] }
                     select (key: line[0..3], vals)).ToDictionary(x => x.key, x => (x.key, x.vals));
        var starts = nodes.Keys.Where(x => x[2] == 'A').Select(x => nodes[x]).ToArray();
        var exits = new long[starts.Length];

        for (var n = 0; n < starts.Length; n++) {
            var cur = starts[n];
            var i = 0;

            while (cur.key[2] != 'Z') {
                cur = nodes[cur.vals[lr[i++ % lr.Length]]];
            }

            exits[n] = i;
        }

        return exits.Aggregate((x, y) => x / GCD(x, y) * y);
    }

    public static long GCD(long a, long b) {
        while (b != 0) {
            (a, b) = (b, a % b);
        }

        return a;
    }
}