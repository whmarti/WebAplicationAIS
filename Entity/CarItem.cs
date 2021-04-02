/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used for the structure of the purchase.
 * Company: AIS - NZ
 * */
using Entity;
using System;


public class CarItem
{
    public int IdCarItem { get; set; }
    public int IdUser { get; set; }
    public int IdProduct { get; set; }
    public int IdCategory { get; set; }
    public String name { get; set; }
    public String color { get; set; }
    public String brand { get; set; }
    public String size { get; set; }
    public String photo { get; set; }
    public String photoD { get; set; }
    public float price { get; set; }
    public float discount { get; set; }
    public String category { get; set; }
    public int quantity { get; set; }
    public int monthDcto { get; set; }
}