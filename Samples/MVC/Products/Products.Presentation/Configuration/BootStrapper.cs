using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Products.Presentation.Configuration
{
    public class BootStrapper
    {
        public void Execute()
        {
            var container = new WindsorContainer();
            container.RegisterWithSelf();

            container.Register(AllTypes
                                   .FromAssemblyContaining<BootStrapper>()
                                   .BasedOn<IApplicationConfiguration>());

            foreach (var configuration in container.ResolveAll<IApplicationConfiguration>())
            {
                configuration.Execute(container);
            }
        }
    }
}