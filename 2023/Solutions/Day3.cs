namespace _2023.Solutions;

public class Day3
{
    public static string Part1(List<string> input)
    {
        var engine = new Engine(input);

        return engine.GetValueOfAllNumbersThatTouchAnySymbol().ToString();
    }

    public static string Part2(List<string> input)
    {
        var engine = new Engine(input);

        return engine.GetValueOfAllGearRatios().ToString();
    }
}

internal class EngineNumber
{
    internal int startIndex { get; set; }
    internal int endIndex { get; set; }
    internal int row { get; set; }
    internal int value { get; set; }

}

internal class EngineSymbol
{
    internal char symbol { get; set; }
    internal int row { get; set; }
    internal int position { get; set; }
}

internal class Engine
{
    internal List<EngineNumber> _engineNumbers;
    internal List<EngineSymbol> _engineSymbols;

    internal Engine(List<string> input)
    {
        _engineNumbers = new List<EngineNumber>();
        _engineSymbols = new List<EngineSymbol>();

        for (var i= 0; i < input.Count; i++)
        {
            var currentNum = "";
            var currentNumStartIndex = -1;
            for (var j = 0; j < input[i].Length; j++)
            {
                var currentChar = input[i][j];
                if (int.TryParse(currentChar.ToString(), out var num))
                {
                    currentNum = $"{currentNum}{num}";
                    if (currentNumStartIndex == -1)
                    {
                        currentNumStartIndex = j;
                    }
                }
                else
                {
                    if (currentChar != '.')
                    {
                        _engineSymbols.Add(new EngineSymbol
                        {
                            position = j,
                            row = i,
                            symbol = currentChar
                        });
                    }

                    if (currentNum != "")
                    {
                        _engineNumbers.Add(new EngineNumber
                        {
                            startIndex = currentNumStartIndex,
                            endIndex = j - 1,
                            row = i,
                            value = int.Parse(currentNum)
                        });

                        currentNum = "";
                        currentNumStartIndex = -1;
                    }
                }
            }

            if (currentNum != "")
            {
                _engineNumbers.Add(new EngineNumber
                {
                    startIndex = currentNumStartIndex,
                    endIndex = input[i].Length - 1,
                    row = i,
                    value = int.Parse(currentNum)
                });
            }
        }
    }

    internal int GetValueOfAllNumbersThatTouchAnySymbol()
    {
        var total = 0;

        foreach (var num in _engineNumbers)
        {
            var rowRange = new[] { num.row - 1, num.row, num.row + 1 };
            var symbolsOnAdjacentRows = _engineSymbols.FindAll(x => rowRange.Contains(x.row));

            var doesSymbolTouchNumber =
                symbolsOnAdjacentRows.Exists(x => x.position >= num.startIndex - 1 && x.position <= num.endIndex + 1);

            if (doesSymbolTouchNumber)
            {
                total += num.value;
            }
        }

        return total;
    }

    internal int GetValueOfAllGearRatios()
    {
        var total = 0;
        foreach (var symbol in _engineSymbols.FindAll(x => x.symbol == '*'))
        {
            var rowRange = new[] { symbol.row - 1, symbol.row, symbol.row + 1 };
            var numbersOnAdjacentRows = _engineNumbers.FindAll(x => rowRange.Contains(x.row));
            var numbersThatTouch = numbersOnAdjacentRows.FindAll(x =>
                symbol.position >= x.startIndex - 1 && symbol.position <= x.endIndex + 1);

            if (numbersThatTouch.Count == 2)
            {
                total += numbersThatTouch[0].value * numbersThatTouch[1].value;
            }
        }

        return total;
    }
}