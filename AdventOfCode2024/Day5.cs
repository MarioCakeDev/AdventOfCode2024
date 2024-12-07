using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser]
[ShortRunJob]
public class Day5
{
    private readonly string _input = File.ReadAllText("day5.txt");

    [Benchmark]
    public int Part1()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        Span<Int128> index = stackalloc Int128[100];

        MemoryExtensions.SpanSplitEnumerator<char> indexReportSplit = inputSpan.Split("\n\n");

        indexReportSplit.MoveNext();
        ReadOnlySpan<char> indexSpan = inputSpan[indexReportSplit.Current];
        MemoryExtensions.SpanSplitEnumerator<char> indexLines = indexSpan.Split('\n');

        foreach (Range indexLineRange in indexLines)
        {
            ReadOnlySpan<char> indexLineSpan = indexSpan[indexLineRange];
            int disallowedPage = int.Parse(indexLineSpan[..2]);
            int pageIndex = int.Parse(indexLineSpan[3..]);
            index[pageIndex] |= Int128.One << disallowedPage;
        }
        indexReportSplit.MoveNext();

        ReadOnlySpan<char> reportsSpan = inputSpan[indexReportSplit.Current];
        MemoryExtensions.SpanSplitEnumerator<char> reports = reportsSpan.Split('\n');

        int sum = 0;
        foreach (Range reportRange in reports)
        {
            Int128 disallowedPages = Int128.Zero;
            ReadOnlySpan<char> report = reportsSpan[reportRange];
            MemoryExtensions.SpanSplitEnumerator<char> numbers = report.Split(',');
            int pageCount = 0;
            int middlePage = 0;
            bool skipReport = false;
            int middleIndex = report.Length / 6;
            foreach (Range numberRange in numbers)
            {
                int pageNumber = int.Parse(report[numberRange]);
                Int128 pageToCheck = Int128.One << pageNumber;
                if ((disallowedPages & pageToCheck) == Int128.Zero)
                {
                    disallowedPages |= index[pageNumber];
                }
                else
                {
                    skipReport = true;
                    break;
                }

                if (pageCount == middleIndex)
                {
                    middlePage = pageNumber;
                }
                pageCount++;
            }

            if (!skipReport)
            {
                sum += middlePage;
            }
        }

        return sum;
    }

    [Benchmark]
    public int Part2()
    {
        return 0;
    }
}