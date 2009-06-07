<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Application.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>my_feed</h2>
    <span id="current-user-info">Currently logged in as michael</span>
    <span id="logout-box"><a href="<%= Url.Action("Logout") %>">logout</a></span>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <title>my_feed</title>
</asp:Content>
