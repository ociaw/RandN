using System;
using Xunit;

namespace RandN
{
    public sealed class Sse2RequiredFact : FactAttribute
    {
        public Sse2RequiredFact()
        {
#if X86_INTRINSICS
            if (!System.Runtime.Intrinsics.X86.Sse2.IsSupported)
                Skip = "SSE2 is not supported";
#endif
        }

        public override String Skip { get; set; }
    }
}
