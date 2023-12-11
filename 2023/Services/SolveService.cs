namespace _2023.Services;

using Interfaces;
using Models;
using Solutions;

public class SolveService : ISolveService
{
    private readonly IFileReaderService _fileReaderService;

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
                    return Day2.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 3:
                if (puzzle.Part == 1)
                {
                    return Day3.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day3.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 4:
                if (puzzle.Part == 1)
                {
                    return Day4.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day4.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 5:
                if (puzzle.Part == 1)
                {
                    return Day5.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day5.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 6:
                if (puzzle.Part == 1)
                {
                    return Day6.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day6.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 7:
                if (puzzle.Part == 1)
                {
                    return Day7.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day7.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 8:
                if (puzzle.Part == 1)
                {
                    return Day8.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day8.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 9:
                if (puzzle.Part == 1)
                {
                    return Day9.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day9.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 10:
                if (puzzle.Part == 1)
                {
                    return Day10.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day10.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;

            case 11:
                if (puzzle.Part == 1)
                {
                    return Day11.Part1(_fileReaderService.ReadFile(puzzle));
                }

                if (puzzle.Part == 2)
                {
                    return Day11.Part2(_fileReaderService.ReadFile(puzzle));
                }

                // Part doesn't exist
                break;
        }

        return "";
    }
}