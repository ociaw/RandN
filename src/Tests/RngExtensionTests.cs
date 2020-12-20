using System;
using System.Collections.Generic;
using Xunit;

namespace RandN
{
    public sealed class RngExtensionTests
    {
        [Fact]
        public void Shuffle()
        {
            var list = new List<Int32> { 1, 2, 3, 4, 5, 6, 7 };
            var rng = new StepRng(0) { Increment = 0 };
            rng.ShuffleInPlace(list);
            Assert.Equal(new[] { 2, 3, 4, 5, 6, 7, 1 }, list);

            Assert.Throws<ArgumentNullException>(() => rng.ShuffleInPlace<StepRng, Int32>(null!));
        }
    }
}
