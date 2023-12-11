public static class Day04 {
    public static int Part1() =>
        (from line in File.ReadAllLines("day04.txt")
         let numbers = Regex.Matches(line.Split(":")[1], @"(\d+)").Select(Parse.Int).ToArray()
         let wins = numbers[0..10].Intersect(numbers[10..]).Count()
         select (int)Math.Pow(2, wins - 1)).Sum();

    public static int Part2() {
        var cards =
            (from kv in File.ReadAllLines("day04.txt").Index()
             let numbers = Regex.Matches(kv.Value.Split(":")[1], @"(\d+)").Select(Parse.Int).ToArray()
             let wins = numbers[0..10].Intersect(numbers[10..]).Count()
             select new Instance(kv.Key + 1, wins)).ToDictionary(x => x.Index, x => x);

        for (var i = 1; i <= cards.Count; i++) {
            for (var j = 0; j < cards[i].Wins; j++) {
                var card = cards[i + j + 1];
                cards[i + j + 1] = card with { Instances = card.Instances + cards[i].Instances };
            }
        }

        return cards.Sum(x => x.Value.Instances);
    }

    private record struct Instance(int Index, int Wins, int Instances = 1);
}