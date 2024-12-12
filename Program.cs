using AdventOfCode;
using System.Linq.Expressions;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        RunMode mode = RunMode.Both;
        int year = 2024;
        int day = 12;
        //int? day = args.Length > 0 ? int.Parse(args[0]) : (int?)null;
        bool useTest = args.Contains("-t");

        string fileName = useTest ? $"samples/{year}/Day{day}.txt" : $"inputs/{year}/Day{day}.txt";

        var input = File.ReadAllLines(fileName);

        IDaySolver solver = CreateDayInstance(day);
        var sample = useTest ? "SAMPLE " : string.Empty; ;
        if (mode == RunMode.Both || mode == RunMode.Part1)
        {
            string run = $"{year} Day {day} Part 1 {sample}";
            Console.WriteLine($"Running {run}...");
            var solution = solver.Part1(input);
            Console.WriteLine($"{run} Solution: {solution}");
            Console.WriteLine();
        }

        if (mode == RunMode.Both || mode == RunMode.Part2)
        {
            string run = $"{year} Day {day} Part 2 {sample}";
            Console.WriteLine($"Running {run}...");
            var solution = solver.Part2(input);
            Console.WriteLine($"{run} Solution: {solution}");
            Console.WriteLine();
        }

        //Console.ReadLine();
    }

    static IDaySolver CreateDayInstance(int? day = null)
    {
        Type dayType = null;
        if (day.HasValue)
        {
            dayType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"Day{day}" && typeof(IDaySolver).IsAssignableFrom(t));
        }
        else
        {
            dayType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.StartsWith($"Day") && typeof(IDaySolver).IsAssignableFrom(t))
                .OrderByDescending(t => int.Parse(t.Name.Substring(3, t.Name.Length - 1)))
                .FirstOrDefault();
        }
        //string className = $"Day{day}";
        //Type dayType = Assembly.GetExecutingAssembly()
        //    .GetTypes()
        //    .FirstOrDefault(t => t.Name == className && typeof(IDaySolver).IsAssignableFrom(t));
        

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
}