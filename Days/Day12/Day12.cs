public class Day12 : IDay
{
    public string Title => "Garden Groups";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        char[][] map = inputLines.Select(Enumerable.ToArray).ToArray();
        List<(int paremeter, int area)> regions = [];

        HashSet<Position> visited = [];
        Queue<Position> queue = [];
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map.Length; x++)
            {
                if (visited.Contains(new Position(x, y)))
                {
                    continue;
                }
                var area = 0;
                var paremeter = 0;
                queue.Enqueue(new Position(x, y));
                while (queue.TryDequeue(out var curr))
                {
                    if (visited.Contains(curr)) {
                        continue;
                    }
                    area++;
                    var surrounding = GetSurroundingPositions(curr);
                    foreach (var pos in surrounding)
                    {
                        if (!IsInMap(pos, map) || map[pos.y][pos.x] != map[curr.y][curr.x])
                        {
                            paremeter++;
                        }
                        else
                        {
                            queue.Enqueue(pos);
                        }
                    }
                    visited.Add(curr);
                }
                regions.Add((paremeter, area));
            }
        }
        var result = regions.Sum(r => (long)(r.area * r.paremeter));
        return Task.FromResult(result.ToString());
    }

    private Position[] GetSurroundingPositions(Position position)
    {
        return [
            position with {x = position.x-1},
            position with {x = position.x+1},
            position with {y = position.y-1},
            position with {y = position.y+1}
        ];
    }

    private bool IsInMap(Position position, char[][] map)
    {
        return position.x >= 0 && position.y >= 0 && position.y < map.Length && position.x < map[0].Length;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        throw new NotImplementedException();
    }
    record Position(int x, int y);
}
