using AdventOfCodeSupport;
using CommunityToolkit.HighPerformance;

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

    private char[] inputP1; 
    private char[] inputP2; 
    
    protected override void InternalOnLoad()
    {
        this.inputP1 = Input.Text.ToCharArray();
        this.inputP2 = Input.Text.ToCharArray();
    }
    
    protected override object InternalPart1()
    {
        ulong sum = 0;
        int stride = Array.IndexOf(this.inputP1, '\n');
        var mutableMap = new Span2D<char>(this.inputP1, 0, this.inputP1.Length / (stride + 1), stride, 1);
        for (var y = 0; y < mutableMap.Height; y++)
        {
            for (var x = 0; x < mutableMap.Width; x++)
            {
                if (char.IsDigit(mutableMap[y,x]) || mutableMap[y,x] == '.')
                {
                    continue;
                }

                foreach (Vector2 direction in adjacent)
                {
                    if (y + direction.Y < 0 || y + direction.Y >= mutableMap.Height)
                        continue;
                    if (x + direction.X < 0 || x + direction.X >= mutableMap.Width)
                        continue;

                    if (!char.IsDigit(mutableMap[y + direction.Y, x + direction.X]))
                    {
                        continue;
                    }

                    this.SweepAndReplace(mutableMap, x + direction.X, y + direction.Y, out ulong number);
                    sum += number;
                }
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        ulong sum = 0;

        int stride = Array.IndexOf(this.inputP2, '\n');
        var mutableMap = new Span2D<char>(this.inputP2, 0, this.inputP2.Length / (stride + 1), stride, 1);
        for (var y = 0; y < mutableMap.Height; y++)
        {
            for (var x = 0; x < mutableMap.Width; x++)
            {
                if (mutableMap[y,x] != '*')
                {
                    continue;
                }

                var adjacentCount = 0;
                ulong sumOfAdjacent = 1;
                foreach (Vector2 direction in adjacent)
                {
                    if (y + direction.Y < 0 || y + direction.Y >= mutableMap.Height)
                        continue;
                    if (x + direction.X < 0 || x + direction.X >= mutableMap.Width)
                        continue;

                    if (!char.IsDigit(mutableMap[y + direction.Y, x + direction.X]))
                    {
                        continue;
                    }

                    this.SweepAndReplace(mutableMap, x + direction.X, y + direction.Y, out ulong number);
                    adjacentCount++;
                    sumOfAdjacent *= number;
                }

                if (adjacentCount == 2)
                {
                    sum += sumOfAdjacent;
                }
            }
        }

        return sum;
    }

    private void SweepAndReplace(Span2D<char> mutableMap, int x, int y, out ulong output)
    {
        int xStart = x;
        while (xStart > 0 && char.IsDigit(mutableMap[y,xStart - 1]))
        {
            xStart--;
        }
        
        int xEnd = x;
        while (xEnd < mutableMap.Width - 1 && char.IsDigit(mutableMap[y,xEnd + 1]))
        {
            xEnd++;
        }
        
        Span<char> number = mutableMap.GetRowSpan(y).Slice(xStart, xEnd - xStart + 1);
        output = ulong.Parse(number);
        for (int i = xStart; i <= xEnd; i++)
        {
            mutableMap[y,i] = '.';
        }
    }
    
    private record struct Vector2(int X, int Y);
}