<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <%--Banner--%>
    <div >
           <img src="../images/IndexManager.jpg" class="img-responsive center-block bannerMain"  />
     </div>
    <h2 class="text-center" style="color:darkslateblue"><img src="../images/IconLogo.JPG" alt="" height="20px"/>Luxury Management Platform</h2>    
    <%--Info--%>
    <div class="row">
        <div class="col-md-4 " >
            <h3>Inventory management</h3>           
                <ul>               
                   <li>Configure each of the lines to offer to the end customer. </li>
                   <li>Update the status of the product warehouse.</li>
                   <li> Highlight the image of each of the products and their promotions for the month.</li>
                </ul>
            
            <p style="text-align:center">
                    
            </p>
           
        </div>
       
        <div class="col-md-4 ">
            <div style="height:237px" class="imagen4"> </div>
            
           
        </div>
        <div class="col-md-4 ">
            <h3>Users and Purchase Orders</h3>
            <p>
                Manage the information of each of the users, their purchase orders and their status. Monitor and configure employee information.
            </p>
            <p style="text-align:center">
                
            </p>
            
        </div>
    </div>
</asp:Content>

