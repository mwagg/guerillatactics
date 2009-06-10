<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Application.Master" 
Inherits="MyFeed.Presentation.Views.FormView<UserCredentials>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <h2>Login to my_feed</h2>
    <% Html.BeginForm(); %>
        <legend>Please enter your credentials</legend>
        <%= Html.ValidationSummary() %>
        <span><%= this.TextBox(uc => uc.Username).AutoLabel() %></span>
        <span><%= this.Password(uc => uc.Password).AutoLabel() %></span>
        <span><%= this.SubmitButton("Login") %></span>
    <% Html.EndForm(); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
