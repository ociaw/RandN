namespace Rand.Distributions
{
    public interface IUniformFactory<TSampler, TResult>
        where TSampler : IDistribution<TResult>
    {
        TSampler Create(TResult low, TResult high);

        TSampler CreateInclusive(TResult low, TResult high);
    }
}
