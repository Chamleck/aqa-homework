using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

interface IDiscountable
{
    void ApplyDiscount(decimal percent);
}

class Book : Product, IDiscountable
{
    public Book() { }

    public void ApplyDiscount(decimal percent)
    {
        Price -= Price * percent / 100;
    }
}

class Phone : Product, IDiscountable
{
    public Phone() { }

    public void ApplyDiscount(decimal percent)
    {
        Price -= Price * percent / 100;
    }
}

class Program
{
    // ===== Generic-методы =====

    static void PrintValue<T>(T value)
    {
        Console.WriteLine(value);
    }

    static void PrintList<T>(List<T> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    static T GetFirst<T>(List<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("List is empty");

        return items[0];
    }

    static T GetLast<T>(List<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("List is empty");

        return items[items.Count - 1];
    }

    static T GetByIndex<T>(List<T> items, int index)
    {
        if (index < 0 || index >= items.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        return items[index];
    }

    static List<T> Repeat<T>(T value, int count)
    {
        var result = new List<T>();
        for (int i = 0; i < count; i++)
        {
            result.Add(value);
        }
        return result;
    }

    static List<T> Copy<T>(List<T> items)
    {
        var result = new List<T>();
        foreach (var item in items)
        {
            result.Add(item);
        }
        return result;
    }

    static List<T> Merge<T>(List<T> first, List<T> second)
    {
        var result = new List<T>();
        foreach (var item in first)
        {
            result.Add(item);
        }
        foreach (var item in second)
        {
            result.Add(item);
        }
        return result;
    }

    static List<T> Reverse<T>(List<T> items)
    {
        var result = new List<T>();
        for (int i = items.Count - 1; i >= 0; i--)
        {
            result.Add(items[i]);
        }
        return result;
    }

    static List<T> Take<T>(List<T> items, int count)
    {
        var result = new List<T>();
        for (int i = 0; i < count && i < items.Count; i++)
        {
            result.Add(items[i]);
        }
        return result;
    }

    // ===== Ограничения where =====

    static void PrintProducts<T>(List<T> products) where T : Product
    {
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}: {product.Price}");
        }
    }

    static T GetMostExpensive<T>(List<T> products) where T : Product
    {
        if (products.Count == 0)
            throw new InvalidOperationException("List is empty");

        var mostExpensive = products[0];
        foreach (var product in products)
        {
            if (product.Price > mostExpensive.Price)
                mostExpensive = product;
        }
        return mostExpensive;
    }

    static List<T> GetProductsCheaperThan<T>(List<T> products, decimal maximumPrice) where T : Product
    {
        var result = new List<T>();
        foreach (var product in products)
        {
            if (product.Price < maximumPrice)
                result.Add(product);
        }
        return result;
    }

    static T CreateProduct<T>(string name, decimal price) where T : Product, new()
    {
        var product = new T();
        product.Name = name;
        product.Price = price;
        return product;
    }

    static void ApplyDiscountToAll<T>(List<T> products, decimal percent) where T : Product, IDiscountable
    {
        foreach (var product in products)
        {
            product.ApplyDiscount(percent);
            Console.WriteLine($"{product.Name}: {product.Price}");
        }
    }

    // ===== Фильтрация без делегатов =====

    static List<int> GetEvenNumbers(List<int> numbers)
    {
        var result = new List<int>();
        foreach (var number in numbers)
        {
            if (number % 2 == 0)
                result.Add(number);
        }
        return result;
    }

    static List<int> GetNumbersGreaterThan(List<int> numbers, int minimum)
    {
        var result = new List<int>();
        foreach (var number in numbers)
        {
            if (number > minimum)
                result.Add(number);
        }
        return result;
    }

    static List<string> GetLongWords(List<string> words, int minimumLength)
    {
        var result = new List<string>();
        foreach (var word in words)
        {
            if (word.Length >= minimumLength)
                result.Add(word);
        }
        return result;
    }

    // ===== Фильтрация через Predicate =====

    static List<T> Filter<T>(List<T> items, Predicate<T> condition)
    {
        var result = new List<T>();
        foreach (var item in items)
        {
            if (condition(item))
                result.Add(item);
        }
        return result;
    }

    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    static bool IsGreaterThanTen(int number)
    {
        return number > 10;
    }

    static bool IsLongWord(string word)
    {
        return word.Length >= 5;
    }

    // ===== Action =====

    static void ExecuteTwice(Action action)
    {
        action();
        action();
    }

    static void PrintHello()
    {
        Console.WriteLine("Hello");
    }

    static void PrintSeparator()
    {
        Console.WriteLine("-----");
    }

    static void ProcessItems<T>(List<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }

    static void PrintNumber(int number)
    {
        Console.WriteLine($"Number: {number}");
    }

