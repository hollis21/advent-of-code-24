public interface IDay
{
    string Title {get;}
    string Day => GetType().Name;
    Task<string> SolutionA(IEnumerable<string> inputLines);
    Task<string> SolutionB(IEnumerable<string> inputLines);
}
