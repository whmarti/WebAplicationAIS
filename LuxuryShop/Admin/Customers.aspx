<%@ Page Title="" Language="C#" MasterPageFile="Manager.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Admin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >
    <div style="display:flex;align-items: center;">
       <div class="row container  " style="align-items: center;display: flex;">
              <div class="col-3 pt-1 " style="flex:2 1 auto; ">
                <a class="navbar-brand" href="nUser.aspx" style="float: left;margin-top:10px;"><i class='fa fa-download'></i>&nbsp;New Customer</a><br />
                <a class="navbar-brand" href="#exampleModalCenter" data-toggle="modal" data-target="#exampleModalCenter"><i class='fa fa-random'></i>&nbsp;Top <%=topVisitors%> more visitants</a>
              </div>
              <div class="col-5 text-center" style="flex:2 1 auto;"> <h3 class="text-center" >Customer Configuration</h3>   <p>( <%=numRows%> )</p> </div>
              <div class="col-4 d-flex" style="flex:2 1 auto;" >    
                 <div style=" float:right;">
                    <asp:TextBox runat="server"  id="txtSearch"/>&nbsp 
                     <asp:LinkButton runat="server" ID="btnRun"  OnClientClick="if ( Page_ClientValidate() ) {hide();}" OnClick="Search_Click" CssClass="greenButton"><i class="fa fa-search"></i> Search</asp:LinkButton>     
                 </div>
                 
              </div>

      </div>
    </div>
       
    <div >
        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowPaging="True" CssClass="table table-striped"  PageSize="10" 
        OnPageIndexChanging="gv_OnPageIndexChanging"  >
          <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
          <pagerstyle backcolor="#e3f4fa"
          height="30px"
          verticalalign="Bottom"
          horizontalalign="Center"
               CssClass="cssPager" 
               />
        <Columns>
            
    <asp:HyperLinkField DataTextField="name" DataNavigateUrlFields="IdUser" DataNavigateUrlFormatString="User.aspx?Id={0}"
             ItemStyle-Width = "30%" HeaderText="Name" />
      <asp:BoundField DataField="lastName" HeaderText="Last Name" ItemStyle-Width = "28%" />
      <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-Width = "28%" />      
      <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width = "5%" />
      <asp:TemplateField ItemStyle-HorizontalAlign="Center"><HeaderTemplate><center><i class='fas fa-trash'></i>&nbspDelete</center></HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="Del" CommandArgument='<%#  Bind("IdUser") %>'  Text="<i class='fas fa-eraser'></i>" runat="server" OnClientClick="return confirm('Are you sure want to delete the current record ?')" OnClick="gvRemUsers_Click" ItemStyle-Width = "1" />
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
<input type="hidden" id="topVisitors" value="<%=topVisitors%>" />
<div id="mensaje" class="alert alert-success " style="visibility: hidden"   >
  This is a success alert—check it out!
</div>

<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Most frequent visitors:</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <!-- Modal Detail Product-->
          <div class="cardM">
                 
                  <div class="cardNodeM2">
                      <div>
                          <input type="text" id="txtFecFrom" value="YYYY-MM-DD"  ondblclick="this.value = '';"/> - <input type="text" id="txtFecTo" value="YYYY-MM-DD"  ondblclick="this.value = '';" /> <a href="javaScript: SearchVisitors()" style="font-size:large"><i class='fa fa-stack-overflow'></i>&nbsp;<i class='fa fa-arrow-circle-o-up'></i></a></div>
                      <table class="table table-striped" id="tblVisitors">
                          <thead>
                            <tr >
                              <th scope="col" style="width: 10%; text-align: center;">#</th>
                              <th scope="col" style="width: 25%; text-align: center;">First</th>
                              <th scope="col" style="width: 25%; text-align: center;">Last</th>
                              <th scope="col" style="width: 25%; text-align: center;">Email</th>
                              <th scope="col" style="width: 15%; text-align: center;">Visits</th>
                            </tr>
                          </thead>
                          <tbody id="tblBodyVisitors">
                              <% int i = 1;
                                  foreach (Entity.User user in lst_TopVisitors) { %>
                            <tr>
                              <th scope="row" style="text-align: center;"><%= i %></th>
                              <td style="text-align: center;"><%= user.name %></td>
                              <td style="text-align: center;"><%= user.lastName %></td>
                              <td style="text-align: center;"><%= user.email %></td>
                              <td style="text-align: center;"><%= user.address %></td>
                            </tr>
                              <%i++;
                                  } %>                            
                          </tbody>
                      </table>
                     
                   </div>
                </div>

      </div>
       <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal" >Back</button>      </div>
        
    </div>
  </div>
</div>

<script src="../Scripts/site/Customers.js"></script>
<script src="../Scripts/site/util.js"></script>
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

    $('#exampleModalCenter').on('show.bs.modal', function(e) {
    
        let name = $(e.relatedTarget).data('name');      
        $('#mod_name').text(name);
   
    });
    function prueba() {
        
        showMsg(true, "Esto es una prueba");

    }

    function showMsg(type, msg) {
        let typeMsg = type ==false ? "alert-danger" : "alert-success";
        let typeMsgOld = type ==false? "alert-success" : "alert-danger";
        $(".alert").css("visibility", "visible");
        $( "div.alert" ).removeClass( typeMsgOld ).addClass( typeMsg );
        $("div.alert").text(msg);
        $( "div.alert" ).fadeIn( 300 ).delay( 2500 ).fadeOut( 400 );      
    } 

    function SearchVisitors() {
        
        $('#cover-spin').show(0);
        let top = $("#topVisitors").val();
        let fecFrom = $("#txtFecFrom").val();
        let fecTo = $("#txtFecTo").val();
        $(".spinner").css("visibility","visible");
        searchVisitors(top,fecFrom,fecTo);       

    } 
    function SearchVisitors() {
        
        $('#cover-spin').show(0);
        let top = $("#topVisitors").val();
        let fecFrom = $("#txtFecFrom").val();
        let fecTo = $("#txtFecTo").val();
        $(".spinner").css("visibility", "visible");
        if (fecFrom != "" && !isValidDate(fecFrom)) {
            alert("Bad format in Start date, please fix it!");
            $('#cover-spin').hide();
            return;
        }
        if (fecTo != "" && !isValidDate(fecTo)) {
            alert("Bad format in Final date, please fix it!");
            $('#cover-spin').hide();
            return;
        }
        searchVisitors(top,fecFrom,fecTo);       

    }

</script>
</asp:Content>

