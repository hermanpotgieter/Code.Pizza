using System;
using NHibernate;

namespace Code.Pizza.Data.NH.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }

        void Commit();

        void Rollback();
    }
}
