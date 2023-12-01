namespace _2023.Interfaces;

using Models;

public interface ISolveService
{
    public string Solve(Puzzle puzzle, List<string> args);
}