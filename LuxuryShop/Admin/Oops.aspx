<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Manager.master" AutoEventWireup="true" CodeFile="Oops.aspx.cs" Inherits="Admin_Oops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="container" style="margin-top:100px;text-align: center;" >
            <div style="min-height:150px">
                <h1>Request not processed</h1>
                <p>The information you require cannot be displayed. Please try again later or modify the request..</p>
                <p style="color: #66CCFF"><%=_mError%></p>
            </div>
    </div>
</asp:Content>

