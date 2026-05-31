
# DryFish.ILib.Random

Random generation utilities for DryFish.ILib - strings, numbers, characters, colors, dates and more.

[![NuGet Version](https://img.shields.io/nuget/v/DryFish.ILib.Random)](https://www.nuget.org/packages/DryFish.ILib.Random)
[![NuGet Downloads](https://img.shields.io/nuget/dt/DryFish.ILib.Random)](https://www.nuget.org/packages/DryFish.ILib.Random)
[![License](https://img.shields.io/github/license/dryfish09/ILib.Random)](https://github.com/dryfish09/ILib.Random/blob/main/LICENSE)
[![Build Status](https://github.com/dryfish09/ILib.Random/actions/workflows/ci.yml/badge.svg)](https://github.com/dryfish09/ILib.Random/actions/workflows/ci.yml)

## ✨ Features

- 📝 **String Array Random** - Random element from user-provided array
- 🔢 **Number Generation** - Integers, longs, doubles, booleans
- 🔤 **Character Generation** - Letters, digits, custom ranges
- 🎨 **Color Generation** - Hex colors, console colors
- 📅 **GUID Generation** - Random unique identifiers
- 📚 **Collection Utilities** - Random items from lists
- 🔒 **Thread-Safe** - Safe for multi-threaded applications
- ⚡ **Lightweight** - Zero external dependencies
- 🔧 **Cross-Platform** - Supports .NET 6/7/8, .NET Standard 2.0, .NET Framework 4.6.2+

## 📦 Installation

### .NET CLI
```bash
dotnet add package DryFish.ILib.Random
```

### Package Manager
```bash
NuGet\Install-Package DryFish.ILib.Random
```

### PackageReference
```xml
<PackageReference Include="DryFish.ILib.Random" Version="2026.1.0" />
```

## 🚀 Quick Start

```csharp
using DryFish.ILib.Random;

// User provides their own array
string[] names = { "an", "binh", "chi" };
int[] numbers = { 1, 2, 3, 4, 5 };
char[] letters = { 'A', 'B', 'C', 'D', 'E' };

// Random from user's array
string randomName = ILibRandom.IRandomFromArray(names);     // returns "binh"
int randomNumber = ILibRandom.IRandomFromArray(numbers);    // returns 3
char randomLetter = ILibRandom.IRandomFromArray(letters);   // returns 'D'

// Random numbers
int dice = ILibRandom.IRandomInt(1, 6);                     // returns 4
long bigNum = ILibRandom.IRandomLong(1000, 9999);          // returns 5432
double percent = ILibRandom.IRandomDouble(0.0, 1.0);       // returns 0.73
bool isHeads = ILibRandom.IRandomBool();                   // returns true

// Random characters
char upper = ILibRandom.IRandomUppercase();                 // returns 'X'
char lower = ILibRandom.IRandomLowercase();                 // returns 'm'
char alphabet = ILibRandom.IRandomAlphabet('A', 'Z');      // returns 'G'

// Random colors
string hexColor = ILibRandom.IRandomHexColor();             // returns "#FF5733"
string consoleColor = ILibRandom.IRandomConsoleColor();     // returns "cyan"

// Random GUID
string guid = ILibRandom.IRandomGuid();                     // returns "a1b2c3d4-..."

// Random from List
var cities = new List<string> { "Hanoi", "Saigon", "Danang" };
string city = ILibRandom.IRandomItem(cities);               // returns "Saigon"
```

## 📚 API Reference

### Array Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomFromArray(string[] array)` | Random element from string array | `ILibRandom.IRandomFromArray(names)` |
| `IRandomFromArray<T>(T[] array)` | Random element from generic array | `ILibRandom.IRandomFromArray(numbers)` |

### Number Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomInt(int min, int max)` | Random integer | `ILibRandom.IRandomInt(1, 10)` |
| `IRandomInt()` | Random integer (0-100) | `ILibRandom.IRandomInt()` |
| `IRandomLong(long min, long max)` | Random long | `ILibRandom.IRandomLong(1000, 9999)` |
| `IRandomDouble(double min, double max)` | Random double | `ILibRandom.IRandomDouble(0.5, 1.5)` |
| `IRandomBool()` | Random boolean | `ILibRandom.IRandomBool()` |

### Character Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomChar(char min, char max)` | Random character | `ILibRandom.IRandomChar('A', 'Z')` |
| `IRandomAlphabet(char min, char max)` | Random letter only | `ILibRandom.IRandomAlphabet('A', 'Z')` |
| `IRandomUppercase()` | Random uppercase letter | `ILibRandom.IRandomUppercase()` |
| `IRandomLowercase()` | Random lowercase letter | `ILibRandom.IRandomLowercase()` |

### Collection Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomItem<T>(IList<T> list)` | Random item from list | `ILibRandom.IRandomItem(myList)` |

### Color Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomHexColor()` | Random hex color | `ILibRandom.IRandomHexColor()` |
| `IRandomConsoleColor()` | Random console color name | `ILibRandom.IRandomConsoleColor()` |

### Utility Methods

| Method | Description | Example |
|--------|-------------|---------|
| `IRandomGuid()` | Random GUID string | `ILibRandom.IRandomGuid()` |

## 💡 Examples

### Basic Usage
```csharp
using DryFish.ILib.Random;

// User provides their own name array
string[] names = { "An", "Binh", "Chi", "Dung" };
string winner = ILibRandom.IRandomFromArray(names);
Console.WriteLine($"Winner: {winner}");
```

### Random Dice Roll
```csharp
int dice = ILibRandom.IRandomInt(1, 6);
Console.WriteLine($"You rolled: {dice}");
```

### Random Password Generator
```csharp
string[] chars = { "A", "B", "C", "1", "2", "3", "!", "@", "#" };
string password = "";
for (int i = 0; i < 8; i++)
{
    password += ILibRandom.IRandomFromArray(chars);
}
Console.WriteLine($"Password: {password}");
```

### Random Color Chooser
```csharp
string color = ILibRandom.IRandomConsoleColor();
Console.WriteLine($"Random color: {color}");
```

## 🔧 Requirements

- .NET 6.0 or later
- .NET Core 6.0+
- .NET Framework 4.6.2+
- .NET Standard 2.0
- Compatible with Windows, Linux, and macOS

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👤 Author

**DryFish**
- GitHub: [@dryfish09](https://github.com/dryfish09)

## 🙏 Acknowledgments

- Built with .NET 6/7/8 and .NET Standard 2.0
- Thread-safe random implementation
- Inspired by DryFish.ILib

---

Made with ❤️ by DryFish
