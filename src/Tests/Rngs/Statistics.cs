using System;
using System.Runtime.Intrinsics.X86;

namespace RandN.Rngs
{
    public static class Statistics
    {
        public static Double ConfidenceInterval => .99;

        public static Double ZScore => 2.576;

        /// <summary>
        /// Length of the block used in <see cref="TestBlockFrequency{TRng}(TRng, UInt32)"/>
        /// </summary>
        public static Int32 FrequencyBlockLength => 8;

        public static Boolean WithinConfidenceBernoulli(UInt64 actual, UInt64 expected, UInt64 sampleCount)
        {
            return WithinConfidenceBernoulli(actual, (Double)expected / sampleCount, sampleCount);
        }

        public static Boolean WithinConfidenceBernoulli(UInt64 actual, Double p, UInt64 sampleCount)
        {
            var popStdDev = Math.Sqrt(p * (1.0 - p));
            var sampleMean = (Double)actual / sampleCount;

            return WithinConfidence(p, popStdDev, sampleMean, sampleCount);
        }

        public static Boolean WithinConfidence(Double popMean, Double popStdDev, Double sampleMean, UInt64 sampleCount)
        {
            var margin = popStdDev / Math.Sqrt(sampleCount) * ZScore;
            var difference = Math.Abs(popMean - sampleMean);
            return difference < margin;
        }

        /// <summary>
        /// A simple test checking that the number of 1s and 0s is about equal.
        /// </summary>
        public static Boolean TestMonobitFrequency<TRng>(TRng rng, UInt32 wordCount)
            where TRng : IRng
        {
            UInt64 oneCount = 0;
            for (UInt32 i = 0; i < wordCount; i++)
                oneCount += Popcnt.PopCount(rng.NextUInt32());

            UInt64 bitCount = wordCount * 32;
            return WithinConfidenceBernoulli(oneCount, bitCount / 2, bitCount);
        }
    }
}
