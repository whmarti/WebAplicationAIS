<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Admin_Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >
    <div style="display:flex;align-items: center;">
       <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1 " style="flex:2 1 auto; ">
                <a class="navbar-brand" href="nCategory.aspx" style="float: left;margin-top:10px;"><i class='fa fa-download'></i> New Category</a>
              </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"> <h3 class="text-center" >Category Configuration</h3>  <p>( <%=numRows%> )</p></div>
              <div class="col-4 d-flex" style="flex:2 1 auto;" >    
                 <div style=" float:right;">
                    <asp:TextBox runat="server"  id="txtSearch"/>&nbsp 
                     <asp:LinkButton runat="server" ID="btnRun"  OnClientClick="if ( Page_ClientValidate() ) {hide();}"  OnClick="Search_Click" CssClass="greenButton"><i class="fa fa-search"></i> Search</asp:LinkButton>     
                 </div>

              </div>

      </div>
    </div>



    <div >
      <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false"  AllowPaging="True" CssClass="table table-striped"  PageSize="10" 
        OnPageIndexChanging="gv_OnPageIndexChanging"   >
       <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
           <pagerstyle backcolor="#e3f4fa"
          height="30px"
          verticalalign="Bottom"
          horizontalalign="Center"
               CssClass="cssPager" 
               />
        <Columns>         
         <asp:HyperLinkField DataTextField="name" DataNavigateUrlFields="IdCategory" DataNavigateUrlFormatString="Category.aspx?Id={0}" ItemStyle-Width = "90%" HeaderText="Name <i class='fas fa-edit'></i>" />       
         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText=""><HeaderTemplate><center><i class='fas fa-trash'></i>&nbspDelete</center></HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="Del" CommandArgument='<%#  Bind("IdCategory") %>'  Text="<i class='fas fa-eraser'></i>" runat="server" OnClientClick="return confirm('Are you sure want to delete the current record ?')" OnClick="gvRemCategories_Click" ItemStyle-Width = "1" />
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
         $('#cover-spin').hide();
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

