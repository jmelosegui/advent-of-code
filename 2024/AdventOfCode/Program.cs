using System.Text.RegularExpressions;
using AdventOfCode2024;
using AdventOfCode2024.Extensions;
using Microsoft.Extensions.DependencyInjection;

if (args.Length == 0)
{
    Console.WriteLine("Please provide the day of the challenge as an argument.");
    return;
}

var services = new ServiceCollection()
    .RegisterAllTypes<Challenge>([typeof(Program).Assembly])
    .BuildServiceProvider();

var day = args[0];

var challenge = services.GetRequiredKeyedService<Challenge>(day);

var basePath = AppContext.BaseDirectory;
var inputFilePath = Path.Combine(basePath, "Challenges", day, $"{day}.input");

using (var _ = new TimeLogger("Solution 1 completed in"))
{
    var solution1 = challenge.Solution1(inputFilePath);
    Console.WriteLine($"Solution 1: {solution1}");
}

using (var _ = new TimeLogger("Solution 2 completed in"))
{
    var solution2 = challenge.Solution2(inputFilePath);
    Console.WriteLine($"Solution 2: {solution2}");
}