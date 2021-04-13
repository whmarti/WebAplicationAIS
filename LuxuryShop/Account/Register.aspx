<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Website_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <style>
    .frame{         
        
        margin: 20px;
        
        position: relative;
    }    
    .imgRegister{  
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
    <div class="row">
    <div class="col-md-6">
        <section id="loginForm">
                <h4>Register Form</h4>
                <hr />                
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-3 control-label">User name</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="The user name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="lastName" CssClass="col-md-3 control-label">Last name</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="lastName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="lastName"
                                CssClass="text-danger" ErrorMessage="The last name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-3 control-label">Email</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="email" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                                CssClass="text-danger" ErrorMessage="The Email field is required." /><br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                         ErrorMessage="Invalid Email" ControlToValidate="email"
                                         SetFocusOnError="True"  CssClass="text-danger"
                                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    
                     <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="address" CssClass="col-md-3 control-label">Shipping address</asp:Label>                        
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="address" Width="280px" CssClass="form-control" style="max-width:480px !important; " MaxLength="15" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="address"
                                CssClass="text-danger" ErrorMessage="The Shipping address is required." />                     
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="phone" CssClass="col-md-3 control-label">Phone</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="phone" CssClass="form-control" MaxLength="15" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="phone"
                                CssClass="text-danger" ErrorMessage="The phone field is required." /><br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                         ErrorMessage="Invalid Phone, only digits, at least 10." ControlToValidate="phone"
                                         SetFocusOnError="True" CssClass="text-danger"
                                         ValidationExpression="0\d{9,}$">
                            </asp:RegularExpressionValidator>   
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-3 control-label">Password <br /><span style="font-size:smaller">(see criteria&nbsp&nbsp<a href="#" onmouseover="showDesc();"   onClick="hideDesc();" class="linkPass"><i class='fa fa-eye'></i></a>)</span></asp:Label>  
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" OnClick="showDesc();" CssClass="form-control" MaxLength="12" />
                             <span id="passDesc" class="iconDesc">Password criteria:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a href="#" onClick="hideDesc();" class="linkPass"><i class='fa fa-check'></i></a><br />
                                7  Min. Characters length<br />
                                2  Letters in Upper Case<br />
                                1  Special Character (!@#$&*)<br />
                                2  Numerals (0-9)<br />
                                3  Letters in Lower Case
                             </span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." /><br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                         ErrorMessage="Invalid password structure" ControlToValidate="Password"
                                         SetFocusOnError="True"  CssClass="text-danger"
                                         ValidationExpression="^(?=(.*[a-z]){3,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{7,}$">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-3 control-label">Re-Password</asp:Label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="PasswordRe" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordRe" CssClass="text-danger" ErrorMessage="The password confirmation field is required." /><br />
                            <asp:CompareValidator runat="server" id="cmpPasswords" controltovalidate="Password" CssClass="text-danger" controltocompare="PasswordRe" operator="Equal" type="String" errormessage="The Password should be equal than the Re-Password!" />
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server"  Text="Register" OnClientClick="if ( Page_ClientValidate() ) {hide();}else hideDesc();" OnClick="CreateUser_Click" CssClass="btn btn-default" style="float: right" ID="btnSave" />
                        </div>
                    </div>
       
        </section>
    </div>
    <div class="col-md-6 Divframe" style="height:500px">
        <section id="socialLoginForm" style="height:100%;position: inherit;" class="frame">

            <img src="../images/Login-Manager2.jpg" width="450px"class="imgRegister" /> 
        </section>
    </div>
                <div class="MessagePanelDiv">
                   <asp:Panel ID="Message" runat="server" CssClass="hidepanel">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <asp:Label ID="labelMessage" runat="server" />
                   </asp:Panel>
                 </div>
</div>
<div id="cover-spin"></div>
<script type="text/javascript">
    $(document).ready(function () {
    window.setTimeout(function () {
        $(".alert").fadeTo(1500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 5000);
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

