<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/application.master" Inherits="ViewPage<ProductsListingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	All Products
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>All Products</h2>
    <ul>
        <%
            Model.Products.Each(product => Html.RenderPartial("ProductSummary", product)); %>
    </ul>
</asp:Content>
