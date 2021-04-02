/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * OrderDetail table.
 * Company: AIS - NZ
 * */
using Entity;
using System;

public class OrderDetail
{
    public int IdOD { get; set; }
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }
    public int quantity { get; set; }
    public float price { get; set; }    


    //Aditional Properties:
    public float discount { get; set; }
    public String brand { get; set; }
    public String product { get; set; }
    public String size { get; set; }
    public String photo { get; set; }
    




}