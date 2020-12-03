namespace RandN
{
    /// <summary>
    /// A factory that produces Random Number Generators.
    /// </summary>
    public interface IRngFactory<out TRng>
        where TRng : IRng
    {
        /// <summary>
        /// Creates a new <typeparamref name="TRng" />.
        /// </summary>
        /// <returns>A new <typeparamref name="TRng" /> instance.</returns>
        TRng Create();
    }
}
