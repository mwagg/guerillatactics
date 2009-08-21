using Castle.Windsor;
using NUnit.Framework;

namespace AutoWiringIoc
{
	public abstract class TestBase
	{
		protected WindsorContainer _container;

		[SetUp]
		public virtual void before_each_test()
		{
			_container = new WindsorContainer();
			DoRegistration();
		}

		protected abstract void DoRegistration();
	}
}