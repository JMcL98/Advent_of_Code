namespace _2023.Solutions;

public static class Day2
{
    public static string Part1(List<string> input, int maxRedCubes, int maxBlueCubes, int maxGreenCubes)
    {
        var total = 0;
        foreach (var game in GetGamesList(input))
        {
            total += game.GetGameNumberIfValidGame(maxRedCubes, maxBlueCubes, maxGreenCubes);
        }

        return total.ToString();
    }

    public static string Part2(List<string> input)
    {
        var total = 0;
        foreach (var game in GetGamesList(input))
        {
            total += game.GetPowerOfGame();
        }

        return total.ToString();
    }

    private static List<Game> GetGamesList(List<string> input)
    {
        var allGames = new List<Game>();
        foreach (var line in input)
        {
            var gameNum = int.Parse(line.Split(':')[0].Split(' ')[1]);
            allGames.Add(new Game(gameNum, line.Split(':')[1]));
        }

        return allGames;
    }
}

internal class Game
{
    private int gameNumber;

    private int minRedCubes = 0;
    private int minBlueCubes = 0;
    private int minGreenCubes = 0;

    internal Game(int gameNumber, string gameText)
    {
        this.gameNumber = gameNumber;
        var splitChars = new[] { ',', ';' };
        var cubeReveals = gameText.Split(splitChars).Select(s => s.Trim()).ToArray();

        foreach (var reveal in cubeReveals)
        {
            var count = int.Parse(reveal.Split(' ')[0]);
            var colour = reveal.Split(' ')[1];

            switch (colour.ToLower())
            {
                case "red":
                    minRedCubes = count > minRedCubes ? count : minRedCubes;
                    break;

                case "blue":
                    minBlueCubes = count > minBlueCubes ? count : minBlueCubes;
                    break;

                case "green":
                    minGreenCubes = count > minGreenCubes ? count : minGreenCubes;
                    break;
            }
        }
    }

    internal int GetGameNumberIfValidGame(int maxRed, int maxBlue, int maxGreen)
    {
        if (minRedCubes <= maxRed && minBlueCubes <= maxBlue && minGreenCubes <= maxGreen)
        {
            return gameNumber;
        }

        return 0;
    }

    internal int GetPowerOfGame()
    {
        return minRedCubes * minBlueCubes * minGreenCubes;
    }
}