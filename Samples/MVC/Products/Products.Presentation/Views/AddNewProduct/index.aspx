<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/application.master" Inherits="FormView<Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add a new product
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add a new product</h2>
    <% Html.BeginForm(); %>
    <fieldset>
        <%= this %>
    </fieldset>
    <% Html.EndForm(); %>
</asp:Content>
