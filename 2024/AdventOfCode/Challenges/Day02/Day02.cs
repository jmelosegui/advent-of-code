using System.Threading.Channels;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024.Day1.Problem1;

[Challenge(Name = "Day02")]
public class Day02Challenge : Challenge 
{
    public override int Solution1(string inputFilePath)
    {
        var reports = ParseInput(inputFilePath);

        var safeReports = 0;
        foreach (var levels in reports)
        {
            if (IsSafeReport(levels))
            {
                safeReports++;
            }
        }

        return safeReports;
    }
    
    public override int Solution2(string inputFilePath)
    {
        var reports = ParseInput(inputFilePath);

        var safeReports = 0;
        foreach (var levels in reports)
        {
            if (IsSafeReport(levels))
            {
                safeReports++;
            }
            else
            {
                var subLevels = Enumerable
                    .Range(0, levels.Length)
                    .Select(i => levels.Where((_, index) => index != i).ToArray());
        
                foreach (var sublevel in subLevels)
                {
                    if (IsSafeReport(sublevel))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        return safeReports;
    }
    
    private int[][] ParseInput(string inputFilePath)
    {
        return File.ReadAllLines(inputFilePath)
            .Select(l =>
            {
                return l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray();
            }).ToArray();
    }

    private bool IsSafeReport(int[] levels)
    {
        var sign = Math.Sign(levels[1] - levels[0]);
        var i = 0;
        var isSafeReport = true;
        foreach (var level in levels)
        {
            if (i == levels.Length - 1)
            {
                continue;
            }

            if (!(Math.Abs(level - levels[i + 1]) < 4 && (i == 0 || sign == Math.Sign(levels[i + 1] - level))))
            {
                isSafeReport = false;
                
                break;
            }

            i++;
        }

        return isSafeReport;
    }
}