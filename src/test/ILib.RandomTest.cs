using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using DryFish.ILib.Random;

namespace DryFish.ILib.Random.Tests;

public class ILibRandomTests
{
    private const int TestIterations = 1000;
    
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
    
    // ========== IRandomChar TESTS ==========
    
    [Theory]
    [InlineData('A', 'Z')]
    [InlineData('a', 'z')]
    [InlineData('0', '9')]
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
    
    // ========== IRandomAlphabet TESTS ==========
    
    [Theory]
    [InlineData('A', 'Z')]
    [InlineData('a', 'z')]
    [InlineData('A', 'F')]
    [InlineData('M', 'Z')]
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
    
    // ========== IRandomDouble TESTS ==========
    
    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(-10.5, 10.5)]
    [InlineData(1.5, 5.5)]
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
        
        // Mỗi số nên xuất hiện khoảng 1000 lần ± 15%
        foreach (var count in counts.Values)
        {
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
            if (ILibRandom.IRandomBool()) trueCount++;
        }
        
        double trueRatio = trueCount / (double)iterations;
        Assert.InRange(trueRatio, 0.45, 0.55);
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
        
        // Assert
        Assert.Contains(randomName, names);
        Assert.InRange(randomAge, 1, 100);
        Assert.InRange(randomGrade, 'A', 'F');
        Assert.IsType<bool>(isActive);
    }
}
