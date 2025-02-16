using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RandN.Rngs;
using Xunit;

namespace RandN;

public sealed class RngExtensionTests
{
    [Fact]
    public void SimpleShuffle()
    {
        Int32[] expectedOrder = [2, 3, 4, 5, 6, 7, 1];

        Span<Int32> span = [1, 2, 3, 4, 5, 6, 7];
        var array = span.ToArray();
        var list = new List<Int32>(array);
        var notList = new NotList(new List<Int32>(list));
        var rng = new StepRng(0) { Increment = 0 };

        rng.ShuffleInPlace(span);
        Assert.True(span.SequenceEqual(expectedOrder));

        rng.ShuffleInPlace(array);
        Assert.Equal(expectedOrder, array);

        rng.ShuffleInPlace(list);
        Assert.Equal(expectedOrder, list);

        rng.ShuffleInPlace(notList);
        Assert.Equal(expectedOrder, notList);

        Assert.Throws<ArgumentNullException>(() => rng.ShuffleInPlace(default(IList<Int32>)!));

        // Doesn't throw with empty input
        rng.ShuffleInPlace(Array.Empty<Int32>());
        rng.ShuffleInPlace(new List<Int32>());
        rng.ShuffleInPlace(Span<Int32>.Empty);
    }

    [Theory]
    [InlineData(0, 10ul)]
    [InlineData(1, 20ul)]
    [InlineData(2, 30ul)]
    [InlineData(3, 40ul)]
    [InlineData(7, 50ul)]
    [InlineData(15, 60ul)]
    [InlineData(16, 65ul)]
    [InlineData(16384, 123ul)]
    [InlineData(32768, 456ul)]
    [InlineData(65536, 789ul)]
    public void CompareShuffle(Int32 count, UInt64 seed)
    {
        var array = Enumerable.Range(0, count).ToArray();
        var notList = new NotList(new List<Int32>(array));
        
        var arrayRng = Pcg32.Create(seed, 123);
        var notListRng = Pcg32.Create(seed, 123);
        
        arrayRng.ShuffleInPlace(array);
        notListRng.ShuffleInPlace(notList);
        
        Assert.Equal(array, notList);
    }
    
    /// <summary>
    /// This is used to ensure we test the non-span path for lists.
    /// </summary>
    /// <param name="wrapped"></param>
    private sealed class NotList(IList<Int32> wrapped) : IList<Int32>
    {
        public IEnumerator<Int32> GetEnumerator() => wrapped.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)wrapped).GetEnumerator();

        public void Add(Int32 item) => wrapped.Add(item);

        public void Clear() => wrapped.Clear();

        public Boolean Contains(Int32 item) => wrapped.Contains(item);

        public void CopyTo(Int32[] array, Int32 arrayIndex) => wrapped.CopyTo(array, arrayIndex);

        public Boolean Remove(Int32 item) => wrapped.Remove(item);

        public Int32 Count => wrapped.Count;

        public Boolean IsReadOnly => wrapped.IsReadOnly;

        public Int32 IndexOf(Int32 item) => wrapped.IndexOf(item);

        public void Insert(Int32 index, Int32 item) => wrapped.Insert(index, item);

        public void RemoveAt(Int32 index) => wrapped.RemoveAt(index);

        public Int32 this[Int32 index]
        {
            get => wrapped[index];
            set => wrapped[index] = value;
        }
    }
}
