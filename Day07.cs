public static partial class Day07 {
    public static int Part1() {
        var map = "2233445566778899AEKDQCJBTA".Batch(2).ToDictionary(k => k[0], v => v[1]);
        return (from line in File.ReadLines("day07.txt")
                let split = line.Split(" ").ToArray()
                let hand = new string(split[0].Select(x => map[x]).ToArray())
                orderby HandValue(hand) descending, hand
                select int.Parse(split[1])).Index().Sum(x => (x.Key + 1) * x.Value);
    }

    public static int Part2() {
        var map = "J02233445566778899AEKDQCTB".Batch(2).ToDictionary(k => k[0], v => v[1]);
        return (from line in File.ReadLines("day07.txt")
                let split = line.Split(" ").ToArray()
                let hand = new string(split[0].Select(x => map[x]).ToArray())
                orderby HandValue(hand) descending, hand
                select int.Parse(split[1])).Index().Sum(x => (x.Key + 1) * x.Value);
    }

    public static int HandValue(string hand) => hand switch {
        "00000" => 0,
        _ when Match(hand, 5) => 0,
        _ when Match(hand, 4) => 1,
        _ when Match(hand, 3, 2) => 2,
        _ when Match(hand, 3) => 3,
        _ when Match(hand, 2, 2) => 4,
        _ when Match(hand, 2) => 5,
        _ => 6
    };

    public static bool Match(string hand, params int[] counts) {
        var groups = hand.Where(x => x != '0').GroupBy(x => x).Select(x => x.Count()).OrderDescending().ToArray();
        groups[0] += hand.Count(x => x == '0');
        return counts.SequenceEqual(groups.Take(counts.Length));
    }

    private static void Test() {
        HandValue("11111").Log("0");
        HandValue("01111").Log("0");
        HandValue("00111").Log("0");
        HandValue("00011").Log("0");
        HandValue("00001").Log("0");
        HandValue("00000").Log("0");

        HandValue("1111A").Log("1");
        HandValue("A1111").Log("1");
        HandValue("0AAA1").Log("1");
        HandValue("01AAA").Log("1");

        HandValue("AAA11").Log("2");
        HandValue("11AAA").Log("2");
        HandValue("0AABB").Log("2");

        HandValue("12AAA").Log("3");
        HandValue("1AAA2").Log("3");
        HandValue("AAA12").Log("3");
        HandValue("0AA12").Log("3");
        HandValue("00A12").Log("3");
        HandValue("01AA2").Log("3");
        HandValue("012AA").Log("3");

        HandValue("1AABB").Log("4");
        HandValue("AABB1").Log("4");

        HandValue("AA123").Log("5");
        HandValue("1AA23").Log("5");
        HandValue("12AA3").Log("5");
        HandValue("123AA").Log("5");
        HandValue("01234").Log("5");

        HandValue("12345").Log("6");
    }
}