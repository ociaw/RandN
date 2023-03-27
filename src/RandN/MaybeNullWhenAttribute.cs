// ReSharper disable All
#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// This is a hacky way to allow us to use the MaybeNullWhenAttribute when we target .NET Standard 2.0. 
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class MaybeNullWhenAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of <see cref="MaybeNullWhenAttribute"/>.
    /// </summary>
    public MaybeNullWhenAttribute(Boolean returnValue) => ReturnValue = returnValue;

    /// <summary>
    /// The return value when the parameter may be null.
    /// </summary>
    public Boolean ReturnValue { get; }
}
#endif
