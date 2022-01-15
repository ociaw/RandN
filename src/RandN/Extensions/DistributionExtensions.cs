using System;

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
        /// <remarks>
        /// This method enables use of LINQ query syntax over <see cref="IDistribution{TResult}"/>. For example:
        /// <code>
        /// IDistribution&lt;int&gt; evenNumbersDistribution =
        ///     from i in Uniform.NewInclusive(0, 5)
        ///     select i * 2;
        /// </code>
        /// </remarks>
        /// <typeparam name="TSource">The generic type of the input distribution.</typeparam>
        /// <typeparam name="TResult">The generic type of the output distribution.</typeparam>
        /// <param name="distribution">The distribution to be transformed.</param>
        /// <param name="selector">The projection to be applied to values from the distribution.</param>
        public static IDistribution<TResult> Select<TSource, TResult>(
            IDistribution<TSource> distribution,
            Func<TSource, TResult> selector) =>
            throw new NotImplementedException();

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
            IDistribution<TSource> distribution,
            Func<TSource, IDistribution<TResult>> selector) =>
            throw new NotImplementedException();

        /// <summary>
        /// Transforms a distribution by mapping values using the selector provided to produce
        /// a new distribution, which is then sampled from.
        /// </summary>
        /// <remarks>
        /// This method enables use of LINQ query syntax over <see cref="IDistribution{TResult}"/>. For example:
        /// <code>
        /// IDistribution&lt;PointF&gt; unitSquarePointDistribution =
        ///     from x in Uniform.NewInclusive(0.0, 1.0)
        ///     from y in Uniform.NewInclusive(0.0, 1.0)
        ///     select new System.Drawing.PointF(x, y);
        /// </code>
        /// </remarks>
        /// <typeparam name="TSource">The generic type of the input distribution.</typeparam>
        /// <typeparam name="TResult">The generic type of the output distribution.</typeparam>
        /// <param name="distribution">The distribution to be transformed.</param>
        /// <param name="intermediateSelector">The projection to be applied to values from the
        /// distribution to produce an intermediate distribution.</param>
        /// <param name="resultSelector">The projection to be applied to values from both distributions.</param>
        public static IDistribution<TResult> SelectMany<TSource, TIntermediate, TResult>(
            this IDistribution<TSource> distribution,
            Func<TSource, IDistribution<TIntermediate>> intermediateSelector,
            Func<TSource, TIntermediate, TResult> resultSelector) =>
            throw new NotImplementedException();
    }
}
