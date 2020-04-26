namespace Rand
{
    /// <summary>
    /// A marker interface used to indicate that an <see cref="IRng"/>
    /// is cryptographically secure.
    /// </summary>
    public interface ICryptoRng : IRng
    { }
}
