namespace Code.Pizza.Core.Abstractions
{
    public class AggregateRoot : Entity, IAggregateRoot<int>
    {
        public virtual int Version { get; set; }
    }
}
