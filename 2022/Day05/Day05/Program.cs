namespace Day05;

public static class Program
{
    static List<Stack> stacks;
    private static int rowIndex;

    public static void Main()
    {
        // Initialise stacks
        var strings = File.ReadAllLines("input.txt");
        ResetStacks(strings);

        // Part 1
        for (var i = rowIndex + 2; i < strings.Length; i++)
        {
            var command = new int[3];
            command[0] = int.Parse(strings[i].Split("from")[0].Split(' ')[1]);
            command[1] = int.Parse(strings[i].Split("from")[1].Split("to")[0]) - 1;
            command[2] = int.Parse(strings[i].Split("from")[1].Split("to")[1]) - 1;
            
            stacks[command[2]].AddCrates(stacks[command[1]].TakeCrates9000(command[0]));
        }

        var code = "";
        foreach (var stack in stacks)
        {
            code += stack.Crates[^1].character;
        }

        Console.WriteLine("Part 1: {0}", code);
        
        // Part 2
        ResetStacks(strings);
        for (var i = rowIndex + 2; i < strings.Length; i++)
        {
            var command = new int[3];
            command[0] = int.Parse(strings[i].Split("from")[0].Split(' ')[1]);
            command[1] = int.Parse(strings[i].Split("from")[1].Split("to")[0]) - 1;
            command[2] = int.Parse(strings[i].Split("from")[1].Split("to")[1]) - 1;
            
            stacks[command[2]].AddCrates(stacks[command[1]].TakeCrates9001(command[0]));
        }

        code = "";
        foreach (var stack in stacks)
        {
            code += stack.Crates[^1].character;
        }
        
        Console.WriteLine("Part 2: {0}", code);
    }

    private static void ResetStacks(string[] strings)
    {
        stacks = new List<Stack>();
        
        rowIndex = 0;
        while (true)
        {
            if (!strings[rowIndex].Contains('['))
            {
                foreach (var c in strings[rowIndex].Where(c => c != ' '))
                {
                    stacks.Add(new Stack());
                }

                break;
            }

            rowIndex++;
        }

        for (var i = 0; i < rowIndex; i++)
        {
            var stackIndex = 0;

            for (var j = 1; j < strings[i].Length; j += 4)
            {
                if (strings[i][j] != ' ')
                {
                    stacks[stackIndex].InsertCrate(strings[i][j]);
                }
    
                stackIndex++;
            }
        }
    }
}

class Crate
{
    public char character { get; }

    public Crate(char c)
    {
        character = c;
    }
}

class Stack
{
    public List<Crate> Crates;

    public Stack()
    {
        Crates = new List<Crate>();
    }

    public void InsertCrate(char c)
    {
        Crates.Insert(0, new Crate(c));
    }

    public List<Crate> TakeCrates9000(int total)
    {
        var temp = new List<Crate>();
        
        for (var i = Crates.Count - 1; i > Crates.Count - 1 - total; i--)
        {
            temp.Add(Crates[i]);
        }
        
        Crates.RemoveRange(Crates.Count - total, total);

        return temp;
    }
    
    public List<Crate> TakeCrates9001(int total)
    {
        var temp = new List<Crate>();
        
        for (var i = Crates.Count - total; i < Crates.Count ; i++)
        {
            temp.Add(Crates[i]);
        }
        
        Crates.RemoveRange(Crates.Count - total, total);

        return temp;
    }

    public void AddCrates(List<Crate> cratesToAdd)
    {
        foreach (var crate in cratesToAdd)
        {
            Crates.Add(crate);
        }
    }
}