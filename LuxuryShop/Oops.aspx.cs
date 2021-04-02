/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page built to show error information captured in others pages. 
 * that the administration module has.
 * Company: AIS - NZ
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Oops : System.Web.UI.Page
{
    public string _mError="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["error"]!=null)
        {
            _mError = Session["error"].ToString();
            Session["error"] = "";
        }
    }
}