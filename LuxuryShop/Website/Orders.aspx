<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Admin_Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

      <div style="display:flex;align-items: center;">
       <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1 " style="flex:2 1 auto; ">
                  <h3 style="color:white;">Orders Placed</h3>                  
              </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"> <h3 class="text-center" >&nbsp;&nbsp;Order History</h3> <p>&nbsp;( <% if ( numRows!="0"){ %><%=numRows %><%}%> )</p> </div>
              <div class="col-4 d-flex" style="flex:2 1 auto;" >    
                 <div style=" float:right;">
                    <asp:TextBox runat="server"   id="txtSearch"/>&nbsp 
                     <asp:LinkButton runat="server" ID="btnRun" OnClick="Search_Click" OnClientClick="if ( Page_ClientValidate() ) {hide();}"  CssClass="greenButton"><i class="fa fa-search"></i> Search</asp:LinkButton>     
                 </div>
              </div>
      </div>
    </div>



    <div >
      <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" AllowPaging="True" CssClass="table table-striped "  PageSize="10" 
        OnPageIndexChanging="gv_OnPageIndexChanging"  >
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
           <pagerstyle backcolor="#e3f4fa"
          height="30px"
          verticalalign="Bottom"
          horizontalalign="Center"
               CssClass="cssPager" 
               />

        <Columns>
           
         <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center>Order</center></HeaderTemplate>
            <ItemTemplate>               
                <a href="javascript:showDetail('OrderDetail.aspx?id=<%#Eval("IdOrder") %>')" ><%#Eval("IdOrder") %></a>
            </ItemTemplate>
         </asp:TemplateField>    
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fa fa-user-circle-o'></i>&nbspClient</center></HeaderTemplate>
                <ItemTemplate >
                    <asp:Literal ID="client" runat="server" Text='<%#Eval("client") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>    
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fa fa-euro'></i>&nbspValue</center></HeaderTemplate>
                <ItemTemplate >
                    <asp:Literal ID="value" runat="server" Text='<%#"€ "+ numberFormat((float)Eval("value")).ToString() %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fa fa-calendar'></i>&nbspDate</center></HeaderTemplate>
                <ItemTemplate >
                    <asp:Literal ID="date" runat="server" Text='<%#Eval("date") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Pay Type" ItemStyle-Width = "8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header-center">
                <ItemTemplate >
                   <a href='#' style="font-size:larger;">
                      <i class="fa fa-cc-<%#Eval("payType")%> fa-2x"></i>
                   </a>
                </ItemTemplate>
            </asp:TemplateField>            

            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fa fa-envelope-o'></i>&nbspEmail</center></HeaderTemplate>
                <ItemTemplate >
                    <asp:Literal ID="value" runat="server" Text='<%#Eval("email") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fa fa-cube'></i>&nbspDetail</center></HeaderTemplate>
            <ItemTemplate>               
                <a href="javascript:showDetail('OrderDetail.aspx?id=<%#Eval("IdOrder") %>')" ><i class='fa fa-clone'></i></a>
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

    function showDetail(pUrl) {
        var detailWindow = window.open(pUrl);
    }
</script>
</asp:Content>

