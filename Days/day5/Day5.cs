public class Day5 : IDay
{
    public string Title => "Print Queue";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var input = inputLines.ToList();
        var rules = new Dictionary<int, HashSet<int>>();
        int total = 0;
        int i = 0;
        for (; input[i] != ""; i++)
        {
            var nums = input[i].Split('|').Select(int.Parse).ToArray();
            if (!rules.ContainsKey(nums[0]))
            {
                rules.Add(nums[0], new HashSet<int>());
            }
            rules[nums[0]].Add(nums[1]);
        }
        // i++ because we need to skip the blank line
        for (i++; i < input.Count; i++)
        {
            var nums = input[i].Split(',').Select(int.Parse).ToArray();
            if (CheckIfValid(nums, rules))
            {
                total += nums[nums.Length / 2];
            }
        }
        return Task.FromResult(total.ToString());
    }

    private bool CheckIfValid(IEnumerable<int> nums, Dictionary<int, HashSet<int>> rules)
    {
        var seen = new HashSet<int>();

        foreach (var num in nums)
        {
            seen.Add(num);
            // If we have a rule for the number, and we've seen any of the numbers that must not be before num
            if (rules.ContainsKey(num) && rules[num].Any(r => seen.Contains(r)))
            {
                return false;
            }
        }
        return true;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var input = inputLines.ToList();
        var rules = new Dictionary<int, HashSet<int>>();
        int total = 0;
        int i = 0;
        for (; input[i] != ""; i++)
        {
            var nums = input[i].Split('|').Select(int.Parse).ToArray();
            if (!rules.ContainsKey(nums[0]))
            {
                rules.Add(nums[0], new HashSet<int>());
            }
            rules[nums[0]].Add(nums[1]);
        }
        // i++ because we need to skip the blank line
        for (i++; i < input.Count; i++)
        {
            var nums = input[i].Split(',').Select(int.Parse).ToList();
            if (!CheckIfValid(nums, rules))
            {
                nums = FixLine(nums, rules);
                total += nums[nums.Count / 2];
            }
        }
        return Task.FromResult(total.ToString());
    }

    private List<int> FixLine(List<int> nums, Dictionary<int, HashSet<int>> rules)
    {
        var seen = new Dictionary<int, int>();
        for (int i = 0; i < nums.Count; i++) {
            if (!seen.ContainsKey(nums[i])) {
                seen.Add(nums[i], i);
            }
            if (rules.ContainsKey(nums[i])) {
                // Get the first number that this num has a rule for and we've seen and move num to be infront of it.
                int ruleViolation = rules[nums[i]].FirstOrDefault(r => seen.ContainsKey(r), -1);
                if (ruleViolation != -1) {
                    nums.Insert(seen[ruleViolation], nums[i]);
                    nums.RemoveAt(i + 1);
                    i = -1;
                    seen.Clear();
                }
            }
        }
        return nums;
    }
}
