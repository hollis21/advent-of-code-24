public class Day4 : IDay
{
    public string Title => "Ceres Search";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var puzzle = inputLines.ToArray();
        var count = 0;
        string word;
        for (int y = 0; y < puzzle.Length; y++)
        {
            for (int x = 0; x < puzzle[y].Length; x++)
            {
                if (x <= puzzle[y].Length - 3 - 1)
                {
                    // check for XMAS and SAMX to the right
                    word = puzzle[y].Substring(x, 4);
                    if (word == "XMAS" || word == "SAMX")
                    {
                        count++;
                    }
                    if (y >= 3)
                    {
                        // check for XMAS and SAMX diag up right
                        word = string.Join("", puzzle[y][x], puzzle[y - 1][x + 1], puzzle[y - 2][x + 2], puzzle[y - 3][x + 3]);
                        if (word == "XMAS" || word == "SAMX")
                        {
                            count++;
                        }
                    }
                    if (y <= puzzle.Length - 3 - 1)
                    {
                        // check for XMAS and SAMX diag down right
                        word = string.Join("", puzzle[y][x], puzzle[y + 1][x + 1], puzzle[y + 2][x + 2], puzzle[y + 3][x + 3]);
                        if (word == "XMAS" || word == "SAMX")
                        {
                            count++;
                        }
                    }
                }
                if (y <= puzzle.Length - 3 - 1)
                {
                    // check for XMAS and SAMX down
                    word = string.Join("", puzzle[y][x], puzzle[y + 1][x], puzzle[y + 2][x], puzzle[y + 3][x]);
                    if (word == "XMAS" || word == "SAMX")
                    {
                        count++;
                    }
                }
            }
        }
        return Task.FromResult(count.ToString());
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var puzzle = inputLines.ToArray();
        var count = 0;
        string word1;
        string word2;
        for (int y = 1; y < puzzle.Length - 1; y++)
        {
            for (int x = 1; x < puzzle[y].Length - 1; x++)
            {
                word1 = string.Join("", puzzle[y - 1][x - 1], puzzle[y][x], puzzle[y + 1][x + 1]);
                word2 = string.Join("", puzzle[y - 1][x + 1], puzzle[y][x], puzzle[y + 1][x - 1]);
                if ((word1 == "MAS" || word1 == "SAM") && (word2 == "MAS" || word2 == "SAM"))
                {
                    count++;
                }
            }
        }
        return Task.FromResult(count.ToString());
    }
}
