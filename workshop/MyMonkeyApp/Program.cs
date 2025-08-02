
using MyMonkeyApp;

namespace MyMonkeyApp;

internal class Program
{
    private static readonly List<string> asciiArts = new()
    {
        "(\"`-\"/)",
        "( . . )",
        "(  -  )",
        "('')_('')",
        "(o.o)",
        "(>_<)"
    };

    public static async Task Main(string[] args)
    {
        var random = new Random();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to the Monkey Console App!");
            Console.WriteLine(asciiArts[random.Next(asciiArts.Count)]);
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. List all monkeys");
            Console.WriteLine("2. Get details for a specific monkey by name");
            Console.WriteLine("3. Get a random monkey");
            Console.WriteLine("4. Exit app");
            Console.Write("\nSelect an option: ");
            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    await ListAllMonkeysAsync();
                    break;
                case "2":
                    await GetMonkeyDetailsAsync();
                    break;
                case "3":
                    await GetRandomMonkeyAsync();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }

    private static async Task ListAllMonkeysAsync()
    {
        var monkeys = await MonkeyHelper.GetMonkeysAsync();
        if (monkeys.Count == 0)
        {
            Console.WriteLine("No monkeys found.");
            return;
        }
        Console.WriteLine("\nAvailable Monkeys:");
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("Name           | Location        | Population");
        Console.WriteLine("---------------------------------------------------");
        foreach (var monkey in monkeys)
        {
            Console.WriteLine($"{monkey.Name,-14} | {monkey.Location,-15} | {monkey.Population}");
        }
        Console.WriteLine("---------------------------------------------------");
    }

    private static async Task GetMonkeyDetailsAsync()
    {
        Console.Write("Enter monkey name: ");
        var name = Console.ReadLine();
        var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name ?? string.Empty);
        if (monkey == null)
        {
            Console.WriteLine("Monkey not found.");
            return;
        }
        Console.WriteLine($"\nName: {monkey.Name}");
        Console.WriteLine($"Location: {monkey.Location}");
        Console.WriteLine($"Population: {monkey.Population}");
        if (!string.IsNullOrWhiteSpace(monkey.AsciiArt))
        {
            Console.WriteLine($"ASCII Art:\n{monkey.AsciiArt}");
        }
    }

    private static async Task GetRandomMonkeyAsync()
    {
        var monkey = await MonkeyHelper.GetRandomMonkeyAsync();
        if (monkey == null)
        {
            Console.WriteLine("No monkeys available.");
            return;
        }
        Console.WriteLine($"\nRandom Monkey:");
        Console.WriteLine($"Name: {monkey.Name}");
        Console.WriteLine($"Location: {monkey.Location}");
        Console.WriteLine($"Population: {monkey.Population}");
        if (!string.IsNullOrWhiteSpace(monkey.AsciiArt))
        {
            Console.WriteLine($"ASCII Art:\n{monkey.AsciiArt}");
        }
        Console.WriteLine($"Random monkey accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times.");
    }
}
