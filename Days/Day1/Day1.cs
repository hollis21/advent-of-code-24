public class Day1 : IDay
{
    public string Title => "Historian Hysteria";

    public async Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        // Make two lists from input
        var left = new List<int>();
        var right = new List<int>();
        string[] split;
        foreach (var line in inputLines) {
            split = line.Split("   ");
            left.Add(int.Parse(split[0]));
            right.Add(int.Parse(split[1]));
        }

        // Sort them
        left.Sort();
        right.Sort();

        // Calculate the 'difference'
        var total = 0;
        for (int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }
        return total.ToString();
    }

    public async Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        // Split input into list and count of numbers
        var left = new List<int>();
        var right = new Dictionary<int, int>();
        string[] split;
        foreach (var line in inputLines) {
            split = line.Split("   ");
            left.Add(int.Parse(split[0]));
            var rightNum  = int.Parse(split[1]);
            if (!right.ContainsKey(rightNum)) {
                right.Add(rightNum, 0);
            }
            right[rightNum]++;
        }

        var total = 0;
        foreach (var val in left) {
            if (right.ContainsKey(val)) {
                total += val * right[val];
            }
        }

        return total.ToString();
    }
}