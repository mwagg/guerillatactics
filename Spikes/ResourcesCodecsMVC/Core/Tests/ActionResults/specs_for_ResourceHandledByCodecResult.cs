using System;
using Core.Code.ActionResults;
using GuerillaTactics.Testing;
using NUnit.Framework;

namespace specs_for_ResourceHandledByCodecResult
{
    public abstract class ResultHandledByCodecResult_base_context : Specification<ResultHandledByCodecResult>
    {
        protected object the_resource;

        protected override ResultHandledByCodecResult CreateSubject()
        {
            return new ResultHandledByCodecResult(the_resource);
        }       
    }

    [TestFixture]
    public class in_general : ResultHandledByCodecResult_base_context
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_resource = new object();
        }

        protected override void When()
        {
        }

        [Test]
        public void the_supplied_resource_should_be_exposed()
        {
            Subject.Resource.should_be_the_same_as(the_resource);
        }

        [Test]
        public void being_executed_is_not_an_expected_operation()
        {
            typeof(InvalidOperationException).should_be_thrown_by(() => Subject.ExecuteResult(null));
        }
    }
}