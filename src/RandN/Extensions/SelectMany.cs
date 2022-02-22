using System;
using System.Diagnostics.CodeAnalysis;

namespace RandN.Extensions
{
    public static partial class DistributionExtensions
    {
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
            distribution.SelectMany(selector, (_, x) => x);

        /// <summary>
        /// Transforms a distribution by mapping values using the selector provided to produce
        /// a new distribution, which is then sampled from.
        /// </summary>
        /// <example>
        /// This method enables use of LINQ query syntax over <see cref="IDistribution{TResult}"/>. For example:
        /// <code>
        /// IDistribution&lt;PointF&gt; unitSquarePointDistribution =
        ///     from x in Uniform.NewInclusive(0.0, 1.0)
        ///     from y in Uniform.NewInclusive(0.0, 1.0)
        ///     select new System.Drawing.PointF(x, y);
        /// </code>
        /// </example>
        /// <typeparam name="TSource">The generic type of the input distribution.</typeparam>
        /// <typeparam name="TIntermediate">The generic type of the intermediate distribution.</typeparam>
        /// <typeparam name="TResult">The generic type of the output distribution.</typeparam>
        /// <param name="distribution">The distribution to be transformed.</param>
        /// <param name="selector">The projection to be applied to values from the
        /// distribution to produce an intermediate distribution.</param>
        /// <param name="resultSelector">The projection to be applied to values from both distributions.</param>
        public static IDistribution<TResult> SelectMany<TSource, TIntermediate, TResult>(
            this IDistribution<TSource> distribution,
            Func<TSource, IDistribution<TIntermediate>> selector,
            Func<TSource, TIntermediate, TResult> resultSelector) =>
            new SelectManyDistribution<TSource, TIntermediate, TResult>(distribution, selector, resultSelector);

        private sealed class SelectManyDistribution<TSource, TIntermediate, TResult> : IDistribution<TResult>
        {
            private readonly IDistribution<TSource> _distribution;
            private readonly Func<TSource, IDistribution<TIntermediate>> _selector;
            private readonly Func<TSource, TIntermediate, TResult> _resultSelector;

            public SelectManyDistribution(
                IDistribution<TSource> distribution,
                Func<TSource, IDistribution<TIntermediate>> selector,
                Func<TSource, TIntermediate, TResult> resultSelector)
            {
                _distribution = distribution;
                _selector = selector;
                _resultSelector = resultSelector;
            }

            public TResult Sample<TRng>(TRng rng) where TRng : notnull, IRng
            {
                var sample1 = _distribution.Sample(rng);
                var distribution = _selector(sample1);
                var sample2 = distribution.Sample(rng);
                return _resultSelector(sample1, sample2);
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

                result = _resultSelector(sample1, sample2);
                return true;
            }
        }
    }
}
