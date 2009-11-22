using Castle.Windsor;
using GuerillaTactics.Common.Eventing;
using LooselyCoupledMVP.Domain;

namespace LooselyCoupledMVP
{
	public class ApplicationController
	{
		private readonly IWindsorContainer _container;
		private readonly IEventHub _eventHub;

		public ApplicationController(IEventHub eventHub, IWindsorContainer container)
		{
			_eventHub = eventHub;
			_container = container;
		}

		public void PublishMessage<T>(T message)
		{
			ExecuteCommands(message);

			_eventHub.Publish(message);
		}

		private void ExecuteCommands<T>(T message)
		{
			foreach (var command in _container.ResolveAll<ICommand<T>>())
			{
				command.Execute(message);
			}
		}
	}
}