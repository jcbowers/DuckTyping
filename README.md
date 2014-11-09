DuckTyping
==========

C# Extension Method to Allow Anonymous Types to Implement Interfaces

# Example:

```
using System;
using JasonSoft.Extensions;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var developer = new { Name = "Jason Bowers" };

            PrintDeveloperName(developer.DuckCast<IDeveloper>());

            Console.ReadKey();
        }

        private static void PrintDeveloperName(IDeveloper developer)
        {
            Console.WriteLine(developer.Name);
        }
    }

    public interface IDeveloper
    {
        string Name { get; }
    }
}
```

# How It Works

The DuckCast extension method works by using Moq to create a dynamic proxy, based on your generic type parameter, with your anonymous class supplying the implementation.

## Limitations

This is a very simple implementation and is meant to work with "property only" anonymous types. Methods on generic type parameters will not fail, they just simply receive the Moq default implementation.

# Contact The Author

https://www.linkedin.com/in/jcbowers


 


