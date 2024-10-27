using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace RandN.Benchmarks;

//[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net48)]
//[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net60)]
//[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net80)]
public class Extensions
{
    public const Int32 Iterations = 16384;

    private readonly StepRng _rng;
    private readonly Int32[] _array;

    public Extensions()
    {
        _rng = new StepRng(0);
        _array = Enumerable.Range(1, 100).ToArray();
    }

    [Benchmark]
    public void ShuffleList()
    {
        var list = new List<Int32>(_array);
        for (Int32 i = 0; i < Iterations; i++)
            _rng.ShuffleInPlace(list);
    }


    [Benchmark]
    public void ShuffleArray()
    {
        var array = new Int32[_array.Length];
        Array.Copy(_array, array, array.Length);
        for (Int32 i = 0; i < Iterations; i++)
            _rng.ShuffleInPlace(array);
    }
}
