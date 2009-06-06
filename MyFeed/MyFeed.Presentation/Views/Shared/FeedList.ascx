<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<FeedUpdateViewModel>>" %>
<%@ Import Namespace="MyFeed.Core.Infrastructure"%>
<ul>
    <% Html.ForEach(ViewData.Model)
        .Li(fu => Html.RenderPartial("FeedUpdate", fu)); %>
</ul>
