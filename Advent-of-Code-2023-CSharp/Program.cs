// See https://aka.ms/new-console-template for more information

using AdventOfCodeSupport;
using BenchmarkDotNet.Configs;

AdventSolutions solutions = new();
AdventBase day = solutions.GetDay(2023, 3);
await day.DownloadInputAsync();

day.Part1();
//await day.SubmitPart1Async();

day.Part2();
//await day.SubmitPart1Async();


#if !DEBUG
day.Benchmark();
#endif
