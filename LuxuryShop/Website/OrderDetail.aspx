<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="Admin_OrderDetail" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../styles/styleOrder.css">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>     
    <link href="../images/LogoPaginaTop2.png" rel="shortcut icon" type="image/x-icon" />
    
    <title>Invoice <%=order.client %></title>
</head>
<body>
    

    <div class="container-fluid my-5 d-flex justify-content-center">
        <div class="card card-1">
            <div class="card-header bg-white">
                <div class="media flex-sm-row flex-column-reverse justify-content-between ">
                    <div class="col my-auto">
                        <h4 class="mb-0">Thanks for your Order,<span class="change-color"> <%=order.client %></span> !</h4>
                    </div>
                    <div class="col-auto text-center my-auto pl-0 pt-sm-4"> <img class="img-fluid my-auto align-items-center mb-0 pt-3" src="../images/LogoPaginaTop2.png" width="115" height="115">
                        <p class="mb-4 pt-0 Glasses"><img src="../images/IconLogo.JPG" alt="" height="20px"/>Luxury Accessories</p>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row justify-content-between mb-3">
                    <div class="col-auto">
                        <h6 class="color-1 mb-0 change-color">Receipt</h6>
                    </div>
                    <div class="col-auto "> <small>Receipt Voucher : <%=order.voucher %></small> </div>
                </div>
                <% foreach (OrderDetail oDetail in order._products) { %>
                <div class="row mt-4">
                    <div class="col">
                        <div class="card card-2">
                            <div class="card-body">
                                <div class="media">
                                    <div class="sq align-self-center "> <img class="img-fluid my-auto align-self-center mr-2 mr-md-4 pl-0 p-0 m-0" src="<%=dirPhoto+"/"+oDetail.photo %>" width="135" height="135" /> </div>
                                    <div class="media-body my-auto text-right">
                                        <div class="row my-auto flex-column flex-md-row">
                                            <div class="col-auto my-auto ">
                                                <h6 class="mb-0"><%=oDetail.brand %></h6>
                                            </div>
                                            <div class="col my-auto "> <small><%=oDetail.product %></small></div>
                                            <div class="col my-auto "> <small>Size : <%=oDetail.size %></small></div>
                                            <div class="col my-auto "> <small>Qty : <%=oDetail.quantity %></small></div>
                                            <div class="col my-auto ">
                                                <h6 class="mb-0">&#8364;<%=numberFormat(oDetail.price) %></h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr class="my-3 ">
                                <div class="row ">
                                    <div class="col-md-3 mb-3"> <small> Track Order <span><i class=" ml-2 fa fa-refresh" aria-hidden="true"></i></span></small> </div>
                                    <div class="col mt-auto">
                                        <div class="progress">
                                            <div class="progress-bar progress-bar rounded" style="width: 18%" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <div class="media row justify-content-between ">
                                            <div class="col-auto text-right"><span> <small class="text-right mr-sm-2"></small> <i class="fa fa-circle active"></i> </span></div>
                                            <div class="flex-col"> <span> <small class="text-right mr-sm-2">Out for delivary</small><i class="fa fa-circle"></i></span></div>
                                            <div class="col-auto flex-col-auto">
                                                <smallclass="text-right mr-sm-2">Delivered</small><span> <i class="fa fa-circle"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <%} %>


                <div class="row mt-4">
                    <div class="col">
                        <div class="row justify-content-between">
                            <div class="col-auto">
                                <p class="mb-1 text-dark"><b>Order Details</b></p>
                            </div>
                            <div class="flex-sm-col text-right col">
                                <p class="mb-1"><b>Total</b></p>
                            </div>
                            <div class="flex-sm-col col-auto">
                                <p class="mb-1">&#8364;<%=numberFormat(order.value) %></p>
                            </div>
                        </div>
                        <div class="row justify-content-between">
                            <div class="flex-sm-col text-right col">
                                <p class="mb-1"> <b>Discount</b></p>
                            </div>
                            <div class="flex-sm-col col-auto">
                                <p class="mb-1">&#8364;<%=numberFormat(totalDiscount) %></p>
                            </div>
                        </div>
                        <div class="row justify-content-between">
                            <div class="flex-sm-col text-right col">
                                <p class="mb-1"><b>GST 18%</b></p>
                            </div>
                            <div class="flex-sm-col col-auto">
                                <p class="mb-1">&#8364;<%=numberFormat(tax) %></p>
                            </div>
                        </div>
                        <div class="row justify-content-between">
                            <div class="flex-sm-col text-right col">
                                <p class="mb-1"><b>Delivery Charges</b></p>
                            </div>
                            <div class="flex-sm-col col-auto">
                                <p class="mb-1">Free</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row invoice ">
                    <div class="col">
                        <p class="mb-1"> Invoice Number : <%=invoiceFormat(order.IdOrder) %></p>
                        <p class="mb-1">Invoice Date : <%=order.dateF %></p>
                        <p class="mb-1">Recepits Voucher: <%=order.voucher %></p>
                        <p class="mb-1">Shipping Address: <%=order.address %></p>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="jumbotron-fluid">
                    <div class="row justify-content-between ">
                        <div class="col-sm-auto col-auto my-auto"><img class="img-fluid my-auto align-self-center " src="../images/accessory2.png" width="115" height="115"></div>
                        <div class="col-auto my-auto ">
                            <h2 class="mb-0 font-weight-bold">TOTAL PAID</h2>
                        </div>
                        <div class="col-auto my-auto ml-auto">
                            <h1 class="display-3 ">&#8364; <%=numberFormat(order.value+tax) %></h1>
                        </div>
                    </div>
                    <div class="row mb-3 mt-3 mt-md-0">
                      <div class="footer">
                        <div class="f1 border-line"> <small class="text-white">Luxury S.A.</small></div>
                        <div class="f1 border-line"> <small class="text-white">Nixon Av,  2367 NZ</small></div>
                        <div class="f1 "><small class="text-white">luxury@luxury.co</small> </div>
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>










</body>
</html>