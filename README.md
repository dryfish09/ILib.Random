
# DryFish.ILib.Random

Random generation utilities for DryFish.ILib - strings, numbers, characters, colors, enums, decimals and more.

[![NuGet Version](https://img.shields.io/nuget/v/DryFish.ILib.Random)](https://www.nuget.org/packages/DryFish.ILib.Random)
[![NuGet Downloads](https://img.shields.io/nuget/dt/DryFish.ILib.Random)](https://www.nuget.org/packages/DryFish.ILib.Random)
[![License](https://img.shields.io/github/license/dryfish09/ILib.Random)](https://github.com/dryfish09/ILib.Random/blob/main/LICENSE)
[![Build Status](https://github.com/dryfish09/ILib.Random/actions/workflows/ci.yml/badge.svg)](https://github.com/dryfish09/ILib.Random/actions/workflows/ci.yml)

## ✨ Features

- 📝 **String Array Random** - Random element from user-provided array
- 🔢 **Number Generation** - Integers, longs, doubles, decimals, booleans
- 🔤 **Character Generation** - Letters, digits, custom ranges with validation
- 🎨 **Color Generation** - Hex colors, console colors
- 📅 **GUID Generation** - Random unique identifiers
- 📚 **Collection Utilities** - Random items from lists and arrays
- 🏷️ **Enum Support** - Random enum values with exclusion support
- 🔒 **Thread-Safe** - Safe for multi-threaded applications
- ⚡ **Lightweight** - Zero external dependencies
- 🚀 **High Performance** - Optimized with caching for enum and byte operations
- 🔧 **Cross-Platform** - Supports .NET 6/7/8/9, .NET Standard 2.0, .NET Framework 4.6.2+

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
<PackageReference Include="DryFish.ILib.Random" Version="2026.2.0" />
```

## 🚀 Quick Start

```csharp
using DryFish.ILib.Random;

// User provides their own array
string[] names = { "an", "binh", "chi" };
int[] numbers = { 1, 2, 3, 4, 5 };

// Random from user's array
string randomName = ILibRandom.IRandomFromArray(names);     // returns "binh"
int randomNumber = ILibRandom.IRandomFromArray(numbers);    // returns 3

// Random numbers
int dice = ILibRandom.IRandomInt(1, 6);                     // returns 4
long bigNum = ILibRandom.IRandomLong(1000, 9999);          // returns 5432
double percent = ILibRandom.IRandomDouble(0.0, 1.0);       // returns 0.73
decimal price = ILibRandom.IRandomDecimal(0m, 100m, 2);    // returns 42.50
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

// Random Enum
enum Status { Active, Inactive, Pending }
Status status = ILibRandom.IRandomEnum<Status>();          // returns Inactive
Status statusExcludingPending = ILibRandom.IRandomEnum(Status.Pending); // returns Active or Inactive
```

## 📚 API Reference

### Array Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomFromArray(string[] array)` | Random element from string array | `ILibRandom.IRandomFromArray(names)` | `ArgumentException` if null/empty |
| `IRandomFromArray<T>(T[] array)` | Random element from generic array | `ILibRandom.IRandomFromArray(numbers)` | `ArgumentException` if null/empty |

### Number Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomInt(int min, int max)` | Random integer (inclusive) | `ILibRandom.IRandomInt(1, 10)` | `ArgumentException` if min > max |
| `IRandomInt()` | Random integer (0-100) | `ILibRandom.IRandomInt()` | - |
| `IRandomLong(long min, long max)` | Random long (inclusive) | `ILibRandom.IRandomLong(1000, 9999)` | `ArgumentException` if min > max |
| `IRandomDouble(double min, double max)` | Random double | `ILibRandom.IRandomDouble(0.5, 1.5)` | `ArgumentException` if min > max |
| `IRandomBool()` | Random boolean | `ILibRandom.IRandomBool()` | - |

### Decimal Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomDecimal(decimal min, decimal max)` | Random decimal | `ILibRandom.IRandomDecimal(0m, 100m)` | `ArgumentException` if min > max |
| `IRandomDecimal(decimal min, decimal max, int precision)` | Random decimal with precision | `ILibRandom.IRandomDecimal(0m, 100m, 2)` | `ArgumentException` if min > max or precision invalid (0-28) |

### Character Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomChar(char min, char max)` | Random character | `ILibRandom.IRandomChar('A', 'Z')` | `ArgumentException` if min > max |
| `IRandomAlphabet(char min, char max)` | Random letter only (A-Z or a-z) | `ILibRandom.IRandomAlphabet('A', 'Z')` | `ArgumentException` if invalid range |
| `IRandomUppercase()` | Random uppercase letter (A-Z) | `ILibRandom.IRandomUppercase()` | - |
| `IRandomLowercase()` | Random lowercase letter (a-z) | `ILibRandom.IRandomLowercase()` | - |

### Collection Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomItem<T>(IList<T> list)` | Random item from list | `ILibRandom.IRandomItem(myList)` | `ArgumentException` if null/empty |

### Enum Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomEnum<T>() where T : Enum` | Random enum value | `ILibRandom.IRandomEnum<Status>()` | `ArgumentException` if enum empty |
| `IRandomEnum<T>(T exclude) where T : Enum` | Random enum value excluding one | `ILibRandom.IRandomEnum(Status.Pending)` | `ArgumentException` if all excluded |

### Color Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomHexColor()` | Random hex color (#RRGGBB) | `ILibRandom.IRandomHexColor()` | - |
| `IRandomConsoleColor()` | Random console color name | `ILibRandom.IRandomConsoleColor()` | - |

### Utility Methods

| Method | Description | Example | Throws |
|--------|-------------|---------|--------|
| `IRandomGuid()` | Random GUID string | `ILibRandom.IRandomGuid()` | - |

## 💡 Examples

### Basic Usage with User-Provided Arrays
```csharp
using DryFish.ILib.Random;

// User provides their own arrays
string[] names = { "An", "Binh", "Chi", "Dung" };
string winner = ILibRandom.IRandomFromArray(names);
Console.WriteLine($"Winner: {winner}");

int[] scores = { 95, 87, 76, 91, 88 };
int randomScore = ILibRandom.IRandomFromArray(scores);
Console.WriteLine($"Random score: {randomScore}");
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
Console.WriteLine($"Random console color: {color}");

string hex = ILibRandom.IRandomHexColor();
Console.WriteLine($"Random hex color: {hex}");
```

### Random Decimal with Precision
```csharp
// Random price with 2 decimal places
decimal price = ILibRandom.IRandomDecimal(0m, 100m, 2);
Console.WriteLine($"Price: ${price:F2}");

// Random temperature with 1 decimal place
decimal temperature = ILibRandom.IRandomDecimal(-10m, 40m, 1);
Console.WriteLine($"Temperature: {temperature:F1}°C");
```

### Random Enum Values
```csharp
enum UserRole { Admin, Moderator, User, Guest }

// Random role
UserRole role = ILibRandom.IRandomEnum<UserRole>();
Console.WriteLine($"Selected role: {role}");

// Random role excluding Admin
UserRole nonAdminRole = ILibRandom.IRandomEnum(UserRole.Admin);
Console.WriteLine($"Non-admin role: {nonAdminRole}");
```

### Random Item from List
```csharp
var menu = new List<string> { "Pho", "Bun Cha", "Banh Mi", "Nem" };
string todaySpecial = ILibRandom.IRandomItem(menu);
Console.WriteLine($"Today's special: {todaySpecial}");
```

### Complex Data Generation
```csharp
// Generate a random person
string[] firstNames = { "An", "Binh", "Chi" };
string[] lastNames = { "Nguyen", "Tran", "Le" };

string firstName = ILibRandom.IRandomFromArray(firstNames);
string lastName = ILibRandom.IRandomFromArray(lastNames);
int age = ILibRandom.IRandomInt(18, 65);
decimal salary = ILibRandom.IRandomDecimal(1000m, 5000m, 2);
bool isActive = ILibRandom.IRandomBool();

var person = new
{
    Name = $"{firstName} {lastName}",
    Age = age,
    Salary = salary,
    IsActive = isActive
};
```

## ⚡ Performance Optimizations

- **Enum Caching**: Enum values are cached after first use for zero allocations on subsequent calls
- **Byte Buffer Reuse**: Thread-static byte buffers for efficient random generation
- **.NET 6+ Optimizations**: Uses `Random.Shared` and `stackalloc` for maximum performance
- **No Reflection Overhead**: Optimized for high-frequency calls

## 🔒 Thread Safety

All methods in `ILibRandom` are thread-safe:
- **.NET 6+**: Uses `Random.Shared` (built-in thread-safe)
- **.NET Standard/Framework**: Uses `[ThreadStatic]` for per-thread instances
- Safe for use in parallel operations, web applications, and multi-threaded services

## 🔧 Requirements

- .NET 6.0 or later (recommended)
- .NET Core 3.1+
- .NET Framework 4.6.2+
- .NET Standard 2.0
- Compatible with Windows, Linux, and macOS

## 🧪 Testing

The library includes comprehensive unit tests with:
- **100% method coverage**
- **Edge case testing** (min/max values, overflow prevention)
- **Distribution testing** (uniformity verification)
- **Performance testing** (benchmarking for critical paths)
- **Thread-safety validation**

Run tests:
```bash
dotnet test
```

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

- Built with .NET 6/7/8/9 and .NET Standard 2.0
- Thread-safe random implementation
- Optimized with caching and modern .NET features

---

Made with ❤️ by DryFish
