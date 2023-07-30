using BenchmarkDotNet.Running;
using MemorySpan;

Console.WriteLine("Performance Benchmark Span versus array manipulation");
var result = BenchmarkRunner.Run(typeof(ArrayBenchmarks));
Console.ReadLine();