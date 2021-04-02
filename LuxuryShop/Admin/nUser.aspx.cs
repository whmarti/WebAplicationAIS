/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to insert a new User (Employee) of the products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_User : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "nUser.aspx";
    private mError mError;
    private UserBL userBL;
    private User user;
    public String titUser = "New Employee";
    String _msg = "", pgUsrBack = "Users.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        if (!(Tools.ValidateCookie(cookUser)) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        try
        {
            if (!IsPostBack)
            {
                lblTitUser.Text = titUser;
                //if (Request.QueryString["Id"]!=null)
                  FillData();                
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
    /// Function that initializes the object to load information.
    /// </summary>
    protected void FillData()
    {
        //user = new User();
        if (Session["typeUser"].ToString()=="Client")
            lblTitUser.Text = "New Customer";
    }

    /// <summary>
    /// Function that invokes the logic layer to insert the registry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Update_Click(object sender, EventArgs e)
    {
        try {           

            String _msg = "<strong> User </strong> inserted <strong> successfully! </strong>";
            user = new User();
            user.name = name.Text.Trim();
            user.type = Session["typeUser"].ToString();
            user.lastName =lastName.Text.Trim();
            user.email = email.Text.Trim();
            user.phone = phone.Text.Trim();
            user.pass = pass.Text.Trim();
            user.address = address.Text.Trim();
            userBL = new UserBL();
            btnSave.Enabled = false;
            userBL.CRUDUserBL(user, "INS",ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);
                btnSave.Enabled = true;
            }
            else
            {
                ShowMessage(_msg, WarningType.Success);
                if (Session["typeUser"].ToString() == "Client")
                    pgUsrBack = "Customers.aspx";
                Response.AddHeader("REFRESH", "3;URL='" + pgUsrBack + "'");
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