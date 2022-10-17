using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace RandN.Benchmarks
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class RngConfigAttribute : Attribute, IConfigSource
    {
        public RngConfigAttribute(UInt64 bytesPerIteration)
        {
            BytesPerIteration = bytesPerIteration;
            var standard = new Job("All Enabled");
            var noAvx2 = new Job("AVX2 Disabled").WithEnvironmentVariable("COMPlus_EnableAVX2", "0");
            var netFramework = Job.Default.WithRuntime(BenchmarkDotNet.Environments.ClrRuntime.Net48).WithId(".NET 4.8");

            Config = ManualConfig.Create(DefaultConfig.Instance)
                .AddColumn(new ThroughputColumn(BytesPerIteration))
                .AddJob(standard)
                .AddJob(noAvx2)
                .AddJob(netFramework);
        }

        public UInt64 BytesPerIteration { get; }

        public IConfig Config { get; }
    }
}
