namespace Day04;

public static class Program
{
    public static void Main()
    {
        var strings = File.ReadAllLines("input.txt");

        int part1Total = 0, part2Total = 0;
        
        foreach (var s in strings)
        {
            var elf1From = int.Parse(s.Split(',')[0].Split('-')[0]);
            var elf1To = int.Parse(s.Split(',')[0].Split('-')[1]);
            var elf2From = int.Parse(s.Split(',')[1].Split('-')[0]);
            var elf2To = int.Parse(s.Split(',')[1].Split('-')[1]);

            var elf1Assignment = new int[elf1To - elf1From + 1];
            var elf2Assignment = new int[elf2To - elf2From + 1];

            var index = 0;
            for (var i = elf1From; i <= elf1To; i++)
            {
                elf1Assignment[index] = i;
                index++;
            }

            index = 0;
            for (var i = elf2From; i <= elf2To; i++)
            {
                elf2Assignment[index] = i;
                index++;
            }

            // Part 1
            if (!elf1Assignment.Except(elf2Assignment).Any() | !elf2Assignment.Except(elf1Assignment).Any())
            {
                part1Total++;
            }

            // Part 2
            if (elf1Assignment.Any(x => elf2Assignment.Contains(x)) || elf2Assignment.Any(y => elf1Assignment.Contains(y)))
            {
                part2Total++;
            }
        }

        Console.WriteLine("Part 1: {0}", part1Total);
        Console.WriteLine("Part 2: {0}", part2Total);
    }
}