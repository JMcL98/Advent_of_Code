namespace Day03;

public static class Program
{
    public static void Main()
    {
        var strings = File.ReadAllLines("input.txt");
        var rucksacks = strings.Select(s => new Rucksack(s)).ToList();

        // Part 1
        Console.WriteLine("Part 1: {0}", rucksacks.Sum(x => x.GetPriorityOfSharedItem()));

        // Part 2
        var total = 0;
        for (var i = 0; i < rucksacks.Count; i += 3)
        {
            total += Rucksack.GetPriorityOfBadge(strings[i], strings[i + 1], strings[i + 2]);
        }

        Console.WriteLine("Part 2: {0}", total);
    }
}

public class Rucksack
{
    private readonly string _compartment1;
    private readonly string _compartment2;

    public Rucksack(string items)
    {
        _compartment1 = items.Substring(0, items.Length / 2);
        _compartment2 = items.Substring(items.Length / 2, items.Length / 2);
    }

    public int GetPriorityOfSharedItem()
    {
        var c = _compartment1.Intersect(_compartment2).First();
        return GetPriorityOfAnyItem(c);
    }

    public static int GetPriorityOfBadge(string rucksack1, string rucksack2, string rucksack3)
    {
        var c = rucksack1.Intersect(rucksack2).Intersect(rucksack3).First();
        return GetPriorityOfAnyItem(c);
    }

    private static int GetPriorityOfAnyItem(char c)
    {
        return c < 97 ? c - 38 : c - 96;
    }
}