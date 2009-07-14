using Castle.Core;
using Castle.MicroKernel;
using NHibernate;

namespace GuerillaTactics.Common.Persistence
{
    public class SessionSubDependencyResolver : ISubDependencyResolver
    {
        private readonly IKernel _kernel;

        public SessionSubDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver,
                              ComponentModel model, DependencyModel dependency)
        {
            return _kernel.Resolve<IPersistenceSessionManager>().GetCurrentSession();
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver,
                               ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof (ISession);
        }
    }
}