<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="nCategory.aspx.cs" Inherits="Admin_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
    <div class="col-md-9" >
        <section id="loginForm">
               <h4>New Category</h4>
                <hr />                
                    
          
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="name" CssClass="col-md-4 control-label">Category name</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="name" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="name"
                                CssClass="text-danger" ErrorMessage="The Category name field is required." />
                        </div>
                    </div>
                   <asp:HiddenField id="IdCategory" runat="server" />

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server"   Text="Insert"  OnClientClick="if ( Page_ClientValidate() ) {hide();}"  OnClick="Update_Click" CssClass="btn btn-default" style="float: right" ID="btnSave" />
                        </div>
                    </div>
         </section>
         <div class="MessagePanelDiv">
               <asp:Panel ID="Message" runat="server" CssClass="hidepanel">
               <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
               <asp:Label ID="labelMessage" runat="server" />
               </asp:Panel>
        </div>

<div id="cover-spin"></div>
        <!-- Modal -->
        <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
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
             
          
              <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1" style="flex:2 1 auto;">  </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"><img id="imgProd" src="" alt="Denim Jeans" style="width:auto;max-width: 450px; max-height:350px" class="imgDet" > </div>
              <div class="col-4 d-flex float:right" style="flex:2 1 auto;" > </div>
            </div>

      </div>
      </div>
       <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal" >Back</button>
      </div>
    </div>
  </div>
</div>


     </div>
   </div>
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

