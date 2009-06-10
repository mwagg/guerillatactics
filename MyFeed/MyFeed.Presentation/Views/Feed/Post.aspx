<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Application.Master" Inherits="MyFeed.Presentation.Views.FormView<PostFeedUpdateRequestModel>" %>

<%@ Import Namespace="MvcContrib.FluentHtml" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <h1>Post an update to my feed</h1>
    <% using (Html.BeginForm()) { %>
        <legend>Post a new feed update</legend>
        <%= Html.ValidationSummary() %>
        <%= this.Hidden(m => m.Username) %>
        <span><%= this.TextArea(m => m.Content).Label("Your update (max 140)") %></span>
        <%= this.SubmitButton("Post update") %>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Post an update to my feed</title>
</asp:Content>
