<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="nProduct.aspx.cs" Inherits="Admin_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
    <div class="col-md-9" >
        <section id="loginForm">
               <h4>New Product</h4>
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
                            <asp:TextBox runat="server" Text="100" ID="stock" CssClass="form-control" style="max-width:480px !important; "/>
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
                        <asp:Label runat="server" AssociatedControlID="discount" CssClass="col-md-4 control-label">Discount  (€ XX.00)</asp:Label>
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
                    </div>\
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlmonth" CssClass="col-md-4 control-label">Month Discount</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control" style="max-width:480px !important;" ></asp:DropDownList>           
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="photo" CssClass="col-md-3 control-label"  style="margin-top:12px">Small Photo</asp:Label>
                        <div class="col-md-9">                           
                           
                            <div class="fallback">  
                                <asp:Label id="lblphoto" CssClass="col-md-4 control-label"  runat="server" /> <asp:FileUpload ID="photo" runat="server" AllowMultiple="false" CssClass="file-upload" style="margin-top:12px;" />  
                            </div>  


                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="photoD" CssClass="col-md-3 control-label" style="margin-top:2px">Detail Photo</asp:Label>
                        <div class="col-md-9">                           
                           
                            <div class="fallback">  
                                <asp:Label id="lblphotoD" CssClass="col-md-4 control-label"  runat="server" /> <asp:FileUpload ID="photoD" runat="server" AllowMultiple="false" CssClass="file-upload" style="margin-top:12px;" />  
                            </div>  

                            
                        </div>
                    </div>
                  
                   

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server"  Text="Insert"  OnClientClick="if ( Page_ClientValidate() ) {hide();}"  OnClick="Update_Click" CssClass="btn btn-default" style="float: right" ID="btnSave" />
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

