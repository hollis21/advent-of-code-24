public class Day7 : IDay
{
    public string Title => "Bridge Repair";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var solve = (string inputLine) => SolveLine(inputLine);
        return Task.FromResult(inputLines.Select(solve).Sum().ToString());
    }

    private double SolveLine(string inputLine, bool includeConcatTest = false)
    {
        var goal = double.Parse(inputLine.Split(':')[0]);
        var inputs = inputLine.Split(":")[1].Trim().Split(' ').Select(double.Parse).ToList();
        var result = SolveSegment(goal, inputs, includeConcatTest);
        if (result != null)
        {
            //Console.WriteLine($"{goal}: {result}");
            return goal;
        }
        return 0;
    }

    private string? SolveSegment(double goal, List<double> inputs, bool includeConcatTest = false)
    {
        if (inputs.Count == 0)
        {
            throw new Exception("Inputs must contain values");
        }
        if (inputs.Count == 1)
        {
            return goal == inputs[0] ? inputs[0].ToString() : null;
        }
        double lastValue = inputs[^1];
        List<double> skipLast = inputs.SkipLast(1).ToList();

        var add = SolveSegment(goal - lastValue, skipLast, includeConcatTest);
        if (add != null)
        {
            return $"{add}+{lastValue}";
        }

        var mul = SolveSegment(goal / lastValue, skipLast, includeConcatTest);
        if (mul != null)
        {
            return $"{mul}*{lastValue}";
        }
        
        if (includeConcatTest)
        {
            if (goal.ToString().EndsWith(lastValue.ToString()))
            {
                double newGoal = double.Parse(goal.ToString().Substring(0, goal.ToString().Length - lastValue.ToString().Length));
                var conc = SolveSegment(newGoal, skipLast, includeConcatTest);
                if (conc != null)
                {
                    return $"{conc}|{lastValue}";
                }
            }
        }

        return null;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var solve = (string inputLine) => SolveLine(inputLine, true);
        return Task.FromResult(inputLines.Select(solve).Sum().ToString());
    }
}
