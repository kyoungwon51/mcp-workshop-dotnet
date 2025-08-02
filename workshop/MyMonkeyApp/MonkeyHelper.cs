using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// Provides helper methods for managing monkey data.
/// </summary>
public static class MonkeyHelper
{
    private static readonly HttpClient httpClient = new();
    private static List<Monkey> monkeys = new();
    private static int randomMonkeyAccessCount = 0;
    private const string monkeyApiUrl = "https://mcp-monkey-server.example.com/api/monkeys"; // Replace with actual MCP server URL

    /// <summary>
    /// Gets all monkeys from the MCP server.
    /// </summary>
    public static async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeys.Count == 0)
        {
            var response = await httpClient.GetAsync(monkeyApiUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            monkeys = JsonSerializer.Deserialize<List<Monkey>>(json) ?? new List<Monkey>();
        }
        return monkeys;
    }

    /// <summary>
    /// Gets a random monkey and tracks access count.
    /// </summary>
    public static async Task<Monkey?> GetRandomMonkeyAsync()
    {
        var monkeyList = await GetMonkeysAsync();
        if (monkeyList.Count == 0) return null;
        randomMonkeyAccessCount++;
        var random = new Random();
        var index = random.Next(monkeyList.Count);
        return monkeyList[index];
    }

    /// <summary>
    /// Finds a monkey by name.
    /// </summary>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        var monkeyList = await GetMonkeysAsync();
        return monkeyList.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the number of times a random monkey has been accessed.
    /// </summary>
    public static int GetRandomMonkeyAccessCount() => randomMonkeyAccessCount;
}
