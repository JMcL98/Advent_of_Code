namespace _2023.Solutions;

public static class Day1
{
    public static string Part1(List<string> input)
    {
        var total = 0;
        foreach (var line in input)
        {
            var allNums = new List<int>();
            foreach (var c in line)
            {
                if (int.TryParse(c.ToString(), out var n))
                {
                    allNums.Add(n);
                }
            }

            if (allNums.Count > 0)
            {
                total += int.Parse(allNums[0].ToString() + allNums[^1]);
            }
        }

        return total.ToString();
    }

    public static string Part2(List<string> input)
    {
        var total = 0;
        foreach (var line in input)
        {
            var allNums = new List<int>();
            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out var n))
                {
                    allNums.Add(n);
                }
                else
                {
                    var checkLettersVal = CheckIfWordNum(line[i..]);
                    if (checkLettersVal.Item1 != 0)
                    {
                        allNums.Add(checkLettersVal.Item1);
                    }

                    if (checkLettersVal.Item2 != 0)
                    {
                        i += checkLettersVal.Item2 - 2;
                    }
                }
            }

            if (allNums.Count > 0)
            {
                total += int.Parse(allNums[0].ToString() + allNums[^1]);
            }
        }

        return total.ToString();
    }

    private static (int, int) CheckIfWordNum(string s)
    {
        for (var i = 2; i < s.Length && i < 5; i++)
        {
            var wordLength = i + 1;
            var subS = s.Substring(0, wordLength);
            switch (subS.ToLower())
            {
                case "one":
                    return (1, wordLength);

                case "two":
                    return (2, wordLength);

                case "three":
                    return (3, wordLength);

                case "four":
                    return (4, wordLength);

                case "five":
                    return (5, wordLength);

                case "six":
                    return (6, wordLength);

                case "seven":
                    return (7, wordLength);

                case "eight":
                    return (8, wordLength);

                case "nine":
                    return (9, wordLength);

                default:
                   // if (int.TryParse(subS[^1].ToString(), out int _))
                   // {
                   //     return (0, wordLength - 1);
                   // }

                    break;
            }
        }

        return (0, 0);
    }

}