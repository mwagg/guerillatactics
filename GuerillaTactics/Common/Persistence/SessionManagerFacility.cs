using Castle.Core;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using NHibernate;

namespace GuerillaTactics.Common.Persistence
{
    public class SessionManagerFacility : AbstractFacility
    {
        private readonly LifestyleType _sessionManagerLifestyleType;
        private readonly ISessionFactory _sessionFactory;

        public SessionManagerFacility(LifestyleType sessionManagerLifestyleType, ISessionFactory sessionFactory)
        {
            _sessionManagerLifestyleType = sessionManagerLifestyleType;
            _sessionFactory = sessionFactory;
        }

        protected override void Init()
        {
            Kernel
                .Register(Component
                              .For<IPersistenceSessionManager>()
                              .ImplementedBy<PersistenceSessionManager>()
                              .LifeStyle.Is(_sessionManagerLifestyleType))
                .Register(Component
                              .For<ISessionFactory>()
                              .Instance(_sessionFactory));
        }
    }
}