using Castle.Windsor;

namespace Products.Presentation.Configuration
{
    public interface IApplicationConfiguration
    {
        void Execute(IWindsorContainer container);
    }
}