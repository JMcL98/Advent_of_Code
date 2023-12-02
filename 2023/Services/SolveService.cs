namespace _2023.Services;

using Interfaces;
using Models;
using Solutions;

public class SolveService : ISolveService
{
    private IFileReaderService _fileReaderService;

    public SolveService(IFileReaderService fileReaderService)
    {
        _fileReaderService = fileReaderService;
    }

    public string Solve(Puzzle puzzle, List<string> args)
    {
        switch (puzzle.Day)
        {
            case 0:
                if (puzzle.Part == 1)
                {
                    return Day0.Part1(int.Parse(args[0]), int.Parse(args[1]));
                }

                if (puzzle.Part == 2)
                {
                    return Day0.Part2(int.Parse(args[0]), int.Parse(args[1]),
                        int.Parse(args[2]));
                }

                // Part doesn't exist.
                break;

            case 1:
                if (puzzle.Part == 1)
                {
                    return Day1.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day1.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 2:
                if (puzzle.Part == 1)
                {
                    return Day2.Part1(_fileReaderService.ReadFile(puzzle), int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                }

                if (puzzle.Part == 2)
                {
                    //return Day2.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;
        }

        return "";
    }
}