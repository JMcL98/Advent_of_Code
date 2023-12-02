namespace _2023.Solutions;

public static class Day2
{
    public static string Part1(List<string> input, int maxRedCubes, int maxBlueCubes, int maxGreenCubes)
    {
        var allGames = new List<Game>();
        foreach (var line in input)
        {
            var gameNum = int.Parse(line.Split(':')[0].Split(' ')[1]);
            allGames.Add(new Game(gameNum, line.Split(':')[1]));
        }

        var total = 0;
        foreach (var game in allGames)
        {
            total += game.GetGameNumberIfValidGame(maxRedCubes, maxBlueCubes, maxGreenCubes);
        }

        return total.ToString();
    }

}

internal class Game
{
    private int gameNumber;
    private int maxRedCubes = 0;
    private int maxBlueCubes = 0;
    private int maxGreenCubes = 0;

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
                    maxRedCubes = count > maxRedCubes ? count : maxRedCubes;
                    break;

                case "blue":
                    maxBlueCubes = count > maxBlueCubes ? count : maxBlueCubes;
                    break;

                case "green":
                    maxGreenCubes = count > maxGreenCubes ? count : maxGreenCubes;
                    break;
            }
        }
    }

    internal int GetGameNumberIfValidGame(int maxRed, int maxBlue, int maxGreen)
    {
        if (maxRedCubes <= maxRed && maxBlueCubes <= maxBlue && maxGreenCubes <= maxGreen)
        {
            return gameNumber;
        }

        return 0;
    }
}