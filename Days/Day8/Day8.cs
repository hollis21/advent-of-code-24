public class Day8 : IDay
{
    public string Title => "Resonant Collinearity";

    public Task<string> SolutionA(IEnumerable<string> inputLines)
    {
        var map = inputLines.ToList();
        var antennas = ParseMap(map);
        var antinodes = new HashSet<Position>();

        foreach (var set in antennas.Values)
        {
            for (int i = 0; i < set.Count; i++)
            {
                Position? pos = set[i];
                for (int j = 0; j < set.Count; j++)
                {
                    Position? otherPos = set[j];
                    if (i == j)
                    {
                        continue;
                    }
                    if (TestInMap(pos + (pos - otherPos), map)) antinodes.Add(pos + (pos - otherPos));
                    if (TestInMap(otherPos + (otherPos - pos), map)) antinodes.Add(otherPos + (otherPos - pos));
                }
            }
        }
        return Task.FromResult(antinodes.Count.ToString());
    }

    private bool TestInMap(Position position, List<string> map)
    {
        var width = map[0].Length;
        var height = map.Count;
        return position.y < height && position.y >= 0 && position.x < width && position.x >= 0;
    }

    private Dictionary<char, List<Position>> ParseMap(List<string> inputLines)
    {
        var antennas = new Dictionary<char, List<Position>>();
        for (int y = 0; y < inputLines.Count; y++)
        {
            string? line = inputLines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char position = line[x];
                if (position == '.')
                {
                    continue;
                }
                if (!antennas.ContainsKey(position))
                {
                    antennas.Add(position, new List<Position>());
                }
                antennas[position].Add(new Position(x, y));
            }
        }
        return antennas;
    }

    public Task<string> SolutionB(IEnumerable<string> inputLines)
    {
        var map = inputLines.ToList();
        var antennas = ParseMap(map);
        var antinodes = new HashSet<Position>();

        foreach (var set in antennas.Values)
        {
            for (int i = 0; i < set.Count; i++)
            {
                Position? pos = set[i];
                for (int j = 0; j < set.Count; j++)
                {
                    Position? otherPos = set[j];
                    if (i == j)
                    {
                        continue;
                    }
                    var delta = otherPos - pos;
                    int mult = 1;
                    while (TestInMap(pos + (delta * mult), map))
                    {
                        antinodes.Add(pos + (delta * mult));
                        mult++;
                    }
                    mult = 1;
                    while (TestInMap(otherPos - (delta * mult), map))
                    {
                        antinodes.Add(otherPos - (delta * mult));
                        mult++;
                    }
                }
            }
        }
        return Task.FromResult(antinodes.Count.ToString());
    }

    record Position(int x, int y)
    {
        public static Position operator +(Position a, Delta d)
        {
            return new Position(a.x + d.dx, a.y + d.dy);
        }
        public static Position operator -(Position a, Delta d)
        {
            return new Position(a.x - d.dx, a.y - d.dy);
        }
        public static Delta operator -(Position a, Position b)
        {
            return new Delta(a.x - b.x, a.y - b.y);
        }
    }
    record Delta(int dx, int dy)
    {
        public static Delta operator *(Delta a, int b)
        {
            return new Delta(a.dx * b, a.dy * b);
        }
    }
}
