/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * Order table.
 * Company: AIS - NZ
 * */
using Entity;
using System;
using System.Collections.Generic;

public class Order
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public float value { get; set; }
    public String date { get; set; }
    public String payType { get; set; }
    public String client { get; set; }
    public String email { get; set; }
    public String voucher { get; set; }
    //Aditional Properties:
    public List<OrderDetail> _products { get; set; }
    public String dateF { get; set; }
    public String address { get; set; }



}