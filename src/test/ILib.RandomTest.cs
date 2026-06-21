using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using DryFish.ILib.Random;

namespace DryFish.ILib.Random.Tests;

public class ILibRandomTests
{
    private const int TestIterations = 1000;
    private const int LargeTestIterations = 10000;
    
    // ========== IRandomFromArray TESTS ==========
    
    [Fact]
    public void IRandomFromArray_ShouldReturnElementFromArray()
    {
        // Arrange
        string[] names = { "an", "bình", "chí" };
        
        // Act
        string result = ILibRandom.IRandomFromArray(names);
        
        // Assert
        Assert.Contains(result, names);
    }
    
    [Fact]
    public void IRandomFromArray_Generic_ShouldReturnElementFromArray()
    {
        // Arrange
        int[] numbers = { 1, 2, 3, 4, 5 };
        
        // Act
        int result = ILibRandom.IRandomFromArray(numbers);
        
        // Assert
        Assert.Contains(result, numbers);
    }
    
    [Fact]
    public void IRandomFromArray_WithNullArray_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomFromArray(null!));
    }
    
    [Fact]
    public void IRandomFromArray_WithEmptyArray_ShouldThrowArgumentException()
    {
        // Arrange
        string[] emptyArray = Array.Empty<string>();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomFromArray(emptyArray));
    }
    
    [Fact]
    public void IRandomFromArray_ShouldReturnAllValuesEventually()
    {
        // Arrange
        string[] names = { "an", "bình", "chí" };
        var results = new HashSet<string>();
        
        // Act
        for (int i = 0; i < TestIterations; i++)
        {
            results.Add(ILibRandom.IRandomFromArray(names));
        }
        
        // Assert
        Assert.Equal(names.Length, results.Count);
    }
    
    // ========== IRandomInt TESTS ==========
    
    [Theory]
    [InlineData(1, 10)]
    [InlineData(0, 100)]
    [InlineData(-10, 10)]
    [InlineData(int.MinValue, int.MaxValue)]
    public void IRandomInt_WithRange_ShouldReturnValueInRange(int min, int max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            int result = ILibRandom.IRandomInt(min, max);
            
            // Assert
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomInt_WithoutParams_ShouldReturnBetween0And100()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            int result = ILibRandom.IRandomInt();
            
            // Assert
            Assert.InRange(result, 0, 100);
        }
    }
    
    [Fact]
    public void IRandomInt_WithMinEqualsMax_ShouldReturnThatValue()
    {
        // Act
        int result = ILibRandom.IRandomInt(42, 42);
        
        // Assert
        Assert.Equal(42, result);
    }
    
    [Fact]
    public void IRandomInt_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomInt(10, 5));
    }
    
    // ========== IRandomChar TESTS ==========
    
    [Theory]
    [InlineData('A', 'Z')]
    [InlineData('a', 'z')]
    [InlineData('0', '9')]
    [InlineData(char.MinValue, char.MaxValue)]
    public void IRandomChar_ShouldReturnValueInRange(char min, char max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            char result = ILibRandom.IRandomChar(min, max);
            
            // Assert
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomChar_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomChar('Z', 'A'));
    }
    
    // ========== IRandomAlphabet TESTS ==========
    
    [Theory]
    [InlineData('A', 'Z')]
    [InlineData('a', 'z')]
    [InlineData('A', 'F')]
    [InlineData('M', 'Z')]
    [InlineData('a', 'f')]
    [InlineData('m', 'z')]
    public void IRandomAlphabet_ShouldReturnOnlyLetters(char min, char max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            char result = ILibRandom.IRandomAlphabet(min, max);
            
            // Assert
            Assert.True(char.IsLetter(result), $"'{result}' is not a letter");
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomAlphabet_WithNonLetter_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomAlphabet('0', '9'));
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomAlphabet('!', '@'));
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomAlphabet('A', 'a')); // Mixed case
    }
    
    [Fact]
    public void IRandomAlphabet_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomAlphabet('Z', 'A'));
    }
    
    // ========== IRandomUppercase TESTS ==========
    
    [Fact]
    public void IRandomUppercase_ShouldReturnOnlyUppercaseLetters()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            char result = ILibRandom.IRandomUppercase();
            
            // Assert
            Assert.True(char.IsUpper(result));
            Assert.True(char.IsLetter(result));
            Assert.InRange(result, 'A', 'Z');
        }
    }
    
    [Fact]
    public void IRandomUppercase_ShouldReturnAllLettersEventually()
    {
        // Arrange
        var results = new HashSet<char>();
        
        // Act
        for (int i = 0; i < 1000; i++)
        {
            results.Add(ILibRandom.IRandomUppercase());
        }
        
        // Assert
        Assert.Equal(26, results.Count);
    }
    
    // ========== IRandomLowercase TESTS ==========
    
    [Fact]
    public void IRandomLowercase_ShouldReturnOnlyLowercaseLetters()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            char result = ILibRandom.IRandomLowercase();
            
            // Assert
            Assert.True(char.IsLower(result));
            Assert.True(char.IsLetter(result));
            Assert.InRange(result, 'a', 'z');
        }
    }
    
    [Fact]
    public void IRandomLowercase_ShouldReturnAllLettersEventually()
    {
        // Arrange
        var results = new HashSet<char>();
        
        // Act
        for (int i = 0; i < 1000; i++)
        {
            results.Add(ILibRandom.IRandomLowercase());
        }
        
        // Assert
        Assert.Equal(26, results.Count);
    }
    
    // ========== IRandomBool TESTS ==========
    
    [Fact]
    public void IRandomBool_ShouldReturnBothTrueAndFalse()
    {
        bool hasTrue = false;
        bool hasFalse = false;
        
        for (int i = 0; i < TestIterations; i++)
        {
            bool result = ILibRandom.IRandomBool();
            if (result) hasTrue = true;
            else hasFalse = true;
            
            if (hasTrue && hasFalse) break;
        }
        
        // Assert
        Assert.True(hasTrue && hasFalse, "Should return both true and false");
    }
    
    // ========== IRandomLong TESTS ==========
    
    [Theory]
    [InlineData(1L, 100L)]
    [InlineData(1000L, 2000L)]
    [InlineData(-100L, 100L)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(long.MinValue, 0L)]
    [InlineData(0L, long.MaxValue)]
    public void IRandomLong_ShouldReturnValueInRange(long min, long max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            long result = ILibRandom.IRandomLong(min, max);
            
            // Assert
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomLong_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomLong(10, 5));
    }
    
    [Fact]
    public void IRandomLong_WithMinEqualsMax_ShouldReturnThatValue()
    {
        // Act
        long result = ILibRandom.IRandomLong(42L, 42L);
        
        // Assert
        Assert.Equal(42L, result);
    }
    
    // ========== IRandomDouble TESTS ==========
    
    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(-10.5, 10.5)]
    [InlineData(1.5, 5.5)]
    [InlineData(double.MinValue / 2, double.MaxValue / 2)]
    public void IRandomDouble_ShouldReturnValueInRange(double min, double max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            double result = ILibRandom.IRandomDouble(min, max);
            
            // Assert
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomDouble_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomDouble(5.5, 1.5));
    }
    
    [Fact]
    public void IRandomDouble_DefaultParams_ShouldReturnBetween0And1()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            double result = ILibRandom.IRandomDouble();
            
            // Assert
            Assert.InRange(result, 0.0, 1.0);
        }
    }
    
    [Fact]
    public void IRandomDouble_WithMinEqualsMax_ShouldReturnThatValue()
    {
        // Act
        double result = ILibRandom.IRandomDouble(3.14, 3.14);
        
        // Assert
        Assert.Equal(3.14, result);
    }
    
    // ========== IRandomDecimal TESTS ==========
    
    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(-10.5, 10.5)]
    [InlineData(1.5m, 5.5m)]
    [InlineData(-1000m, 1000m)]
    public void IRandomDecimal_ShouldReturnValueInRange(decimal min, decimal max)
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            decimal result = ILibRandom.IRandomDecimal(min, max);
            
            // Assert
            Assert.InRange(result, min, max);
        }
    }
    
    [Fact]
    public void IRandomDecimal_WithMinGreaterThanMax_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomDecimal(5.5m, 1.5m));
    }
    
    [Fact]
    public void IRandomDecimal_WithMinEqualsMax_ShouldReturnThatValue()
    {
        // Act
        decimal result = ILibRandom.IRandomDecimal(3.14m, 3.14m);
        
        // Assert
        Assert.Equal(3.14m, result);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(28)]
    public void IRandomDecimal_WithPrecision_ShouldRespectPrecision(int precision)
    {
        // Arrange
        decimal min = 0m;
        decimal max = 100m;
        
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            decimal result = ILibRandom.IRandomDecimal(min, max, precision);
            
            // Assert
            Assert.InRange(result, min, max);
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(result)[3])[2];
            Assert.True(decimalPlaces <= precision, $"Result {result} has {decimalPlaces} decimal places, expected <= {precision}");
        }
    }
    
    [Fact]
    public void IRandomDecimal_WithInvalidPrecision_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomDecimal(0m, 1m, -1));
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomDecimal(0m, 1m, 29));
    }
    
    // ========== IRandomItem TESTS ==========
    
    [Fact]
    public void IRandomItem_WithList_ShouldReturnElementFromList()
    {
        // Arrange
        var list = new List<string> { "apple", "banana", "cherry" };
        
        // Act
        string result = ILibRandom.IRandomItem(list);
        
        // Assert
        Assert.Contains(result, list);
    }
    
    [Fact]
    public void IRandomItem_WithEmptyList_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyList = new List<string>();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomItem(emptyList));
    }
    
    [Fact]
    public void IRandomItem_WithNullList_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomItem<string>(null!));
    }
    
    [Fact]
    public void IRandomItem_WithListOfInts_ShouldReturnValidElement()
    {
        // Arrange
        var numbers = new List<int> { 10, 20, 30, 40, 50 };
        
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            int result = ILibRandom.IRandomItem(numbers);
            
            // Assert
            Assert.Contains(result, numbers);
        }
    }
    
    // ========== IRandomEnum TESTS ==========
    
    private enum TestEnum
    {
        Value1,
        Value2,
        Value3,
        Value4,
        Value5
    }
    
    private enum SingleValueEnum
    {
        OnlyValue
    }
    
    [Fact]
    public void IRandomEnum_ShouldReturnValidEnumValue()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            TestEnum result = ILibRandom.IRandomEnum<TestEnum>();
            
            // Assert
            Assert.True(Enum.IsDefined(typeof(TestEnum), result));
        }
    }
    
    [Fact]
    public void IRandomEnum_ShouldReturnAllEnumValuesEventually()
    {
        // Arrange
        var results = new HashSet<TestEnum>();
        
        // Act
        for (int i = 0; i < 1000; i++)
        {
            results.Add(ILibRandom.IRandomEnum<TestEnum>());
        }
        
        // Assert
        Assert.Equal(5, results.Count);
    }
    
    [Fact]
    public void IRandomEnum_WithExclusion_ShouldNotReturnExcludedValue()
    {
        // Arrange
        TestEnum excluded = TestEnum.Value3;
        
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            TestEnum result = ILibRandom.IRandomEnum(excluded);
            
            // Assert
            Assert.NotEqual(excluded, result);
            Assert.True(Enum.IsDefined(typeof(TestEnum), result));
        }
    }
    
    [Fact]
    public void IRandomEnum_WithExclusion_ShouldStillReturnOtherValues()
    {
        // Arrange
        TestEnum excluded = TestEnum.Value3;
        var results = new HashSet<TestEnum>();
        
        // Act
        for (int i = 0; i < 1000; i++)
        {
            results.Add(ILibRandom.IRandomEnum(excluded));
        }
        
        // Assert
        Assert.Contains(TestEnum.Value1, results);
        Assert.Contains(TestEnum.Value2, results);
        Assert.Contains(TestEnum.Value4, results);
        Assert.Contains(TestEnum.Value5, results);
        Assert.DoesNotContain(TestEnum.Value3, results);
    }
    
    [Fact]
    public void IRandomEnum_WithAllValuesExcluded_ShouldThrowArgumentException()
    {
        // Arrange - SingleValueEnum only has one value
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ILibRandom.IRandomEnum(SingleValueEnum.OnlyValue));
    }
    
    // ========== IRandomGuid TESTS ==========
    
    [Fact]
    public void IRandomGuid_ShouldReturnValidGuidString()
    {
        // Act
        string result = ILibRandom.IRandomGuid();
        
        // Assert
        Assert.True(Guid.TryParse(result, out _));
    }
    
    [Fact]
    public void IRandomGuid_ShouldReturnDifferentValues()
    {
        // Arrange
        var results = new HashSet<string>();
        
        // Act
        for (int i = 0; i < 100; i++)
        {
            results.Add(ILibRandom.IRandomGuid());
        }
        
        // Assert
        Assert.Equal(100, results.Count);
    }
    
    // ========== IRandomHexColor TESTS ==========
    
    [Fact]
    public void IRandomHexColor_ShouldReturnValidHexColor()
    {
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            string result = ILibRandom.IRandomHexColor();
            
            // Assert
            Assert.StartsWith("#", result);
            Assert.Equal(7, result.Length);
            Assert.True(int.TryParse(result.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out _));
        }
    }
    
    // ========== IRandomConsoleColor TESTS ==========
    
    [Fact]
    public void IRandomConsoleColor_ShouldReturnValidConsoleColor()
    {
        // Arrange
        var validColors = new HashSet<string> 
        { 
            "black", "darkblue", "darkgreen", "darkcyan", "darkred", 
            "darkmagenta", "darkyellow", "gray", "grey", "darkgray", 
            "darkgrey", "blue", "green", "cyan", "red", "magenta", 
            "yellow", "white"
        };
        
        for (int i = 0; i < TestIterations; i++)
        {
            // Act
            string result = ILibRandom.IRandomConsoleColor();
            
            // Assert
            Assert.Contains(result, validColors);
        }
    }
    
    // ========== EDGE CASE TESTS ==========
    
    [Fact]
    public void RandomDistribution_ShouldBeRoughlyUniform()
    {
        // Test với 6 mặt (như xúc xắc)
        var counts = new Dictionary<int, int>();
        int min = 1, max = 6;
        
        for (int i = 0; i < 6000; i++)
        {
            int result = ILibRandom.IRandomInt(min, max);
            counts[result] = counts.GetValueOrDefault(result) + 1;
        }
        
        // Mỗi số nên xuất hiện khoảng 1000 lần ± 20%
        foreach (var count in counts.Values)
        {
            Assert.InRange(count, 800, 1200);
        }
    }
    
    [Fact]
    public void BoolDistribution_ShouldBeApproximatelyFiftyFifty()
    {
        int trueCount = 0;
        int iterations = 10000;
        
        for (int i = 0; i < iterations; i++)
        {
            if (ILibRandom.IRandomBool()) trueCount++;
        }
        
        double trueRatio = trueCount / (double)iterations;
        Assert.InRange(trueRatio, 0.45, 0.55);
    }
    
    [Fact]
    public void LongDistribution_ShouldBeUniform()
    {
        // Arrange
        var counts = new Dictionary<long, int>();
        long min = 0, max = 9;
        
        // Act
        for (int i = 0; i < 10000; i++)
        {
            long result = ILibRandom.IRandomLong(min, max);
            counts[result] = counts.GetValueOrDefault(result) + 1;
        }
        
        // Assert - mỗi số nên xuất hiện khoảng 1000 lần ± 20%
        foreach (var count in counts.Values)
        {
            Assert.InRange(count, 800, 1200);
        }
    }
    
    // ========== PERFORMANCE TESTS ==========
    
    [Fact]
    public void IRandomEnum_ShouldBeFast()
    {
        // Arrange
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // Act
        for (int i = 0; i < 10000; i++)
        {
            _ = ILibRandom.IRandomEnum<TestEnum>();
        }
        
        stopwatch.Stop();
        
        // Assert - should complete in under 100ms
        Assert.True(stopwatch.ElapsedMilliseconds < 100, 
            $"Took {stopwatch.ElapsedMilliseconds}ms for 10,000 enum randomizations");
    }
    
    // ========== INTEGRATION TESTS ==========
    
    [Fact]
    public void MultipleMethods_ShouldWorkTogether()
    {
        // Arrange
        string[] names = { "an", "bình", "chí" };
        
        // Act - kết hợp nhiều methods
        string randomName = ILibRandom.IRandomFromArray(names);
        int randomAge = ILibRandom.IRandomInt(1, 100);
        char randomGrade = ILibRandom.IRandomAlphabet('A', 'F');
        bool isActive = ILibRandom.IRandomBool();
        decimal randomPrice = ILibRandom.IRandomDecimal(0m, 100m, 2);
        TestEnum randomEnum = ILibRandom.IRandomEnum<TestEnum>();
        string randomColor = ILibRandom.IRandomConsoleColor();
        
        // Assert
        Assert.Contains(randomName, names);
        Assert.InRange(randomAge, 1, 100);
        Assert.InRange(randomGrade, 'A', 'F');
        Assert.IsType<bool>(isActive);
        Assert.InRange(randomPrice, 0m, 100m);
        Assert.True(Enum.IsDefined(typeof(TestEnum), randomEnum));
        Assert.NotNull(randomColor);
    }
}
