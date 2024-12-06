using AdventOfCode2024;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;

Console.WriteLine(new Day4().Part2());

Console.WriteLine("Press any key to run benchmarks");
Console.ReadLine();

BenchmarkRunner.Run<Day4>(DefaultConfig.Instance
    .WithSummaryStyle(DefaultConfig.Instance.SummaryStyle
        .WithTimeUnit(TimeUnit.Millisecond)
    )
);