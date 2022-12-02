namespace Day02;

public static class Program
{
    public static void Main()
    {
        // Initialize Variables
        var strings = File.ReadAllLines("input.txt");
        var games = strings.Select(s => new Game(s)).ToList();

        // Part 1
        Console.WriteLine("Part 1: {0}", games.Sum(x => x.CalculateScore(1)));

        // Part 2
        Console.WriteLine("Part 2: {0}", games.Sum(x => x.CalculateScore(2)));
    }
}

public class Game
{
    private readonly char _elfMove;
    private readonly char _myMove;

    public Game(string moves)
    {
        _elfMove = moves[0];
        _myMove = moves[2];
    }

    public int CalculateScore(int part)
    {
        var score = 0;

        switch (part)
        {
            case 1:
                switch (_myMove)
                {
                    case 'X':
                        score = 1;
                        score += _elfMove == 'C' ? 6 : 0;
                        score += _elfMove == 'A' ? 3 : 0;
                        break;

                    case 'Y':
                        score = 2;
                        score += _elfMove == 'A' ? 6 : 0;
                        score += _elfMove == 'B' ? 3 : 0;
                        break;

                    case 'Z':
                        score = 3;
                        score += _elfMove == 'B' ? 6 : 0;
                        score += _elfMove == 'C' ? 3 : 0;
                        break;
                }

                break;

            case 2:
                score = _myMove switch
                {
                    'X' => _elfMove switch
                    {
                        'A' => 3,
                        'B' => 1,
                        'C' => 2,
                        _ => 0
                    },
                    
                    'Y' => _elfMove switch
                    {
                        'A' => 3 + 1,
                        'B' => 3 + 2,
                        'C' => 3 + 3,
                        _ => 0
                    },
                    
                    'Z' => _elfMove switch
                    {
                        'A' => 6 + 2,
                        'B' => 6 + 3,
                        'C' => 6 + 1,
                        _ => 0
                    }
                };
                
                break;
        }

        return score;
    }
}