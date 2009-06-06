<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FeedUpdateViewModel>" %>
<p><%= ViewData.Model.Content %></p>
<span>Posted <%= ViewData.Model.PublishedDateTime %></span>
