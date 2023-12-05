using AdventOfCodeSupport;

namespace Advent_of_Code_2023_CSharp._2023;

public class Day05 : AdventBase
{
    private long[] seeds = null!;
    private List<Category> categories = null!;
    
    protected override void InternalOnLoad()
    {
        this.categories = new List<Category>();
        this.seeds = Input.Blocks[0].Lines[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();
        foreach(InputBlock block in Input.Blocks.Skip(1))
        {
            string[] lines = block.Lines;
            
            List<Range> ranges = new();
            for (var i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Length == 0)
                {
                    continue;
                }
                
                long[] parts = line.Split(' ').Select(long.Parse).ToArray();
                ranges.Add(new Range(parts[1], parts[1] + parts[2] - 1, parts[0]));
            }
            
            this.categories.Add(new Category(lines[0], ranges.OrderBy(r => r.Min).ToArray()));
        }
    }

    protected override object InternalPart1()
    {
        var min = long.MaxValue;
        foreach (long seed in this.seeds)
        {
            long value = seed;
            foreach (Category category in this.categories)
            {
                value = category.GetNextStep(value);
            }
            
            if (value < min)
            {
                min = value;
            }
        }
        
        return min;
    }

    protected override object InternalPart2()
    {
        var min = long.MaxValue;
        for (var index = 0; index < this.seeds.Length; index += 2)
        {
            for (var seed = this.seeds[index]; seed < this.seeds[index] + this.seeds[index + 1]; seed++)
            {
                long value = seed;
                foreach (Category category in this.categories)
                {
                    value = category.GetNextStep(value);
                }

                if (value < min)
                {
                    min = value;
                }
            }
        }

        return min;
    }

    private record Category(string GroupName, Range[] Ranges)
    {
        public long GetNextStep(long value)
        {
            foreach (Range range in this.Ranges)
            {
                if (range.Min <= value && value <= range.MaxInclusive)
                {
                    return range.TargetBase + (value - range.Min);
                }
            }

            return value;
        }
    }

    private record struct Range(long Min, long MaxInclusive, long TargetBase);
}