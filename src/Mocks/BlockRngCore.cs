using System;
using RandN.Implementation;

namespace RandN
{
    public sealed class StepBlockRng : ISeekableBlockRngCore<UInt32, UInt32>
    {
        public UInt32 BlockCounter { get; set; }

        public Int32 BlockLength => 8;

        public void Generate(Span<UInt32> results)
        {
            if (results.Length != BlockLength)
                throw new ArgumentException($"{nameof(results)}.{nameof(results.Length)} must equal {nameof(BlockLength)}.");

            BlockCounter = unchecked(BlockCounter + 1);
            results.Fill(BlockCounter);
        }

        public void Regenerate(Span<UInt32> results)
        {
            if (results.Length != BlockLength)
                throw new ArgumentException($"{nameof(results)}.{nameof(results.Length)} must equal {nameof(BlockLength)}.");

            results.Fill(BlockCounter);
        }
    }
}