    static void PrintUpperCase(string text)
    {
        Console.WriteLine(text.ToUpper());
    }

    // ===== Func =====

    static int Calculate(int first, int second, Func<int, int, int> operation)
    {
        return operation(first, second);
    }

    static int Add(int first, int second)
    {
        return first + second;
    }

    static int Subtract(int first, int second)
    {
        return first - second;
    }

    static int Multiply(int first, int second)
    {
        return first * second;
    }

    // ===== Main =====

    static void Main(string[] args)
    {
        Console.WriteLine("=== PrintValue ===");
        PrintValue(42);
        PrintValue("hello");
        PrintValue(true);

        Console.WriteLine("\n=== PrintList ===");
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var words = new List<string> { "apple", "banana", "kiwi" };
        PrintList(numbers);
        PrintList(words);

        Console.WriteLine("\n=== GetFirst / GetLast ===");
        Console.WriteLine($"First number: {GetFirst(numbers)}");
        Console.WriteLine($"Last number: {GetLast(numbers)}");
        Console.WriteLine($"First word: {GetFirst(words)}");
        Console.WriteLine($"Last word: {GetLast(words)}");

        Console.WriteLine("\n=== GetByIndex ===");
        Console.WriteLine($"Number at index 2: {GetByIndex(numbers, 2)}");
        Console.WriteLine($"Word at index 1: {GetByIndex(words, 1)}");

        Console.WriteLine("\n=== Repeat ===");
        var repeated = Repeat("x", 3);
        PrintList(repeated);

        Console.WriteLine("\n=== Copy ===");
        var numbersCopy = Copy(numbers);
        PrintList(numbersCopy);

        Console.WriteLine("\n=== Merge ===");
        var moreNumbers = new List<int> { 6, 7, 8 };
        var merged = Merge(numbers, moreNumbers);
        PrintList(merged);

        Console.WriteLine("\n=== Reverse ===");
        var reversedNumbers = Reverse(numbers);
        PrintList(reversedNumbers);

        Console.WriteLine("\n=== Take ===");
        var taken = Take(numbers, 3);
        PrintList(taken);

        Console.WriteLine("\n=== PrintProducts ===");
        var books = new List<Book>
        {
            new Book { Name = "C# in Depth", Price = 500 },
            new Book { Name = "Clean Code", Price = 700 }
        };
        var phones = new List<Phone>
        {
            new Phone { Name = "iPhone", Price = 1000 },
            new Phone { Name = "Pixel", Price = 800 }
        };
        PrintProducts(books);
        PrintProducts(phones);

        Console.WriteLine("\n=== GetMostExpensive ===");
        Console.WriteLine($"Most expensive book: {GetMostExpensive(books).Name}");
        Console.WriteLine($"Most expensive phone: {GetMostExpensive(phones).Name}");

        Console.WriteLine("\n=== GetProductsCheaperThan ===");
        var cheapBooks = GetProductsCheaperThan(books, 600);
        PrintProducts(cheapBooks);

        Console.WriteLine("\n=== CreateProduct ===");
        var newBook = CreateProduct<Book>("New Book", 300);
        var newPhone = CreateProduct<Phone>("New Phone", 900);
        Console.WriteLine($"{newBook.Name}: {newBook.Price}");
        Console.WriteLine($"{newPhone.Name}: {newPhone.Price}");

        Console.WriteLine("\n=== ApplyDiscountToAll ===");
        ApplyDiscountToAll(books, 10);
        ApplyDiscountToAll(phones, 10);

        Console.WriteLine("\n=== GetEvenNumbers ===");
        PrintList(GetEvenNumbers(numbers));

        Console.WriteLine("\n=== GetNumbersGreaterThan ===");
        PrintList(GetNumbersGreaterThan(numbers, 2));

        Console.WriteLine("\n=== GetLongWords ===");
        PrintList(GetLongWords(words, 5));

        Console.WriteLine("\n=== Filter with Predicate ===");
        PrintList(Filter(numbers, IsEven));
        PrintList(Filter(numbers, IsGreaterThanTen));
        PrintList(Filter(words, IsLongWord));

        Console.WriteLine("\n=== ExecuteTwice ===");
        ExecuteTwice(PrintHello);
        ExecuteTwice(PrintSeparator);

        Console.WriteLine("\n=== ProcessItems ===");
        ProcessItems(numbers, PrintNumber);
        ProcessItems(words, PrintUpperCase);

        Console.WriteLine("\n=== Calculate ===");
        Console.WriteLine($"Add: {Calculate(5, 3, Add)}");
        Console.WriteLine($"Subtract: {Calculate(5, 3, Subtract)}");
        Console.WriteLine($"Multiply: {Calculate(5, 3, Multiply)}");
    }
}