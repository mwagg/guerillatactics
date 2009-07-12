using System;
using NHibernate;

namespace GuerillaTactics.Common.Persistence
{
    public class PersistenceSessionManager : IPersistenceSessionManager, IDisposable
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _currentSession;
        private ITransaction _currentTransaction;
        private bool _rollbackOnComplete;
        private bool _currentSessionIsAlreadyComplete;
        private bool _isDisposed;

        public PersistenceSessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetCurrentSession()
        {
            if(_currentSession == null)
            {
                OpenCurrentSession();
            }

            return _currentSession;
        }

        private void OpenCurrentSession()
        {
            _currentSession = _sessionFactory.OpenSession();
            _currentTransaction = _currentSession.BeginTransaction();
        }

        public void CompleteCurrentSession()
        {
            if(_currentSession == null)
            {
                return;
            }

            if(_currentSessionIsAlreadyComplete)
            {
                throw new InvalidOperationException("The current session has already been completed and an attempt is being made to complete it again.");
            }

            if (_rollbackOnComplete == false)
            {
                _currentTransaction.Commit();
            }
            else
            {
                _currentTransaction.Rollback();
            }

            _currentSession.Close();

            _currentSessionIsAlreadyComplete = true;
        }

        public void RollbackOnComplete()
        {
            _rollbackOnComplete = true;
        }

        public void Dispose()
        {
            if(_isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name, "The persistence session manager is already disposed and another call to Dispose has been made.");
            }

            _isDisposed = true;
            CompleteCurrentSession();
        }
    }
}