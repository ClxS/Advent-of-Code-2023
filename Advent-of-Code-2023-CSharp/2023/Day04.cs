using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCodeSupport;
using CommunityToolkit.HighPerformance;

namespace Advent_of_Code_2023_CSharp._2023;

public class Day04 : AdventBase
{
    private string[] lines = null!;
    
    protected override void InternalOnLoad()
    {
        this.lines = Input.Text.Split('\n');
    }
    
    protected override object InternalPart1()
    {
        long sum = 0;
        
        Span<byte> count = stackalloc byte[100];
        foreach (string line in lines)
        {
            if (line.Length == 0)
            {
                continue;
            }
            
            MatchCollection matches = Regex.Matches(line, @"(\d+)");
            for (var i = 11; i < matches.Count; ++i)
            {
                string value = matches[i].Value;
                int index = int.Parse(value);
                count[index]++;
            }

            var winCount = 0;
            for (var i = 1; i <= 10; ++i)
            {
                string value = matches[i].Value;
                int index = int.Parse(value);
                if (count[index] > 0)
                {
                    winCount++;
                }
            }
                
            // Zero out the count buffer
            Unsafe.InitBlock(ref count[0], 0, 100);

            if (winCount > 0)
            {
                sum += 1 << (winCount - 1);
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        long total = 0;
        
        Span<byte> rowMatchCount = stackalloc byte[100];
        Span<int> multiplier = stackalloc int[255];
        for(int i = 0; i < 255; i++)
        {
            multiplier[i] = 1;
        }
        
        for (var gameIdx = 0; gameIdx < this.lines.Length; gameIdx++)
        {
            string line = this.lines[gameIdx];
            if (line.Length == 0)
            {
                continue;
            }

            total += multiplier[gameIdx];
            MatchCollection matches = Regex.Matches(line, @"(\d+)");
            for (var i = 11; i < matches.Count; ++i)
            {
                string value = matches[i].Value;
                int index = int.Parse(value);
                rowMatchCount[index]++;
            }

            var winCount = 0;
            for (var i = 1; i <= 10; ++i)
            {
                string value = matches[i].Value;
                int index = int.Parse(value);
                if (rowMatchCount[index] > 0)
                {
                    winCount++;
                }
            }

            // Zero out the count buffer
            Unsafe.InitBlock(ref rowMatchCount[0], 0, 100);

            for(int i = gameIdx + 1; i < gameIdx + 1 + winCount; ++i)
            {
                multiplier[i] += multiplier[gameIdx];
            }
        }

        return total;
    }
}