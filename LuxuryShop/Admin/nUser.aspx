<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="nUser.aspx.cs" Inherits="Admin_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
    <div class="col-md-9" >
        <section id="loginForm">
               <h4><asp:Label runat="server" id="lblTitUser" CssClass="col-md-4 control-label"></asp:Label></h4>
            <br />
                <hr />                             
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="name" CssClass="col-md-4 control-label">User name</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="name" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="name"
                                CssClass="text-danger" ErrorMessage="The User name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="lastName" CssClass="col-md-4 control-label">Last name</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="lastName" CssClass="form-control" style="max-width:480px !important; "/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="lastName"
                                CssClass="text-danger" ErrorMessage="The Last name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-4 control-label">Email</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="email" CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                                CssClass="text-danger" ErrorMessage="The Email name field is required." /><br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                         ErrorMessage="Invalid Email" ControlToValidate="email"
                                         SetFocusOnError="True" CssClass="text-danger"
                                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="phone" CssClass="col-md-4 control-label">Phone</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="phone" CssClass="form-control" style="max-width:480px !important; " MaxLength="15" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="phone"
                                CssClass="text-danger" ErrorMessage="The Phone field is required." /> <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                         ErrorMessage="Invalid Phone, only digits, at least 10." ControlToValidate="phone"
                                         SetFocusOnError="True" CssClass="text-danger"
                                         ValidationExpression="0\d{9,}$">
                            </asp:RegularExpressionValidator>   
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server"  AssociatedControlID="address" CssClass="col-md-4 control-label">Shipping address</asp:Label>                        
                        <div class="col-md-8">
                            <asp:TextBox runat="server"  ID="address" CssClass="form-control" style="max-width:480px !important; " MaxLength="15" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="address"
                                CssClass="text-danger" ErrorMessage="The Shipping address is required." />                     
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlState" CssClass="col-md-4 control-label">State</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" style="max-width:480px !important;" ></asp:DropDownList>                             
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlState" CssClass="text-danger" ErrorMessage="The State field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="pass" CssClass="col-md-4 control-label">Password  &nbsp&nbsp&nbsp<span style="font-size:smaller">(see criteria&nbsp&nbsp<a href="#" onmouseover="showDesc();"   onClick="hideDesc();" class="linkPass"><i class='fa fa-eye'></i></a>)</span></asp:Label>                       
                        <div class="col-md-8">
                              
                            <asp:TextBox runat="server" ID="pass"  onClick="showDesc();" TextMode="Password" CssClass="form-control" style="max-width:480px !important; " MaxLength="12" />
                             <span id="passDesc" class="iconDesc">Password criteria:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a href="#" onClick="hideDesc();" class="linkPass"><i class='fa fa-check'></i></a><br />
                                7  Min. Characters length<br />
                                2  Letters in Upper Case<br />
                                1  Special Character (!@#$&*)<br />
                                2  Numerals (0-9)<br />
                                3  Letters in Lower Case
                             </span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="pass"
                                CssClass="text-danger" ErrorMessage="The Pass field is required." /><br />
                           
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                         ErrorMessage="Invalid password structure" ControlToValidate="pass"
                                         SetFocusOnError="True"  CssClass="text-danger"
                                         ValidationExpression="^(?=(.*[a-z]){3,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{7,}$">
                            </asp:RegularExpressionValidator>                           
                        </div>
                    </div>     
                    
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="pass" CssClass="col-md-4 control-label">Re-Password</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="passwordRe" TextMode="Password"  CssClass="form-control" style="max-width:480px !important; " />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordRe" CssClass="text-danger" ErrorMessage="The password confirmation field is required." /><br />
                            <asp:CompareValidator runat="server" id="cmpPasswords" controltovalidate="pass" CssClass="text-danger" controltocompare="passwordRe" operator="Equal" type="String" errormessage="The Password should be equal than the Re-Password!" />
                        </div>
                    </div>
                                    
                   

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server"  Text="|< Back" OnClientClick="history.go(-1); return false;"   CssClass="btn btn-warning"  ID="Back" />
                            <asp:LinkButton runat="server"  Text="Insert" OnClientClick="if ( Page_ClientValidate() ) {hide();}"   OnClick="Update_Click" CssClass="btn btn-default" style="float: right" ID="btnSave"><i class='fa fa-save'></i> Insert</asp:LinkButton>
                        </div>
                    </div>
       <asp:HiddenField id="typeUser" runat="server" />
       <asp:HiddenField id="IdUser" runat="server" />
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
        <h5 class="modal-title" id="exampleModalLongTitle">User Detail:</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <!-- Modal Detail User-->
          <div class="cardM">
             
          
              <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1" style="flex:2 1 auto;">  </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"><img id="imgProd" src="" alt="Denim Jeans" style="width:auto;max-width: 450px; max-height:350px" class="imgDet" > </div>
              <div class="col-4 d-flex float:right" style="flex:2 1 auto;" > </div>
            </div>

      </div>
      </div>
       <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal" id="btnSave1">Back</button>
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
    }, 2000);
    });


    $('#exampleModalCenter').on('show.bs.modal', function(e) {
        let img = "";      

        $('#imgProd').attr("src", "../images/Users/" + img);    
        $('#exampleModalLongTitle').text("Image: "+img);
   
    });

    function hide(){
        $('#cover-spin').show(0);
        return true;
    }

    function showDesc(){
        $('#passDesc').show();
        return true;
    }
    function hideDesc(){
        $('#passDesc').hide();
        return true;
    }
</script>
</asp:Content>

