﻿using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser(false)]
[ShortRunJob]
public class Day3
{
    private readonly string _input = File.ReadAllText("day3.txt");

    [Benchmark]
    public int Part1()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        MemoryExtensions.SpanSplitEnumerator<char> multiplicationCandidates = inputSpan.Split("mul(");

        int sum = 0;
        foreach (Range candidateRange in multiplicationCandidates)
        {
            ReadOnlySpan<char> candidate = inputSpan[candidateRange];
            int endOfMultiplication = candidate.IndexOf(')');

            if (endOfMultiplication == -1)
            {
                continue;
            }

            candidate = candidate[..endOfMultiplication];
            int? firstNumber = null;

            int firstNumberRange = 0;
            foreach (char c in candidate)
            {
                if (char.IsDigit(c))
                {
                    firstNumberRange++;
                    continue;
                }

                if (c == ',' && firstNumberRange > 0)
                {
                    firstNumber = int.Parse(candidate[..firstNumberRange]);
                }

                break;
            }

            if (!firstNumber.HasValue)
            {
                continue;
            }

            bool isNumber = true;
            foreach (char c in candidate[(firstNumberRange + 1)..])
            {
                if (!char.IsDigit(c))
                {
                    isNumber = false;
                    break;
                }
            }

            if (!isNumber)
            {
                continue;
            }

            sum += firstNumber.Value * int.Parse(candidate[(firstNumberRange + 1)..]);
        }

        return sum;
    }

    [Benchmark]
    public int Part2()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        ReadOnlySpan<char> doString = "do()";
        ReadOnlySpan<char> dontString = "don't()";

        ReadOnlySpan<char> startSpan = inputSpan;
        int sum = 0;
        while (true)
        {
            int doEndIndex = startSpan.IndexOf(dontString);

            ReadOnlySpan<char> enabledSpan = startSpan;
            if(doEndIndex != -1)
            {
                enabledSpan = startSpan[..doEndIndex];
            }

            MemoryExtensions.SpanSplitEnumerator<char> multiplicationCandidates = enabledSpan.Split("mul(");

            foreach (Range candidateRange in multiplicationCandidates)
            {
                ReadOnlySpan<char> candidate = enabledSpan[candidateRange];
                int endOfMultiplication = candidate.IndexOf(')');

                if (endOfMultiplication == -1)
                {
                    continue;
                }

                candidate = candidate[..endOfMultiplication];
                int? firstNumber = null;

                int firstNumberRange = 0;
                foreach (char c in candidate)
                {
                    if (char.IsDigit(c))
                    {
                        firstNumberRange++;
                        continue;
                    }

                    if (c == ',' && firstNumberRange > 0)
                    {
                        firstNumber = int.Parse(candidate[..firstNumberRange]);
                    }

                    break;
                }

                if (!firstNumber.HasValue)
                {
                    continue;
                }

                bool isNumber = true;
                foreach (char c in candidate[(firstNumberRange + 1)..])
                {
                    if (!char.IsDigit(c))
                    {
                        isNumber = false;
                        break;
                    }
                }

                if (!isNumber)
                {
                    continue;
                }

                sum += firstNumber.Value * int.Parse(candidate[(firstNumberRange + 1)..]);
            }

            if (doEndIndex == -1)
            {
                return sum;
            }

            int doIndex = startSpan[doEndIndex..].IndexOf(doString);

            if (doIndex == -1)
            {
                return sum;
            }

            startSpan = startSpan[(doEndIndex+doIndex)..];
        }
    }
}