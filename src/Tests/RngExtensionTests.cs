using System;
using System.Collections.Generic;
using Xunit;

namespace RandN;

public sealed class RngExtensionTests
{
    [Fact]
    public void Shuffle()
    {
        var list = new List<Int32> { 1, 2, 3, 4, 5, 6, 7 };
        var rng = new StepRng(0) { Increment = 0 };
        rng.ShuffleInPlace(list);
        var expectedOrder = new[] { 2, 3, 4, 5, 6, 7, 1 };
        Assert.Equal(expectedOrder, list);

        Span<Int32> span = stackalloc Int32[] { 1, 2, 3, 4, 5, 6, 7 };
        rng.ShuffleInPlace(span);
        Assert.True(span.SequenceEqual(expectedOrder));

        Assert.Throws<ArgumentNullException>(() => rng.ShuffleInPlace(default(IList<Int32>)!));

        // Doesn't throw with empty input
        rng.ShuffleInPlace(Array.Empty<Int32>());
        rng.ShuffleInPlace(new List<Int32>());
        rng.ShuffleInPlace(Span<Int32>.Empty);
    }
}
