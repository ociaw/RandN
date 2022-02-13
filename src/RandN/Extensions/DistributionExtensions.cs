using System;
using System.Diagnostics.CodeAnalysis;

namespace RandN.Extensions
{
    /// <summary>
    /// Contains extension methods for the <see cref="IDistribution{TResult}"/> interface.
    /// </summary>
    public static class DistributionExtensions
    {
        /// <summary>
        /// Transforms a distribution by mapping its values using the selector provided.
        /// This method implements the "map" operator from functional programming principles.
        /// </summary>
        /// <example>
        /// This method enables use of LINQ query syntax over <see cref="IDistribution{TResult}"/>. For example:
        /// <code>
        /// IDistribution&lt;int&gt; evenNumbersDistribution =
        ///     from i in Uniform.NewInclusive(0, 5)
        ///     select i * 2;
        /// </code>
        /// </example>
        /// <typeparam name="TSource">The generic type of the input distribution.</typeparam>
        /// <typeparam name="TResult">The generic type of the output distribution.</typeparam>
        /// <param name="distribution">The distribution to be transformed.</param>
        /// <param name="selector">The projection to be applied to values from the distribution.</param>
        public static IDistribution<TResult> Select<TSource, TResult>(
            this IDistribution<TSource> distribution,
            Func<TSource, TResult> selector) =>
            new SelectDistribution<TSource, TResult>(distribution, selector);

        /// <summary>
        /// Transforms a distribution by mapping values using the selector provided to produce
        /// a new distribution, which is then sampled from.
        /// This method implements the "bind" operator from functional programming principles.
        /// </summary>
        /// <typeparam name="TSource">The generic type of the input distribution.</typeparam>
        /// <typeparam name="TResult">The generic type of the output distribution.</typeparam>
        /// <param name="distribution">The distribution to be transformed.</param>
        /// <param name="selector">The projection to be applied to values from the distribution.</param>
        public static IDistribution<TResult> SelectMany<TSource, TResult>(
            this IDistribution<TSource> distribution,
            Func<TSource, IDistribution<TResult>> selector) =>
            new SelectManyDistribution<TSource, TResult>(distribution, selector);

        private sealed class SelectDistribution<TSource, TResult> : IDistribution<TResult>
        {
            private readonly IDistribution<TSource> _distribution;
            private readonly Func<TSource, TResult> _selector;

            public SelectDistribution(IDistribution<TSource> distribution, Func<TSource, TResult> selector)
            {
                _distribution = distribution;
                _selector = selector;
            }

            public TResult Sample<TRng>(TRng rng) where TRng : notnull, IRng
            {
                var sample = _distribution.Sample(rng);
                return _selector(sample);
            }

            public Boolean TrySample<TRng>(TRng rng, [MaybeNullWhen(false)] out TResult result) where TRng : notnull, IRng
            {
                if (_distribution.TrySample(rng, out var sample))
                {
                    result = _selector(sample);
                    return true;
                }
                else
                {
                    result = default;
                    return false;
                }
            }
        }

        private sealed class SelectManyDistribution<TSource, TResult> : IDistribution<TResult>
        {
            private readonly IDistribution<TSource> _distribution;
            private readonly Func<TSource, IDistribution<TResult>> _selector;

            public SelectManyDistribution(IDistribution<TSource> distribution, Func<TSource, IDistribution<TResult>> selector)
            {
                _distribution = distribution;
                _selector = selector;
            }

            public TResult Sample<TRng>(TRng rng) where TRng : notnull, IRng
            {
                var sample = _distribution.Sample(rng);
                var distribution = _selector(sample);
                return distribution.Sample(rng);
            }

            public Boolean TrySample<TRng>(TRng rng, [MaybeNullWhen(false)] out TResult result) where TRng : notnull, IRng
            {
                if (!_distribution.TrySample(rng, out var sample1))
                {
                    result = default;
                    return false;
                }

                var distribution = _selector(sample1);

                if (!distribution.TrySample(rng, out var sample2))
                {
                    result = default;
                    return false;
                }

                result = sample2;
                return true;
            }
        }
    }
}
