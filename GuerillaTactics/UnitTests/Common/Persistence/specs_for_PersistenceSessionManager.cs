// ReSharper disable InconsistentNaming

using System;
using GuerillaTactics.Common.Persistence;
using GuerillaTactics.Testing;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_PersistenceSessionManager
{
    public abstract class base_context : Specification<PersistenceSessionManager>
    {
        protected ISessionFactory the_session_factory;
        protected ISession the_session_returned_from_the_session_factory;
        protected ITransaction the_transaction_returned_when_one_is_started_on_the_session;

        protected override void EstablishContext()
        {
            the_session_factory = MockRepository.GenerateStub<ISessionFactory>();
        }

        protected override PersistenceSessionManager CreateSubject()
        {
            return new PersistenceSessionManager(the_session_factory);
        }

        protected void given_the_session_factory_returns_a_session_when_asked_to_open_a_new_session()
        {
            the_session_returned_from_the_session_factory = MockRepository.GenerateStub<ISession>();
            the_session_factory.Stub(sf => sf.OpenSession()).Return(the_session_returned_from_the_session_factory);
            the_transaction_returned_when_one_is_started_on_the_session = MockRepository.GenerateStub<ITransaction>();
            the_session_returned_from_the_session_factory.Stub(s => s.BeginTransaction()).Return(the_transaction_returned_when_one_is_started_on_the_session);
        }
    }

    namespace given_the_session_factory_will_return_a_session_when_asked
    {
        public abstract class base_context : specs_for_PersistenceSessionManager.base_context
        {
            protected override void EstablishContext()
            {
                base.EstablishContext();

                given_the_session_factory_returns_a_session_when_asked_to_open_a_new_session();
            }
        }

        [TestFixture]
        public class when_the_first_request_for_the_current_session_is_made : base_context
        {
            private ISession the_session;

            protected override void When()
            {
                the_session = Subject.GetCurrentSession();
            }

            [Test]
            public void a_new_session_should_be_opened_with_the_session_factory()
            {
                the_session_factory.AssertWasCalled(sf => sf.OpenSession());
            }

            [Test]
            public void the_session_opened_with_the_session_factory_should_be_returned()
            {
                the_session.should_be_the_same_as(the_session_returned_from_the_session_factory);
            }

            [Test]
            public void a_transaction_should_be_started_on_the_current_session()
            {
                the_session.AssertWasCalled(s => s.BeginTransaction());
            }
        }

        [TestFixture]
        public class when_the_current_session_is_completed : base_context
        {
            protected override bool RethrowExceptionsThrownDuringWhen
            {
                get
                {
                    return false;
                }
            }

            protected override void When()
            {
                Subject.CompleteCurrentSession();
            }

            [Test]
            public void no_exceptions_should_be_thrown()
            {
                ExceptionThrownDuringWhen.should_be_null();
            }
        }

        namespace and_a_first_request_for_the_current_session_has_already_been_made
        {
            public abstract class base_context : given_the_session_factory_will_return_a_session_when_asked.base_context
            {
                protected override void When()
                {
                    Subject.GetCurrentSession();
                }
            }

            [TestFixture]
            public class when_a_second_request_for_the_current_session_is_made : base_context
            {
                private ISession the_session;

                protected override void When()
                {
                    base.When();

                    the_session = Subject.GetCurrentSession();
                }

                [Test]
                public void no_additional_session_should_be_opened_with_the_session_factory()
                {
                    the_session_factory.AssertWasCalled(sf => sf.OpenSession(), options => options.Repeat.Once());
                }

                [Test]
                public void the_same_session_returned_in_the_first_request_shold_be_returned_in_the_second()
                {
                    the_session.should_be_the_same_as(the_session_returned_from_the_session_factory);
                }
            }

            [TestFixture]
            public class when_the_current_session_is_completed : base_context
            {
                protected override void When()
                {
                    base.When();

                    Subject.CompleteCurrentSession();
                }

                [Test]
                public void the_current_sessions_transaction_should_be_committed()
                {
                    the_transaction_returned_when_one_is_started_on_the_session.AssertWasCalled(t => t.Commit());
                }

                [Test]
                public void the_current_session_should_be_closed()
                {
                    the_session_returned_from_the_session_factory.AssertWasCalled(s => s.Close());
                }
            }

            namespace and_a_request_is_made_to_rollback_the_transaction_on_complete
            {
                public abstract class base_context : 
                    and_a_first_request_for_the_current_session_has_already_been_made.base_context
                {
                    protected override void When()
                    {
                        base.When();

                        Subject.RollbackOnComplete();
                    }
                }

                [TestFixture]
                public class when_the_current_session_is_completed : base_context
                {
                    protected override void When()
                    {
                        base.When();

                        Subject.CompleteCurrentSession();
                    }

                    [Test]
                    public void the_current_transaction_should_not_be_commited()
                    {
                        the_transaction_returned_when_one_is_started_on_the_session.AssertWasNotCalled(
                            t => t.Commit());
                    }

                    [Test]
                    public void the_current_transaction_should_be_rolled_back()
                    {
                        the_transaction_returned_when_one_is_started_on_the_session.AssertWasCalled(
                            t => t.Rollback());
                    }
                }
            }

            namespace and_the_current_session_has_been_completed
            {
                public abstract class base_context : 
                    and_a_first_request_for_the_current_session_has_already_been_made.base_context
                {
                    protected override void When()
                    {
                        base.When();

                        Subject.CompleteCurrentSession();
                    }
                }

                [TestFixture]
                public class when_an_attempt_is_made_to_complete_the_current_session_again : base_context
                {
                    protected override void When()
                    {
                        base.When();

                        Subject.CompleteCurrentSession();
                    }

                    protected override bool RethrowExceptionsThrownDuringWhen
                    {
                        get
                        {
                            return false;
                        }
                    }

                    [Test]
                    public void an_exception_should_be_thrown()
                    {
                        ExceptionThrownDuringWhen.should_be_instance_of_type(typeof(InvalidOperationException));
                    }
                }
            }
        }
    }
}