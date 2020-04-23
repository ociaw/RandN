using System;
using System.Security.Cryptography;

namespace Rand
{
    /// <summary>
    /// Wraps <see cref="RNGCryptoServiceProvider"/>.
    /// </summary>
    public sealed class CryptoServiceProvider : IRng, IDisposable
    {
        private readonly RNGCryptoServiceProvider _rng;

        private CryptoServiceProvider(RNGCryptoServiceProvider rng)
        {
            _rng = rng;
        }

        public uint NextUInt32()
        {
            Span<byte> buffer = stackalloc byte[4];
            Fill(buffer);
            return BitConverter.ToUInt32(buffer);
        }

        public ulong NextUInt64()
        {
            Span<byte> buffer = stackalloc byte[8];
            Fill(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        public void Fill(Span<byte> buffer) => _rng.GetBytes(buffer);

        public void Dispose() => _rng.Dispose();

        public sealed class Factory : IRngFactory<CryptoServiceProvider>
        {
            public static Factory Instance { get; } = new Factory();

            public CryptoServiceProvider Create()
            {
                var rng = new RNGCryptoServiceProvider();
                return new CryptoServiceProvider(rng);
            }
        }
    }
}
