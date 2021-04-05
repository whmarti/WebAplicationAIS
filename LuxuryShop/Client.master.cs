/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Master page designed for the general information pages of the Web site.
 * Company: AIS - NZ
 **/
using System;
using System.Configuration;
using System.Web;

public partial class Client : System.Web.UI.MasterPage
{
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    public string client = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Tools.ValidateCookie(cookClient))
        {
            HttpCookie rqstCookie = Tools.GetCookie(cookClient);
            client = rqstCookie.Value;
        }
    }
}
