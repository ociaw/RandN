using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RandN.Implementation
{
    /// <summary>
    /// A helper class to implement <see cref="IRng"/> methods via either NextUInt32 or NextUInt64.
    /// Little Endian order is used where relevant.
    /// </summary>
    /// <remarks>
    /// Based off of Rust's rand core crate 0.5.1.
    /// https://docs.rs/rand_core/0.5.1/src/rand_core/impls.rs.html
    /// </remarks>
    public static class Filler
    {
        /// <summary>
        /// Implement NextUInt64 via NextUInt32, using Little Endian order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 NextUInt64ViaUInt32<TRng>(TRng rng)
            where TRng : notnull, IRng
        {
            // Use Little Endian; we explicitly generate one value before the next.
            var x = (UInt64)rng.NextUInt32();
            var y = (UInt64)rng.NextUInt32();
            return (y << 32) | x;
        }

        /// <summary>
        /// Implement NextUInt32 via NextUInt64.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 NextUInt32ViaUInt64<TRng>(TRng rng) where TRng : notnull, IRng => unchecked((UInt32)rng.NextUInt64());

        /// <summary>
        /// Implement FillBytes via NextUInt64 and NextUInt32, little-endian order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillBytesViaNext<TRng>(TRng rng, Span<Byte> destination)
            where TRng : notnull, IRng
        {
            while (destination.Length > 8)
            {
                var num = rng.NextUInt64();
                BinaryPrimitives.WriteUInt64LittleEndian(destination, num);
                destination = destination.Slice(8);
            }

            if (destination.Length > 4)
            {
                Span<Byte> chunk = stackalloc Byte[8];
                var num = rng.NextUInt64();
                BinaryPrimitives.WriteUInt64LittleEndian(chunk, num);
                chunk.Slice(0, destination.Length).CopyTo(destination);
            }
            else if (destination.Length > 0)
            {
                Span<Byte> chunk = stackalloc Byte[4];
                var num = rng.NextUInt32();
                BinaryPrimitives.WriteUInt32LittleEndian(chunk, num);
                chunk.Slice(0, destination.Length).CopyTo(destination);
            }
        }

        /// <summary>
        /// Fills <paramref name="dest"/> with UInt32 chunks from <paramref name="src"/>.
        /// </summary>
        /// <returns>A tuple with the number of UInt32 chunks consumed from <paramref name="src"/> and the number of bytes filled into <paramref name="dest"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 consumedUInt32, Int32 filledBytes) FillViaUInt32Chunks(ReadOnlySpan<UInt32> src, Span<Byte> dest)
        {
            const Int32 size = sizeof(UInt32);
            var chunkSizeByte = Math.Min(src.Length * size, dest.Length);
            var chunkSize = (chunkSizeByte + size - 1) / size;
            if (BitConverter.IsLittleEndian)
            {
                var srcBytes = MemoryMarshal.AsBytes(src).Slice(0, chunkSizeByte);
                srcBytes.CopyTo(dest);
            }
            else
            {
                for (var i = 0; i < chunkSize; i++)
                {
                    Span<Byte> leBytes = stackalloc Byte[size];
                    BinaryPrimitives.WriteUInt32LittleEndian(leBytes, src[i]);
                    var byteCount = Math.Min(size, chunkSizeByte - i * size);
                    leBytes.Slice(0, i * byteCount).CopyTo(dest);
                    dest = dest.Slice(byteCount);
                }
            }
            return (chunkSize, chunkSizeByte);
        }

        /// <summary>
        /// Fills <paramref name="dest"/> with UInt64 chunks from <paramref name="src"/>.
        /// </summary>
        /// <returns>A tuple with the number of UInt64 chunks consumed from <paramref name="src"/> and the number of bytes filled into <paramref name="dest"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 consumedUInt64, Int32 filledBytes) FillViaUInt64(ReadOnlySpan<UInt64> src, Span<Byte> dest)
        {
            const Int32 size = sizeof(UInt64);
            var chunkSizeByte = Math.Min(src.Length * size, dest.Length);
            var chunkSize = (chunkSizeByte + size - 1) / size;
            if (BitConverter.IsLittleEndian)
            {
                var srcBytes = MemoryMarshal.AsBytes(src).Slice(0, chunkSizeByte);
                srcBytes.CopyTo(dest);
            }
            else
            {
                for (var i = 0; i < chunkSize; i++)
                {
                    Span<Byte> leBytes = stackalloc Byte[size];
                    BinaryPrimitives.WriteUInt64LittleEndian(leBytes, src[i]);
                    var byteCount = Math.Min(size, chunkSizeByte - i * size);
                    leBytes.Slice(0, i * byteCount).CopyTo(dest);
                    dest = dest.Slice(byteCount);
                }
            }
            return (chunkSize, chunkSizeByte);
        }
    }
}
