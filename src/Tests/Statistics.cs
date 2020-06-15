using System;
using System.Runtime.Intrinsics.X86;

namespace RandN
{
    public static class Statistics
    {
        /// <summary>
        /// A 99% confidence interval, used for tests with reproducible RNGs.
        /// </summary>
        public const Double ConfidenceInterval = .99;

        public const Double ZScore = 2.576;

        /// <summary>
        /// We use this for when we use non-reproducible random numbers, because we *really* do not want false negatives.
        /// About one in one billion tests will fail incorrectly.
        /// </summary>
        public const Double WideConfidenceInterval = 0.999999999;

        public const Double WideZScore = 6.109;

        /// <summary>
        /// Length of the block used in <see cref="TestBlockFrequency{TRng}(TRng, UInt32)"/>
        /// </summary>
        public static Int32 FrequencyBlockLength => 8;

        public static Boolean WithinConfidenceBernoulli(UInt64 actual, UInt64 expected, UInt64 sampleCount, Double zScore = ZScore)
        {
            return WithinConfidenceBernoulli(actual, (Double)expected / sampleCount, sampleCount, zScore);
        }

        public static Boolean WithinConfidenceBernoulli(UInt64 actual, Double p, UInt64 sampleCount, Double zScore = ZScore)
        {
            var popStdDev = Math.Sqrt(p * (1.0 - p));
            var sampleMean = (Double)actual / sampleCount;

            return WithinConfidence(p, popStdDev, sampleMean, sampleCount, zScore);
        }

        public static Boolean WithinConfidence(Double popMean, Double popStdDev, Double sampleMean, UInt64 sampleCount, Double zScore = ZScore)
        {
            var margin = popStdDev / Math.Sqrt(sampleCount) * zScore;
            var difference = Math.Abs(popMean - sampleMean);
            return difference < margin;
        }

        public static Boolean WithinConfidence(Decimal popMean, Decimal popStdDev, Decimal sampleMean, UInt64 sampleCount, Double zScore = ZScore)
        {
            var margin = popStdDev / (Decimal)Math.Sqrt(sampleCount) * (Decimal)zScore;
            var difference = Math.Abs(popMean - sampleMean);
            return difference < margin;
        }

        /// <summary>
        /// A simple test checking that the number of 1s and 0s is about equal.
        /// </summary>
        public static Boolean TestMonobitFrequency<TRng>(TRng rng, UInt32 wordCount, Double zScore = ZScore)
            where TRng : IRng
        {
            UInt64 oneCount = 0;
            for (UInt32 i = 0; i < wordCount; i++)
                oneCount += Popcnt.PopCount(rng.NextUInt32());

            UInt64 bitCount = wordCount * 32;
            return WithinConfidenceBernoulli(oneCount, bitCount / 2, bitCount, zScore);
        }
    }
}
