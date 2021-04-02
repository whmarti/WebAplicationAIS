/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage the information of the 
 * User table.
 * Company: AIS - NZ
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        public int IdUser { get; set; }
        public String type { get; set; }
        public String name { get; set; }
        public String lastName { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public String pass { get; set; }
        public String date { get; set; }

        public void user()
        {
            IdUser = 0;
            type = "";
            name = "";
            lastName = "";
            email = "";
            phone = "";
            pass = "";
            address = "";
        }
    }
}
