using System.Text.RegularExpressions;

public class Day3 : IDay
{
    public string Title => "Mull It Over";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        int result = 0;
        string mulRegex = @"mul\((?<left>\d+),(?<right>\d+)\)";
        foreach (string line in inputLines)
        {
            foreach (Match match in Regex.Matches(line, mulRegex))
            {
                result += int.Parse(match.Groups["left"].Value) * int.Parse(match.Groups["right"].Value);
            }
        }
        return Task.FromResult(result.ToString());
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        int result = 0;
        string mulRegex = @"(mul\((?<left>\d+),(?<right>\d+)\))|(don\'t\(\))|(do\(\))";

        var line = string.Join("", inputLines);

        bool process = true;
        var matches = Regex.Matches(line, mulRegex);
        foreach (Match match in Regex.Matches(line, mulRegex))
        {
            switch (match.Value)
            {
                case "do()":
                    process = true;
                    break;
                case "don't()":
                    process = false;
                    break;
                default:
                    if (process)
                    {
                        result += int.Parse(match.Groups["left"].Value) * int.Parse(match.Groups["right"].Value);
                    }
                    break;
            }
        }
        return Task.FromResult(result.ToString());
    }
}
