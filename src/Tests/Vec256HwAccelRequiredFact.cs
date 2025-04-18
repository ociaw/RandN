using System;
using Xunit;

namespace RandN;

public sealed class Vec256HwAccelRequiredFact : FactAttribute
{
    public Vec256HwAccelRequiredFact()
    {
#if NET8_0_OR_GREATER
            if (!System.Runtime.Intrinsics.Vector256.IsHardwareAccelerated)
                Skip = "Vector256 is not hardware accelerated";
#elif NET6_0
            if (!System.Runtime.Intrinsics.X86.Avx2.IsSupported)
                Skip = "Vector256 is not hardware accelerated";
#endif
    }

    public override String Skip { get; set; }
}
