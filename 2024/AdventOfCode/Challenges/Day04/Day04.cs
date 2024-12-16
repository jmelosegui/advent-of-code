using AdventOfCode2024.Extensions;

namespace AdventOfCode2024.Day1.Problem1;

[Challenge(Name = "Day04")]
public class Day04Challenge : Challenge
{
    public override long Solution1(string inputPath)
    {
        string[] input = File.ReadAllLines(inputPath);

        int result = 0;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                result += XmasCount(input, x, y);
            }
        }

        return result;
    }

    private int XmasCount(string[] input, int x, int y)
    {
        var count = 0;
        
        for (int i = 0; i < 8; i++)
        {
            double angle = i * Math.PI / 4;
            int dx = (int) Math.Round(Math.Cos(angle));
            int dy = (int) Math.Round(Math.Sin(angle));

            if (Enumerable.Range(0, 4).All(j =>
                    y + j * dy >= 0 && y + j * dy < input.Length &&
                    x + j * dx >= 0 && x + j * dx < input[0].Length &&
                    input[y + j * dy][x + j * dx] == "XMAS"[j]))
            {
                count++;
            }
        }

        return count;
    }

    public override long Solution2(string inputPath)
    {
        string[] input = File.ReadAllLines(inputPath);

        int result = 0;

        for (int y = 0; y < input[0].Length - 2; y++)
        {
            for (int x = 0; x < input.Length - 2; x++)
            {
                if (input[y + 1][x + 1] == 'A')
                {
                    if (CheckXShape(input, x, y))
                    {
                        result++;
                    }
                }
            }
        }

        return result;
    }

    private bool CheckXShape(string[] input, int x, int y)
    {
        char topLeft = input[y][x];
        char topRight = input[y][x + 2];
        char bottomLeft = input[y + 2][x];
        char bottomRight = input[y + 2][x + 2];

        if ((topLeft == 'M' && topRight == 'S' && bottomLeft == 'M' && bottomRight == 'S') ||
            (topLeft == 'S' && topRight == 'M' && bottomLeft == 'S' && bottomRight == 'M'))
        {
            return true;
        }

        if ((topLeft == 'M' && topRight == 'M' && bottomLeft == 'S' && bottomRight == 'S') ||
            (topLeft == 'S' && topRight == 'S' && bottomLeft == 'M' && bottomRight == 'M'))
        {
            return true;
        }

        return false;
    }
}