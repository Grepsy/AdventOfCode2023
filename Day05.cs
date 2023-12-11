public static partial class Day05 {
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex DigitsRegex();

    public static long Part1() {
        var sections = File.ReadAllText("day05.txt").Split("\n\n");
        var sectionMaps = (from section in sections[1..].Index()
                           from map in section.Value.Split("\n")[1..]
                           let p = map.Split(" ").Select(long.Parse).ToArray()
                           select (i: section.Key, map: new Map(p[0], p[1], p[2]))).ToLookup(x => x.i, x => x.map);

        return DigitsRegex().Matches(sections[0].Split(":")[1]).Select(Parse.Long).Min(p => {
            for (var i = 0; i < sectionMaps.Count; i++) {
                p = sectionMaps[i].FirstOrDefault(x => x.Match(p))?.Next(p) ?? p;
            }
            return p;
        });
    }

    public static long Part2() {
        var sections = File.ReadAllText("day05.txt").Split("\n\n");
        var sectionMaps = (from section in sections[1..]
                           let maps = from map in section.Split("\n")[1..]
                                      let p = map.Split(" ").Select(long.Parse).ToArray()
                                      select new Map(p[0], p[1], p[2])
                           select maps.ToArray()).ToArray();

        return DigitsRegex().Matches(sections[0].Split(":")[1]).Select(Parse.Long)
            .Batch(2)
            .SelectMany(x => Range.Long(x[0], x[1]))
            .AsParallel()
            .Min(p => {
                for (var i = 0; i < sectionMaps.Length; i++) {
                    for (var j = 0; j < sectionMaps[i].Length; j++) {
                        if (sectionMaps[i][j].Match(p)) {
                            p = sectionMaps[i][j].Next(p);
                            break;
                        }
                    }
                }
                return p;
            });
    }

    private record Map(long dst, long src, long len) {
        public bool Match(in long p) => p >= src && p < src + len;
        public long Next(in long p) => p + (dst - src);
    }
}