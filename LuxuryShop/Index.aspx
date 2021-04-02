<%@ Page Title="" Language="C#" MasterPageFile="~/Client.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <%--Banner--%>
    <div >
           <img src="images/Portada3.jpg" class="img-responsive center-block bannerMain"  />
     </div>
    <%--Info--%>
    <div class="row">
        <div class="col-md-4 " >
            <h2>Bags and purses</h2>
            <p>
                There are certain brands of women's wallets that have been differentiated from the rest by different factors.

This product is the fashion accessory par excellence and that is known by exclusive designers who are always creating what a woman dreams of having. </p>
            <p style="text-align:center">
                <a class="btn btn-default" href="Website/bag.aspx?s=b">Look more &raquo;</a>
            </p>
            <div class="imagen1" style="height:220px"> </div>
        </div>
       
        <div class="col-md-4 ">
            <div style="height:237px" class="imagen2"> </div>
            
            <div  style="height:220px"><h2>Shoes</h2>
            <p >
                One of the most important accessories in every woman's wardrobe are shoes. We never have enough and we always need one more pair for different occasions and outfit we wear. The shoes allow us to complete our look and give it that final touch.
            </p>
            <p style="text-align:center">
                <a class="btn btn-default" href="Website/bag.aspx?s=s">Look more &raquo;</a>
            </p> </div>
        </div>
        <div class="col-md-4 ">
            <h2>Accessories</h2>
            <p>
                Accessories are the pieces that are responsible for complementing and enhancing our wardrobe. Every outfit that makes you look fabulous must have its accessories, these can be a watch, earrings, chains, bracelets, among others.
            </p>
            <p style="text-align:center">
                <a class="btn btn-default" href="Website/bag.aspx?s=a">Look more &raquo;</a>
            </p>
            <div class="imagen3" style="height:220px"> </div>
        </div>
    </div>

   
</asp:Content>

