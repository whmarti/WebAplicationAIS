/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * Product table.
 * Company: AIS - NZ
 * */
using System;

namespace Entity
{
    public class Product
    {
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        
        public String name { get; set; }
        public int stock { get; set; }
        public String color { get; set; }
        public String brand { get; set; }
        public String size { get; set; }
        public String photo { get; set; }
        public String photoD { get; set; }
        public float price { get; set; }
        public float discount { get; set; }
        public String category{ get; set; }
        public int monthDcto { get; set; }
        public void product()
        {
            IdProduct = 0;
            IdCategory = 0;
            name = "";
            stock = 0;
            color = "";
            brand = "";
            size = "";
            photo = "";
            photoD = "";
            price = 0;
            discount = 0;
            category = "";
            monthDcto = 0;
        }
    }
}
