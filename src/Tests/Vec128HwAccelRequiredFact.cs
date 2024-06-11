using System;
using Xunit;

namespace RandN;

public sealed class Vec128HwAccelRequiredFact : FactAttribute
{
    public Vec128HwAccelRequiredFact()
    {
#if NET8_0_OR_GREATER
            if (!System.Runtime.Intrinsics.Vector128.IsHardwareAccelerated)
                Skip = "Vector128 is not hardware accelerated";
#elif NET6_0
            if (!System.Runtime.Intrinsics.X86.Sse2.IsSupported)
                Skip = "Vector128 is not hardware accelerated";
#endif
    }

    public override String Skip { get; set; }
}
