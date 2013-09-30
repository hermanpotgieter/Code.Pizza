using System;

namespace Code.Pizza.Core.Abstractions
{
    public class Entity : IEntity<int>
    {
        public virtual int ID { get; set; }
    }
}
