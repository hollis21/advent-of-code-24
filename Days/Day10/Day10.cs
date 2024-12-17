public class Day10 : IDay
{
    public string Title => "Hoof It";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        int[][] map = inputLines.Select(r => r.Select(c => c == '.' ? -1 : int.Parse(c.ToString())).ToArray()).ToArray();
        List<Position> heads = [];
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    heads.Add(new Position(j, i));
                }
            }
        }
        var result = heads.Sum(h =>
        {
            HashSet<Position> visited = [];
            return GetTrailHeadScore(map, h, visited);
        });
        return Task.FromResult(result.ToString());
    }

    private static int GetTrailHeadScore(int[][] map, Position head, HashSet<Position> visited)
    {
        visited.Add(head);
        if (map[head.Y][head.X] == 9)
        {
            return 1;
        }
        var surrounding = GetSurroundingPositions(map, head).Where(p => !visited.Contains(p)).ToArray();
        if (surrounding.Length == 0)
        {
            return 0;
        }
        var sum = surrounding.Sum(p => GetTrailHeadScore(map, p, visited));
        return sum;
    }

    private static Position[] GetSurroundingPositions(int[][] map, Position position)
    {
        Position[] positions = [
            position with { X = position.X + 1 },
            position with { X = position.X - 1 },
            position with { Y = position.Y + 1 },
            position with { Y = position.Y - 1 }
        ];
        return positions.Where(p => CheckIfInMap(map, p) && map[p.Y][p.X] == map[position.Y][position.X] + 1).ToArray();
    }

    private static bool CheckIfInMap(int[][] map, Position position)
    {
        return !(position.X < 0 || position.X >= map[0].Length || position.Y < 0 || position.Y >= map.Length);
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        int[][] map = inputLines.Select(r => r.Select(c => c == '.' ? -1 : int.Parse(c.ToString())).ToArray()).ToArray();
        List<Position> heads = [];
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    heads.Add(new Position(j, i));
                }
            }
        }
        var result = heads.Sum(h =>
        {
            Dictionary<Position, int> visited = [];
            return GetTrailHeadRating(map, h, visited);
        });
        return Task.FromResult(result.ToString());
    }

    private static int GetTrailHeadRating(int[][] map, Position pos, Dictionary<Position, int> visited)
    {
        if (visited.TryGetValue(pos, out int value))
        {
            return value;
        }
        if (map[pos.Y][pos.X] == 9)
        {
            visited.Add(pos, 1);
            return 1;
        }
        var surrounding = GetSurroundingPositions(map, pos).ToArray();
        if (surrounding.Length == 0)
        {
            visited.Add(pos, 0);
            return 0;
        }
        var sum = surrounding.Sum(p => GetTrailHeadRating(map, p, visited));
        visited.Add(pos, sum);
        return sum;
    }

    private record Position(int X, int Y);
}
