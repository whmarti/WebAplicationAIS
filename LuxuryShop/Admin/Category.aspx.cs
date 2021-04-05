/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to update the Categories of the products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

public partial class Admin_Category : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "Category.aspx";
    private mError mError;
    private CategoryBL prodBL;
    private Category category;
   
    String _msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        if (!(Tools.ValidateCookie(cookUser)) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        try
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["Id"]!=null)
                FillData(Convert.ToInt32(Request.QueryString["Id"]));
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
    protected void FillData(int pIdCategory)
    {
        category = new Category();
        prodBL = new CategoryBL();
        category = prodBL.getCategoryBL(pIdCategory,ref mError);
        if (category != null)
        {          
            IdCategory.Value = category.IdCategory.ToString();
            name.Text = category.name.Trim();  
        }
        else
        {
            _msg = "<strong> Error! </strong> The category is not available.";
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
            if (Page.IsValid)
            {
                String _msg = "<strong> Category </strong> updated <strong> successfully! </strong>";
                category = new Category();
                category.IdCategory = Convert.ToInt32(IdCategory.Value);
                category.name = name.Text.Trim();
                prodBL = new CategoryBL();
                btnSave.Enabled = false;
                prodBL.CRUDCategoryBL(category, "UPD", ref mError);
                if (mError.code == "1")
                {
                    _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                    ShowMessage(_msg, WarningType.Danger);
                    btnSave.Enabled = true;
                }
                else
                {
                    ShowMessage(_msg, WarningType.Success);
                    Response.AddHeader("REFRESH", "3;URL='Categories.aspx'");
                }
                if (mError.code != "1" && mError.code != "")
                {
                    Response.Write("Error DB: " + mError.mssg);
                    throw new Exception("Error DB: " + mError.mssg);
                }
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