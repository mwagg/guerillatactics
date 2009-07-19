<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Application.Master" 
Inherits="MyFeed.Presentation.Views.FormView<FeedUpdateViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <h2>Post an update to my_feed</h2>
    
    <% Html.BeginForm(); %>
    <fieldset>
        <%= Html.ValidationSummary() %>
        <span><%= this.TextArea(f => f.Content) %></span>
        <input type="submit" value="Post" />
    </fieldset>
    <% Html.EndForm(); %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
