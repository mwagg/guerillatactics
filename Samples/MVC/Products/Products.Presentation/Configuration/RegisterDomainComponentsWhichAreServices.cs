using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Products.Presentation.Configuration
{
    public class RegisterDomainComponentsWhichAreServices : IApplicationConfiguration
    {
        public void Execute(IWindsorContainer container)
        {
            container.Register(AllTypes.FromAssembly(Assemblies.CoreAssembly)
                                   .Where(type => type.Namespace.Contains(".Domain.") && type.GetInterfaces().Length > 0)
                                   .WithService.FirstInterface()
                                   .Configure(config => config.LifeStyle.Transient));
        }
    }
}