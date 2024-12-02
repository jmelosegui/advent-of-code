using System.Collections;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024;

[Challenge(Name = "Day01")]
public class Day01Challenge : Challenge
{
    public override int Solution1(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException("FileNotFound",inputFilePath);
        }
        var (leftList, rightList) = ParseInput(inputFilePath);

        var result = 0;
        foreach (var (index, number) in leftList.Index())
        {
            result += Math.Abs(number - rightList[index]);
        }

        return result;
    }

    public override int Solution2(string inputFilePath)
    {
        var (leftList, rightList) = ParseInput(inputFilePath);
        var result = 0; 
            
        foreach (var number in leftList)
        {
            result += number * rightList.Count(n => n == number);
        }
        
        return result;
    }

    private (int[], int[]) ParseInput(string input)
    {
        if (!File.Exists(input))
        {
            throw new FileNotFoundException();
        }

        var lines = File.ReadAllLines(input);

        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (string line in lines)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftList.Add(int.Parse(numbers[0]));
            rightList.Add(int.Parse(numbers[1]));
        }

        leftList.Sort();
        rightList.Sort();
        return (leftList.ToArray(), rightList.ToArray());
    }
}