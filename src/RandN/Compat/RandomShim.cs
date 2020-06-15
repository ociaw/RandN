using System;
using RandN.Distributions;

namespace RandN
{
    /// <summary>
    /// A shim able to wrap any <see cref="IRng"/> as a <see cref="Random"/>.
    /// </summary>
    public sealed class RandomShim<TRng> : Random
        where TRng : IRng
    {
        private readonly TRng _rng;
        private static readonly UniformDouble _doubleDist = Uniform.New(0.0, 1.0);

        /// <summary>
        /// Constructs a new <see cref="Random"/> wrapper over <paramref name="rng"/> .
        /// </summary>
        public RandomShim(TRng rng) => _rng = rng;

        /// <inheritdoc />
        public override Int32 Next() => Next(Int32.MaxValue);

        /// <inheritdoc />
        public override Int32 Next(Int32 maxValue) => Next(0, maxValue);

        /// <inheritdoc />
        public override Int32 Next(Int32 minValue, Int32 maxValue) => UniformInt32.Create(minValue, maxValue).Sample(_rng);

        /// <inheritdoc />
        public override void NextBytes(Byte[] buffer) => _rng.Fill(buffer);

#if !NETSTANDARD2_0
        /// <inheritdoc />
        public override void NextBytes(Span<Byte> buffer) => _rng.Fill(buffer);
#endif

        /// <inheritdoc />
        public override Double NextDouble() => _doubleDist.Sample(_rng);

        /// <inheritdoc />
        protected override Double Sample() => NextDouble();
    }

    /// <summary>
    /// Contains static helper methods for <see cref="RandomShim{TRng}"/>.
    /// </summary>
    public static class RandomShim
    {
        /// <summary>
        /// Constructs a new <see cref="Random"/> wrapper over <paramref name="rng"/> .
        /// </summary>
        public static RandomShim<TRng> Create<TRng>(TRng rng) where TRng : IRng => new RandomShim<TRng>(rng);
    }
}
