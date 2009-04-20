using Castle.Windsor;
using GuerillaTactics.Common.Eventing;
using LooselyCoupledMVP.Domain;

namespace LooselyCoupledMVP
{
    public class ApplicationController
    {
        private readonly IEventHub _eventHub;
        private readonly IWindsorContainer _container;

        public ApplicationController(IEventHub eventHub, IWindsorContainer container)
        {
            _eventHub = eventHub;
            _container = container;
        }

        public void PublishMessage<T>(T message)
        {
            _eventHub.Publish(message);
        }

        public void ExecuteCommand<T>(T command)
        {
            foreach(ICommand<T> commandInstance in _container.ResolveAll(typeof(ICommand<T>)))
            {
                commandInstance.Execute(command);
            }
            
        }
    }
}