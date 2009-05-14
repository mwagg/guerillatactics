using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Code.ActionResults;
using Core.Code.Filters;
using Core.Code.ResponseCodecs;
using GuerillaTactics.Testing;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_ResponseCanBeHandledByAttribute
{
    public abstract class MyResponseCodec : IResponseCodec
    {
        public abstract bool CanExecute(string[] acceptTypes);
        public abstract ActionResult Execute(RouteData routeData, ResultHandledByCodecResult result);
    }

    public abstract class ResponseCanBeHandledByAttribute_base_context : Specification<ResponseCanBeHandledByAttribute>
    {
        protected Type the_codec_type;
        protected ActionResult the_original_result;

        protected override ResponseCanBeHandledByAttribute CreateSubject()
        {
            return new ResponseCanBeHandledByAttribute(the_codec_type);
        }
    }

    namespace given_the_codec_type_does_not_derive_from_IResponseCodec
    {
        public abstract class base_context : ResponseCanBeHandledByAttribute_base_context
        {
            protected override void EstablishContext()
            {
                base.EstablishContext();

                the_codec_type = typeof(string);
            }
        }

        [TestFixture]
        public class when_creating_a_new_instance : base_context
        {
            private Exception the_exception_thrown_during_construction;

            protected override ResponseCanBeHandledByAttribute CreateSubject()
            {
                try
                {
                    return base.CreateSubject();
                }
                catch (Exception ex)
                {
                    the_exception_thrown_during_construction = ex;
                    return null;
                }
            }

            protected override void When()
            {
            }

            [Test]
            public void then_an_ArgumentException_should_be_thrown()
            {
                the_exception_thrown_during_construction.should_be_instance_of_type(typeof(ArgumentException));
            }
        }
    }

    namespace given_the_codec_type_derives_from_IResponseCodec
    {
        public abstract class base_context : ResponseCanBeHandledByAttribute_base_context
        {
            protected ActionExecutedContext the_executed_context;
            protected IServiceLocator service_locator;

            protected override void EstablishContext()
            {
                base.EstablishContext();

                service_locator = MockRepository.GenerateStub<IServiceLocator>();
                ServiceLocator.SetLocatorProvider(() => service_locator);

                the_codec_type = typeof (MyResponseCodec);
                the_executed_context = new ActionExecutedContext
                {
                    Result = the_original_result,
                };
            }
        }

        namespace and_the_original_result_is_not_a_ResourceHandledByCodecResult
        {
            public abstract class base_context : given_the_codec_type_derives_from_IResponseCodec.base_context
            {
                protected override void EstablishContext()
                {
                    the_original_result = new ViewResult();

                    base.EstablishContext();
                }
            }

            [TestFixture]
            public class when_processing_the_request_after_the_action_has_executed : base_context
            {
                protected override void When()
                {
                    Subject.OnActionExecuted(the_executed_context);
                }

                [Test]
                public void the_service_locator_should_not_be_asked_to_resolve_a_codec()
                {
                    service_locator.AssertWasNotCalled(sl => sl.GetInstance(Arg<Type>.Is.Anything));
                }

                [Test]
                public void the_original_result_should_not_be_changed()
                {
                    the_executed_context.Result.should_be_the_same_as(the_original_result);
                }
            }
        }

        namespace and_the_original_result_is_a_ResourceHandledByCodecResult
        {
            public abstract class base_context : given_the_codec_type_derives_from_IResponseCodec.base_context
            {
                protected MyResponseCodec the_codec;
                private HttpContextBase the_http_context;
                protected string[] the_accept_types;
                private HttpRequestBase the_http_request;

                protected override void EstablishContext()
                {
                    the_original_result = new ResultHandledByCodecResult(new object());

                    base.EstablishContext();

                    the_accept_types = new[] { "text/blah" };
                    the_http_context = MockRepository.GenerateStub<HttpContextBase>();
                    the_http_request = MockRepository.GenerateStub<HttpRequestBase>();
                    the_http_request.Stub(r => r.AcceptTypes).Return(the_accept_types);
                    the_http_context.Stub(c => c.Request).Return(the_http_request);

                    the_executed_context.RequestContext = new RequestContext(the_http_context, new RouteData());

                    the_codec = MockRepository.GenerateStub<MyResponseCodec>();
                    service_locator.Stub(sl => sl.GetInstance(typeof (MyResponseCodec))).Return(the_codec);
                }
            }

            [TestFixture]
            public class when_processing_the_request_after_the_action_has_executed : base_context
            {
                protected override void When()
                {
                    Subject.OnActionExecuted(the_executed_context);
                }

                [Test]
                public void an_instance_of_the_codec_should_be_resolved_through_the_ServiceLocator()
                {
                    service_locator.AssertWasCalled(sl => sl.GetInstance(typeof(MyResponseCodec)));
                }

                [Test]
                public void the_codec_should_be_asked_if_it_can_execute()
                {
                    the_codec.AssertWasCalled(c => c.CanExecute(the_accept_types));
                }
            }

            namespace and_the_codec_cannot_execute
            {
                public abstract class base_context : and_the_original_result_is_a_ResourceHandledByCodecResult.base_context
                {
                    protected override void EstablishContext()
                    {
                        base.EstablishContext();

                        the_codec.Stub(c => c.CanExecute(the_accept_types)).Return(false);
                    }
                }

                [TestFixture]
                public class when_processing_the_request_after_the_action_has_executed : base_context
                {
                    protected override void When()
                    {
                        Subject.OnActionExecuted(the_executed_context);
                    }

                    [Test]
                    public void the_original_result_should_not_be_changed()
                    {
                        the_executed_context.Result.should_be_the_same_as(the_original_result);
                    }
                }
            }

            namespace and_the_codec_can_be_executed
            {
                public abstract class base_context : and_the_original_result_is_a_ResourceHandledByCodecResult.base_context
                {
                    protected ActionResult the_codecs_result;
                    private RouteData the_route_data;

                    protected override void EstablishContext()
                    {
                        base.EstablishContext();

                        the_route_data = new RouteData();
                        the_codecs_result = new ViewResult();
                        the_codec.Stub(c => c.CanExecute(the_accept_types)).Return(true);
                        the_codec.Stub(c => c.Execute(the_route_data, 
                            (ResultHandledByCodecResult)the_executed_context.Result))
                            .Return(the_codecs_result);
                    }
                }

                [TestFixture]
                public class when_processing_the_request_after_the_action_has_executed : base_context
                {
                    protected override void When()
                    {
                        Subject.OnActionExecuted(the_executed_context);
                    }

                    [Test]
                    public void the_result_should_be_replaced_by_the_result_from_the_codec()
                    {
                        the_executed_context.Result.should_be_the_same_as(the_codecs_result);
                    }
                }
            }
        }
    }
}