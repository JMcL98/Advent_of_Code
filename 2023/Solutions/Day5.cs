namespace _2023.Solutions;

public static class Day5
{
    private const string Seed = "seed";
    private const string Soil = "soil";
    private const string Fertilizer = "fertilizer";
    private const string Water = "water";
    private const string Light = "light";
    private const string Temperature = "temperature";
    private const string Humidity = "humidity";
    private const string Location = "location";

    public static string Part1(List<string> input)
    {
        return GetLowestLocationValue(GetInitialSeeds(input), GetAllMaps(input));
    }

    public static string Part2(List<string> input)
    {
        return GetLowestLocationValue(GetInitialSeedsUsingRanges(input), GetAllMaps(input));
    }

    private static string GetLowestLocationValue(List<long> seeds, List<SeedMap> maps)
    {
        var soilMaps = maps.FindAll(x => x.To == Soil);
        var fertilizerMaps = maps.FindAll(x => x.To == Fertilizer);
        var waterMaps = maps.FindAll(x => x.To == Water);
        var lightMaps = maps.FindAll(x => x.To == Light);
        var temperatureMaps = maps.FindAll(x => x.To == Temperature);
        var humidityMaps = maps.FindAll(x => x.To == Humidity);
        var locationMaps = maps.FindAll(x => x.To == Location);

        var lowestLocation = long.MaxValue;

        var i = 0;
        var startTime = DateTime.Now;

        foreach (var seed in seeds)
        {
            var val = seed;
            var currentDestinationMap = Soil;
            while (true)
            {
                var currentVal = val;
                var newVal = currentVal;
                var currentMaps = currentDestinationMap switch
                {
                    Soil => soilMaps,
                    Fertilizer => fertilizerMaps,
                    Water => waterMaps,
                    Light => lightMaps,
                    Temperature => temperatureMaps,
                    Humidity => humidityMaps,
                    Location => locationMaps,
                    _ => throw new Exception()
                };
                foreach (var map in currentMaps)
                {
                    newVal = map.MapValue(currentVal);
                    if (newVal != currentVal)
                    {
                        break;
                    }
                }

                val = newVal;

                if (currentDestinationMap == Location)
                {
                    if (val < lowestLocation)
                    {
                        lowestLocation = val;
                    }
                    break;
                }

                currentDestinationMap = GetNextDestinationMap(currentDestinationMap);
            }

            // Logging
            i++;
            if (i % 1000000 == 0)
            {
                var timeToNow = DateTime.Now - startTime;
                var timePerRun = timeToNow / (double)i;
                Console.WriteLine($"{i} / {seeds.Count}: {(((double)i / (double)seeds.Count) * 100):F2}%. Time to complete: {timePerRun * (seeds.Count - i)}");
            }
        }

        return lowestLocation.ToString();
    }

    private static List<SeedMap> GetAllMaps(List<string> input)
    {
        var maps = new List<SeedMap>();
        var currentBuffer = new List<string>();
        var currentMap = ("", "");
        foreach (var line in input)
        {
            if (line.Contains("map"))
            {
                maps.AddRange(PopulateSeedMaps(currentBuffer, currentMap.Item1, currentMap.Item2));

                currentBuffer.Clear();
                currentMap.Item1 = line.Split('-')[0];
                currentMap.Item2 = line.Split('-')[2].Split(' ')[0];
                continue;
            }
            if (currentMap == ("", "") || string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            currentBuffer.Add(line);
        }
        maps.AddRange(PopulateSeedMaps(currentBuffer, currentMap.Item1, currentMap.Item2));

        return maps;
    }

    private static List<long> GetInitialSeeds(List<string> input)
    {
        var seeds = new List<long>();
        var seedVals = input[0].Split(": ")[1].Split(' ');
        foreach (var seedVal in seedVals)
        {
            seeds.Add(long.Parse(seedVal));
        }

        return seeds;
    }

    private static List<long> GetInitialSeedsUsingRanges(List<string> input)
    {
        var seeds = new List<long>();
        var seedVals = input[0].Split(": ")[1].Split(' ');

        long startVal = 0;
        for (var i = 0; i < seedVals.Length; i++)
        {
            if (i % 2 == 0)
            {
                startVal = long.Parse(seedVals[i]);
            }
            else
            {
                var count = long.Parse(seedVals[i]);
                for (var j = 0; j < count; j++)
                {
                    seeds.Add(startVal + j);
                }
            }
        }

        return seeds;
    }

    private static List<SeedMap> PopulateSeedMaps(List<string> maps, string from, string to)
    {
        var seedMaps = new List<SeedMap>();
        foreach (var map in maps)
        {
            var sourceIndex = map.Split(' ')[1];
            var destIndex = map.Split(' ')[0];
            var l = map.Split(' ')[2];
            seedMaps.Add(new SeedMap(from, to, long.Parse(sourceIndex), long.Parse(destIndex), long.Parse(l)));
        }

        return seedMaps;
    }

    private static string GetNextDestinationMap(string currentDestinationMap)
    {
        switch (currentDestinationMap)
        {
            case Soil:
                return Fertilizer;

            case Fertilizer:
                return Water;

            case Water:
                return Light;

            case Light:
                return Temperature;

            case Temperature:
                return Humidity;

            case Humidity:
                return Location;

            default:
                return "";
        }
    }
}

internal class SeedMap
{
    internal string From { get; set; }
    internal string To { get; set; }
    internal long SourceStartIndex { get; set; }
    internal long DestinationStartIndex { get; set; }
    internal long Length { get; set; }

    internal SeedMap(string from, string to, long sourceStartIndex, long destinationStartIndex, long length)
    {
        From = from;
        To = to;
        SourceStartIndex = sourceStartIndex;
        DestinationStartIndex = destinationStartIndex;
        Length = length;
    }

    internal long MapValue(long val)
    {
        if (SourceStartIndex <= val && val < SourceStartIndex + Length)
        {
            var diff = DestinationStartIndex - SourceStartIndex;
            return val + diff;
        }

        return val;
    }
}