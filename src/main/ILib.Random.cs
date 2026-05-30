using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DryFish.ILib.Random;

/// <summary>
/// Random generation utilities for DryFish.ILib
/// </summary>
public static class IRandom
{
    private static readonly System.Random _random = new System.Random();

    // ========== STRING METHODS ==========
    
    /// <summary>
    /// Returns a random element from the specified array
    /// </summary>
    public static string FromArray(string[] array)
    {
        if (array == null || array.Length == 0)
            throw new ArgumentException("Array cannot be null or empty");
        return array[_random.Next(array.Length)];
    }

    /// <summary>
    /// Returns a random element from the specified list (alias for FromArray with List)
    /// </summary>
    public static string FromList(List<string> list) => FromArray(list.ToArray());

    /// <summary>
    /// Returns a random name from predefined Vietnamese names
    /// </summary>
    public static string VietnameseName()
    {
        string[] firstNames = { "An", "Bình", "Chí", "Dũng", "Hoa", "Hạnh", "Khánh", "Linh", "Mai", "Nam", "Phúc", "Quân", "Thảo", "Trang", "Tuấn", "Vân", "Vũ", "Xuân" };
        string[] lastNames = { "Nguyễn", "Trần", "Lê", "Phạm", "Huỳnh", "Võ", "Đặng", "Bùi", "Đỗ", "Hồ" };
        return $"{lastNames[_random.Next(lastNames.Length)]} {firstNames[_random.Next(firstNames.Length)]}";
    }

    /// <summary>
    /// Returns a random GUID as string
    /// </summary>
    public static string Guid() => System.Guid.NewGuid().ToString();

