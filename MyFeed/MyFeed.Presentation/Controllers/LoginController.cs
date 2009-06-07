using System.Web.Mvc;
using MyFeed.Core.Domain.Services;
using MyFeed.Core.Infrastructure.MVC;
using MyFeed.Presentation.Models.Login;

namespace MyFeed.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _authenticationService;

        public LoginController(IUserAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(UserCredentials userCredentials)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationService.AuthenticateUser(userCredentials.Username,
                                                            userCredentials.Password))
                {
                    return RedirectToRoute(Routes.HomePage);
                }

                ModelState.AddModelError("user-credentials",
                                         "Username or password incorrect. Please try again");
            }
            
            userCredentials.Password = string.Empty;
            return View(userCredentials);
        }
    }
}