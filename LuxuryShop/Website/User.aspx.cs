/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to update the Users and Clients of the tool.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_User : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private readonly String sessionClient = ConfigurationManager.AppSettings["sessionClient"];
    private readonly String Passkey = ConfigurationManager.AppSettings["Passkey"];
    private const String currentPage = "User.aspx";
    private mError mError;
    private UserBL userBL;
    private User user;
    String _msg = "",url = "../Index.aspx";
    public String idClient;
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        if ((!(Tools.ValidateCookie(cookClient)) || !(Tools.ValidateCookie(sessionClient))) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        idClient = Tools.GetCookie(sessionClient).Value;

        if (Session["urlOld"]!=null) {
            if (!String.IsNullOrEmpty(Session["urlOld"].ToString()))
            {
                url = Session["urlOld"].ToString();
            }
        }

        try
        {
            if (!IsPostBack)
            {
                if(!String.IsNullOrEmpty(idClient))
                FillData(Convert.ToInt32(idClient));
            }
            if (mError.code == "1" )
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
    /// Function that invokes the logic layer to to bring the category information.
    /// </summary>
    protected void FillData(int pIdUser)
    {
        user = new User();
        userBL = new UserBL();
        user = userBL.getUserBL(pIdUser,ref mError);
        if (user != null)
        {
            IdUser.Value = user.IdUser.ToString();
            name.Text = user.name.Trim();
            typeUser.Value = user.type.Trim();
            name.Text = user.name.Trim();
            lastName.Text = user.lastName.Trim();
            email.Text = user.email.Trim();
            phone.Text= user.phone.Trim();
            state.Value = user.state.Trim();
            address.Text = user.address.Trim();
            pass.Text = Passkey.Trim();
            passwordRe.Text = Passkey.Trim();
            pass.Attributes["type"] = "password";
            passwordRe.Attributes["type"] = "password";            
        }
        else
        {
            _msg = "<strong> Error! </strong> The user is not available.";
            ShowMessage(_msg, WarningType.Danger);
        }
    }

    /// <summary>
    /// Function that invokes the logic layer to update the registry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Update_Click(object sender, EventArgs e)
    {
        try {           

            String _msg = "<strong> User </strong> updated <strong> successfully! </strong>";
            user = new User();
            user.IdUser = Convert.ToInt32(IdUser.Value);
            user.name = name.Text.Trim();
            user.type = typeUser.Value.Trim();
            user.lastName =lastName.Text.Trim();
            user.email = email.Text.Trim();
            user.phone = phone.Text.Trim();
            user.pass = pass.Text.Trim();
            user.address = address.Text.Trim();
            user.state = state.Value;
            userBL = new UserBL();
            btnSave.Enabled = false;
            userBL.CRUDUserBL(user, "UPD",ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);
                btnSave.Enabled = true;                
            }
            else
            {
                Tools.SetCookie(cookClient, user.name + " " + user.lastName);
                ShowMessage(_msg, WarningType.Success);               
                Response.AddHeader("REFRESH", "3;URL='"+ url+"'");                
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

    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["urlOld"].ToString());
    }
}