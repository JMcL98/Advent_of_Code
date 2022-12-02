namespace Day02;

public static class Program
{
    // 1,X = Rock. 2,Y = Paper. 3,Z = Scissors.
    public static void Main()
    {
        // Initialize Variables
        var strings = File.ReadAllLines("input.txt");
        var games = new List<Game>();

        foreach (var s in strings)
        {
            games.Add(new Game(s));
        }

        Console.WriteLine("Part 1: {0}", games.Sum(x => x.CalculateScore()));
        

    }
}

public class Game
{
    public char ElfMove;
    public char MyMove;

    public Game(string moves)
    {
        ElfMove = moves[0];
        MyMove = moves[2];
    }

    public int CalculateScore()
    {
        int score;
        switch (MyMove)
        {
            case 'X':
                score = 1;
                score += ElfMove == 'C' ? 6 : 0;
                score += ElfMove == 'A' ? 3 : 0;
                break;
                
            case 'Y':
                score = 2;
                score += ElfMove == 'A' ? 6 : 0;
                score += ElfMove == 'B' ? 3 : 0;
                break;
            
            case 'Z':
                score = 3;
                score += ElfMove == 'B' ? 6 : 0;
                score += ElfMove == 'C' ? 3 : 0;
                break;
            
            default:
                return 0;
        }

        return score;
    }
}