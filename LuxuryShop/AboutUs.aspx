<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
    .DivAboutframe{         
        
        margin: 20px;        
        position: relative;
    }    
    .imgContact{  
        max-height: 100%;  
        max-width: 100%; 
        position: absolute;  
        top: 0;  
        bottom: 0;  
        left: 0;  
        right: 0;  
        margin: auto;
    }
</style>
    <div class="row rowInfo">
    <div class="col-md-6">
        <section id="loginForm">
          
              
                <h4>About Us</h4>
                <hr />
               
                <div class="form-group">                  
                    <div class="col-md-8 "><span class="text-justify">
                        We are a leading company in the clothing fashion accessories market, 
                        commercialized through the internet. The products we offer are mainly 
                        leather bags, suitcases, wallets, purses and some skin materials, 
                        shoes and accessories of different kinds. All of them from prestigious 
                        and recognized designers. We have physical coverage to any city in
                        New Zealand and in June 2021 through representatives, at an 
                        intercontinental level.<br /><br />
                        We have the support of regionally recognized suppliers who will provide
                        the best Portfolios of luxury items and accessories for all occasions. 
                        Managing the best brands and designs and the most optimal delivery 
                        times for your chosen item.
                    </span>
                    </div>
                </div>
                
                
                
               
       
        </section>
    </div>
    <div class="col-md-6 DivAboutframe" style="height:355px;">
        <section id="socialLoginForm" style="height:100%;position: inherit;" class="frame">

            <img src="images/AboutUs.jpg"  width="450px" class="imgContact"/> 
        </section>
    </div>
</div>
</asp:Content>

