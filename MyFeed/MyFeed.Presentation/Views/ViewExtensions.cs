using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MyFeed.Presentation.Views
{
    public static class ViewExtensions
    {
        public static void RenderCurrentUserInfoPartial(this HtmlHelper html)
        {
            if (html.ViewData.ContainsKey("CurrentUsername"))
            {
                html.RenderPartial("LoggedInUserInfo");
            }
            else
            {
                html.RenderPartial("AnonymousUserInfo");
            }
        }
    }
}