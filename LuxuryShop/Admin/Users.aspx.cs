/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page created to manage Users with access to both the Web module 
 * (clients) and the administration module (employees).
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagDefaultM = ConfigurationManager.AppSettings["pagDefaultM"];
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String usrAccountAuth = ConfigurationManager.AppSettings["usrAccountAuth"];
    private readonly String sessionUser = ConfigurationManager.AppSettings["sessionUser"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private String currentPage = "Users.aspx", typeUser="Admin";
    private UserBL userBL;
    private mError mError;
    public String numRows = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("","");
        HideMessage();
        
        if (!(Tools.ValidateCookie(usrAccountAuth)) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        try { 
            if (!IsPostBack)
            {
                if (Session["searchUsr"] != null)
                    txtSearch.Text = Session["typeUser"].ToString() == typeUser ? Session["searchUsr"].ToString() : "";
                else
                    Session["searchUsr"] = "";

                if (Session["PageIndexUsr"] != null)
                { gvUsers.PageIndex = Session["PageIndexUsr"].ToString() != "" ? Convert.ToInt32(Session["PageIndexUsr"].ToString()) : 0; }
                FillData();
                Session["typeUser"] = typeUser;
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
    /// Function that handles the pagination event of the Gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUsers.PageIndex = e.NewPageIndex;

            FillData();
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
    /// Function that invokes the logic layer to fetch the table elements from the database and fill the gridview.
    /// with the information.
    /// </summary>
    protected void FillData()
    {
        userBL = new UserBL();
        System.Collections.Generic.List<Entity.User> _dt = txtSearch.Text == "" ? userBL.getUsersBL(typeUser,ref mError) : userBL.getUserSearch_BL(typeUser,txtSearch.Text, ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvUsers.DataSource = _dt;
                gvUsers.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvUsers.DataSource = null;
                gvUsers.DataBind();
                numRows = " ";
            }
        }
        else
        {
            gvUsers.DataSource = null;
            gvUsers.DataBind();
            numRows = " ";
        }
    }

    /// <summary>
    /// Function that invokes the logic layer to fetch the table elements from the database (based on a filter) 
    /// and fill the gridview with the information.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Search_Click(object sender, EventArgs e)
    {
        try
        {
            Session["PageIndexUsr"] = "0";
            Session["searchUsr"] = txtSearch.Text;
            userBL = new UserBL();           
            
            System.Collections.Generic.List<Entity.User> _dt = userBL.getUserSearch_BL(typeUser,txtSearch.Text,ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvUsers.DataSource = _dt;
                gvUsers.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvUsers.DataSource = null;
                gvUsers.DataBind();
                numRows = " ";
            }
        }
        else
        {
            gvUsers.DataSource = null;
            gvUsers.DataBind();
            numRows = " ";
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
        string _script = "$('#cover-spin').hide();";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
    }

    /// <summary>
    /// Function that handles the deletion of a selected record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRemUsers_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        DeleteData(Convert.ToInt32(btn.CommandArgument));
        FillData();
    }

    /// <summary>
    /// Function that invokes the logic layer to execute the deletion of a selected record.
    /// </summary>
    /// <param name="pId"></param>
    protected void DeleteData(int pId)
    {
        String _msg = "<strong> User </strong> deleted <strong> successfully! </strong>";
        try
        {
            HttpCookie rqstCookie = Tools.GetCookie(sessionUser);
            int _idUser = Convert.ToInt32(rqstCookie.Value);
            if (pId== _idUser)
            {
                _msg = "<strong> Error! </strong> Your own user can not be deleted.";
                ShowMessage(_msg, WarningType.Danger);
                return;
            }

            
            userBL = new UserBL();
            User _user = new User();
            _user.user();
            _user.IdUser = pId;
            _user.state = "Deleted";
            userBL.CRUDUserBL(_user, "DEL", ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);

            }
            else
            {
                ShowMessage(_msg, WarningType.Success);
                //Response.AddHeader("REFRESH", "3;URL='Categories.aspx'");
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
            //Response.Redirect(pagErr);
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

    /// <summary>
    /// Function that hide that informational square.
    /// </summary>
    public void HideMessage()
    {
        Panel PanelMessage = Master.FindControl("MainContent").FindControl("Message") as Panel;
        PanelMessage.Visible = false;
    }
}