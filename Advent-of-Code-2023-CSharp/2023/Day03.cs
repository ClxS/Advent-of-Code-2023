using AdventOfCodeSupport;

namespace Advent_of_Code_2023_CSharp._2023;

public class Day03 : AdventBase
{
    private Vector2[] adjacent = new Vector2[]
    {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(-1, -1),
        new Vector2(1, 1),
        new Vector2(-1, 1),
        new Vector2(1, -1),
    };
    
    protected override object InternalPart1()
    {
        ulong sum = 0;
        char[][] mutableMap = Input.Lines.Select(line => line.ToCharArray()).ToArray();
        for (var y = 0; y < Input.Lines.Length; y++)
        {
            for (var x = 0; x < Input.Lines[y].Length; x++)
            {
                if (char.IsDigit(mutableMap[y][x]) || mutableMap[y][x] == '.' || mutableMap[y][x] == ' ')
                {
                    continue;
                }

                foreach (Vector2 direction in adjacent)
                {
                    if (y + direction.Y < 0 || y + direction.Y >= Input.Lines.Length)
                        continue;
                    if (x + direction.X < 0 || x + direction.X >= Input.Lines[y].Length)
                        continue;

                    if (char.IsDigit(mutableMap[y + direction.Y][x + direction.X]))
                    {
                        SweepAndReplace(mutableMap, x + direction.X, y + direction.Y, out ulong number);
                        sum += number;
                    }
                }
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        ulong sum = 0;
        char[][] mutableMap = Input.Lines.Select(line => line.ToCharArray()).ToArray();
        for (var y = 0; y < Input.Lines.Length; y++)
        {
            for (var x = 0; x < Input.Lines[y].Length; x++)
            {
                if (char.IsDigit(mutableMap[y][x]) || mutableMap[y][x] == '.' || mutableMap[y][x] == ' ')
                {
                    continue;
                }

                List<ulong> adjacentNumbers = new();
                foreach (Vector2 direction in adjacent)
                {
                    if (y + direction.Y < 0 || y + direction.Y >= Input.Lines.Length)
                        continue;
                    if (x + direction.X < 0 || x + direction.X >= Input.Lines[y].Length)
                        continue;

                    if (char.IsDigit(mutableMap[y + direction.Y][x + direction.X]))
                    {
                        SweepAndReplace(mutableMap, x + direction.X, y + direction.Y, out ulong number);
                        adjacentNumbers.Add(number);
                    }
                }

                if (adjacentNumbers.Count == 2)
                {
                    sum += adjacentNumbers[0] * adjacentNumbers[1];
                }
            }
        }

        return sum;
    }

    private void SweepAndReplace(char[][] mutableMap, int x, int y, out ulong output)
    {
        var xStart = x;
        while (xStart > 0 && char.IsDigit(mutableMap[y][xStart - 1]))
        {
            xStart--;
        }
        
        var xEnd = x;
        while (xEnd < mutableMap[y].Length - 1 && char.IsDigit(mutableMap[y][xEnd + 1]))
        {
            xEnd++;
        }
        
        char[] number = mutableMap[y][xStart..(xEnd + 1)];
        output = ulong.Parse(number);
        for (int i = xStart; i <= xEnd; i++)
        {
            mutableMap[y][i] = ' ';
        }
    }
    
    private record struct Vector2(int X, int Y);
}