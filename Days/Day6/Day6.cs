public class Day6 : IDay
{
    public string Title => "Guard Gallivant";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var map = inputLines.Select(line => line.ToArray()).ToArray();
        Position currPosition = GetStarting(map);
        var facing = "N";
        var count = 1;
        map[currPosition.y][currPosition.x] = 'X';
        while (true)
        {
            var newPosition = Move(currPosition, facing);
            if (IsOutsideMap(newPosition, map))
            {
                // DrawMap(map);
                return Task.FromResult(count.ToString());
            }
            if (map[newPosition.y][newPosition.x] == '#')
            {
                facing = Turn90(facing);
                continue;
            }
            
            currPosition = newPosition;
            if (map[currPosition.y][currPosition.x] != 'X')
            {
                map[currPosition.y][currPosition.x] = 'X';
                count++;
            }
        }
    }

    private string Turn90(string facing)
    {
        List<string> directions = ["N", "E", "S", "W"];
        return directions[(directions.IndexOf(facing) + 1) % 4];
    }

    private Position GetStarting(char[][] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == '^')
                {
                    return new(x: j, y: i);
                }
            }
        }
        throw new Exception("Starting position not found");
    }

    private bool IsOutsideMap<T>(Position position, T[][] map)
    {
        return position.x < 0 || position.x >= map[0].Length ||
            position.y < 0 || position.y >= map.Length;
    }

    private Position Move(Position position, string facing)
    {
        switch (facing)
        {
            case "N":
                return position with { y = position.y - 1 };
            case "S":
                return position with { y = position.y + 1 };
            case "W":
                return position with { x = position.x - 1 };
            case "E":
                return position with { x = position.x + 1 };
        }
        throw new Exception("Invalid facing");
    }

    private bool IsObstacle(Position position, char[][] map)
    {
        return map[position.y][position.x] == '#';
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var map = inputLines.Select(line => line.ToArray()).ToArray();
        Position currPosition = GetStarting(map);
        var startingPosition = currPosition with { };
        var facing = "N";
        // Build potential new obstacle list
        var newObstacles = new HashSet<Position>();
        while (true)
        {
            var nextPosition = Move(currPosition, facing);
            if (IsOutsideMap(nextPosition, map))
            {
                break;
            }
            if (IsObstacle(nextPosition, map))
            {
                facing = Turn90(facing);
                continue;
            }
            newObstacles.Add(nextPosition with { });
            currPosition = nextPosition;
        }
        // Remove starting position if it was added
        newObstacles.Remove(startingPosition);
        int count = 0;
        foreach (var newObs in newObstacles)
        {
            var mapCopy = map.Select(r => r.Select(c => c).ToArray()).ToArray();
            if (WillMakeCircle(startingPosition with { }, "N", mapCopy, newObs))
            {
                count++;
            }
        }
        return Task.FromResult(count.ToString());
    }

    private bool WillMakeCircle(Position position, string facing, char[][] map, Position newObs)
    {
        var starting = position with { };
        map[newObs.y][newObs.x] = '#';
        var visited = new HashSet<(Position position, string facing)>() { (position, "N") };
        while (true)
        {
            var nextPosition = Move(position, facing);
            if (IsOutsideMap(nextPosition, map))
            {
                return false;
            }
            if (IsObstacle(nextPosition, map))
            {
                facing = Turn90(facing);
                continue;
            }
            if (visited.Contains((nextPosition, facing)))
            {
                // DrawMap(map, visited, starting, newObs);
                return true;
            }
            position = nextPosition;
            visited.Add((position, facing));
        }
    }

    private char FacingToDrawing(char current, string facing)
    {
        var line = facing == "N" || facing == "S" ? '|' : '-';
        if (current != '.' & current != line)
        {
            return '+';
        }
        else
        {
            return line;
        }
    }

    private void DrawMap(char[][] map, HashSet<(Position position, string facing)>? visited = null, Position? starting = null, Position? newObs = null)
    {
        if (visited != null)
        {
            foreach (var visit in visited)
            {
                map[visit.position.y][visit.position.x] = FacingToDrawing(map[visit.position.y][visit.position.x], visit.facing);
            }
        }
        if (starting != null)
        {
            map[starting.y][starting.x] = '^';
        }
        if (newObs != null)
        {
            map[newObs.y][newObs.x] = 'O';
        }
        foreach (var row in map)
        {
            Console.WriteLine(string.Join("", row));
        }
        Console.WriteLine();
    }

    record Position(int x, int y);
}
