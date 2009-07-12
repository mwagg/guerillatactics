using NHibernate;

namespace GuerillaTactics.Common.Persistence
{
    public interface IPersistenceSessionManager
    {
        ISession GetCurrentSession();
        void RollbackOnComplete();
        void CompleteCurrentSession();
    }
}