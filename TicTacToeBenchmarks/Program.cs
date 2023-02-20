using BenchmarkDotNet.Running;

namespace TicTacToeBenchmarks;

internal class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<GameBenchmarks>();
    }
}
