using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestErrorHandling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String err = Request.QueryString["e"];
        if(err== "401")
           throw new HttpException(401, "Auth Failed");
        else if (err == "403")
            throw new HttpException(403, "Auth Forbidden");
        else if (err == "404")
            throw new HttpException(404, "File not found");
        else if (err == "500")
            throw new HttpException(500, "Internal Error");

    }
}