using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser(false)]
[ShortRunJob]
public class Day1
{
    private readonly string _input = File.ReadAllText("day1.txt");

    [Benchmark]
    public int Part1()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        int lineCount = inputSpan.Count('\n') + 1;
        int lineLength = inputSpan.IndexOf('\n') + 1;
        // const int lineCount = 1000;
        // const int lineLength = 14;

        Span<int> firstColumn = stackalloc int[lineCount];
        Span<int> secondColumn = stackalloc int[lineCount];

        for(int i = 0; i < lineCount; i++)
        {
            int offset = lineLength*i;
            firstColumn[i] = int.Parse(inputSpan[offset..(5+offset)]);
            secondColumn[i] = int.Parse(inputSpan[(8+offset)..(13+offset)]);
        }

        firstColumn.Sort();
        secondColumn.Sort();

        int differenceSum = 0;

        for (int i = 0; i < lineCount; i++)
        {
            differenceSum += Math.Abs(firstColumn[i] - secondColumn[i]);
        }

        return differenceSum;
    }

    [Benchmark]
    public int Part2()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        int lineCount = inputSpan.Count('\n') + 1;
        int lineLength = inputSpan.IndexOf('\n') + 1;

        Dictionary<int, int> numbers = new(lineCount);
        Dictionary<int, int> counts = new(lineCount);

        for (int i = 0; i < lineCount; i++)
        {
            int offset = lineLength*i;
            CollectionsMarshal.GetValueRefOrAddDefault(numbers, int.Parse(inputSpan[offset..(5+offset)]), out _)++;
            CollectionsMarshal.GetValueRefOrAddDefault(counts, int.Parse(inputSpan[(8+offset)..(13+offset)]), out _)++;
        }

        int sum = 0;

        foreach ((int number, int count) in numbers)
        {
            sum += number * count * counts.GetValueOrDefault(number);
        }

        return sum;
    }
}