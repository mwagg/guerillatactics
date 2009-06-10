<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="current-user-info">
    <span>Currently logged in as
        <%= ViewData["CurrentUsername"] as string%>
    </span>
    <a href="<%= Url.Action(Routes.Logout) %>">logout</a>
</div>
