<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>
    <% Html.BeginForm(); %>
        <fieldset>
            <legend>Please enter your credentials</legend>
            <ol>
                <li><label for="username">Username</label><input type="text" name="username" /></li>
                <li><label for="password">Password</label><input type="password" name="password" /></li>
                <li><input type="submit" value="Login" /></li>
            </ol>
        </fieldset>
    <% Html.EndForm(); %>

</asp:Content>
