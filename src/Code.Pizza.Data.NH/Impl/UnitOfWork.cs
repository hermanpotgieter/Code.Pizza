using System;
using System.Data;
using Code.Pizza.Common.Utilities;
using Code.Pizza.Data.NH.Interfaces;
using NHibernate;

namespace Code.Pizza.Data.NH.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        private readonly ITransaction transaction;

        private bool comitted;
        private bool rolledBack;
        private bool disposed;

        public ISession Session
        {
            get
            {
                return this.session;
            }
        }

        public UnitOfWork(ISession session)
        {
            Guard.AgainstNull(() => session);

            this.session = session;
            this.transaction = session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            this.GuardAgainstInvalidOperations();

            try
            {
                this.transaction.Commit();
            }
            catch(Exception)
            {
                this.Rollback();
                throw;
            }
            this.comitted = true;
        }

        public void Rollback()
        {
            this.GuardAgainstInvalidOperations();

            this.transaction.Rollback();
            this.rolledBack = true;
        }

        // Implement IDisposable.
        // Do not make this method virtual. A derived class should not be able to override this method.
        public void Dispose()
        {
            this.Dispose(true);
            // This object will be cleaned up by the Dispose method. Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly or indirectly by a user's code. 
        // Managed and unmanaged resources can be disposed.
        // If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!this.disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if(disposing)
                {
                    if(!this.comitted && !this.rolledBack)
                    {
                        this.Commit();
                    }

                    this.session.Close();
                    this.session.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
            }
            this.disposed = true;
        }

        private void GuardAgainstInvalidOperations()
        {
            Guard.Against<InvalidOperationException>(!this.transaction.IsActive,
                "NHibernate ITransaction instance is not active.");

            Guard.Against<InvalidOperationException>(this.comitted || this.rolledBack,
                "You can't call Commit() or Rollback() on a transaction which has already been committed or rolled back.");
        }
    }
}
