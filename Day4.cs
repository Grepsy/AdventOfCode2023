public static class Day4 {
    public static int Part1() =>
        (from line in File.ReadAllLines("day4.txt")
         let numbers = Regex.Matches(line.Split(":")[1], @"(\d+)").Select(x => int.Parse(x.Value)).ToArray()
         let wins = numbers[0..10].Intersect(numbers[10..]).Count()
         select (int)Math.Pow(2, wins - 1)).Sum();

    public static int Part2() {
        var cards =
            (from kv in File.ReadAllLines("day4.txt").Index()
             let numbers = Regex.Matches(kv.Value.Split(":")[1], @"(\d+)").Select(x => int.Parse(x.Value)).ToArray()
             let wins = numbers[0..10].Intersect(numbers[10..]).Count()
             select new Instance(kv.Key + 1, wins)).ToDictionary(x => x.Index, x => x);

        for (var i = 1; i <= cards.Count; i++) {
            for (var j = 0; j < cards[i].Wins; j++) {
                cards[i + j + 1].Instances += cards[i].Instances;
            }
        }

        return cards.Sum(x => x.Value.Instances);
    }

    private record Instance(int Index, int Wins) {
        public int Instances { get; set; } = 1;
    }
}