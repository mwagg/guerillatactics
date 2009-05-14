<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Person>>" %>
<%@ Import Namespace="Spike.Models"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
    
    <script type="text/javascript">
        $(function(){
            $('#the-anchor').click(function(e) {
                e.preventDefault();
                
                $.getJSON('/', null, function(data){
                    for (var i in data) {
                        var person = data[i];
                        $('#people').append('<li>' + person.Name + '</li>');
                    }
                });
            });
        });
    </script>
    
    <ul id="people">
        <%
            foreach (var person in ViewData.Model)
            {
                %><li><%= person.Name %></li>
           <% } %>
    </ul>
    
    <a id="the-anchor" href="#">Click me</a>
</asp:Content>
