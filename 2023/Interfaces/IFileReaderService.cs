namespace _2023.Interfaces;

using Models;

public interface IFileReaderService
{
    public List<string> ReadFile(Puzzle puzzle);
}