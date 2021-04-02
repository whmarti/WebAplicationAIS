<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Admin_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
    <div class="col-md-9" >
        <section id="loginForm">
               <h4>Update Product</h4>
                <hr />                
                    
                    <div class="form-group">
                        <asp:HiddenField id="IdProduct" runat="server"  />
                        <asp:Label runat="server" AssociatedControlID="ddlIdCategory" CssClass="col-md-4 control-label">Category</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlIdCategory" runat="server" CssClass="form-control" style="max-width:480px !important;" ></asp:DropDownList>                             
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlIdCategory" CssClass="text-danger" ErrorMessage="The Category field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="name" CssClass="col-md-4 control-label">Product name</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="name" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="name"
                                CssClass="text-danger" ErrorMessage="The Product name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="stock" CssClass="col-md-4 control-label">Stock</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="stock" CssClass="form-control" style="max-width:480px !important; "/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="stock"
                                CssClass="text-danger" ErrorMessage="The Stock field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="color" CssClass="col-md-4 control-label">Color</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="color" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="color"
                                CssClass="text-danger" ErrorMessage="The Color field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="brand" CssClass="col-md-4 control-label">Brand</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="brand" CssClass="form-control" style="max-width:480px !important; "/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="brand"
                                CssClass="text-danger" ErrorMessage="The Brand field is required." />
                        </div>
                    </div>

                   <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="size" CssClass="col-md-4 control-label">Size</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="size" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="size"
                                CssClass="text-danger" ErrorMessage="The Size name field is required." />
                        </div>
                    </div>
                      <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="price" CssClass="col-md-4 control-label">Price (€ XX.00)</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="price" CssClass="form-control" style="max-width:480px !important; "/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="price"
                                CssClass="text-danger" ErrorMessage="The Price field is required." /><br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                         ErrorMessage="Invalid Value." ControlToValidate="price"
                                         SetFocusOnError="True" CssClass="text-danger"
                                         ValidationExpression="(^[1-9]\d*)+(\.\d{1,2})?$">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="discount" CssClass="col-md-4 control-label">Discount (€ XX.00)</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="discount" CssClass="form-control" style="max-width:480px !important; "/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="discount"
                                CssClass="text-danger" ErrorMessage="The Discount field is required." />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                         ErrorMessage="Invalid Value." ControlToValidate="discount"
                                         SetFocusOnError="True" CssClass="text-danger"
                                         ValidationExpression="^\d+(\.\d{1,2})?$">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlmonth" CssClass="col-md-4 control-label">Month Discount</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control" style="max-width:480px !important;" ></asp:DropDownList>           
                        </div>
                    </div>
            

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="photo" CssClass="col-md-3 control-label"  style="margin-top:12px;">Small Photo  <button type="button" class="btn btn-light btn-sm mr-1 my-1" data-toggle="modal" data-name="photo" data-target="#exampleModalCenter"><i class="fa fa-camera-retro pr-2"></i> Image</button></asp:Label>
                        <div class="col-md-9">    
                            <div class="fallback">  
                                <asp:Label id="lblphoto"  CssClass="col-md-4 control-label" runat="server" style="font-size:11px; margin-top:3px;"/> <asp:FileUpload ID="photo" runat="server" AllowMultiple="false" CssClass="file-upload" style="margin-top:12px;" />                                 
                            </div>  
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server"  AssociatedControlID="photoD" CssClass="col-md-3 control-label" style="margin-top:2px;">Detail Photo<button type="button" class="btn btn-light btn-sm mr-1 my-1" data-toggle="modal" data-name="photoD" data-target="#exampleModalCenter"><i class="fa fa-camera-retro pr-2"></i> Image</button></asp:Label>
                        <div class="col-md-9">                           
                           
                            <div class="fallback">  
                                <asp:Label id="lblphotoD" style="font-size:11px; margin-top:3px;" CssClass="col-md-4 control-label"  runat="server" /> <asp:FileUpload ID="photoD" runat="server" AllowMultiple="false" CssClass="file-upload" style="margin-top:12px;" />  
                            </div>  

                            
                        </div>
                    </div>
                  
                   

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server"   Text="Update" OnClientClick="if ( Page_ClientValidate() ) {hide();}" OnClick="Update_Click" CssClass="btn btn-default" style="float: right" ID="btnSave" />
                        </div>
                    </div>
       <asp:HiddenField id="img" runat="server" />
       <asp:HiddenField id="imgD" runat="server" />
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


    $('#exampleModalCenter').on('show.bs.modal', function(e) {
        let img = "";
        if($(e.relatedTarget).data('name')=="photo")
          img = $("#" + '<%= img.ClientID %>').val();
        else
          img = $("#" + '<%= imgD.ClientID %>').val();

        $('#imgProd').attr("src", "../images/Products/" + img);    
        $('#exampleModalLongTitle').text("Image: "+img);
   
    });

    function hide(){
        $('#cover-spin').show(0);
        return true;
    }
</script>
</asp:Content>

