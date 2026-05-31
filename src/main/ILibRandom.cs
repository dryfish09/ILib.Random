using System;
using System.Collections.Generic;

namespace DryFish.ILib.Random
{
    /// <summary>
    /// Random generation utilities for DryFish.ILib
    /// </summary>
    public static class ILibRandom
    {
        private static readonly System.Random _random = new System.Random();
        
        /// <summary>
        /// Returns a random element from the specified array
        /// </summary>
        /// <param name="array">Array of strings</param>
        /// <returns>Random element from array</returns>
        public static string IRandomFromArray(string[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array cannot be null or empty");
                
            return array[_random.Next(array.Length)];
        }
        
        /// <summary>
        /// Returns a random element from the specified array (generic)
        /// </summary>
        /// <typeparam name="T">Type of array elements</typeparam>
        /// <param name="array">Array of type T</param>
        /// <returns>Random element from array</returns>
        public static T IRandomFromArray<T>(T[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array cannot be null or empty");
                
            return array[_random.Next(array.Length)];
        }
        
        /// <summary>
        /// Returns a random integer between min and max (inclusive)
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random integer</returns>
        public static int IRandomInt(int min, int max)
        {
            return _random.Next(min, max + 1);
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
        /// <param name="min">Minimum character</param>
        /// <param name="max">Maximum character</param>
        /// <returns>Random character</returns>
        public static char IRandomChar(char min, char max)
        {
            return (char)_random.Next(min, max + 1);
        }
        
        /// <summary>
        /// Returns a random alphabet character between min and max
        /// </summary>
        /// <param name="min">Minimum alphabet (e.g., 'A')</param>
        /// <param name="max">Maximum alphabet (e.g., 'Z')</param>
        /// <returns>Random alphabet character</returns>
        public static char IRandomAlphabet(char min, char max)
        {
            if (!char.IsLetter(min) || !char.IsLetter(max))
                throw new ArgumentException("Only alphabet characters allowed");
                
            return (char)_random.Next(min, max + 1);
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
            if (min > max) throw new ArgumentException("min must be <= max");
            long range = max - min;
            long rand = (long)(_random.NextDouble() * (range + 1));
            return min + rand;
        }
        
        /// <summary>
        /// Returns a random double between min and max
        /// </summary>
        public static double IRandomDouble(double min = 0.0, double max = 1.0)
        {
            if (min > max) throw new ArgumentException("min must be <= max");
            return min + (_random.NextDouble() * (max - min));
        }
        
        /// <summary>
        /// Returns a random item from a list
        /// </summary>
        /// <typeparam name="T">Type of list elements</typeparam>
        /// <param name="list">List of type T</param>
        /// <returns>Random item from list</returns>
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
            string[] colors = { "black", "darkblue", "darkgreen", "darkcyan", "darkred", 
                                "darkmagenta", "darkyellow", "gray", "grey", "darkgray", 
                                "darkgrey", "blue", "green", "cyan", "red", "magenta", 
                                "yellow", "white" };
            return IRandomFromArray(colors);
        }
    }
}
