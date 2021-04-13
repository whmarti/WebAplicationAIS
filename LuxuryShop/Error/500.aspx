<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="500.aspx.cs" Inherits="Error_500" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" style="height:100%">
    <div class="container" style="margin-top:100px;text-align: center;" >
            <div style="min-height:150px">
                <h1>500 - Internal server error</h1>
                 <p>The information you require cannot be displayed. Please try again later or modify the request..</p>
                 <p style="color: #66CCFF"><%=_mError%></p>
            </div>
    </div>
</asp:Content>

