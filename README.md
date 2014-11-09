DuckTyping
==========

C# Extension Method to Allow Anonymous Types to Implement Interfaces

## Example:

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

## How It Works

The DuckCast extension method works by using Moq to create a dynamic proxy, based on your generic type parameter, with your anonymous class supplying the implementation.

### Limitations

This is a very simple implementation and is meant to work with "property only" anonymous types. Methods on generic type parameters will not fail, they just simply receive the Moq default implementation.

## Contact The Author

https://www.linkedin.com/in/jcbowers

## License

The MIT License (MIT)

Copyright (c) 2014 Jason C. Bowers

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

