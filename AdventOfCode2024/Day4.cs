using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024;

[MemoryDiagnoser]
[ShortRunJob]
public class Day4
{
    private readonly string _input = File.ReadAllText("day4.txt");

    [Benchmark]
    public int Part1()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        int rows = inputSpan.Count('\n') + 1;
        int columns = inputSpan.IndexOf('\n') + 1;
        int columnsEndOffset = columns - 4;
        int rowsEndOffset = rows - 3;

        int rowOffset1 = columns;
        int rowOffset2 = columns * 2;
        int rowOffset3 = columns * 3;
        int xIndex = -1;

        int sum = 0;
        while (true)
        {
            int newIndex = inputSpan[(xIndex+1)..].IndexOf('X');
            xIndex += newIndex + 1;
            if (newIndex == -1)
            {
                return sum;
            }
            int column = xIndex % columns;

            if (column < columnsEndOffset && inputSpan[xIndex..(xIndex + 4)] is "XMAS")
            {
                sum++;
            }

            if (column >= 3 && inputSpan[(xIndex-3)..(xIndex+1)] is "SAMX")
            {
                sum++;
            }

            int row = xIndex / columns;

            if (row < rowsEndOffset &&
                inputSpan[xIndex + rowOffset1] == 'M' &&
                inputSpan[xIndex + rowOffset2] == 'A' &&
                inputSpan[xIndex + rowOffset3] == 'S'
            )
            {
                sum++;
            }

            if (row >= 3 &&
                inputSpan[xIndex - rowOffset1] == 'M' &&
                inputSpan[xIndex - rowOffset2] == 'A' &&
                inputSpan[xIndex - rowOffset3] == 'S'
            )
            {
                sum++;
            }

            if (row < rowsEndOffset && column < columnsEndOffset &&
                inputSpan[xIndex + rowOffset1 + 1] == 'M' &&
                inputSpan[xIndex + rowOffset2 + 2] == 'A' &&
                inputSpan[xIndex + rowOffset3 + 3] == 'S'
            )
            {
                sum++;
            }

            if (row >= 3 && column < columnsEndOffset &&
                inputSpan[xIndex - rowOffset1 + 1] == 'M' &&
                inputSpan[xIndex - rowOffset2 + 2] == 'A' &&
                inputSpan[xIndex - rowOffset3 + 3] == 'S'
            )
            {
                sum++;
            }

            if (row < rowsEndOffset && column >= 3 &&
                inputSpan[xIndex + rowOffset1 - 1] == 'M' &&
                inputSpan[xIndex + rowOffset2 - 2] == 'A' &&
                inputSpan[xIndex + rowOffset3 - 3] == 'S'
            )
            {
                sum++;
            }

            if (row >= 3 && column >= 3 &&
                inputSpan[xIndex - rowOffset1 - 1] == 'M' &&
                inputSpan[xIndex - rowOffset2 - 2] == 'A' &&
                inputSpan[xIndex - rowOffset3 - 3] == 'S'
            )
            {
                sum++;
            }
        }
    }

    [Benchmark]
    public int Part2()
    {
        ReadOnlySpan<char> inputSpan = _input.AsSpan();
        int rows = inputSpan.Count('\n') + 1;
        int columns = inputSpan.IndexOf('\n') + 1;
        int columnsEndOffset = columns - 3;
        int rowsEndOffset = rows - 2;

        int rowOffset1 = columns;
        int rowOffset2 = columns * 2;
        int mIndex = -1;

        int sum = 0;
        while (true)
        {
            int newIndex = inputSpan[(mIndex+1)..].IndexOf('M');
            mIndex += newIndex + 1;
            if (newIndex == -1)
            {
                return sum;
            }
            int x = mIndex % columns;
            int y = mIndex / columns;

            if (y < rowsEndOffset && x < columnsEndOffset &&
                inputSpan[mIndex + 2] == 'M' &&
                inputSpan[mIndex + 1 + rowOffset1] == 'A' &&
                inputSpan[mIndex + rowOffset2] == 'S' && inputSpan[mIndex + rowOffset2 + 2] == 'S'
               )
            {
                sum++;
            }

            if (y < rowsEndOffset && x < columnsEndOffset &&
                inputSpan[mIndex + 2] == 'S' &&
                inputSpan[mIndex + 1 + rowOffset1] == 'A' &&
                inputSpan[mIndex + rowOffset2] == 'M' && inputSpan[mIndex + rowOffset2 + 2] == 'S'
            )
            {
                sum++;
            }


            if (y < rowsEndOffset && x >= 2 &&
                inputSpan[mIndex - 2] == 'S' &&
                inputSpan[mIndex - 1 + rowOffset1] == 'A' &&
                inputSpan[mIndex + rowOffset2] == 'M' && inputSpan[mIndex + rowOffset2 - 2] == 'S'
            )
            {
                sum++;
            }

            if (y >= 2 && x < columnsEndOffset &&
                inputSpan[mIndex + 2] == 'M' &&
                inputSpan[mIndex + 1 - rowOffset1] == 'A' &&
                inputSpan[mIndex - rowOffset2] == 'S' && inputSpan[mIndex - rowOffset2 + 2] == 'S'
               )
            {
                sum++;
            }
        }
    }
}