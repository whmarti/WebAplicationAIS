/***************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used for handling error messages between the layers 
 * of the application.
 * Company: AIS - NZ
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class mError
    {
        public String code { get; set; }
        public String mssg { get; set; }

        public mError(String pCode, String pMssg)
        {
            code = pCode;  //0: No Error; -1:Database Error;  
            mssg = pMssg;
        }
    }
}
