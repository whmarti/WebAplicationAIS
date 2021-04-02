/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to register a new User (Client) of the products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Website_Register : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private string pagErr = "../" + ConfigurationManager.AppSettings["pagError"];
    private mError mError;
    private UserBL userBL;
    private User user;

    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        try
        {
            if (!IsPostBack)
            {
                
            }
            if (mError.code == "1")
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
            Response.Redirect(pagErr);
        }
    }

    /// <summary>
    /// Function that invokes the logic layer to insert the registry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        try
        {
            String _msg= "<strong> User </strong>  logged in <strong> successfully! </strong>";
            int _userId;
            user = new User();
            user.name = UserName.Text.Trim();
            user.lastName=lastName.Text.Trim();
            user.email=email.Text.Trim();
            user.phone=phone.Text.Trim();
            user.pass=Password.Text.Trim();
            user.address = address.Text.Trim();

            user.type = "Client";
            userBL = new UserBL();
            btnSave.Enabled = false;
            _userId = userBL.CRUDUserBL(user,"INS",ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);
                btnSave.Enabled = true;
            }
            else
            {
                ShowMessage(_msg, WarningType.Success);
                Response.AddHeader("REFRESH", "4;URL='/index.aspx'");
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
            Response.Redirect(pagErr);
        }
        string _script = "$('#cover-spin').hide();";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
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