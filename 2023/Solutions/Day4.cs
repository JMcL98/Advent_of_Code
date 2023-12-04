namespace _2023.Solutions;

public static class Day4
{
    public static string Part1(List<string> input)
    {
        var total = 0;
        foreach (var scratchCard in CreateScratchCards(input))
        {
            total += scratchCard.GetPointValueOfCard();
        }

        return total.ToString();
    }

    public static string Part2(List<string> input)
    {
        var allScratchCards = CreateScratchCards(input);

        for (var i = 0; i < allScratchCards.Count; i++)
        {
            for (var j = i + 1; j <= i + allScratchCards[i].GetNumberOfIntersectedValues(); j++)
            {
                allScratchCards[j].Copies += allScratchCards[i].Copies;
            }
        }

        return allScratchCards.Sum(scratchCard => scratchCard.Copies).ToString();
    }

    private static List<ScratchCard> CreateScratchCards(List<string> input)
    {
        var allScratchCards = new List<ScratchCard>();

        foreach (var line in input)
        {
            var winningNumbers = line.Split(':')[1].Split('|')[0];
            var ownedNumbers = line.Split(':')[1].Split('|')[1];
            allScratchCards.Add(new ScratchCard(winningNumbers, ownedNumbers));
        }

        return allScratchCards;
    }
}

internal class ScratchCard
{
    internal int Copies { get; set; }
    private List<int> _winningNumbers;
    private List<int> _ownedNumbers;

    internal ScratchCard(string winningNumbers, string ownedNumbers)
    {
        Copies = 1;
        SetWinningNumbersAndOwnedNumbers(winningNumbers.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList(), ownedNumbers.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
    }

    private void SetWinningNumbersAndOwnedNumbers(List<string> winningNumbers, List<string> ownedNumbers)
    {
        _winningNumbers = new();
        _ownedNumbers = new();

        foreach (var winningNum in winningNumbers)
        {
            _winningNumbers.Add(int.Parse(winningNum));
        }

        foreach (var ownedNum in ownedNumbers)
        {
            _ownedNumbers.Add(int.Parse(ownedNum));
        }
    }

    internal int GetPointValueOfCard()
    {
        var intersection = _winningNumbers.Intersect(_ownedNumbers).ToList();
        if (intersection.Count > 0)
        {
            return (int)(1 * Math.Pow(2, intersection.Count - 1));
        }

        return 0;
    }

    internal int GetNumberOfIntersectedValues()
    {
        return _winningNumbers.Intersect(_ownedNumbers).ToList().Count;
    }
}