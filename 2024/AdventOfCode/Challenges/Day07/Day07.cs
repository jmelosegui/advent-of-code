using System.IO.Compression;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024.Day07;

[Challenge(Name = "Day07")]
public class Day07Challenge : Challenge
{
    public override long Solution1(string inputPath)
    {
        var input = ParseInput(inputPath);
        var operators = new[] {"+", "*"};
        var result = 0L;
        foreach (var (test, equation) in input)
        {
            var combinations = GetPermutations(operators, equation.Length - 1);
            foreach (var combination in combinations)
            {
                var value = GetEquationValue(combination, equation);
                if (test == value)
                {
                    result += test;
                    break;
                }
            }
        }

        return result;
    }

    public override long Solution2(string inputPath)
    {
        var input = ParseInput(inputPath);
        var operators = new[] {"+", "*", "||"};
        var result = 0L;
        foreach (var (test, equation) in input)
        {
            var combinations = GetPermutations(operators, equation.Length - 1);
            foreach (var combination in combinations)
            {
                var value = GetEquationValue(combination, equation);
                if (test == value)
                {
                    result += test;
                    break;
                }
            }
        }

        return result;
    }

    private long GetEquationValue(string[] combination, int[] values)
    {
        long result = values[0];
        for (int i = 1; i <= combination.Length; i++)
        {
            result = combination[i - 1] switch
            {
                "+" => result + values[i],
                "*" => result * values[i],
                "||" => result * (long)Math.Pow(10, (int)Math.Floor(Math.Log10(values[i]) + 1)) + values[i],
                _ => throw new Exception("Invalid operator")
            };
        }

        return result;
    }

    private static string[][] GetPermutations(string[] operators, int places)
    {
        int totalPermutations = (int) Math.Pow(operators.Length, places);
        string[][] results = new string[totalPermutations][];

        for (int i = 0; i < totalPermutations; i++)
        {
            string[] current = new string[places];
            int index = i;

            for (int j = 0; j < places; j++)
            {
                current[j] = operators[index % operators.Length];
                index /= operators.Length;
            }

            results[i] = current;
        }

        return results;
    }


    private static List<(long, int[])> ParseInput(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);
        var result = new List<(long, int[])>();
        foreach (var line in lines)
        {
            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            result.Add((long.Parse(parts[0]),
                parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));
        }

        return result;
    }
}