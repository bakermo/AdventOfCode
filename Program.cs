using AdventOfCode;
using System.Reflection;

RunMode mode = RunMode.Both;
int year = 2024;
int day = 2;
bool useTest = false;

string fileName = useTest ? $"samples/{year}/Day{day}.txt" : $"inputs/{year}/Day{day}.txt";

var input = File.ReadAllLines(fileName);

IDaySolver solver = CreateDayInstance(day);

if (mode == RunMode.Both || mode == RunMode.Part1)
{
    string run = $"{year} Day {day} Part 1";
    Console.WriteLine($"Running {run}...");
    var solution = solver.Part1(input);
    Console.WriteLine($"{run} Solution: {solution}");
    Console.WriteLine();
}

if (mode == RunMode.Both || mode == RunMode.Part2)
{
    string run = $"{year} Day {day} Part 2";
    Console.WriteLine($"Running {run}...");
    var solution = solver.Part2(input);
    Console.WriteLine($"{run} Solution: {solution}");
    Console.WriteLine();
}

//Console.ReadLine();

IDaySolver CreateDayInstance(int day)
{
    string className = $"Day{day}";
    Type dayType = Assembly.GetExecutingAssembly()
        .GetTypes()
        .FirstOrDefault(t => t.Name == className && typeof(IDaySolver).IsAssignableFrom(t));

    if (dayType != null)
    {
        return (IDaySolver)Activator.CreateInstance(dayType);
    }

    return null;
}

enum RunMode
{
    Part1,
    Part2,
    Both
}