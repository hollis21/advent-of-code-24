public class Day11 : IDay
{
    public string Title => "Plutonian Pebbles";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        List<long> input = inputLines.First().Split(" ").Select(long.Parse).ToList();
        int numOfIterations = 25;
        for (int i = 0; i < numOfIterations; i++)
        {
            for (int j = 0; j < input.Count; j++)
            {
                long current = input[j];
                string currentString = current.ToString();
                if (current == 0)
                {
                    input[j] = 1;
                }
                else if (currentString.Length % 2 == 0)
                {
                    input[j] = long.Parse(currentString.Substring(0, currentString.Length / 2));
                    input.Insert(j + 1, long.Parse(currentString.Substring(currentString.Length / 2)));
                    j++;
                }
                else
                {
                    input[j] = current * 2024;
                }
            }
        }

        return Task.FromResult(input.Count.ToString());
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        List<long> input = inputLines.First().Split(" ").Select(long.Parse).ToList();

        // Dictionary<(long num, int blinks), long[]> cache = [];
        // var result = input.SelectMany(i => ProcessStoneV1(i, 75, cache)).Count();

        var result = ProcessStoneV2(input, 75);
        return Task.FromResult(result.ToString());
    }

    // Caching was not the answer, leaving this here for posterity's sake.
    private long[] ProcessStoneV1(long num, int blinks, Dictionary<(long num, int blinks), long[]> cache)
    {
        if (cache.TryGetValue((num, blinks), out var value))
        {
            return value;
        }
        if (blinks == 0)
        {
            return [num];
        }
        long[] result;
        string numAsString = num.ToString();
        if (num == 0)
        {
            result = ProcessStoneV1(1, blinks - 1, cache);
        }
        else if (numAsString.Length % 2 == 0)
        {
            var left = long.Parse(numAsString.Substring(0, numAsString.Length / 2));
            var right = long.Parse(numAsString.Substring(numAsString.Length / 2));
            result = [.. ProcessStoneV1(left, blinks - 1, cache), .. ProcessStoneV1(right, blinks - 1, cache)];
        }
        else
        {
            result = ProcessStoneV1(num * 2024, blinks - 1, cache);
        }

        cache.Add((num, blinks), result);
        return result;
    }


    private long ProcessStoneV2(List<long> nums, int blinks)
    {
        Dictionary<long, long> numCounts = nums.GroupBy(n => n).ToDictionary(g => g.Key, g => (long)g.Count());
        for (int i = 0; i < blinks; i++)
        {
            var oldCounts = numCounts.ToList();
            numCounts.Clear();
            foreach (var kvp in oldCounts)
            {
                foreach (var newNum in BasicProcess(kvp.Key))
                {
                    if (numCounts.TryGetValue(newNum, out long currCount))
                    {
                        numCounts[newNum] = currCount + kvp.Value;
                    }
                    else
                    {
                        numCounts.Add(newNum, kvp.Value);
                    }
                }
            }
        }
        return numCounts.Values.Sum();
    }

    private long[] BasicProcess(long num)
    {
        string numAsString = num.ToString();
        if (num == 0)
        {
            return [1];
        }
        if (numAsString.Length % 2 == 0)
        {
            var left = long.Parse(numAsString.Substring(0, numAsString.Length / 2));
            var right = long.Parse(numAsString.Substring(numAsString.Length / 2));
            return [left, right];
        }
        return [num * 2024];
    }
}

