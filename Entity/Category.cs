/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * Category table.
 * Company: AIS - NZ
 * */
using System;


namespace Entity
{
    public class Category
    {
        public int IdCategory { get; set; }
        public String name { get; set; }
        public void category(){
           IdCategory = 0;
           name = "";
        }
    }
    
}
