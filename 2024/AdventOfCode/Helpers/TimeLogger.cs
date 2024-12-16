using System.Diagnostics;

namespace AdventOfCode2024.Extensions;

public class TimeLogger : IDisposable
{
    private readonly Stopwatch _stopwatch;
    private readonly string _message;
    
    public TimeLogger(string message)
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        _message = message;
    }
    
    public void Dispose()
    {
        _stopwatch.Stop();
        Console.WriteLine($"{_message}: {_stopwatch.Elapsed}");
    }
}