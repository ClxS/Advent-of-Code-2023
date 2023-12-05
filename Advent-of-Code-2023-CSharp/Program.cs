// See https://aka.ms/new-console-template for more information

using AdventOfCodeSupport;
using BenchmarkDotNet.Configs;

AdventSolutions solutions = new();
AdventBase day = solutions.GetDay(2023, 5);

await day.DownloadInputAsync();

#if DEBUG
day.Part1();
day.Part2();
#else
if (await day.CheckPart1Async() == true && await day.CheckPart2Async() == true)
    day.Benchmark();
#endif
