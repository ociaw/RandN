using System;

namespace RandN;

public static class Statistics
{
    /// <summary>
    /// A 99% confidence interval, used for tests with reproducible RNGs.
    /// </summary>
    public const Double ZScore = 2.576;

    /// <summary>
    /// We use this for when we use non-reproducible random numbers, because we *really* do not want false negatives.
    /// About one in one billion tests will fail incorrectly.
    /// Represents a 99.9999999% confidence interval.
    /// </summary>
    public const Double WideZScore = 6.109;

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
    public static Boolean TestMonobitFrequency32<TRng>(TRng rng, UInt32 wordCount, Double zScore = ZScore)
        where TRng : IRng
    {
        UInt64 oneCount = 0;
        for (UInt32 i = 0; i < wordCount; i++)
            oneCount += PopCount(rng.NextUInt32());

        UInt64 bitCount = wordCount * 32;
        return WithinConfidenceBernoulli(oneCount, bitCount / 2, bitCount, zScore);
    }

    /// <summary>
    /// A simple test checking that the number of 1s and 0s is about equal.
    /// </summary>
    public static Boolean TestMonobitFrequency64<TRng>(TRng rng, UInt32 wordCount, Double zScore = ZScore)
        where TRng : IRng
    {
        UInt64 oneCount = 0;
        for (UInt32 i = 0; i < wordCount; i++)
            oneCount += PopCount(rng.NextUInt64());

        UInt64 bitCount = wordCount * 64;
        return WithinConfidenceBernoulli(oneCount, bitCount / 2, bitCount, zScore);
    }

    /// <summary>
    /// A simple test checking that the number of 1s and 0s is about equal.
    /// </summary>
    public static Boolean TestMonobitFrequencyFill<TRng>(TRng rng, UInt32 byteCount, Double zScore = ZScore)
        where TRng : IRng
    {
        var buffer = new Byte[byteCount];
        rng.Fill(buffer);
        UInt64 oneCount = 0;
        for (UInt32 i = 0; i < byteCount; i++)
            oneCount += PopCount(buffer[i]);

        UInt64 bitCount = byteCount * 8;
        return WithinConfidenceBernoulli(oneCount, bitCount / 2, bitCount, zScore);
    }

    private static UInt32 PopCount(UInt32 num)
    {
#if X86_INTRINSICS
            return (UInt32)System.Numerics.BitOperations.PopCount(num);
#else
        UInt32 count = 0;
        for (Int32 i = 0; i < 32; i++)
        {
            count += num & 1;
            num >>= 1;
        }
        return count;
#endif
    }

    private static UInt32 PopCount(UInt64 num)
    {
#if X86_INTRINSICS
            return (UInt32)System.Numerics.BitOperations.PopCount(num);
#else
        UInt64 count = 0;
        for (Int32 i = 0; i < 64; i++)
        {
            count += num & 1;
            num >>= 1;
        }
        return (UInt32)count;
#endif
    }

    private static UInt32 PopCount(Byte num)
    {
#if X86_INTRINSICS
            return (UInt32)System.Numerics.BitOperations.PopCount(num);
#else
        UInt64 count = 0;
        for (Int32 i = 0; i < 8; i++)
        {
            count += (UInt32)num & 1;
            num >>= 1;
        }
        return (UInt32)count;
#endif
    }
}