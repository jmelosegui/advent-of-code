using System.Text.RegularExpressions;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024;

[Challenge(Name = "Day03")]
public partial class Day03Challenge : Challenge
{
    [GeneratedRegex(@"mul\((?<left>\d{1,3}),(?<right>\d{1,3})\)")]
    private static partial Regex Multiply();
    
    public override int Solution1(string inputPath)
    {
        string input = File.ReadAllText(inputPath);
        MatchCollection matches = Multiply().Matches(input);
        
        int sum = 0;
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                int left = int.Parse(match.Groups["left"].Value);
                int right = int.Parse(match.Groups["right"].Value);
                sum += left * right;
            }
        }

        return sum;
    }

    [GeneratedRegex(@"mul\((?<left>\d{1,3}),(?<right>\d{1,3})\)|do\(\)|don't\(\)")]
    private static partial Regex DosAndDonts();
    
    public override int Solution2(string inputPath)
    {
        string input = File.ReadAllText(inputPath);
        var matches = DosAndDonts().Matches(input);

        var flag = true;
        int sum = 0;
        foreach (Match match in matches)
        {
            switch (match.Value)
            {
                case "do()":
                    flag = true;
                    break;
                case "don't()":
                    flag = false;
                    break;
                default:
                    if (flag)
                    {
                        int left = int.Parse(match.Groups["left"].Value);
                        int right = int.Parse(match.Groups["right"].Value);
                        sum += left * right;
                    }
                    break;
            } 
        }

        return sum;
    }
}