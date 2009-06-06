<%@ Control Language="C#" 
    Inherits="MyFeed.Presentation.Views.BasePartialView<PostFeedUpdateRequestModel>" %>
<%@ Import Namespace="MvcContrib.FluentHtml"%>
<%@ Import Namespace="MyFeed.Presentation.Models"%>

<form action="<%= Url.Action("Post") %> method="post">
<legend>Post a new update</legend>
<%= this.TextBox(m => m.Content)
    .Label("Your post content (max 140)") %>
    <%= this.Hidden(m => m.Username) %>
</form>