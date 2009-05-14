using System;
using System.Web.Mvc;

namespace Core.Code.ActionResults
{
    public class ResultHandledByCodecResult : ActionResult
    {
        private readonly object _resource;

        public ResultHandledByCodecResult(object resource)
        {
            _resource = resource;
        }

        public object Resource
        {
            get { return _resource; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            throw new InvalidOperationException("A ResultHandledByCodecResult is has been asked to Execute."
                                    + "This is not intended to happen.");
        }
    }
}