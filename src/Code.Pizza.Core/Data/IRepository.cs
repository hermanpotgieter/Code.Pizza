using System.Collections.Generic;
using Code.Pizza.Common.Paging;
using Code.Pizza.Common.Specifications;
using Code.Pizza.Core.Abstractions;

namespace Code.Pizza.Core.Data
{
    public interface IRepository<in TKey, TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        TEntity FindById(TKey id);

        TEntity FindSingle(ISpecification<TEntity> specification);

        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification);

        IPagedCollection<TEntity> FindPagedCollection<T>(OrderBy<TEntity, T> orderBy, int page, int pageSize);

        TEntity Save(TEntity entity);

        void Delete(TKey id);

        void Delete(TEntity entity);
    }

    public interface IRepository<TEntity> : IRepository<int, TEntity> where TEntity : class, IAggregateRoot<int> {}
}
