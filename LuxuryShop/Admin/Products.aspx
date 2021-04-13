<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Admin_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >
    <div style="display:flex;align-items: center;">
       <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1 " style="flex:2 1 auto; ">
                <a class="navbar-brand" href="nProduct.aspx" style="float: left;margin-top:10px;"><i class='fa fa-download'></i> New Product</a>
                  
              </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"> <h3 class="text-center" >Product Configuration</h3> <p>( <%=numRows%> )</p> </div>
              <div class="col-4 d-flex" style="flex:2 1 auto;" >    
                 <div style=" float:right;">                   
                    <div>
                        <asp:Calendar ID="Calendar1" runat="server" Visible="false" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    </div>
                    <asp:TextBox ID="txtDate" ReadOnly="true" runat="server"></asp:TextBox>                    
                      <asp:LinkButton runat="server" ID="LinkButton1" OnClick="Calendar_Click" OnClientClick="if ( Page_ClientValidate() ) {hide();}"  CssClass="greenButton"><i class="fa fa-calendar"></i></asp:LinkButton>     
                      <asp:TextBox runat="server"  id="txtSearch"/>&nbsp 
                     <asp:LinkButton runat="server" ID="btnRun" OnClick="Search_Click" OnClientClick="if ( Page_ClientValidate() ) {hide();}"  CssClass="greenButton"><i class="fa fa-search"></i> Search</asp:LinkButton>     
                 </div>
              </div>
      </div>
    </div>



    <div class="table">
      <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" AllowPaging="True" CssClass="table table-striped "  PageSize="10" 
        OnPageIndexChanging="gv_OnPageIndexChanging"  >
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
           <pagerstyle backcolor="#e3f4fa"
          height="30px"
          verticalalign="Bottom"
          horizontalalign="Center"
               CssClass="cssPager" 
               />

        <Columns>
            
    <asp:HyperLinkField DataTextField="name" DataNavigateUrlFields="IdProduct" DataNavigateUrlFormatString="Product.aspx?Id={0}"
             ItemStyle-Width = "30%" HeaderText="Name" />
            <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width = "10%" />
            <asp:BoundField DataField="brand" HeaderText="Brand" ItemStyle-Width = "20%" />
            <asp:BoundField DataField="size" HeaderText="Size" ItemStyle-Width = "10%" />

            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center>Promo.</center></HeaderTemplate>
                <ItemTemplate>  
                    <%#  ((float)Convert.ToDouble(Eval("discount")) > 0)? "<i class='fa fa-diamond'></i>":"No" %>
                </ItemTemplate>
           </asp:TemplateField>

            <asp:BoundField DataField="photo" HeaderText="Photo" ItemStyle-Width = "25%" />
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fas fa-trash'></i>&nbspDelete</center></HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="Del" CommandArgument='<%#  Bind("IdProduct") %>'   Text="<i class='fas fa-eraser'></i>" runat="server" OnClientClick="return confirm('Are you sure want to delete the current record ?')" OnClick="gvRemProducts_Click" ItemStyle-Width = "1" />
            </ItemTemplate>
         </asp:TemplateField>
       
    </Columns>
    </asp:GridView>
        
    </div>

<div class="MessagePanelDiv">
    <asp:Panel ID="Message" runat="server" CssClass="hidepanel">
     <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
     <asp:Label ID="labelMessage" runat="server" />
    </asp:Panel>         
</div>
<div id="cover-spin"></div>
<script type="text/javascript">
     $(document).ready(function () {
    window.setTimeout(function () {
        $(".alert").fadeTo(1300, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 1900);
    });
    function hide(){
        $('#cover-spin').show(0);
        return true;
    }

</script>
</asp:Content>

