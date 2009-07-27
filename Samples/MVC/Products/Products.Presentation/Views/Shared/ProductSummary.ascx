<%@ Control Language="C#" Inherits="ViewUserControl<Product>" %>

<li>
    <span>Id: <%= Model.Id %></span>
    <span>Name: <%= Model.Name %></span>
    <span>Description: <%= Model.Description %></span>
    <span>Price: <%= Model.Price %></span>
    <span>Department: <%= Model.Department.Name %></span>
</li>

