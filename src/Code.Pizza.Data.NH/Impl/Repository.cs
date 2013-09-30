using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Code.Pizza.Common.Paging;
using Code.Pizza.Common.Specifications;
using Code.Pizza.Common.Utilities;
using Code.Pizza.Core.Abstractions;
using Code.Pizza.Core.Data;
using Code.Pizza.Data.NH.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace Code.Pizza.Data.NH.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot<int>
    {
        protected readonly ISession session;

        public Repository(IUnitOfWork unitOfWork)
        {
            Guard.AgainstNull(() => unitOfWork);

            this.session = unitOfWork.Session;
        }

        public virtual TEntity FindById(int id)
        {
            TEntity entity = this.session.Get<TEntity>(id);

            return entity;
        }

        public virtual TEntity FindSingle(ISpecification<TEntity> specification)
        {
            Guard.AgainstNull(() => specification);

            TEntity entity = this.session.Query<TEntity>().Where(specification.GetExpression()).SingleOrDefault();

            return entity;
        }

        public IEnumerable<TEntity> FindAll()
        {
            List<TEntity> entities = this.session.Query<TEntity>().ToList();

            return entities;
        }

        public virtual IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            Guard.AgainstNull(() => specification);

            Expression<Func<TEntity, bool>> expression = specification.GetExpression();

            List<TEntity> entities = this.session.Query<TEntity>().Where(expression).ToList();

            return entities;
        }

        public IPagedCollection<TEntity> FindPagedCollection<T>(OrderBy<TEntity, T> orderBy, int page, int pageSize)
        {
            Guard.Against<ArgumentOutOfRangeException>(page < 1, "Page cannot be less than 1.");
            Guard.Against<ArgumentOutOfRangeException>(pageSize < 1, "PageSize cannot be less than 1.");

            Guard.AgainstNull(() => orderBy);

            Expression<Func<TEntity, T>> orderExpression = orderBy.GetExpression();
            int firstResults = (page - 1) * pageSize;
            int itemCount = this.session.Query<TEntity>().Count();

            List<TEntity> entities = this.session.Query<TEntity>().OrderBy(orderExpression).Skip(firstResults).Take(pageSize).ToList();

            PagedSet<TEntity> pagedCollection = new PagedSet<TEntity>(entities, page, pageSize, itemCount);

            return pagedCollection;
        }

        public virtual TEntity Save(TEntity entity)
        {
            Guard.AgainstNull(() => entity);

            try
            {
                this.session.SaveOrUpdate(entity);
            }
            catch (NonUniqueObjectException)
            {
                this.session.Merge(entity);
            }

            return entity;
        }

        public virtual void Delete(int id)
        {
            TEntity entity = this.session.Get<TEntity>(id);

            if (entity != null)
            {
                this.session.Delete(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Guard.AgainstNull(() => entity);

            this.Delete(entity.ID);
        }
    }
}
