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

        return "";
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
        }
    }

    internal int GetValueOfAllNumbersThatTouchAnySymbol()
    {
        var total = 0;

        var numList = new List<int>();
        foreach (var num in _engineNumbers)
        {
            numList.Add( num.value);
        }

        foreach (var num in _engineNumbers)
        {
            var rowRange = new[] { num.row - 1, num.row, num.row + 1 };
            var symbolsOnAdjacentRows = _engineSymbols.FindAll(x => rowRange.Contains(x.row));

            var symbolsThatTouch =
                symbolsOnAdjacentRows.FindAll(x => x.position >= num.startIndex - 1 && x.position <= num.endIndex + 1);

            var doesSymbolTouchNumber =
                symbolsOnAdjacentRows.Exists(x => x.position >= num.startIndex - 1 && x.position <= num.endIndex + 1);

            if (doesSymbolTouchNumber)
            {
                total += num.value;
            }
        }

        return total;
    }
}