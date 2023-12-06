namespace _2023.Solutions;

public static class Day6
{
    public static string Part1(List<string> input)
    {
        var timeStrings = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var distStrings = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var races = new List<Race>();

        // Skip first item in list
        for (var i = 1; i < timeStrings.Length; i++)
        {
            races.Add(new Race(int.Parse(timeStrings[i]), int.Parse(distStrings[i])));
        }

        long result = 1;
        foreach (var race in races)
        {
            result *= race.CalculateHowManyOptionsToWin();
        }

        return result.ToString();
    }

    public static string Part2(List<string> input)
    {
        var time = long.Parse(input[0].Split(':')[1].Replace(" ", ""));
        var distance = long.Parse(input[1].Split(':')[1].Replace(" ", ""));

        return new Race(time, distance).CalculateHowManyOptionsToWin().ToString();
    }

    private class Race
    {
        private readonly long _time;
        private readonly long _distanceToBeat;

        internal Race(long time, long distanceToBeat)
        {
            _time = time;
            _distanceToBeat = distanceToBeat;
        }

        internal long CalculateHowManyOptionsToWin()
        {
            var count = 0;

            for (var timeToHoldButton = 0; timeToHoldButton <= _time; timeToHoldButton++)
            {
                var distance = (_time - timeToHoldButton) * timeToHoldButton;
                if (distance > _distanceToBeat)
                {
                    count++;
                }
            }

            return count;
        }
    }
}