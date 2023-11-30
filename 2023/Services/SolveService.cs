namespace _2023.Services;

using Interfaces;
using Solutions;

public class SolveService : ISolveService
{
    public string Solve(int day, int part, List<string> args)
    {
        switch (day)
        {
            case 0:
                if (part == 1)
                {
                    return Day0.Part1(int.Parse(args[0]), int.Parse(args[1]));
                }
                else
                {
                    return Day0.Part2(int.Parse(args[0]), int.Parse(args[1]),
                        int.Parse(args[2]));
                }

                break;
        }

        return "";
    }
}