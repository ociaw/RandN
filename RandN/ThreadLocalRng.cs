using System;
using System.Threading;
using RandN.Rngs;

namespace RandN
{
    /// <summary>
    /// A cryptographically secure thread local generator. All members in this type are thread-safe.
    /// </summary>
    public sealed class ThreadLocalRng : IRng, ICryptoRng
    {
        private static readonly ThreadLocal<ChaCha> _threadLocal = new ThreadLocal<ChaCha>(() =>
        {
            using var seeder = CryptoServiceProvider.Create();
            return ChaCha.GetChaCha8Factory().Create(seeder);
        });

        /// <summary>
        /// Creates a <see cref="ThreadLocalRng"/>.
        /// </summary>
        public static ThreadLocalRng Get() => new ThreadLocalRng();

        /// <inheritdoc />
        public void Fill(Span<Byte> buffer) => _threadLocal.Value.Fill(buffer);

        /// <inheritdoc />
        public UInt32 NextUInt32() => _threadLocal.Value.NextUInt32();

        /// <inheritdoc />
        public UInt64 NextUInt64() => _threadLocal.Value.NextUInt64();
    }
}
