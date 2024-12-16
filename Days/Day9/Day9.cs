public class Day9 : IDay
{
    public string Title => "Disk Fragmenter";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var input = inputLines.First();
        var blocks = LoadMemory(input);

        // Compress
        var left = 0;
        var right = blocks.Count - 1;
        while (left < right)
        {
            if (blocks[left] is null)
            {
                while (left < right && blocks[right] is null)
                {
                    right--;
                }
                blocks[left] = blocks[right];
                blocks[right] = null;
            }
            left++;
        }

        return Task.FromResult(CalculateChecksum(blocks).ToString());
    }

    private List<int?> LoadMemory(string input)
    {
        var blocks = new List<int?>();
        var id = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var blockCount = int.Parse(input[i].ToString());
            for (int f = 0; f < blockCount; f++)
            {
                blocks.Add(i % 2 == 0 ? id : null);
            }
            if (i % 2 == 0)
            {
                id++;
            }
        }
        return blocks;
    }

    private long CalculateChecksum(List<int?> blocks)
    {
        long result = 0;
        for (int i = 0; i < blocks.Count; i++)
        {
            var block = blocks[i];
            if (block is not null)
            {
                result += block.Value * i;
            }
        }
        return result;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var input = inputLines.First();
        var blocks = LoadMemory(input);
        var idsAlreadyMoved = new HashSet<int>();

        int segLength = 0;
        int? currId = null;
        for (var i = blocks.Count - 1; i >= 0; i--)
        {
            var block = blocks[i];
            if (block != currId)
            {
                if (currId != null)
                {
                    var newLocation = FindIndexOfFreeSpaceOfSize(segLength, blocks);
                    if (newLocation != -1 && newLocation < i && !idsAlreadyMoved.Contains(currId.Value)) {
                        idsAlreadyMoved.Add(currId.Value);
                        for (var j = newLocation; j < segLength + newLocation; j++) {
                            blocks[j] = currId;
                            
                        }
                        for (var j = i + 1; j < segLength + i + 1; j++) {
                            blocks[j] = null;
                        }
                    }
                }
                currId = block;
                segLength = 1;
            }
            else
            {
                segLength++;
            }
        }

        return Task.FromResult(CalculateChecksum(blocks).ToString());
    }

    private int FindIndexOfFreeSpaceOfSize(int size, List<int?> blocks) {
        int currSize = 0;
        for (var i = 0; i < blocks.Count; i++) {
            if (!blocks[i].HasValue) {
                currSize++;
            } else {
                currSize = 0;
            }
            if (currSize == size) {
                return i + 1 - currSize;
            }
        }
        return -1;
    }
}
