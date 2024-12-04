using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser(false)]
[ShortRunJob]
public class Day2
{
    private readonly string _input = File.ReadAllText("day2.txt");

    [Benchmark]
    public int Part1()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        MemoryExtensions.SpanSplitEnumerator<char> reports = inputSpan.Split('\n');
        int safeReports = 0;

        foreach (Range reportRange in reports)
        {
            ReadOnlySpan<char> report = inputSpan[reportRange];
            MemoryExtensions.SpanSplitEnumerator<char> numbers = report.Split(' ');
            int? previousNumber = null;
            bool? biggerThanPrevious = null;
            bool safe = true;
            foreach (Range numberRange in numbers)
            {
                if(previousNumber is null)
                {
                    previousNumber = int.Parse(report[numberRange]);
                    continue;
                }

                int currentNumber = int.Parse(report[numberRange]);
                if(biggerThanPrevious is null)
                {
                    biggerThanPrevious = currentNumber > previousNumber;
                }

                if (biggerThanPrevious is true && currentNumber - previousNumber is < 1 or > 3)
                {
                    safe = false;
                    break;
                }

                if (biggerThanPrevious is false && previousNumber - currentNumber is < 1 or > 3)
                {
                    safe = false;
                    break;
                }

                previousNumber = currentNumber;
            }

            if (safe)
            {
                safeReports++;
            }
        }

        return safeReports;
    }

    [Benchmark]
    public int Part2()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        MemoryExtensions.SpanSplitEnumerator<char> reports = inputSpan.Split('\n');
        int safeReports = 0;

        Span<int> numbersSpan = stackalloc int[8];
        foreach (Range reportRange in reports)
        {
            ReadOnlySpan<char> report = inputSpan[reportRange];
            MemoryExtensions.SpanSplitEnumerator<char> numbers = report.Split(' ');
            int i = 0;
            foreach (Range numberRange in numbers)
            {
                numbersSpan[i++] = int.Parse(report[numberRange]);
            }

            int numberCount = i;

            if (IsSafe(numbersSpan, numberCount))
            {
                safeReports++;
                continue;
            }

            for (i = 0; i < numberCount; i++)
            {
                if (IsSafe(numbersSpan, numberCount, i))
                {
                    safeReports++;
                    break;
                }
            }
        }

        return safeReports;

        static bool IsSafe(in ReadOnlySpan<int> numbersSpan, int length, int? skip = null)
        {
            int? previousNumber = null;
            bool? biggerThanPrevious = null;
            bool safe = true;
            for (int i = 0; i < length; i++)
            {
                if (i == skip)
                {
                    continue;
                }

                int currentNumber = numbersSpan[i];
                if (previousNumber is null)
                {
                    previousNumber = currentNumber;
                    continue;
                }

                if (biggerThanPrevious is null)
                {
                    biggerThanPrevious = currentNumber > previousNumber;
                }

                if (biggerThanPrevious is true && currentNumber - previousNumber is < 1 or > 3)
                {
                    safe = false;
                    break;
                }

                if (biggerThanPrevious is false && previousNumber - currentNumber is < 1 or > 3)
                {
                    safe = false;
                    break;
                }

                previousNumber = currentNumber;
            }

            return safe;
        }
    }
}