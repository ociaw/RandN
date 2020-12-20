using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using Xunit;

namespace RandN.Rngs
{
    public sealed class ThreadLocalRngTests
    {
        [Fact]
        public void Construction()
        {
            var rng = ThreadLocalRng.Instance;
            Assert.NotNull(rng);
            Assert.True(Statistics.TestMonobitFrequency32(rng, 100_000, Statistics.WideZScore));
            Assert.True(Statistics.TestMonobitFrequency64(rng, 100_000, Statistics.WideZScore));
            Assert.True(Statistics.TestMonobitFrequencyFill(rng, 100_000, Statistics.WideZScore));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(16)]
        public void UniqueInstancePerThread(Int32 threadCount)
        {
            // In this test, we get the internal RNG instance stored in each thread, shove it into
            // a shared dictionary, and then assert that the dictionary contains as many items as
            // threads were spawned. If it doesn't, that means that at least two of the RNGs equal
            // each other by reference (since they don't override Equals or GetHashCode).
            var dictionary = new ConcurrentDictionary<ChaCha, Int32>();
            var exceptions = new ConcurrentBag<Exception>();
            void GetInternalRng()
            {
                try
                {
                    var wrapper = ThreadLocalRng.Instance;
                    wrapper.NextUInt32(); // Make sure the RNG is instantiated
                    var threadLocalField = typeof(ThreadLocalRng).GetField("ThreadLocal", BindingFlags.NonPublic | BindingFlags.Static);
                    var threadLocal = (ThreadLocal<ChaCha>)threadLocalField!.GetValue(wrapper);
                    var internalRng = threadLocal.Value;
                    dictionary.TryAdd(internalRng, 0);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            var threads = new Thread[threadCount];
            for (Int32 i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(GetInternalRng);
                threads[i].Start();
            }

            foreach (var thread in threads)
                thread.Join();

            Assert.Empty(exceptions);
            Assert.Equal(threadCount, dictionary.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        public void ClusterThread(Int32 threadCount)
        {
            // In this test, we spawn a bunch of threads and try to get them to use their
            // ThreadLocalRng concurrently, after which they add something to the completed bag. If
            // the bag doesn't have the same number of items in the bag as threads spawned, we know
            // that something went wrong.

            // We first run a quick benchmark to get a rough idea of how fast the RNG runs.
            const Int32 benchmarkIterations = 4000;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (Int32 i = 0; i < benchmarkIterations; i++)
                ThreadLocalRng.Instance.NextUInt32();
            stopwatch.Stop();

            // We want each thread to do about .25 seconds of work.
            TimeSpan targetTime = TimeSpan.FromMilliseconds(250);
            Double multiplier = targetTime.TotalMilliseconds / stopwatch.Elapsed.TotalMilliseconds;
            Int32 iterations = (Int32)(benchmarkIterations * multiplier);

            var completed = new ConcurrentBag<Int32>();
            void DoWork()
            {
                var rng = ThreadLocalRng.Instance;
                for (Int32 i = 0; i < iterations; i++)
                    rng.NextUInt32();

                completed.Add(iterations);
            }

            var threads = new Thread[threadCount];
            for (Int32 i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(DoWork);
                threads[i].Start();
            }

            foreach (var thread in threads)
                thread.Join();

            Assert.Equal(threadCount, completed.Count);
        }
    }
}
