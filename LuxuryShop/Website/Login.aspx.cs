/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to allow an client of Luxury S.A. enter with his credentials
 * to the Web site module of the application to be able to make a purchase.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Website_Login : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagDefault = "../" + ConfigurationManager.AppSettings["pagDefault"];
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLogin"];
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    private readonly String cookAddrClient = ConfigurationManager.AppSettings["cookAddrClient"];
    private readonly String sessionClient = ConfigurationManager.AppSettings["sessionClient"];
    private readonly String stateClient = ConfigurationManager.AppSettings["stateClient"];
    private static readonly String cookDateCreate = ConfigurationManager.AppSettings["cookDateCreate"];
    private string pagErr = "../" + ConfigurationManager.AppSettings["pagError"];
    private mError mError;
    private String mpageRed, option;
    private UserBL userBl;
    private User user;   
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");               
        mpageRed = (Request.QueryString["s"] != "" && Request.QueryString["s"] != null) ? Request.QueryString["pag"]+"?s=" +Request.QueryString["s"] : (Request.QueryString["pag"]!=null) ?Request.QueryString["pag"] : pagDefault;
        if (!IsPostBack)
        {

            if (Request.QueryString["opt"] != null)
            {
                option = Request.QueryString["opt"];
                if (option == "logOut")
                {
                    Tools.DeleteCookie(cookClient);
                    Tools.DeleteCookie(sessionClient);
                    Response.Redirect(pagDefault);
                }
            }

        }

    }

    /// <summary>
    /// Function that invokes the logic layer to validate the information.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Login_Click(object sender, EventArgs e)
    {
        String _pagErr = pagErr;

        try
        {

            String _msg = "";
            userBl = new UserBL();
            btnLogin.Enabled = false;
            user = userBl.getUserLoginBL(email.Text.Trim(), password.Text.Trim(),"Client", ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);
                btnLogin.Enabled = true;
            }
            else
            {
                if (user.state == "Inactive")
                {
                    mError.mssg = "Your account is inactive. Please contact the Web administrator."; 

                    btnLogin.Enabled = true;
                    _msg = "<strong> Hello " + user.name + " " + user.lastName + " </strong>. " + mError.mssg;
                    ShowMessage(_msg, WarningType.Warning);
                    string _script = "$('#cover-spin').hide();";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
                }
                else
                {
                    Tools.CreateCookie(cookClient, user.name + " " + user.lastName);
                    Tools.CreateCookie(sessionClient, user.IdUser.ToString());
                    Tools.CreateCookie(cookAddrClient, user.address.ToString());
                    Tools.CreateCookie(stateClient, user.state.ToString());
                    _msg = "<strong> Hello " + user.name + " " + user.lastName + " </strong> access successfully! ";
                    ShowMessage(_msg, WarningType.Success);
                    string _script = "$('#cover-spin').hide();";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
                    Response.Redirect(mpageRed, false);
                }
            }
            if (mError.code != "1" && mError.code != "")
            {
                Response.Write("Error DB: " + mError.mssg);
                throw new Exception("Error DB: " + mError.mssg);
            }

        }
        catch (Exception ex)
        {
            if (mError.code == "0" && showErr == 1)
                Session["error"] = "Error BL: " + ex.Message;
            else if (mError.code == "1" && showErr == 1)
                Session["error"] = "Error BD: " + mError.mssg;
            else
                Session["error"] = ex.Message;
            Response.Redirect(_pagErr);
        }

    }

    /// <summary>
    /// Function that informs in a square the results of the transactionts via user interface.
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="type"></param>
    public void ShowMessage(string Message, WarningType type)
    {
        Panel PanelMessage = Master.FindControl("MainContent").FindControl("Message") as Panel;
        Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

        labelMessage.Text = Message;
        PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
        PanelMessage.Attributes.Add("role", "alert");
        PanelMessage.Visible = true;
    }

  
}