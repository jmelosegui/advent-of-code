using System.Drawing;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024.Day08;

[Challenge(Name = "Day08")]
public class Day08Challenge : Challenge
{
    public override long Solution1(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);
        var input = ParseInput(lines);
        var result = new HashSet<Point>();
        
        var combinationsCollection = GetCombinations(input);
        foreach (var kv in combinationsCollection)
        {
            var combinations = kv.Value;
            foreach (var (p1, p2) in combinations)
            {
                AddAntiNodes(result, lines, p1, p2, false);
                AddAntiNodes(result, lines, p2, p1, false);
            }
        }

        return result.Count;
    }

    public override long Solution2(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);
        var input = ParseInput(lines);
        var result = new HashSet<Point>();

        var combinationsCollection = GetCombinations(input);
        foreach (var kv in combinationsCollection)
        {
            var combinations = kv.Value;
            foreach (var (p1, p2) in combinations)
            {
                AddAntiNodes(result, lines, p1, p2, true);
                AddAntiNodes(result, lines, p2, p1, true);
            }
        }

        return result.Count;
    }

    private void AddAntiNodes(HashSet<Point> result, string[] lines, Point start, Point direction, bool iterate)
    {
        var currentPoint = new Point(
            start.X + (start.X - direction.X),
            start.Y + (start.Y - direction.Y)
        );

        if (IsInBounds(lines, currentPoint))
        {
            result.Add(currentPoint);
        }

        if (iterate)
        {
            result.Add(start);
            while (true)
            {
                currentPoint = new Point(
                    currentPoint.X + (start.X - direction.X),
                    currentPoint.Y + (start.Y - direction.Y)
                );

                if (IsInBounds(lines, currentPoint))
                {
                    result.Add(currentPoint);
                }
                else
                {
                    break;
                }
            }
        }
    }

    private bool IsInBounds(string[] input, Point point)
    {
        return point.X >= 0 && point.X < input[0].Length && point.Y >= 0 && point.Y < input.Length;
    }

    private Dictionary<char, List<(Point, Point)>> GetCombinations(Dictionary<char, List<Point>> input)
    {
        var result = new Dictionary<char, List<(Point, Point)>>();

        foreach (var kv in input)
        {
            var combinations = CalculateCombinations(kv.Value.Count, 2);

            var list = new List<(Point, Point)>();
            for (int i = 0; i < kv.Value.Count; i++)
            {
                for (int j = i + 1; j < kv.Value.Count; j++)
                {
                    list.Add((kv.Value[i], kv.Value[j]));
                }
            }

            result[kv.Key] = list;
        }

        return result;
    }

    private int CalculateCombinations(int n, int k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    private int Factorial(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        return n * Factorial(n - 1);
    }

    private Dictionary<char, List<Point>> ParseInput(string[] lines)
    {
        var result = new Dictionary<char, List<Point>>();
        foreach (var (y, line) in lines.Index())
        {
            foreach (var (x, c) in line.Index())
            {
                if (c == '#' || c == '.')
                {
                    continue;
                }

                if (!result.ContainsKey(c))
                {
                    result[c] = new List<Point>();
                }

                result[c].Add(new Point(x, y));
            }
        }

        return result;
    }
}