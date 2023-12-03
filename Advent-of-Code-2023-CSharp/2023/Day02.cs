using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace Advent_of_Code_2023_CSharp._2023;

public class Day02 : AdventBase
{
    protected override object InternalPart1()
    {
        ulong sum = 0;
        foreach (string line in Input.Lines)
        {
            string[] parts = line.Split(":");
            string gameName = parts[0];
            ulong gameId = ulong.Parse(gameName.Split(" ")[1]);
            string[] gameParts = parts[1].Split(";");

            var possible = true;
            foreach (string part in gameParts)
            {
                possible &= part.Split(", ").All(colour =>
                {
                    string[] colourParts = colour.Trim().Split(" ");
                    return colourParts[1] switch
                    {
                        "red" => int.Parse(colourParts[0]) <= 12,
                        "green" => int.Parse(colourParts[0]) <= 13,
                        "blue" => int.Parse(colourParts[0]) <= 14,
                        _ => false
                    };
                });
                
                if (!possible)
                    break;
            }
            
            if (possible)
                sum += gameId;

        }

        return sum;
    }

    protected override object InternalPart2()
    {
        ulong sum = 0;
        foreach (string line in Input.Lines)
        {
            string[] parts = line.Split(":");
            string[] gameParts = parts[1].Split(";");
            
            ulong redCount = 0;
            ulong greenCount = 0;
            ulong blueCount = 0;

            foreach (string part in gameParts)
            {
                foreach(var colour in part.Split(", "))
                {
                    string[] colourParts = colour.Trim().Split(" ");
                    ulong count = ulong.Parse(colourParts[0]);
                    switch(colourParts[1]) 
                    {
                        case "red": if (count > redCount) redCount = count; break;
                        case "green": if (count > greenCount) greenCount = count; break;
                        case "blue": if (count > blueCount) blueCount = count; break;
                    };
                }
            }

            sum += redCount * blueCount * greenCount;
        }

        return sum;
    }
}