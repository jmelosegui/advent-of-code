using AdventOfCode2024;
using AdventOfCode2024.Extensions;
using Microsoft.Extensions.DependencyInjection;

if (args.Length == 0)
{
    Console.WriteLine("Please provide the day of the challenge as an argument.");
    return;
}

var services = new ServiceCollection()
    .RegisterAllTypes<Challenge>(new[] {typeof(Program).Assembly})
    .BuildServiceProvider();

var day = args[0];

var challenge = services.GetRequiredKeyedService<Challenge>(day);

var basePath = AppContext.BaseDirectory;
var testFilePath = Path.Combine(basePath, "Challenges", day, $"{day}.test");
var inputFilePath = Path.Combine(basePath, "Challenges", day, $"{day}.input");

var solution1 = challenge.Solution1(inputFilePath);
var solution2 = challenge.Solution2(inputFilePath);

Console.WriteLine($"Solution 1: {solution1}, Solution 2: {solution2}");