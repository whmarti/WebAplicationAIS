using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Oops : System.Web.UI.Page
{
    public string _mError = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["error"] != null)
        {
            _mError = Session["error"].ToString();
            Session["error"] = "";
        }
    }
}