namespace Day01;

public static class Program
{
    public static void Main()
    {
        // Initialize Variables
        var strings = File.ReadAllLines("input.txt");
        var elfCalories = new List<int> { 0 };
        var elfIndex = 0;

        foreach (var s in strings)
        {
            if (string.IsNullOrWhiteSpace(s))
            { 
                elfCalories.Add(0);
                elfIndex++;
                continue;
            }

            elfCalories[elfIndex] += int.Parse(s);
        }

        // Part 1
        Console.WriteLine("Part 1: {0}", elfCalories.Max());
        
        // Part 2
        elfCalories.Sort();
        Console.WriteLine("Part 2: {0}", elfCalories[^1] + elfCalories[^2] + elfCalories[^3]);
    }
}