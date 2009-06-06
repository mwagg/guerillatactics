using System;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using NUnit.Framework;

namespace specs_for_DisposableAction
{
    public abstract class base_context : Specification<DisposableAction>
    {
        protected Action the_dispose_action;
        protected bool the_dispose_action_has_been_called;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_dispose_action = () => the_dispose_action_has_been_called = true;
        }

        protected override DisposableAction CreateSubject()
        {
            return new DisposableAction(the_dispose_action);
        }
    }

    [TestFixture]
    public class during_construction_with_an_initial_action : base_context
    {
        private bool initial_action_has_been_called;

        protected override DisposableAction CreateSubject()
        {
            return new DisposableAction(() => initial_action_has_been_called = true,
                () => the_dispose_action_has_been_called = true);
        }

        protected override void When()
        {
        }

        [Test]
        public void the_initial_action_should_be_called()
        {
            initial_action_has_been_called.should_be_true();
        }

        [Test]
        public void the_dispose_action_should_not_have_been_called()
        {
            the_dispose_action_has_been_called.should_be_false();
        }
    }

    [TestFixture]
    public class when_disposing : base_context
    {
        protected override void When()
        {
            Subject.Dispose();
        }

        [Test]
        public void the_dispose_action_should_be_called()
        {
            the_dispose_action_has_been_called.should_be_true();
        }

        [Test]
        public void calling_dispose_again_throws_an_exception()
        {
            typeof(ObjectDisposedException).should_be_thrown_by(() => Subject.Dispose());
        }
    }
}