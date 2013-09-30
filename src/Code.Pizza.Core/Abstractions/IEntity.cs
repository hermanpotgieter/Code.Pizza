namespace Code.Pizza.Core.Abstractions
{
    public interface IEntity<TKey>
    {
        TKey ID { get; set; }
    }
}
