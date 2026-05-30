using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DryFish.ILib.Random.Tests;

public class IRandomTests
{
    private const int TestIterations = 1000;

    // ========== STRING METHOD TESTS ==========

    public class StringTests
    {
        [Fact]
        public void FromArray_ShouldReturnElementFromArray()
        {
            // Arrange
            string[] array = { "apple", "banana", "cherry" };

            // Act
            string result = IRandom.FromArray(array);

            // Assert
            Assert.Contains(result, array);
        }

        [Fact]
        public void FromArray_WithEmptyArray_ShouldThrowArgumentException()
        {
            // Arrange
            string[] emptyArray = Array.Empty<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.FromArray(emptyArray));
        }

        [Fact]
        public void FromArray_WithNullArray_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.FromArray(null!));
        }

        [Fact]
        public void FromList_ShouldReturnElementFromList()
        {
            // Arrange
            var list = new List<string> { "red", "green", "blue" };

            // Act
            string result = IRandom.FromList(list);

            // Assert
            Assert.Contains(result, list);
        }

        [Fact]
        public void VietnameseName_ShouldReturnValidName()
        {
            // Act
            string name = IRandom.VietnameseName();

            // Assert
            Assert.NotEmpty(name);
            Assert.Contains(" ", name); // Should have space between last and first name
        }

        [Fact]
        public void Guid_ShouldReturnValidGuidString()
        {
            // Act
            string guidString = IRandom.Guid();

            // Assert
            Assert.NotNull(guidString);
            Assert.True(Guid.TryParse(guidString, out _));
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void String_WithDefaultParams_ShouldReturnCorrectLength(int length)
        {
            // Act
            string result = IRandom.String(length);

            // Assert
            Assert.Equal(length, result.Length);
            // Should contain alphanumeric only (default: numbers + lowercase + uppercase)
            Assert.All(result, c => Assert.True(char.IsLetterOrDigit(c)));
        }

        [Fact]
        public void String_WithNumbersOnly_ShouldReturnOnlyDigits()
        {
            // Act
            string result = IRandom.String(10, includeNumbers: true, includeLowercase: false, includeUppercase: false);

            // Assert
            Assert.All(result, c => Assert.True(char.IsDigit(c)));
        }

        [Fact]
        public void String_WithLowercaseOnly_ShouldReturnOnlyLowercaseLetters()
        {
            // Act
            string result = IRandom.String(10, includeNumbers: false, includeLowercase: true, includeUppercase: false);

            // Assert
            Assert.All(result, c => Assert.True(char.IsLower(c)));
        }

        [Fact]
        public void String_WithUppercaseOnly_ShouldReturnOnlyUppercaseLetters()
        {
            // Act
            string result = IRandom.String(10, includeNumbers: false, includeLowercase: false, includeUppercase: true);

            // Assert
            Assert.All(result, c => Assert.True(char.IsUpper(c)));
        }

        [Fact]
        public void String_WithZeroLength_ShouldReturnEmptyString()
        {
            // Act
            string result = IRandom.String(0);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void String_WithNoCharSets_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                IRandom.String(5, false, false, false, false));
        }
    }

    // ========== NUMBER METHOD TESTS ==========

    public class NumberTests
    {
        [Theory]
        [InlineData(1, 10)]
        [InlineData(0, 100)]
        [InlineData(-10, 10)]
        public void Int_WithRange_ShouldReturnValueInRange(int min, int max)
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                int result = IRandom.Int(min, max);

                // Assert
                Assert.InRange(result, min, max);
            }
        }

        [Fact]
        public void Int_WithoutParams_ShouldReturnBetween0And100()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                int result = IRandom.Int();

                // Assert
                Assert.InRange(result, 0, 100);
            }
        }

        [Theory]
        [InlineData(1, 1000)]
        [InlineData(1000, 2000)]
        public void Long_ShouldReturnValueInRange(long min, long max)
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                long result = IRandom.Long(min, max);

                // Assert
                Assert.InRange(result, min, max);
            }
        }

        [Fact]
        public void Long_WithMinGreaterThanMax_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.Long(10, 5));
        }

        [Theory]
        [InlineData(0.0, 1.0)]
        [InlineData(-10.5, 10.5)]
        [InlineData(5.5, 5.5)]
        public void Double_ShouldReturnValueInRange(double min, double max)
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                double result = IRandom.Double(min, max);

                // Assert
                Assert.InRange(result, min, max);
            }
        }

        [Fact]
        public void Double_WithMinGreaterThanMax_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.Double(10.5, 5.5));
        }

        [Fact]
        public void Bool_ShouldReturnBothTrueAndFalse()
        {
            bool hasTrue = false;
            bool hasFalse = false;

            for (int i = 0; i < TestIterations; i++)
            {
                bool result = IRandom.Bool();
                if (result) hasTrue = true;
                else hasFalse = true;

                if (hasTrue && hasFalse) break;
            }

            // Assert
            Assert.True(hasTrue && hasFalse, "Bool should return both true and false values");
        }
    }

    // ========== CHARACTER METHOD TESTS ==========

    public class CharacterTests
    {
        [Theory]
        [InlineData('A', 'Z')]
        [InlineData('a', 'z')]
        [InlineData('0', '9')]
        public void Char_ShouldReturnValueInRange(char min, char max)
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                char result = IRandom.Char(min, max);

                // Assert
                Assert.InRange(result, min, max);
            }
        }

        [Fact]
        public void Char_WithMinGreaterThanMax_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.Char('Z', 'A'));
        }

        [Fact]
        public void UppercaseLetter_ShouldReturnOnlyUppercaseLetters()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                char result = IRandom.UppercaseLetter();

                // Assert
                Assert.True(char.IsUpper(result), $"'{result}' is not uppercase");
                Assert.True(char.IsLetter(result), $"'{result}' is not a letter");
            }
        }

        [Fact]
        public void LowercaseLetter_ShouldReturnOnlyLowercaseLetters()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                char result = IRandom.LowercaseLetter();

                // Assert
                Assert.True(char.IsLower(result), $"'{result}' is not lowercase");
                Assert.True(char.IsLetter(result), $"'{result}' is not a letter");
            }
        }

        [Fact]
        public void Letter_ShouldReturnBothUppercaseAndLowercase()
        {
            bool hasUpper = false;
            bool hasLower = false;

            for (int i = 0; i < TestIterations; i++)
            {
                char result = IRandom.Letter();
                if (char.IsUpper(result)) hasUpper = true;
                else if (char.IsLower(result)) hasLower = true;

                if (hasUpper && hasLower) break;
            }

            // Assert
            Assert.True(hasUpper && hasLower, "Letter should return both uppercase and lowercase letters");
        }

        [Fact]
        public void Digit_ShouldReturnOnlyDigits()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                char result = IRandom.Digit();

                // Assert
                Assert.True(char.IsDigit(result), $"'{result}' is not a digit");
            }
        }
    }

    // ========== COLLECTION METHOD TESTS ==========

    public class CollectionTests
    {
        [Fact]
        public void Item_WithList_ShouldReturnElementFromList()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            int result = IRandom.Item(list);

            // Assert
            Assert.Contains(result, list);
        }

        [Fact]
        public void Item_WithArray_ShouldReturnElementFromArray()
        {
            // Arrange
            int[] array = { 10, 20, 30, 40, 50 };

            // Act
            int result = IRandom.Item(array);

            // Assert
            Assert.Contains(result, array);
        }

        [Fact]
        public void Item_WithEmptyList_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyList = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.Item(emptyList));
        }

        [Fact]
        public void Item_WithNullList_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.Item<string>(null!));
        }

        [Fact]
        public void Shuffle_ShouldReturnListWithSameElements()
        {
            // Arrange
            var original = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Act
            var shuffled = IRandom.Shuffle(original);

            // Assert
            Assert.Equal(original.Count, shuffled.Count);
            Assert.Equal(original.OrderBy(x => x), shuffled.OrderBy(x => x));
        }

        [Fact]
        public void Shuffle_ShouldNotReturnSameOrderAlways()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            bool isDifferent = false;

            // Act - shuffle multiple times to check for different ordering
            for (int i = 0; i < 50; i++)
            {
                var shuffled = IRandom.Shuffle(list);
                if (!shuffled.SequenceEqual(list))
                {
                    isDifferent = true;
                    break;
                }
            }

            // Assert
            Assert.True(isDifferent, "Shuffle should change order at least sometimes");
        }
    }

    // ========== COLOR METHOD TESTS ==========

    public class ColorTests
    {
        [Fact]
        public void HexColor_ShouldReturnValidHexColor()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                string hexColor = IRandom.HexColor();

                // Assert
                Assert.StartsWith("#", hexColor);
                Assert.Equal(7, hexColor.Length); // # + 6 hex digits
                Assert.Matches("^#[0-9A-F]{6}$", hexColor);
            }
        }

        [Fact]
        public void ConsoleColor_ShouldReturnValidConsoleColor()
        {
            var validColors = new HashSet<string>
            {
                "black", "darkblue", "darkgreen", "darkcyan", "darkred", "darkmagenta", "darkyellow",
                "gray", "grey", "darkgray", "darkgrey", "blue", "green", "cyan", "red",
                "magenta", "yellow", "white"
            };

            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                string color = IRandom.ConsoleColor();

                // Assert
                Assert.Contains(color, validColors);
            }
        }
    }

    // ========== DATE & TIME METHOD TESTS ==========

    public class DateTimeTests
    {
        [Fact]
        public void DateTime_ShouldReturnDateInRange()
        {
            // Arrange
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 12, 31);

            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                DateTime result = IRandom.DateTime(start, end);

                // Assert
                Assert.InRange(result, start, end);
            }
        }

        [Fact]
        public void DateTime_WithSameStartAndEnd_ShouldReturnThatDate()
        {
            // Arrange
            var date = new DateTime(2024, 6, 15);

            // Act
            DateTime result = IRandom.DateTime(date, date);

            // Assert
            Assert.Equal(date, result);
        }

        [Fact]
        public void DateTime_WithStartGreaterThanEnd_ShouldThrowArgumentException()
        {
            // Arrange
            var start = new DateTime(2024, 12, 31);
            var end = new DateTime(2024, 1, 1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => IRandom.DateTime(start, end));
        }

        [Fact]
        public void DateTime_ShouldReturnDifferentValues()
        {
            // Arrange
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 12, 31);
            var results = new HashSet<DateTime>();

            for (int i = 0; i < 100; i++)
            {
                results.Add(IRandom.DateTime(start, end));
            }

            // Assert - with 100 random dates in a year range, likely multiple distinct values
            Assert.True(results.Count > 1, "Should generate multiple distinct dates");
        }
    }

    // ========== ENUM METHOD TESTS ==========

    public class EnumTests
    {
        public enum TestEnum
        {
            Value1,
            Value2,
            Value3,
            Value4,
            Value5
        }

        [Fact]
        public void Enum_ShouldReturnValidEnumValue()
        {
            var expectedValues = Enum.GetValues<TestEnum>();

            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                TestEnum result = IRandom.Enum<TestEnum>();

                // Assert
                Assert.Contains(result, expectedValues);
            }
        }

        [Fact]
        public void Enum_ShouldReturnAllValuesEventually()
        {
            var foundValues = new HashSet<TestEnum>();
            var allValues = Enum.GetValues<TestEnum>();

            for (int i = 0; i < TestIterations && foundValues.Count < allValues.Length; i++)
            {
                foundValues.Add(IRandom.Enum<TestEnum>());
            }

            // Assert
            Assert.Equal(allValues.Length, foundValues.Count);
        }

        [Fact]
        public void Enum_WithDayOfWeek_ShouldReturnValidDay()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                // Act
                var result = IRandom.Enum<DayOfWeek>();

                // Assert
                Assert.InRange((int)result, 0, 6);
            }
        }
    }

    // ========== DISTRIBUTION TESTS ==========

    public class DistributionTests
    {
        [Fact]
        public void RandomDistribution_ShouldBeRoughlyUniform()
        {
            // Test that Int distribution is roughly uniform
            var counts = new Dictionary<int, int>();
            int min = 1, max = 6; // Like a dice roll
            
            for (int i = 0; i < 6000; i++)
            {
                int result = IRandom.Int(min, max);
                counts[result] = counts.GetValueOrDefault(result) + 1;
            }
            
            // Expected average: 1000 rolls per number (6000/6)
            foreach (var count in counts.Values)
            {
                // Allow 15% deviation (850-1150)
                Assert.InRange(count, 850, 1150);
            }
        }

        [Fact]
        public void BoolDistribution_ShouldBeApproximatelyFiftyFifty()
        {
            int trueCount = 0;
            int iterations = 10000;
            
            for (int i = 0; i < iterations; i++)
            {
                if (IRandom.Bool()) trueCount++;
            }
            
            double trueRatio = trueCount / (double)iterations;
            // Should be between 45% and 55%
            Assert.InRange(trueRatio, 0.45, 0.55);
        }
    }

    // ========== EDGE CASE TESTS ==========

    public class EdgeCaseTests
    {
        [Fact]
        public void Int_WithMinEqualsMax_ShouldReturnThatValue()
        {
            // Act
            int result = IRandom.Int(42, 42);

            // Assert
            Assert.Equal(42, result);
        }

        [Fact]
        public void Long_WithMinEqualsMax_ShouldReturnThatValue()
        {
            // Act
            long result = IRandom.Long(100, 100);

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void Double_WithMinEqualsMax_ShouldReturnThatValue()
        {
            // Act
            double result = IRandom.Double(3.14, 3.14);

            // Assert
            Assert.Equal(3.14, result);
        }

        [Fact]
        public void String_WithLargeLength_ShouldHandleGracefully()
        {
            // Act
            string result = IRandom.String(10000);

            // Assert
            Assert.Equal(10000, result.Length);
        }
    }
}
