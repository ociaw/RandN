using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace RandN.Benchmarks
{
    public class Extensions
    {
        public const Int32 Iterations = 16384;

        private readonly StepRng _rng;
        private readonly List<Int32> _list;

        public Extensions()
        {
            _rng = new StepRng(0);
            _list = new List<Int32>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20
            };
        }

        [Benchmark]
        public void ShuffleList()
        {
            var list = new List<Int32>(_list);
            _rng.ShuffleInPlace(list);
        }
    }
}
