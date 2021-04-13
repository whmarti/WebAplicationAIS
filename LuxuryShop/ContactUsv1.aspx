<%@ Page Title="" Language="C#" MasterPageFile="Client.master" AutoEventWireup="true" CodeFile="ContactUsv1.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <link href="styles/StyleContact.css" rel="stylesheet" />
    <link rel="stylesheet" href="styles/about.css">
    <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
    <style>

       
    </style>
<div class="row rowInfo">
    <div class="col-md-6">
        <section id="ContactInfo">
              
                <h4>Contact Us</h4>
                <hr />
               
                <div class="form-group">                  
                    <div class="col-md-12"><span class="text-justify">
                        If you want information about any of our products or are interested in 
                        their distribution, contact our collaborators or send us a message 
                        (panel on the right), we will be pleased to answer your questions.
                        Once your concern is received, we will contact you as soon as 
                        possible to provide you with the most suitable information.
                        <br /><br />
                        Thanks a lot.<br /><br />
                        <b>Headquarters and Offices:</b> Fitzherbert, Palmerston <br />
                        North 4410 - Hawke's Bay - NZ - <a class="navbar-brand" href="#exampleModalCenter" data-toggle="modal" data-target="#exampleModalCenter"><i class='fa fa-random'></i>&nbsp;Location</a><br /><br />
                        <b>Telephone:</b> (+64) 0204 018 64 49<br /><br />
                        <b>Administration:</b> manager@luxury.co.nz<br />
                        <b>Purchases:</b> purchases@luxury.co.nz<br />
                        <b>Sales:</b> sales@luxury.co.nz<br />
                        <b>Quality:</b> quality@luxury.co.nz<br />
                        <b>Communication and marketing:</b> marketing@luxury.co.nz
                    </span>
                    </div>
                </div>
                
                
                
               
       
        </section>
    </div>
    <div class="col-md-6">
        <section id="MessageInfo">

           <div class="col-md-12 ContImgMsg" style="height:391px;">
       
             <div class="col-md-4 ContactMsg" >
                <img style="max-width:120px; min-width:140px;" class="ImageMsg" src="images/Message.JPG" alt="">
             </div>
             <div class="col-md-8 ">
                 <div class="ContactMsg">
                    <div style="width: 100%;">
                    
                        <div class="form-group">
                        <label for="exampleFormControlInput1">Email address</label>
                        <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
                        </div>       
                        <div class="form-group">
                            <label for="exampleFormControlInput1">Subject</label>
                            <input type="email" class="form-control" id="exampleFormControlInput2" >
                        </div>                           
                        <div class="form-group">
                            <label for="exampleFormControlTextarea1">Message</label>
                            <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
                        </div> 
                        <div class="form-group text-center">
                             <a class="btn btn-success" href="Website/bag.aspx?s=s">Send &raquo;</a>
                        </div>

                     </div>
                  </div>
              </div>
          
           </div>

        </section>

    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Location:</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <!-- Modal Detail Product-->
          <div class="cardM">
                 
                 <div class="cardNodeM2">
                     <div id="map"></div>
                </div>

      </div>
       <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal" >Back</button>      </div>
        
    </div>
  </div>
</div>
</div>
<script src="../scripts/site/about.js"></script>
     <script  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBpp_5YhYd5xTGmVVSs0OV6zSG844hzwdw&callback=initMap&libraries=&v=weekly" async ></script>
  
 <script type="text/javascript">
     
$('#exampleModalCenter').on('show.bs.modal', function(e) {
    
        //let name = $(e.relatedTarget).data('name');      
        //$('#mod_name').text(name);
   
    });

</script>

</asp:Content>

