public static class Day1 {
    public static int Part1() =>
        (from line in File.ReadAllLines("day1.txt")
         let numbers = Regex.Replace(line, @"[^\d]", string.Empty)
         let first = numbers[0] - 48
         let second = numbers[^1] - 48
         select (first * 10) + second).Sum();

    public static int Part2() =>
        (from line in File.ReadAllLines("day1.txt")
         let line2 = line
            .Replace("one", "one1one")
            .Replace("two", "two2two")
            .Replace("three", "three3three")
            .Replace("four", "four4four")
            .Replace("five", "five5five")
            .Replace("six", "six6six")
            .Replace("seven", "seven7seven")
            .Replace("eight", "eight8eight")
            .Replace("nine", "nine9nine")
         let numbers = Regex.Replace(line2, @"[^\d]", string.Empty)
         let first = numbers[0] - 48
         let second = numbers[^1] - 48
         select (first * 10) + second).Sum();
}