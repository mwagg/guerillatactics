using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GuerillaTactics.Common.Eventing;
using LooselyCoupledMVP.Domain;
using LooselyCoupledMVP.Infrastructure;
using LooselyCoupledMVP.Presentation.Views;

namespace LooselyCoupledMVP
{
    public class BootStrapper
    {
        private WindsorContainer _container;

        public void BootStrap()
        {
            _container = new WindsorContainer();
            ConfigureContainer();

            RunUI();
        }

        private void ConfigureContainer()
        {
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
            _container.Register(Component.For<ApplicationController>());

            RegisterPresentationTypes();
            RegisterEventHub();

            RegisterCommands();

            _container.AddFacility<AutoEventHubRegistrationFacility>();
        }

        private void RegisterCommands()
        {
            _container.Register(AllTypes.FromAssembly(GetType().Assembly)
                                    .BasedOn(typeof (ICommand<>))
                                    .WithService.FirstInterface()
                                    .Configure(config => config.LifeStyle.Transient));
        }

        private void RegisterEventHub()
        {
            _container.Register(Component.For<IEventHub>().ImplementedBy<EventHub>());
        }

        private void RegisterPresentationTypes()
        {
            _container.Register(AllTypes.FromAssembly(GetType().Assembly)
                                    .BasedOn(typeof (object))
                                    .If(type => type.Namespace.StartsWith("LooselyCoupledMVP.Presentation"))
                                    .Configure(config => config.LifeStyle.Transient));
        }

        private void RunUI()
        {
            var dataEntryForm = _container.Resolve<DataEntryForm>();
            var resultsForm = _container.Resolve<ResultsForm>();

            resultsForm.Show();

            Application.Run(dataEntryForm);
        }
    }
}