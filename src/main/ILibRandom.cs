using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

        // Cache byte array for performance
        [ThreadStatic]
        private static byte[]? _byteBuffer;

        private static byte[] GetByteBuffer()
        {
            if (_byteBuffer == null)
            {
                _byteBuffer = new byte[8];
            }
            return _byteBuffer;
        }

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
                
#if NET6_0_OR_GREATER
            // Safe calculation avoiding overflow when max = int.MaxValue
            return (int)(min + System.Random.Shared.NextInt64((long)max - min + 1));
#else
            double range = (double)max - (double)min;
            int offset = (int)(_random.NextDouble() * (range + 1.0));
            return min + offset;
#endif
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
                
            // Validate both characters are letters
            if (!char.IsLetter(min) || !char.IsLetter(max))
                throw new ArgumentException("Both min and max must be alphabet characters");
            
            // Validate the range only contains letters
            // Check if min and max are both uppercase or both lowercase
            bool minIsUpper = char.IsUpper(min);
            bool maxIsUpper = char.IsUpper(max);
            
            if (minIsUpper != maxIsUpper)
                throw new ArgumentException("min and max must be both uppercase or both lowercase");
            
            // Validate range doesn't include non-letter characters
            // For uppercase: A-Z only (65-90)
            // For lowercase: a-z only (97-122)
            int minValue = (int)min;
            int maxValue = (int)max;
            
            if (minIsUpper)
            {
                if (minValue < 'A' || maxValue > 'Z')
                    throw new ArgumentException("Range must be within A-Z for uppercase letters");
            }
            else
            {
                if (minValue < 'a' || maxValue > 'z')
                    throw new ArgumentException("Range must be within a-z for lowercase letters");
            }
            
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
        /// Returns a random long between min and max (inclusive)
        /// </summary>
        public static long IRandomLong(long min, long max)
        {
            if (min > max) 
                throw new ArgumentException("min must be <= max");
            
            if (min == max)
                return min;

#if NET6_0_OR_GREATER
            // Handle max == long.MaxValue case safely
            if (max < long.MaxValue)
            {
                return System.Random.Shared.NextInt64(min, max + 1);
            }

            // For max == long.MaxValue, we need special handling
            ulong range = (ulong)(max - min);
            if (range == ulong.MaxValue)
            {
                Span<byte> buffer = stackalloc byte[8];
                System.Random.Shared.NextBytes(buffer);
                return BitConverter.ToInt64(buffer);
            }

            // Rejection sampling for uniform distribution
            ulong limit = ulong.MaxValue - ulong.MaxValue % (range + 1);
            Span<byte> bytes = stackalloc byte[8];
            ulong uval;
            do
            {
                System.Random.Shared.NextBytes(bytes);
                uval = BitConverter.ToUInt64(bytes);
            } while (uval >= limit);  // Use >= for perfect uniformity

            return min + (long)(uval % (range + 1));
#else
            // For older frameworks, use rejection sampling to avoid bias
            ulong range = (ulong)(max - min);
            
            // Handle full range (ulong.MaxValue)
            if (range == ulong.MaxValue)
            {
                byte[] buffer = GetByteBuffer();
                _random.NextBytes(buffer);
                return BitConverter.ToInt64(buffer, 0);
            }
            
            // Rejection sampling with >= for perfect uniformity
            ulong limit = ulong.MaxValue - ulong.MaxValue % (range + 1);
            byte[] bytes = GetByteBuffer();
            ulong uval;
            
            do
            {
                _random.NextBytes(bytes);
                uval = BitConverter.ToUInt64(bytes, 0);
            } while (uval >= limit);  // Use >= for perfect uniformity
            
            return min + (long)(uval % (range + 1));
#endif
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
        /// Returns a random decimal between min and max
        /// </summary>
        public static decimal IRandomDecimal(decimal min, decimal max)
        {
            if (min > max)
                throw new ArgumentException("min must be <= max");
            
            if (min == max)
                return min;
            
            // Get a random double and convert to decimal
            double randomDouble = _random.NextDouble();
            decimal randomDecimal = (decimal)randomDouble;
            
            // Scale to the range
            decimal range = max - min;
            return min + (randomDecimal * range);
        }
        
        /// <summary>
        /// Returns a random decimal between min and max with specified precision
        /// </summary>
        public static decimal IRandomDecimal(decimal min, decimal max, int precision)
        {
            if (min > max)
                throw new ArgumentException("min must be <= max");
            
            if (precision < 0 || precision > 28)
                throw new ArgumentException("precision must be between 0 and 28");
            
            if (min == max)
                return min;
            
            // Safer approach: generate random decimal and round to precision
            decimal randomValue = IRandomDecimal(min, max);
            return Math.Round(randomValue, precision, MidpointRounding.AwayFromZero);
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

        // Generic static class for caching enum values
        private static class EnumCache<T> where T : Enum
        {
            public static readonly T[] Values = (T[])Enum.GetValues(typeof(T));
        }

        /// <summary>
        /// Returns a random element from an enumeration
        /// </summary>
        public static T IRandomEnum<T>() where T : Enum
        {
            var values = EnumCache<T>.Values;
            if (values.Length == 0)
                throw new ArgumentException("Enum has no values");
            return values[_random.Next(values.Length)];
        }

        /// <summary>
        /// Returns a random element from an enumeration with exclusion
        /// </summary>
        public static T IRandomEnum<T>(T exclude) where T : Enum
        {
            var values = EnumCache<T>.Values;
            if (values.Length == 0)
                throw new ArgumentException("Enum has no values");

            // Check if there's at least one non-excluded value
            bool hasOther = false;
            foreach (var val in values)
            {
                if (!val.Equals(exclude))
                {
                    hasOther = true;
                    break;
                }
            }
            if (!hasOther)
                throw new ArgumentException("All values in the enum are excluded");

            // Random selection with rejection
            T result;
            do
            {
                result = values[_random.Next(values.Length)];
            } while (result.Equals(exclude));
            return result;
        }
    }
}
