<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Application.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<FeedUpdateViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <h1><%= ViewData["username"] %>'s feed</h1>
    <% Html.RenderPartial("FeedList", ViewData.Model); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>
        <%= ViewData["username"] %>'s feed</title>
</asp:Content>
