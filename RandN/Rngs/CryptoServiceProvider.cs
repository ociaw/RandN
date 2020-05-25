using System;
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
            return BitConverter.ToUInt32(buffer);
        }

        public UInt64 NextUInt64()
        {
            Span<Byte> buffer = stackalloc Byte[8];
            Fill(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        public void Fill(Span<Byte> buffer) => _rng.GetBytes(buffer);

        public void Dispose() => _rng.Dispose();

        public sealed class Factory : IRngFactory<CryptoServiceProvider>
        {
            internal Factory() { }

            public CryptoServiceProvider Create() => CryptoServiceProvider.Create();
        }
    }
}
