using System;
using System.Runtime.Intrinsics.X86;

namespace RandN.Rngs
{
    public static class Statistics
    {
        public static Double ConfidenceInterval => .99;

        public static Double ZScore => 2.576;

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
    }
}
