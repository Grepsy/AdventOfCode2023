﻿using System.Runtime.CompilerServices;
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

    public static void Deconstruct<T>(this IEnumerable<T> list, out T a, out T b, out T c) {
        a = list.ElementAt(0);
        b = list.ElementAt(1);
        c = list.ElementAt(2);
    }
}

public static class Range {
    public static IEnumerable<long> Long(long start, long count) {
        for (var i = start; i < start + count; i++) {
            yield return i;
        }
    }
}