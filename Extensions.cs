using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;

public static class Parse {
    public static int Int(this Match match) => int.Parse(match.Value);
    public static long Long(this Match match) => long.Parse(match.Value);
}

public static class LogExtensions {
    public static T Log<T>(this T obj, string prefix = "") {
        Console.WriteLine(prefix + obj);
        return obj;
    }

    public static IEnumerable<T> Log<T>(this IEnumerable<T> list) {
        list.ForEach(x => Log(x));
        return list;
    }

    public static T[] Log<T>(this T[] list) => Log(list.AsEnumerable()).ToArray();

    public static T Dump<T>(this T obj, bool indent = false) {
        if (obj is ITuple) {
            Console.WriteLine(obj);
        }
        else {
            var text = JsonSerializer.Serialize(obj, typeof(T), new JsonSerializerOptions { WriteIndented = indent, IncludeFields = true });
            Console.WriteLine(text);
        }
        return obj;
    }

    public static IEnumerable<T> Dump<T>(this IEnumerable<T> obj, bool indent = false, int size = 32) {
        var text = JsonSerializer.Serialize(obj.Take(size), typeof(IEnumerable<T>), new JsonSerializerOptions { WriteIndented = indent, IncludeFields = true });
        Console.WriteLine(text);
        return obj;
    }
}


public static class FunctionalExtensions {
    public static Func<T, bool> Not<T>(Func<T, bool> func) => (arg) => !func(arg);

    public static IEnumerable<T> LoopWhile<T>(this T obj, Func<T, T> transform, Func<T, bool> predicate) {
        var result = transform(obj);
        while (predicate(result)) {
            yield return result;
            result = transform(result);
        }
    }
}

public static class EnumerableExtensions {
    public static TResult Apply<T, TResult>(this IEnumerable<T> args, Func<T, TResult> function) => function(args.First());
    public static TResult Apply<T, TResult>(this IEnumerable<T> args, Func<T, T, TResult> function) => function(args.ElementAt(0), args.ElementAt(1));

    public static void Deconstruct<T>(this T[] list, out T a) {
        a = list[0];
    }

    public static void Deconstruct<T>(this T[] list, out T a, out T b) {
        a = list[0];
        b = list[1];
    }

    public static void Deconstruct<T>(this T[] list, out T a, out T b, out T c) {
        a = list[0];
        b = list[1];
        c = list[2];
    }

    public static void Deconstruct<T>(this T[] list, out T a, out T b, out T c, out T d) {
        a = list[0];
        b = list[1];
        c = list[2];
        d = list[3];
    }

    public static void Deconstruct<T>(this T[] list, out T a, out T b, out T c, out T d, out T e) {
        a = list[0];
        b = list[1];
        c = list[2];
        d = list[3];
        e = list[4];
    }

    public static void Deconstruct<T>(this IEnumerable<T> list, out T a, out T b, out T c) {
        a = list.ElementAt(0);
        b = list.ElementAt(1);
        c = list.ElementAt(2);
    }

    public static IEnumerable<T> Speed<T>(this IEnumerable<T> seq) {
        var sw = Stopwatch.StartNew();
        var count = 0L;
        foreach (var item in seq) {
            yield return item;

            if (count++ % 1e7 == 0) {
                Console.WriteLine($"{count / 1e6:0}M {count / sw.Elapsed.TotalSeconds / 1e6:0.00}M/sec");
                count = 0L;
                sw.Restart();
            }
        }
    }

    public static IEnumerable<int> GroupSizes(this string value, char character) {
        int i = 0, s = 0;
        while (i < value.Length) {
            if (value[i] == character) {
                s++;
            }
            else if (s != 0) {
                yield return s;
                s = 0;
            }

            i++;
        }

        if (s != 0) {
            yield return s;
        }
    }

    public static string ReplaceIndices(this string value, IList<int> indices, char character) {
        Span<char> span = stackalloc char[value.Length];
        value.AsSpan().CopyTo(span);

        for (var i = 0; i < indices.Count; i++) {
            span[indices[i]] = character;
        }

        return new string(span);
    }

    public static bool IsSeqEqual<T>(this T[] a, T[] b) where T : IEqualityOperators<T, T, bool> {
        if (a.Length != b.Length) {
            return false;
        }

        for (var i = 0; i < a.Length; i++) {
            if (a[i] != b[i]) {
                return false;
            }
        }

        return true;
    }

    public static bool IsSeqEqual<T>(this T[][] a, T[][] b) where T : IEqualityOperators<T, T, bool> {
        if (a.Length != b.Length || a[0].Length != b[0].Length) {
            return false;
        }

        for (var y = 0; y < a.Length; y++) {
            for (var x = 0; x < a[0].Length; x++) {
                if (a[y][x] != b[y][x]) {
                    return false;
                }
            }
        }

        return true;
    }

    public static string AsString(this IEnumerable<char> chars) => new(chars.ToArray());
}

public static class Range {
    public static IEnumerable<long> Long(long start, long count) {
        for (var i = start; i < start + count; i++) {
            yield return i;
        }
    }
}