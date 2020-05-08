using System;
using System.Buffers.Binary;

namespace RandN.RngHelpers
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
        public static UInt64 NextUInt64ViaUInt32<TRng>(TRng rng)
            where TRng : IRng
        {
            // Use Little Endian; we explicitly generate one value before the next.
            var x = (UInt64)rng.NextUInt32();
            var y = (UInt64)rng.NextUInt32();
            return (y << 32) | x;
        }

        /// <summary>
        /// Implement NextUInt32 via NextUInt64.
        /// </summary>
        public static UInt32 NextUInt32ViaUInt64<TRng>(TRng rng) where TRng : IRng => unchecked((UInt32)rng.NextUInt64());

        /// <summary>
        /// Implement FillBytes via NextUInt64 and NextUInt32, little-endian order.
        /// </summary>  
        public static void FillBytesViaNext<TRng>(TRng rng, Span<Byte> destination)
            where TRng : IRng
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
                chunk.Slice(destination.Length).CopyTo(destination);
            }
            else if (destination.Length > 0)
            {
                Span<Byte> chunk = stackalloc Byte[4];
                var num = rng.NextUInt32();
                BinaryPrimitives.WriteUInt32LittleEndian(chunk, num);
                chunk.Slice(destination.Length).CopyTo(destination);
            }
        }
    }
}
