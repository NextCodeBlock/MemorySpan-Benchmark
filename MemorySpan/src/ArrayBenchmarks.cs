using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace MemorySpan;

[RankColumn]
[Orderer(SummaryOrderPolicy.SlowestToFastest)]
[MemoryDiagnoser]
public class ArrayBenchmarks
{
    private int[] _myArray;

    [Params(10, 1000, 10000)] 
    public int Size { get; set; } = 10;

    [GlobalSetup]
    public void Setup()
    {
        _myArray = new int[Size];

        for (var i = 0; i < Size; i++)
            _myArray[i] = i;
    }

    [Benchmark(Baseline = true, Description = "Sum by using LINQ")]
    public int SumWithLinq()
    {
        var newArray = _myArray.Skip(Size / 2).Take(Size / 4);
        return newArray.Sum(x => x);
    }

    [Benchmark(Description = "Sum by using ArrayCopy")]
    public int SumWithArrayCopy()
    {
        var sum = 0;
        var newArray = new int[Size / 4];
        Array.Copy(_myArray, Size / 2, newArray, 0, Size / 4);

        foreach (var item in newArray)
            sum += item;

        return sum;
    }

    [Benchmark(Description = "Sum by using SPAN")]
    public int SumWithSpan()
    {
        var sum = 0;
        var newArray = _myArray.AsSpan().Slice(Size / 2, Size / 4);

        foreach (var item in newArray)
            sum += item;

        return sum;
    }
}