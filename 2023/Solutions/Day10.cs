namespace _2023.Solutions;

public static class Day10
{
    public static string Part1(List<string> input)
    {
        var pipes = PopulatePipes(input);
        var previousPipe = pipes[0];
        var currentPipe = pipes.First(x => x.Type == PipeType.S);

        var count = 0;
        while (true)
        {
            var nextPipe = currentPipe.GetNextPipe(previousPipe);
            if (nextPipe.Type == PipeType.S)
            {
                break;
            }

            previousPipe = currentPipe;
            currentPipe = nextPipe;

            count++;
        }

        return ((int)Math.Ceiling((double)count / 2)).ToString();
    }

    public static string Part2(List<string> input)
    {

        return "";
    }

    private static List<Pipe> PopulatePipes(List<string> input)
    {
        var pipes = new List<Pipe>();
        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var c = input[y][x];
                if (c != '.')
                {
                    pipes.Add(new Pipe(x, y, c));
                }
            }
        }

        foreach (var pipe in pipes)
        {
            var (x, y) = pipe.Position;
            if (pipe.NorthPipe is null && pipe.Type is PipeType.S or PipeType.V or PipeType.NE or PipeType.NW)
            {
                var connectingPipe = pipes.FirstOrDefault(p => p.Position.x == x && p.Position.y == y - 1);
                if (connectingPipe is not null && connectingPipe.Type is not PipeType.H and not PipeType.NE and not PipeType.NW)
                {
                    pipe.NorthPipe = connectingPipe;
                    connectingPipe.SouthPipe = pipe;
                }
            }

            if (pipe.EastPipe is null && pipe.Type is PipeType.S or PipeType.H or PipeType.NE or PipeType.SE)
            {
                var connectingPipe = pipes.FirstOrDefault(p => p.Position.x == x + 1 && p.Position.y == y);
                if (connectingPipe is not null && connectingPipe.Type is not PipeType.V and not PipeType.NE and not PipeType.SE)
                {
                    pipe.EastPipe = connectingPipe;
                    connectingPipe.WestPipe = pipe;
                }
            }

            if (pipe.SouthPipe is null && pipe.Type is PipeType.S or PipeType.V or PipeType.SE or PipeType.SW)
            {
                var connectingPipe = pipes.FirstOrDefault(p => p.Position.x == x && p.Position.y == y + 1);
                if (connectingPipe is not null && connectingPipe.Type is not PipeType.H and not PipeType.SE and not PipeType.SW)
                {
                    pipe.SouthPipe = connectingPipe;
                    connectingPipe.NorthPipe = pipe;
                }
            }

            if (pipe.WestPipe is null && pipe.Type is PipeType.S or PipeType.H or PipeType.NW or PipeType.SW)
            {
                var connectingPipe = pipes.FirstOrDefault(p => p.Position.x == x - 1 && p.Position.y == y);
                if (connectingPipe is not null && connectingPipe.Type is not PipeType.V and not PipeType.SW and not PipeType.NW)
                {
                    pipe.WestPipe = connectingPipe;
                    connectingPipe.EastPipe = pipe;
                }
            }
        }

        return pipes;
    }

    private class Pipe
    {
        internal (int x, int y) Position;
        internal PipeType Type;
        internal Pipe? NorthPipe = null;
        internal Pipe? EastPipe = null;
        internal Pipe? SouthPipe = null;
        internal Pipe? WestPipe = null;

        internal Pipe(int x, int y, char type)
        {
            Position.x = x;
            Position.y = y;

            Type = type switch
            {
                '|' => PipeType.V,
                '-' => PipeType.H,
                'L' => PipeType.NE,
                'J' => PipeType.NW,
                'F' => PipeType.SE,
                '7' => PipeType.SW,
                'S' => PipeType.S,
                _ => Type
            };
        }

        internal Pipe GetNextPipe(Pipe previousPipe)
        {
            if (NorthPipe is not null && NorthPipe != previousPipe)
            {
                return NorthPipe;
            }

            if (EastPipe is not null && EastPipe != previousPipe)
            {
                return EastPipe;
            }

            if (SouthPipe is not null && SouthPipe != previousPipe)
            {
                return SouthPipe;
            }

            if (WestPipe is not null && WestPipe != previousPipe)
            {
                return WestPipe;
            }

            throw new Exception();
        }

        internal int SeekLoop(int count, Pipe previousPipe)
        {
            var thisCount = count + 1;

            if (NorthPipe is not null && NorthPipe != previousPipe)
            {
                thisCount = NorthPipe.SeekLoop(thisCount, this);
            }

            if (EastPipe is not null && EastPipe != previousPipe)
            {
                thisCount = EastPipe.SeekLoop(thisCount, this);
            }

            if (SouthPipe is not null && SouthPipe != previousPipe)
            {
                thisCount = SouthPipe.SeekLoop(thisCount, this);
            }

            if (WestPipe is not null && WestPipe != previousPipe)
            {
                thisCount = WestPipe.SeekLoop(thisCount, this);
            }

            return thisCount;
        }
    }

    private enum PipeType
    {
        V,
        H,
        NE,
        NW,
        SE,
        SW,
        S
    }
}