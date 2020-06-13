using System;
using BenchmarkDotNet.Configs;

namespace RandN.Benchmarks
{
    internal sealed class RngConfigAttribute : Attribute, IConfigSource
    {
        public RngConfigAttribute(UInt64 bytesPerIteration)
        {
            BytesPerIteration = bytesPerIteration;
            Config = ManualConfig.Create(DefaultConfig.Instance).AddColumn(new ThroughputColumn(BytesPerIteration));
        }

        public UInt64 BytesPerIteration { get; }

        public IConfig Config { get; }
    }
}
