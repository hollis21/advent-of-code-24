public class Day2 : IDay
{
    public string Title => "Red-Nosed Reports";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        return Task.FromResult(inputLines.Count(IsSafeA).ToString());
    }

    private bool IsSafeA(string inputLine)
    {
        var comparer = (int a, int b) => a > b ? 1 : (a < b ? -1 : 0);
        var levels = inputLine.Split(' ').Select(int.Parse).ToArray();
        int upOrDown = comparer(levels[0], levels[1]);
        if (upOrDown == 0)
        {
            return false;
        }
        for (int i = 1; i < levels.Length; i++)
        {
            if (comparer(levels[i - 1], levels[i]) != upOrDown)
            {
                return false;
            }
            if (Math.Abs(levels[i - 1] - levels[i]) > 3)
            {
                return false;
            }
        }
        return true;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        return Task.FromResult(inputLines.Count(IsSafeB).ToString());
    }

    private bool IsSafeB(string inputLine)
    {
        if (IsSafeA(inputLine))
        {
            return true;
        }
        var levels = inputLine.Split(' ').ToArray();
        for (int i = 0; i < levels.Length; i++)
        {
            var iRemoved = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();
            if (IsSafeA(string.Join(' ', iRemoved)))
            {
                return true;
            }
        }
        return false;
    }
}
