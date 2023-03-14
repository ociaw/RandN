using System;
using System.Diagnostics.CodeAnalysis;

namespace RandN.Extensions;

public static partial class DistributionExtensions
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
}
