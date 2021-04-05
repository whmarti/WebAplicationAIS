/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to manage the Products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Products : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String usrAccountAuth = ConfigurationManager.AppSettings["usrAccountAuth"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "Products.aspx";
    private ProductBL prodBL;
    private mError mError;
    public String numRows="", legendSearch="Name, Brand, Cat...";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("","");
        HideMessage();
        if (!(Tools.ValidateCookie(cookUser)) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);

        try { 
            if (!IsPostBack)
            {
                if (Session["searchBag"] != null)
                    txtSearch.Text = Session["searchBag"].ToString();
                else
                    Session["searchBag"] = "";

                if (Session["PageIndexBag"] != null)
                { gvProducts.PageIndex = Session["PageIndexBag"].ToString() != "" ? Convert.ToInt32(Session["PageIndexBag"].ToString()) : 0; }
                FillData();

                if (Session["searchBag"].ToString() != "")
                    txtSearch.Text = Session["searchBag"].ToString();
                else
                    txtSearch.Text = legendSearch;

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
            gvProducts.PageIndex = e.NewPageIndex;
            Session["PageIndexBag"] = e.NewPageIndex;         

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
        prodBL = new ProductBL();
        String _search = txtSearch.Text.Trim(); 
        String _date = txtDate.Text.Trim();
        if (_search == legendSearch)
            _search = "";
        System.Collections.Generic.List<Entity.Product> _dt = txtSearch.Text == ""? prodBL.getProductsBL(ref mError): prodBL.getProductSearch_BL(_search, _date, ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvProducts.DataSource = _dt;
                gvProducts.DataBind();
                numRows = " "+_dt.Count()+" Rows";
            }
            else
            {
                gvProducts.DataSource = null;
                gvProducts.DataBind();
                numRows = " ";
            }
        }
        else
        {
            gvProducts.DataSource = null;
            gvProducts.DataBind();
            numRows = " ";
        }
        Session["numRows"] = numRows;
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
            if(txtSearch.Text.Trim()== legendSearch)
                txtSearch.Text="";

            Session["PageIndexBag"] = "0";
            Session["searchBag"] = txtSearch.Text;
            prodBL = new ProductBL();
        System.Collections.Generic.List<Entity.Product> _dt = prodBL.getProductSearch_BL(txtSearch.Text, txtDate.Text ,ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvProducts.DataSource = _dt;
                gvProducts.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvProducts.DataSource = null;
                gvProducts.DataBind();
                numRows = "0 Rows";
            }
        }
        else
        {
            gvProducts.DataSource = null;
            gvProducts.DataBind();
            numRows = "0 Rows";
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
        Session["numRows"] = numRows;
        string _script = "$('#cover-spin').hide();";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
    }

    /// <summary>
    /// Function that handles the deletion of a selected record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRemProducts_Click(object sender, EventArgs e)
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

            String _msg = "<strong> Product </strong> deleted <strong> successfully! </strong>";
            prodBL = new ProductBL();
            Product _prod = new Product();
            _prod.product();
            _prod.IdProduct = pId;
            prodBL.CRUDProductBL(_prod, "DEL", ref mError);
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
    /// Function that show the Calendar.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Calendar_Click(object sender, EventArgs e)
    {
        Calendar1.Visible = true;
        numRows=Session["numRows"].ToString();
    }
    /// <summary>
    /// Function that send the date from the Calendar control to the date text field.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = Calendar1.SelectedDate.ToShortDateString();
        Calendar1.Visible = false;
        numRows = Session["numRows"].ToString();
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