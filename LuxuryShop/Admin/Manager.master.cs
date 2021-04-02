/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Master page designed for the pages of the tool administration module.
 * Company: AIS - NZ
 **/
using System;
using System.Configuration;
using System.Web;

public partial class Manager : System.Web.UI.MasterPage
{
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    public string user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Tools.ValidateCookie(cookUser))
        {
            HttpCookie rqstCookie = Tools.GetCookie(cookUser);
            user = rqstCookie.Value;
        }
    }
}
