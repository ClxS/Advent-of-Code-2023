using System.Reflection;
using AdventOfCodeSupport;
using BenchmarkDotNet.Attributes;

namespace Advent_of_Code_2023_CSharp._2023;

public class Day01 : AdventBase
{
    private string[] lines;
    private string[] reversedLines;

    protected override void InternalOnLoad()
    {
        this.lines = Input.Lines;
        reversedLines = Input.Lines.Select(line => new string(line.Reverse().ToArray())).ToArray();
    }

    private readonly static string[] checkedValuesP1 = {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
    };
    
    private readonly static string[] checkedValuesP2 = {
        "0", "zero", "1", "one", "2", "two", "3", "three", "4", "four", "5", "five", "6", "six", "7", "seven", "8", "eight", "9", "nine",
    };

    private readonly static string[] inverseCheckedValuesP2 = {
        "0", "orez", "1", "eno", "2", "owt", "3", "eerht", "4", "ruof", "5", "evif", "6", "xis", "7", "neves", "8",
        "thgie", "9", "enin",
    };

    
    protected override object InternalPart1()
    {
        return this.Do(checkedValuesP1, checkedValuesP1, 1);
    }

    protected override object InternalPart2()
    {
        return this.Do(checkedValuesP2, inverseCheckedValuesP2, 2);
    }
    
    private object Do(IReadOnlyList<string> values, IReadOnlyList<string> inverseValues, int factor)
    {
        var sum = 0;
        for (var index = 0; index < this.lines.Length; index++)
        {
            string line = this.lines[index];
            string reversedLine = this.reversedLines[index];
            int firstIndex = GetMinimumValueKey(values, line);
            int lastIndex = GetMinimumValueKey(inverseValues, reversedLine);

            sum += (firstIndex / factor) * 10 + (lastIndex / factor);
        }

        return sum;
    }

    private static int GetMinimumValueKey(IReadOnlyList<string> values, string line)
    {
        var min = int.MaxValue;
        int minKey = -1;
        for (var i = 0; i < values.Count; i++)
        {
            int index = line.IndexOf(values[i], StringComparison.Ordinal);
            if (index < min && index >= 0)
            {
                min = index;
                minKey = i;
            }
        }

        return minKey;
    }
}