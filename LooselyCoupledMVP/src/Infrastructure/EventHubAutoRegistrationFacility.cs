using System;
using Castle.Core;
using Castle.MicroKernel.Facilities;
using GuerillaTactics.Common.Eventing;

namespace LooselyCoupledMVP.Infrastructure
{
    public class EventHubAutoRegistrationFacility : AbstractFacility
    {
        private IEventHub _eventHub;

        protected override void Init()
        {
            Kernel.ComponentCreated += HandleComponentCreated;
            Kernel.ComponentDestroyed += HandleComponentDestroyed;
        }

        private void HandleComponentDestroyed(ComponentModel model, object instance)
        {
            GetEventHub().Unregister(instance);
        }

        private IEventHub GetEventHub()
        {
            if(_eventHub == null)
            {
                _eventHub = Kernel.Resolve<IEventHub>();
            }

            return _eventHub;
        }

        private void HandleComponentCreated(ComponentModel model, object instance)
        {
            foreach (Type interfaceType in instance.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof (IMessageSubscriber<>))
                {
                    GetEventHub().Register(instance);
                }
            }
        }
    }
}