<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="current-user-info">
    <span>
        Not logged in
    </span>
    <a href="<%= Url.Action("Index", "Login") %>">login</a>
</div>