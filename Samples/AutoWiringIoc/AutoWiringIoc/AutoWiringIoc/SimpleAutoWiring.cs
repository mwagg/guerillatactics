using AutoWiringIoc.Services;
using Castle.MicroKernel.Registration;
using NUnit.Framework;

namespace AutoWiringIoc
{
	[TestFixture]
	public class SimpleAutoWiring : TestBase
	{
		protected override void DoRegistration()
		{
			_container.Register(AllTypes.FromAssembly(GetType().Assembly)
									.Where(t => t.GetInterfaces().Length > 0)
									.WithService.FirstInterface());
		}

		[Test]
		public void simple_wiring_picks_up_the_correct_service_implementation()
		{
			Assert.That(_container.Resolve<ISomeService>(), Is.TypeOf(typeof(SomeServiceImplementation)));
		}
	}
}