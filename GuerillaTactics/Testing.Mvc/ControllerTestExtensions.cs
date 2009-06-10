using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace GuerillaTactics.Testing.Mvc
{
    public static class ControllerTestExtensions
    {
        public static void CreateStubContext(this Controller controller, RouteData routeData)
        {
            var httpContextBase = MockRepository.GenerateStub<HttpContextBase>();
            httpContextBase.Stub(c => c.Request).Return(MockRepository.GenerateStub<HttpRequestBase>());
            httpContextBase.Stub(c => c.Response).Return(MockRepository.GenerateStub<HttpResponseBase>());
            httpContextBase.Stub(c => c.Session).Return(MockRepository.GenerateStub<HttpSessionStateBase>());

            controller.ControllerContext = new ControllerContext(httpContextBase, routeData, controller);
        }
    }
}