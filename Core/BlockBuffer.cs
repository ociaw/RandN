using System;
using System.Buffers;

namespace Cuhogaus
{
    public sealed class BlockBuffer
    {
        private readonly byte[] _buffer;

        private readonly SpanAction<byte, int> _refill;

        public int Length => _buffer.Length;

        public int Position => _buffer.Length - BufferLeft;

        private int BufferLeft { get; set; }

        public void Fill(Span<byte> destination)
        {
            Span<byte> span = destination;
            while (!span.IsEmpty)
                span = FillInternal(span);
        }

        private Span<byte> FillInternal(Span<byte> destination)
        {
            int fillCount = Math.Min(BufferLeft, destination.Length);
            _buffer.AsSpan(Position, fillCount).CopyTo(destination);
            BufferLeft -= fillCount;
            if (BufferLeft == 0)
                Refill();
            if (destination.Length == fillCount)
                return Span<byte>.Empty;
            return destination.Slice(fillCount, destination.Length - fillCount);
        }

        private void Refill()
        {
            _refill(_buffer, 0);
            BufferLeft = _buffer.Length;
        }
    }
}
