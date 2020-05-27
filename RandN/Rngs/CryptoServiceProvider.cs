using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace RandN.Rngs
{
    /// <summary>
    /// Wraps <see cref="RNGCryptoServiceProvider"/>.
    /// </summary>
    public sealed class CryptoServiceProvider : IRng, ICryptoRng, IDisposable
    {
        private static readonly Factory _factory = new Factory();
        private readonly RNGCryptoServiceProvider _rng;

        private CryptoServiceProvider(RNGCryptoServiceProvider rng) => _rng = rng;

        public static CryptoServiceProvider Create() => new CryptoServiceProvider(new RNGCryptoServiceProvider());

        public static Factory GetFactory() => _factory;

        public UInt32 NextUInt32()
        {
            Span<Byte> buffer = stackalloc Byte[4];
            Fill(buffer);
            return BinaryPrimitives.ReadUInt32LittleEndian(buffer);
        }

        public UInt64 NextUInt64()
        {
            Span<Byte> buffer = stackalloc Byte[8];
            Fill(buffer);
            return BinaryPrimitives.ReadUInt64LittleEndian(buffer);
        }

        public void Fill(Span<Byte> buffer)
        {
#if NETSTANDARD2_0
            var tmp = new Byte[buffer.Length];
            _rng.GetBytes(tmp);
            tmp.CopyTo(buffer);
#else
            _rng.GetBytes(buffer);
#endif
        }

        public void Dispose() => _rng.Dispose();

        public sealed class Factory : IRngFactory<CryptoServiceProvider>
        {
            internal Factory() { }

            public CryptoServiceProvider Create() => CryptoServiceProvider.Create();
        }
    }
}
