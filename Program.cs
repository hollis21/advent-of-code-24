IDay[] days = [
    new Day1()
    , new Day2()
    , new Day3()
    , new Day4()
    , new Day5()
];

Console.WriteLine("Mike's Advent of Code 2024");
Console.WriteLine("-Available Days-");
for (int i = 0; i < days.Length; i++)
{
    Console.WriteLine($"{i + 1} - {days[i].Day} - {days[i].Title}");
}
Console.WriteLine("Select a day:");
var response = Console.ReadLine();

if (!int.TryParse(response, out int selectedDay) || selectedDay <= 0 || selectedDay > days.Length)
{
    Console.WriteLine();
    Console.WriteLine("Invalid entry. Exiting.");
    return;
}

Console.WriteLine("Solution A or B?");
response = Console.ReadLine();
if (response == null || (response.ToUpper() != "A" && response.ToUpper() != "B"))
{
    Console.WriteLine();
    Console.WriteLine("Invalid entry. Exiting.");
    return;
}

IDay day = days[selectedDay - 1];

var inputFile = $"Days/{day.GetType().Name}/input.txt";
if (!File.Exists(inputFile))
{
    Console.WriteLine();
    Console.WriteLine("Input file missing. Exiting.");
    return;
}

using (var sr = new StreamReader(inputFile))
{
    string? result = null;
    var lines = sr.ToIEnumerable();
    if (response.ToUpper() == "A")
    {
        result = await day.SolutionA(lines);
    }
    else
    {
        result = await day.SolutionB(lines);
    }
    Console.WriteLine();
    Console.WriteLine("-Solution-");
    Console.WriteLine(result);
}