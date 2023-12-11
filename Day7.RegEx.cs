/// <summary>
/// This one is dedicated to Robson for his love of RegEx
/// </summary>
public static class Day7_Regex {
    public static int Part1() {
        var map = "2233445566778899AEKDQCJBTA".Batch(2).ToDictionary(k => k[0], v => v[1]);
        Regex[] types = [
            new(@"([1-9A-E])\1{4}"), // 0 Five
            new(@"([1-9A-E])\1{3}"), // 1 Four
            new(@"(?:([1-9A-E])\1{1}([1-9A-E])\2{2}|([1-9A-E])\3{2}([1-9A-E])\4{1})"), // 2 FullHouse
            new(@"([1-9A-E])\1{2}"), // 3 Three
            new(@"([1-9A-E])\1.?([1-9A-E])\2"), // 4 TwoPair
            new(@"([1-9A-E])\1"), // 5 Pair
            new(@"([1-9A-E])") // 6 High
        ];

        return (from line in File.ReadLines("day7.txt")
                let split = line.Split(" ").ToArray()
                let hand = new string(split[0].Select(x => map[x]).ToArray())
                let value = Array.FindIndex(types, x => x.IsMatch(new(hand.Order().ToArray())))
                orderby value descending, hand
                select int.Parse(split[1])).Index().Sum(x => (x.Key + 1) * x.Value);
    }

    public static int Part2() {
        var map = "J02233445566778899AEKDQCTB".Batch(2).ToDictionary(k => k[0], v => v[1]);
        Regex[] types = [
            new(@"(?:([0-9A-E])\1{4}|0([0-9A-E])\2{3}|00([0-9A-E])\3{2}|000([0-9A-E])\4{1}|0000[0-9A-E])"), // 0 Five
            new(@"(?:([0-9A-E])(?:0|\1){3}|0([0-9A-E])\2{2}|00([0-9A-E])\3|000[0-9A-E]|0.([0-9A-E])\4{2}|00.([0-9A-E])\5)"), // 1 Four
            new(@"(?:([1-9A-E])\1{1}([1-9A-E])\2{2}|([1-9A-E])\3{2}([1-9A-E])\4{1}|0[1-9A-E]([1-9A-E])\5{2}|00([1-9A-E])\6{2}|0([1-9A-E])\7{1}([1-9A-E])\8{1}|0([1-9A-E])\11{2}[1-9A-E])"), // 2 FullHouse
            new(@"(?:([0-9A-E])(?:0|\1){2}|0([0-9A-E])\2|00[0-9A-E]|0.([0-9A-E])\3|0.{2}([0-9A-E])\4)"), // 3 Three
            new(@"(?:([1-9A-E])\1.?([1-9A-E])\2|0.*?([1-9A-E])\3)"), // 4 TwoPair
            new(@"(?:([0-9A-E])\1|0[1-9A-E])"), // 5 Pair
            new(@"([1-9A-E])") // 6 High
        ];

        return (from line in File.ReadLines("day7.txt")
                let split = line.Split(" ").ToArray()
                let hand = new string(split[0].Select(x => map[x]).ToArray())
                let value = Array.FindIndex(types, x => x.IsMatch(new(hand.Order().ToArray())))
                orderby value descending, hand
                select int.Parse(split[1])).Index().Sum(x => (x.Key + 1) * x.Value);
    }
}