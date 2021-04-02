/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to manage the Orders made by the customers.
 * Company: AIS - NZ
 * */
 using Business;
using Entity;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Orders : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    private readonly String sessionClient = ConfigurationManager.AppSettings["sessionClient"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "Orders.aspx";
    private OrderBL orderBL;
    private mError mError;
    public String numRows = "0", idClient, legendSearch = "# Order, Client name...";


    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        HideMessage();
        if ((!(Tools.ValidateCookie(cookClient)) || !(Tools.ValidateCookie(sessionClient))) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        idClient = Tools.GetCookie(sessionClient).Value;

        try
        {
            if (!IsPostBack)
            {
                if (Session["searchOrder"] != null)
                    txtSearch.Text = Session["searchOrder"].ToString();
                else
                    Session["searchOrder"] = "";


                if (Session["PageIndexBag"] != null)
                { gvOrders.PageIndex = Session["PageIndexBag"].ToString() != "" ? Convert.ToInt32(Session["PageIndexBag"].ToString()) : 0; }
                FillData(Convert.ToInt32(idClient));
                if (Session["searchOrder"].ToString() != "")
                    txtSearch.Text = Session["searchOrder"].ToString();
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
            gvOrders.PageIndex = e.NewPageIndex;
            Session["PageIndexBag"] = e.NewPageIndex;
            FillData(Convert.ToInt32(idClient));
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
    protected void FillData(int pIdClient)
    {
        orderBL = new OrderBL();
        String _search = txtSearch.Text.Trim();
        if (_search == legendSearch)
            _search = "";
        System.Collections.Generic.List<Order> _dt = txtSearch.Text == "" ? orderBL.getOrdersBL(pIdClient,ref mError) : orderBL.getOrderSearch_BL(pIdClient, _search, ref mError);
        if (_dt != null)
        {
            if (_dt.Count > 0)
            {
                gvOrders.DataSource = _dt;
                gvOrders.DataBind();
                numRows = " " + _dt.Count() + " Rows";
            }
            else
            {
                gvOrders.DataSource = null;
                gvOrders.DataBind();
                numRows = "0 Rows";
            }
        }
        else
        {
            gvOrders.DataSource = null;
            gvOrders.DataBind();
            numRows = "0 Rows";
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
            if (txtSearch.Text.Trim() == legendSearch)
                txtSearch.Text = "";

            Session["PageIndexBag"] = "0";
            Session["searchOrder"] = txtSearch.Text;
            orderBL = new OrderBL();
            System.Collections.Generic.List<Order> _dt = orderBL.getOrderSearch_BL(Convert.ToInt32(idClient), txtSearch.Text, ref mError);
            if (_dt != null)
            {
                if (_dt.Count > 0)
                {
                    gvOrders.DataSource = _dt;
                    gvOrders.DataBind();
                    numRows = " " + _dt.Count() + " Rows";
                }
                else
                {
                    gvOrders.DataSource = null;
                    gvOrders.DataBind();
                    numRows = "0 Rows";
                }
            }
            else
            {
                gvOrders.DataSource = null;
                gvOrders.DataBind();
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

    /// <summary>
    /// Function that returns a date in format: dd MMM yyyy
    /// </summary>
    /// <param name="pNumber"></param>
    /// <returns></returns>
    public string numberFormat(float pNumber)
    {
        return pNumber.ToString("N", CultureInfo.InvariantCulture); ;
    }
}