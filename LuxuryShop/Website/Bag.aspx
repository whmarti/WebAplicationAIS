<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="Bag.aspx.cs" Inherits="Website_Bag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--Banner--%>
    <div>                  
       <asp:Image  ID="Image2" runat="server"   CssClass="img-responsive center-block bannerProduct" />             
     </div>  
    
    <div class="row">       
       <div class="bannerClient"> 
            <table style="width: 100%;text-align:center;">
		       <tr>
                  <td  style="text-align: right;width:30%;">Offers: <span style="padding-top:4px;"><asp:CheckBox ID="chkOffer" runat="server"  /></span><br />
                   <%if (orderbyT != "")
                       {%>   <i class='fa fa-arrow-circle-<%= orderbyT == "asc"? "up" : "down" %>'></i><%}%>&nbsp<asp:DropDownList ID="ddlOrder" runat="server" style="" ></asp:DropDownList> 
                  </td>
			      <td style="text-align: center;width:30%;"><h2><asp:Label runat="server" ID="lblTitle">hi</asp:Label></h2></td>  
                  <td style="text-align: left;width:35%;">&nbsp&nbsp<asp:TextBox runat="server"  id="txtSearch"/>&nbsp 
                  <asp:LinkButton runat="server" ID="btnRun"  OnClientClick="if ( Page_ClientValidate() ) {hide();}"  OnClick="Search_Click" CssClass="greenButton"><i id="lens" class="fa fa-search"></i> Search</asp:LinkButton></td>	               
		       </tr>             	
            </table>        
        </div>         
    </div>

    <%--Info--%>
    <div class="row" >
      <asp:DataList ID="EmpDataList" runat="server" ItemStyle-Width="100%" width="100%">  
        
        <ItemTemplate  >  
          <table width="100%">  
       
            <tr>  
                
               <td >  
        
                <div class="card">
                  <div class="cardNode1">
                    <img id="lens" src="<%= dirPhoto.ToString()%>/<%#Eval("photo")%>" alt="Denim Jeans" style="">
                  </div>
                  <div class="cardNode2">
                      <h4 class="firstLetter" style=" color: #dcdc78;"><%#Eval("name")%> </h4>
                      <p style="font-size:smaller">Exclusive model by <%#Eval("brand")%>. (Size: <%#Eval("size")%>)</p>
                      <hr />
                      <span class='text-info mr-1 <%#Convert.ToString(Eval("discount"))=="0"? "priceSDcto" : "priceDcto"%>' ><%# (Convert.ToString(Eval("discount"))!="0" && Convert.ToInt32(Eval("monthDcto")) == (DateTime.Now.Month)  )? "€"+Eval("discount") :""%></span>

                      <span class="price mx-3 <%#(Convert.ToString(Eval("discount"))=="0" || Convert.ToInt32(Eval("monthDcto")) != (DateTime.Now.Month))? "" : "priceCDcto"%>">€<%#numberFormat(Eval("price"))%></span><span style="float:right; padding-right:4px;padding-bottom:3px;">                       
                          <button type="button" class="btn btn-light btn-sm mr-1 my-1" data-name='<%#Eval("name")%>' data-color='<%#Eval("color")%>' data-brand='<%#Eval("brand")%>' data-size='<%#Eval("size")%>' data-photo='<%= dirPhoto.ToString()%>/<%#Eval("photoD")%>' data-price='<%#Eval("price")%>' data-discount='<%#Eval("discount")%>' data-toggle="modal" data-target="#exampleModalCenter"><i class="fas fa-info-circle pr-2"></i>Details</button>
                       <button type="button" class="btn btn-info btn-sm mr-1 my-1 producto" data-idp='<%#Eval("IdProduct")%>' ><i class="fas fa-shopping-cart pr-2"></i>Add to cart</button>
                       </span>
                   </div>
                </div>
                   <br></br>
          
               </td>  
          
            </tr>  
        
          </table>          
        </ItemTemplate>       
      
      </asp:DataList>  
      
    </div>   

    <div class="row"  style="text-align:center">
      <table id="Paging" runat="server" style="display: inline-block;">  
       <tr>  
        <td>   <%if (numRows > 10 ) { %> 
            <asp:Button ID="Button1" runat="server" Font-Bold="true" Text="First"   onclick="Button1_Click" style="" />&nbsp
        
            <asp:Button ID="Button2" runat="server" Font-Bold="true" Text="Previous" onclick="Button2_Click"   
                     />&nbsp
             <asp:Button ID="Button3" runat="server" Font-Bold="true" Text="Next" onclick="Button3_Click"  />&nbsp
        
             <asp:Button ID="Button4" runat="server" Font-Bold="true" Text="Last" onclick="Button4_Click" />&nbsp&nbsp&nbsp<a href="#lens" ><i  class="fa fa-binoculars" ></i></a>&nbsp&nbsp <%=numRows%> Rows  <%  }  %>  </td>        
    </tr>  
      </table>  
    </div>
     <input type="hidden" id="nCarItems" value="<%=nCarItems%>" />
