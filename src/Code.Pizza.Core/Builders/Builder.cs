using Code.Pizza.Core.Abstractions;

namespace Code.Pizza.Core.Builders
{
    public abstract class Builder<T> where T : Entity
    {
        public abstract T Create();
    }
}
