using AdventOfCode2024.Extensions;

namespace AdventOfCode2024;

[Challenge(Name = "Day05")]
public class Day05Challenge : Challenge
{
    private (Dictionary<int, HashSet<int>>, IEnumerable<List<int>>) ParseInput(string inputFilePath)
    {
        Dictionary<int, HashSet<int>> rules = new Dictionary<int, HashSet<int>>();
        List<List<int>> updates = new List<List<int>>();
        bool isRule = true;
        foreach (var line in File.ReadAllLines(inputFilePath))
        {
            if (line == string.Empty)
            {
                isRule = false;
                continue;
            }

            if (isRule)
            {
                var pages = line.Split('|').Select(int.Parse).ToList();
                if (!rules.TryGetValue(pages[0], out var set))
                {
                    set = new HashSet<int>();
                    rules[pages[0]] = set;
                }

                rules[pages[0]].Add(pages[1]);
            }
            else
            {
                updates.Add(line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
            }
        }

        return (rules, updates);
    }

    public override int Solution1(string inputFilePath)
    {
        var (rules, updates) = ParseInput(inputFilePath);
        int result = 0;
        foreach (var update in updates)
        {
            bool isValid = true;
            for (var i = update.Count - 1; i >= 0; i--)
            {
                if (rules.TryGetValue(update[i], out var set))
                {
                    if (set.Overlaps(update[0..i]))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                result += update[update.Count / 2];
            }
        }

        return result;
    }

    public override int Solution2(string inputFilePath)
    {
        var (rules, updates) = ParseInput(inputFilePath);
        var com = new PageComparer(rules);
        int result = 0;
        foreach (var update in updates)
        {
            for (var i = update.Count - 1; i >= 0; i--)
            {
                if (rules.TryGetValue(update[i], out var set))
                {
                    if (set.Overlaps(update[0..i]))
                    {
                        update.Sort(com);
                        result += update[update.Count / 2];
                        break;
                    }
                }
            }
        }

        return result;
    }

    private class PageComparer : IComparer<int>
    {
        private Dictionary<int, HashSet<int>> rules;

        public PageComparer(Dictionary<int, HashSet<int>> rules)
        {
            this.rules = rules;
        }

        public int Compare(int x, int y)
        {
            if (rules.TryGetValue(x, out var set))
            {
                if (set.Contains(y))
                {
                    return -1;
                }
            }

            return 1;
        }
    }
}