namespace _2023.Services;

using Models;
using Interfaces;

class FileReaderService : IFileReaderService
{
    private readonly string _directoryPath;

    public FileReaderService(string path)
    {
        _directoryPath = path;
    }

    public List<string> ReadFile(Puzzle puzzle)
    {
        var file = Path.Combine(_directoryPath, $"{puzzle.Day}_{puzzle.Part}.txt");

        if (File.Exists(file))
        {
            return File.ReadAllLines(file).ToList();
        }

        throw new FileNotFoundException("File not found", fileName: file);
    }
}