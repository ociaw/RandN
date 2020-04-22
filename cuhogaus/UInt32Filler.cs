using System;
using System.Buffers.Binary;

namespace Cuhogaus
{
    internal sealed class UInt32Filler
    {
        public static ulong ToUInt64(uint first, uint second) => (((ulong)first) << sizeof(uint)) | second;

        public static void Fill(Span<byte> buffer, Func<uint> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var current = buffer;
            while (current.Length != 0)
            {
                uint num = source();
                Int32 written = FillInternal(current, num);

                if (current.Length == written)
                    return; // The buffer is now filled.

                current = current.Slice(current.Length);
            }
        }

        private static Int32 FillInternal(Span<byte> buffer, uint num)
        {
            Span<byte> bytes = stackalloc byte[sizeof(uint)];
            BinaryPrimitives.TryWriteUInt32LittleEndian(bytes, num);

            Int32 length = Math.Min(buffer.Length, bytes.Length);
            for (int i = 0; i < length; i++)
                buffer[i] = bytes[i];

            return length;
        }
    }
}