<div id="mensaje" class="alert alert-success " style="visibility: hidden"   >
  This is a success alert—check it out!
</div>


     
<div class="spinner" style="visibility: hidden"></div>
<div id="cover-spin"></div>

<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Product Detail:</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <!-- Modal Detail Product-->
          <div class="cardM">
                  <div class="cardNode1">
                    <img src="../images/nophoto.jpg" id="imgProd" alt="Denim Jeans" style="width:auto;max-width: 450px; max-height:300px" >
                  </div>
                  <div class="cardNodeM2">
                      <h4 class="firstLetter" style=" color: #dcdc78;" id="mod_name"> </h4>
                      <p style="font-size:smaller; width:30%;display:inline-block; background-color: #fcfded">Color:</p>
                      <p style="font-size:smaller; width:60%;display:inline-block; background-color: #f6f0f0" id="mod_color"  ><%#Eval("color")%></p>
                      <p style="font-size:smaller; width:30%;display:inline-block; background-color: #fcfded">Brand:</p>
                      <p style="font-size:smaller; width:60%;display:inline-block; background-color: #f6f0f0"" id="mod_brand" ><%#Eval("brand")%></p>
                       <p style="font-size:smaller; width:30%;display:inline-block; background-color: #fcfded">Size:</p>
                      <p style="font-size:smaller; width:60%;display:inline-block; background-color: #f6f0f0"" id="mod_size" ><%#Eval("size")%></p>

                      <hr />
                      <p style="font-size:smaller; width:30%;display:inline-block; background-color: #fcfded">Price:</p>
                      <p id="mod_price" style="font-size:smaller; width:60%;display:inline-block; background-color: #f6f0f0"">$<%#Eval("price")%></p>
                      <p style="font-size:smaller; width:30%;display:inline-block; background-color: #fcfded">Price Dsct.:</p>
                      <p id="mod_discount" style="font-size:smaller; width:60%;display:inline-block; background-color: #f6f0f0"">$<%#Eval("discount")%></p>
                     
                   </div>
                </div>

      </div>
       <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal" >Back</button>      </div>
        
    </div>
  </div>
</div>

<script src="../Scripts/site/Bag.js"></script>
<script src="../Scripts/site/util.js"></script>
<script type="text/javascript">
    if ($("#nCarItems").val()>0) {
        $("#prodsCar").text('<-'+$("#nCarItems").val()+'->');
    }
$('#exampleModalCenter').on('show.bs.modal', function(e) {
    
    let name = $(e.relatedTarget).data('name');  
    let brand = $(e.relatedTarget).data('brand');  
    let price = $(e.relatedTarget).data('price');  
    let discount = $(e.relatedTarget).data('discount');  
    let color = $(e.relatedTarget).data('color');  
    let size = $(e.relatedTarget).data('size');
    let img = $(e.relatedTarget).data('photo');
    $('#mod_name').text(name);
    $('#mod_brand').text("Exclusive model by "+brand)+".";    
    $('#mod_price').text("$"+price+" €");
    $('#mod_discount').text("$" + discount+" €");
    $('#mod_color').text(color);
    $('#mod_size').text(size);
    $('#mod_size').text(size);
    $('#imgProd').attr("src", img);   
    });
 
    $('.producto').on('click', function (e) {
        $('#cover-spin').show(0);
        let idP = e.target.dataset.idp;
        $(".spinner").css("visibility","visible");
        addItem(idP);       
    });

    function hide(){
        $('#cover-spin').show(0);
        return true;
    }

    //$(document).ready(function () {
    //    function toggleAlert(){
    //        $(".alert").toggleClass('in out'); 
    //        return false; 
    //    }      
        
    //});

    function showMsg(type, msg) {
        let typeMsg = type ==false ? "alert-danger" : "alert-success";
        let typeMsgOld = type ==false? "alert-success" : "alert-danger";
        $(".alert").css("visibility", "visible");
        $( "div.alert" ).removeClass( typeMsgOld ).addClass( typeMsg );
        $("div.alert").text(msg);
        $( "div.alert" ).fadeIn( 300 ).delay( 2500 ).fadeOut( 400 );      
    } 
    
</script>
</asp:Content>

