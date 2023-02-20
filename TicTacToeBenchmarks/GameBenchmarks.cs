using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using TicTacToeBenchmarks.Runner;

namespace TicTacToeBenchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GameBenchmarks
{
    private readonly char[] _player1WinSequence = new char[] { '1', '2', '5', '3', '9', '6' };
    private readonly char[] _drawSequence = new char[] { '1', '2', '3', '4', '6', '5', '7', '9', '8' };


    [BenchmarkCategory("Win"), Benchmark(Baseline = true)]
    public void SingleBoard_Player1Win()
    {
        GameBoardRunner.PlaySingleBoard(_player1WinSequence);
    }

    [BenchmarkCategory("Draw"), Benchmark(Baseline = true)]
    public void SingleBoard_Draw()
    {
        GameBoardRunner.PlaySingleBoard(_drawSequence);
    }

    [BenchmarkCategory("Win"), Benchmark]
    public void MultiBoard_Player1Win()
    {
        GameBoardRunner.PlayMultiBoard(_player1WinSequence);
    }
    [BenchmarkCategory("Draw"), Benchmark]
    public void MultiBoard_Draw()
    {
        GameBoardRunner.PlayMultiBoard(_drawSequence);
    }
    [BenchmarkCategory("Win"), Benchmark]
    public void JaggedBoard_Player1Win()
    {
        GameBoardRunner.PlayJaggedBoard(_player1WinSequence);
    }
    [BenchmarkCategory("Draw"), Benchmark]
    public void JaggedBoard_Draw()
    {
        GameBoardRunner.PlayJaggedBoard(_drawSequence);
    }
}