    /// <summary>
    /// Returns a random string of specified length with optional character sets
    /// </summary>
    public static string String(int length, bool includeNumbers = true, bool includeLowercase = true, bool includeUppercase = true, bool includeSpecial = false)
    {
        if (length <= 0) return string.Empty;
        
        var chars = new List<char>();
        if (includeNumbers) chars.AddRange("0123456789");
        if (includeLowercase) chars.AddRange("abcdefghijklmnopqrstuvwxyz");
        if (includeUppercase) chars.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (includeSpecial) chars.AddRange("!@#$%^&*()_+-=[]{};:?/.><,");
        
        if (chars.Count == 0) throw new ArgumentException("At least one character set must be selected");
        
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Count)]).ToArray());
    }

    // ========== NUMBER METHODS ==========

    /// <summary>
    /// Returns a random integer between min and max (inclusive)
    /// </summary>
    public static int Int(int min, int max) => _random.Next(min, max + 1);

    /// <summary>
    /// Returns a random integer between 0 and 100 (inclusive)
    /// </summary>
    public static int Int() => _random.Next(101);

    /// <summary>
    /// Returns a random long between min and max (inclusive)
    /// </summary>
    public static long Long(long min, long max)
    {
        if (min > max) throw new ArgumentException("min must be <= max");
        long range = max - min;
        long rand = (long)(_random.NextDouble() * (range + 1));
        return min + rand;
    }

    /// <summary>
    /// Returns a random double between min and max
    /// </summary>
    public static double Double(double min = 0.0, double max = 1.0)
    {
        if (min > max) throw new ArgumentException("min must be <= max");
        return min + (_random.NextDouble() * (max - min));
    }

    /// <summary>
    /// Returns a random float between min and max
    /// </summary>
    public static float Float(float min = 0.0f, float max = 1.0f)
    {
        if (min > max) throw new ArgumentException("min must be <= max");
        return (float)(min + (_random.NextDouble() * (max - min)));
    }

    /// <summary>
    /// Returns a random decimal between min and max
    /// </summary>
    public static decimal Decimal(decimal min = 0.0m, decimal max = 1.0m)
    {
        if (min > max) throw new ArgumentException("min must be <= max");
        decimal range = max - min;
        decimal rand = (decimal)_random.NextDouble();
        return min + (rand * range);
    }

    /// <summary>
    /// Returns a random boolean value
    /// </summary>
    public static bool Bool() => _random.Next(2) == 1;

    // ========== CHARACTER METHODS ==========

    /// <summary>
    /// Returns a random character between min and max (based on ASCII/Unicode values)
    /// </summary>
    public static char Char(char min, char max)
    {
        if (min > max) throw new ArgumentException("min must be <= max");
        return (char)_random.Next(min, max + 1);
    }

    /// <summary>
    /// Returns a random uppercase letter (A-Z)
    /// </summary>
    public static char UppercaseLetter() => (char)_random.Next('A', 'Z' + 1);

    /// <summary>
    /// Returns a random lowercase letter (a-z)
    /// </summary>
    public static char LowercaseLetter() => (char)_random.Next('a', 'z' + 1);

    /// <summary>
    /// Returns a random letter (uppercase or lowercase)
    /// </summary>
    public static char Letter() => Bool() ? UppercaseLetter() : LowercaseLetter();

    /// <summary>
    /// Returns a random digit (0-9)
    /// </summary>
    public static char Digit() => (char)_random.Next('0', '9' + 1);

    // ========== COLLECTION METHODS ==========

    /// <summary>
    /// Returns a random item from any IList
    /// </summary>
    public static T Item<T>(IList<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("List cannot be null or empty");
        return list[_random.Next(list.Count)];
    }

    /// <summary>
    /// Returns a random item from any array
    /// </summary>
    public static T Item<T>(T[] array) => Item(array.AsEnumerable().ToList());

    /// <summary>
    /// Shuffles a list using Fisher-Yates algorithm
    /// </summary>
    public static IList<T> Shuffle<T>(IList<T> list)
    {
        var shuffled = new List<T>(list);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }
        return shuffled;
    }

    /// <summary>
    /// Returns a random subset of specified size from a list
    /// </summary>
    public static IList<T> Subset<T>(IList<T> list, int size)
    {
        if (size > list.Count) size = list.Count;
        return Shuffle(list).Take(size).ToList();
    }

    // ========== COLOR METHODS ==========

    /// <summary>
    /// Returns a random hex color code (e.g., #FF5733)
    /// </summary>
    public static string HexColor() => $"#{_random.Next(0x1000000):X6}";

    /// <summary>
    /// Returns a random RGB color as tuple
    /// </summary>
    public static (int R, int G, int B) RgbColor() => (_random.Next(256), _random.Next(256), _random.Next(256));

    /// <summary>
    /// Returns a random console color name supported by ILib
    /// </summary>
    public static string ConsoleColor()
    {
        string[] colors = { "black", "darkblue", "darkgreen", "darkcyan", "darkred", "darkmagenta", "darkyellow", 
                            "gray", "grey", "darkgray", "darkgrey", "blue", "green", "cyan", "red", 
                            "magenta", "yellow", "white" };
        return Item(colors);
    }

    // ========== DATE & TIME METHODS ==========

    /// <summary>
    /// Returns a random DateTime between start and end
    /// </summary>
    public static DateTime DateTime(DateTime start, DateTime end)
    {
        if (start > end) throw new ArgumentException("start must be <= end");
        long range = (end - start).Ticks;
        long randTicks = (long)(_random.NextDouble() * range);
        return start.AddTicks(randTicks);
    }

    /// <summary>
    /// Returns a random DateTime in the last 30 days
    /// </summary>
    public static DateTime RecentDateTime() => DateTime(DateTime.Now.AddDays(-30), DateTime.Now);

    /// <summary>
    /// Returns a random future DateTime within next 30 days
    /// </summary>
    public static DateTime FutureDateTime() => DateTime(DateTime.Now, DateTime.Now.AddDays(30));

    // ========== UTILITY METHODS ==========

    /// <summary>
    /// Returns a random enum value of type T
    /// </summary>
    public static T Enum<T>() where T : System.Enum
    {
        var values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(_random.Next(values.Length))!;
    }
}
