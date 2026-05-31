using System;
using System.Collections.Generic;

namespace DryFish.ILib.Random
{
    /// <summary>
    /// Random generation utilities for DryFish.ILib
    /// </summary>
    public static class ILibRandom
    {
#if NET6_0_OR_GREATER
        // .NET 6+ has built-in thread-safe Random.Shared
        private static System.Random _random => System.Random.Shared;
#else
        // Thread-safe for older frameworks using ThreadStatic
        [ThreadStatic]
        private static System.Random? _localRandom;
        private static System.Random _random => _localRandom ??= new System.Random();
#endif

        // Cached colors array - avoid allocation on every call
        private static readonly string[] ConsoleColors = {
            "black", "darkblue", "darkgreen", "darkcyan", "darkred", 
            "darkmagenta", "darkyellow", "gray", "grey", "darkgray", 
            "darkgrey", "blue", "green", "cyan", "red", "magenta", 
            "yellow", "white"
        };

        /// <summary>
        /// Returns a random element from the specified array
        /// </summary>
        public static string IRandomFromArray(string[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array cannot be null or empty");
                
            return array[_random.Next(array.Length)];
        }
        
        /// <summary>
        /// Returns a random element from the specified array (generic)
        /// </summary>
        public static T IRandomFromArray<T>(T[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array cannot be null or empty");
                
            return array[_random.Next(array.Length)];
        }
        
        /// <summary>
        /// Returns a random integer between min and max (inclusive)
        /// </summary>
        public static int IRandomInt(int min, int max)
        {
            if (min > max)
                throw new ArgumentException("min must be <= max");
                
            double range = (double)max - (double)min;
            int offset = (int)(_random.NextDouble() * (range + 1.0));
            return min + offset;
        }
        
        /// <summary>
        /// Returns a random integer between 0 and 100
        /// </summary>
        public static int IRandomInt()
        {
            return _random.Next(101);
        }
        
        /// <summary>
        /// Returns a random character between min and max
        /// </summary>
        public static char IRandomChar(char min, char max)
        {
            if (min > max)
                throw new ArgumentException("min must be <= max");
                
            return (char)_random.Next(min, max + 1);
        }
        
        /// <summary>
        /// Returns a random alphabet character between min and max
        /// </summary>
        public static char IRandomAlphabet(char min, char max)
        {
            if (min > max)
                throw new ArgumentException("min must be <= max");
            if (!char.IsLetter(min) || !char.IsLetter(max))
                throw new ArgumentException("Only alphabet characters allowed");
                
            char result;
            do
            {
                result = (char)_random.Next(min, max + 1);
            } while (!char.IsLetter(result));
            
            return result;
        }
        
        /// <summary>
        /// Returns a random uppercase letter (A-Z)
        /// </summary>
        public static char IRandomUppercase()
        {
            return (char)_random.Next('A', 'Z' + 1);
        }
        
        /// <summary>
        /// Returns a random lowercase letter (a-z)
        /// </summary>
        public static char IRandomLowercase()
        {
            return (char)_random.Next('a', 'z' + 1);
        }
        
        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        public static bool IRandomBool()
        {
            return _random.Next(2) == 1;
        }
        
        /// <summary>
        /// Returns a random long between min and max
        /// </summary>
        public static long IRandomLong(long min, long max)
        {
            if (min > max) 
                throw new ArgumentException("min must be <= max");

            ulong range = (ulong)(max - min);
            if (range == ulong.MaxValue)
            {
                byte[] buf = new byte[8];
                _random.NextBytes(buf);
                return BitConverter.ToInt64(buf, 0);
            }

            byte[] bytes = new byte[8];
            _random.NextBytes(bytes);
            ulong uval = BitConverter.ToUInt64(bytes, 0);
            return min + (long)(uval % (range + 1));
        }
        
        /// <summary>
        /// Returns a random double between min and max
        /// </summary>
        public static double IRandomDouble(double min = 0.0, double max = 1.0)
        {
            if (min > max) 
                throw new ArgumentException("min must be <= max");
                
            return min + (_random.NextDouble() * (max - min));
        }
        
        /// <summary>
        /// Returns a random item from a list
        /// </summary>
        public static T IRandomItem<T>(IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("List cannot be null or empty");
                
            return list[_random.Next(list.Count)];
        }
        
        /// <summary>
        /// Returns a random GUID as string
        /// </summary>
        public static string IRandomGuid()
        {
            return System.Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// Returns a random hex color code (e.g., #FF5733)
        /// </summary>
        public static string IRandomHexColor()
        {
            return $"#{_random.Next(0x1000000):X6}";
        }
        
        /// <summary>
        /// Returns a random console color name supported by ILib
        /// </summary>
        public static string IRandomConsoleColor()
        {
            return IRandomFromArray(ConsoleColors);
        }
    }
}
