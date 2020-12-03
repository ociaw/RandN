namespace RandN
{
    /// <summary>
    /// A marker interface used to indicate that an <see cref="IDistribution{TResult}"/>
    /// is portable - i.e. reproducible regardless of the machine architecture or runtime.
    /// Produces portable values random of <typeparamref name="TResult"/>.
    /// </summary>
    /// <remarks>
    /// Implementations are immutable and therefore thread safe. Results are also reproducible
    /// within the same assembly version.
    /// </remarks>
    /// <typeparam name="TResult">The type that is produced by this distribution.</typeparam>
    public interface IPortableDistribution<TResult> : IDistribution<TResult>
    { }
}
