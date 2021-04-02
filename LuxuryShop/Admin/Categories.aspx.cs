/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to manage the Categories of the products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Categories : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagDefaultM = ConfigurationManager.AppSettings["pagDefaultM"];
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];    
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "Categories.aspx";
    private CategoryBL catBL;
    private mError mError;
    public String numRows = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("","");
        HideMessage();
        if (!(Tools.ValidateCookie(cookUser)) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);      

        try { 
            if (!IsPostBack)
            {
                if (Session["searchCat"] != null)
                    txtSearch.Text = Session["searchCat"].ToString();
                else
                    Session["searchCat"] = "";

                if (Session["PageIndexCat"] != null)
                { gvCategories.PageIndex = Session["PageIndexCat"].ToString() != "" ? Convert.ToInt32(Session["PageIndexCat"].ToString()) : 0; }
                FillData();
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
            gvCategories.PageIndex = e.NewPageIndex;
            Session["PageIndexCat"] = e.NewPageIndex;
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
        catBL = new CategoryBL();        
        System.Collections.Generic.List<Entity.Category> _dt = txtSearch.Text == "" ? catBL.getCategoriesBL(ref mError) : catBL.getCategorySearch_BL(txtSearch.Text, ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvCategories.DataSource = _dt;
                gvCategories.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvCategories.DataSource = null;
                gvCategories.DataBind();
                numRows = " ";
            }
        }
        else
        {
            gvCategories.DataSource = null;
            gvCategories.DataBind();
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
            Session["PageIndexCat"] = "0";
            Session["searchCat"] = txtSearch.Text;
            catBL = new CategoryBL();

            System.Collections.Generic.List<Entity.Category> _dt = catBL.getCategorySearch_BL(txtSearch.Text ,ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvCategories.DataSource = _dt;
                gvCategories.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvCategories.DataSource = null;
                gvCategories.DataBind();
            }
        }
        else
        {
            gvCategories.DataSource = null;
            gvCategories.DataBind();
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
    protected void gvRemCategories_Click(object sender, EventArgs e)
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
        try
        {

            String _msg = "<strong> Category </strong> deleted <strong> successfully! </strong>";
            catBL = new CategoryBL();
            Category _categ = new Category();
            _categ.category();
            _categ.IdCategory = pId;
            catBL.CRUDCategoryBL(_categ, "DEL", ref mError);
            if (mError.code == "1")
            {
                _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                ShowMessage(_msg, WarningType.Danger);
            }
            else
            {
                ShowMessage(_msg, WarningType.Success);
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