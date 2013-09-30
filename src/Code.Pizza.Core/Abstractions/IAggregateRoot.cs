namespace Code.Pizza.Core.Abstractions
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        int Version { get; set; }
    }
}
