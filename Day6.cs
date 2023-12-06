public static partial class Day6 {
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex DigitsRegex();

    public static int Part1() {
        var lines = File.ReadAllLines("day6.txt");
        var times = DigitsRegex().Matches(lines[0]).Select(Parse.Int);
        var records = DigitsRegex().Matches(lines[1]).Select(Parse.Int);
        var races = times.Zip(records).ToArray();

        return (from race in races
                let wins =
                    from speed in Enumerable.Range(1, race.First)
                    let distance = (race.First - speed) * speed
                    where distance > race.Second
                    select distance
                select wins.Count()).Aggregate((a, b) => a * b);
    }

    public static int Part2() {
        var lines = File.ReadAllLines("day6.txt");
        var time = long.Parse(Regex.Replace(lines[0], @"\D+", ""));
        var record = long.Parse(Regex.Replace(lines[1], @"\D+", ""));
        var wins = from speed in Range.Long(1, time)
                   let distance = (time - speed) * speed
                   where distance > record
                   select distance;

        return wins.Count();
    }

}