using AutoWiringIoc.ManualServices;
using AutoWiringIoc.Services;
using Castle.MicroKernel.Registration;
using NUnit.Framework;

namespace AutoWiringIoc
{
	public class ManualOverride : TestBase
	{
		protected override void DoRegistration()
		{
			_container.Register(Component.For<ISomeService>()
									.ImplementedBy<SomeOtherServiceImplementation>());

			_container.Register(AllTypes.FromAssembly(GetType().Assembly)
									.Where(t => t.GetInterfaces().Length > 0)
									.WithService.FirstInterface());
		}

		[Test]
		public void the_manually_registered_component_is_returned()
		{
			Assert.That(_container.Resolve<ISomeService>(), Is.TypeOf(typeof(SomeOtherServiceImplementation)));
		}
	}
}