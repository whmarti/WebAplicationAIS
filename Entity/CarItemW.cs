/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * CarItem table.
 * Company: AIS - NZ
 * */
using Entity;
using System;

public class CarItemW
{
    public int IdCarItem { get; set; }
    public int IdUser { get; set; }    
    public int IdProduct { get; set; }
    public int quantity { get; set; }
    public float price { get; set; }
}