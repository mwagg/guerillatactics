<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Application.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <h2>my_feed</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>my_feed</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="side" runat="server">
    <% Html.RenderCurrentUserInfoPartial(); %>
</asp:Content>
