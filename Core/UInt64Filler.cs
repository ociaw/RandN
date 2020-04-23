using System;
using System.Buffers.Binary;

namespace Rand
{
    internal sealed class UInt64Filler
    {
        public static uint ToUInt32(ulong num) => (uint)num;

        public static void Fill(Span<byte> buffer, Func<ulong> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var current = buffer;
            while (current.Length != 0)
            {
                ulong num = source();
                Int32 written = FillInternal(current, num);

                if (current.Length == written)
                    return; // The buffer is now filled.

                current = current.Slice(current.Length);
            }
        }

        private static Int32 FillInternal(Span<byte> buffer, ulong num)
        {
            Span<byte> bytes = stackalloc byte[sizeof(ulong)];
            BinaryPrimitives.TryWriteUInt64LittleEndian(bytes, num);

            Int32 length = Math.Min(buffer.Length, bytes.Length);
            for (int i = 0; i < length; i++)
                buffer[i] = bytes[i];

            return length;
        }
    }
}
