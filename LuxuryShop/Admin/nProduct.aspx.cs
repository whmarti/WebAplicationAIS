/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to insert a new Products to sell.
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
using System.Globalization;

public partial class Admin_Product : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String defaultPhoto = ConfigurationManager.AppSettings["defaultPhoto"];
    private readonly String dirPhoto = ConfigurationManager.AppSettings["dirPhoto"];
    private readonly String dirSavePhoto = ConfigurationManager.AppSettings["dirSavePhoto"];
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = ConfigurationManager.AppSettings["pagError"];
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private const String currentPage = "nProduct.aspx";
    private mError mError;
    private ProductBL prodBL;
    private Product product;
    private CategoryBL categBL;
    private List<Category> categories;
    
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
        product = new Product();
        prodBL = new ProductBL();
        categBL = new CategoryBL();
        categories=categBL.getCategoriesBL(ref mError);
        ddlIdCategory.DataSource = categories;
        ddlIdCategory.DataTextField = "name";
        ddlIdCategory.DataValueField = "IdCategory";
        ddlIdCategory.DataBind();
        ddlIdCategory.SelectedValue = product.IdCategory.ToString();
        price.Text = ".00";
        discount.Text = "0.00";
        bind_month_ddl();
        if (categories.Count==0)
        {
            _msg = "There are no < strong > Categories </ strong >  in Database.";
            ShowMessage(_msg, WarningType.Danger);
        }

    }

    /// <summary>
    /// Function that fills the promotions month control.
    /// </summary>
    private void bind_month_ddl()
    {
        for (int i = 1; i <= 12; i++)
        {
            ddlmonth.Items.Add(new System.Web.UI.WebControls.ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(i), i.ToString()));
        }
        ddlmonth.Items.Add(new System.Web.UI.WebControls.ListItem("No Promotion", "0"));
    }

    /// <summary>
    /// Function that invokes the logic layer to insert the registry.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Update_Click(object sender, EventArgs e)
    {
        try {
            if (Page.IsValid)
            {
                string _fileName = "";
                string _fileNameD = "";
                string _savepath = "";
                string _savefile = "";
                HttpPostedFile _file = photo.PostedFile;
                int _fileSizeInBytes = _file.ContentLength;
                string _fileExtension = "";
                _fileName = _file.FileName;
                if (!string.IsNullOrEmpty(_fileName))
                {
                    _fileExtension = Path.GetExtension(_fileName);
                    //_savepath = Path.Combine(Request.PhysicalApplicationPath, dirPhoto);
                    _savepath = Path.Combine(Request.PhysicalApplicationPath, dirSavePhoto);
                    _savefile = Path.Combine(_savepath, _file.FileName);
                    _file.SaveAs(_savefile);
                }
                _file = photoD.PostedFile;
                _fileSizeInBytes = _file.ContentLength;
                _fileNameD = _file.FileName;
                if (!string.IsNullOrEmpty(_fileNameD))
                {
                    _fileExtension = Path.GetExtension(_fileNameD);
                    //_savepath = Path.Combine(Request.PhysicalApplicationPath, dirPhoto);
                    _savepath = Path.Combine(Request.PhysicalApplicationPath, dirSavePhoto);
                    _savefile = Path.Combine(_savepath, _file.FileName);
                    _file.SaveAs(_savefile);
                }

                String _msg = "<strong> Product </strong> inserted <strong> successfully! </strong>";
                int _monthDcto = 0;
                product = new Product();
                product.IdProduct = 0;
                product.IdCategory = Convert.ToInt32(ddlIdCategory.SelectedValue);
                product.name = name.Text.Trim();
                product.stock = Convert.ToInt32(stock.Text);
                product.color = color.Text.Trim();
                product.brand = brand.Text.Trim();
                product.size = size.Text.Trim();
                if (_fileName == "")
                    product.photo = lblphoto.Text == "" ? defaultPhoto : lblphoto.Text.Trim();
                else
                    product.photo = _fileName;
                if (_fileNameD == "")
                    product.photoD = lblphotoD.Text == "" ? defaultPhoto : lblphotoD.Text.Trim();
                else
                    product.photoD = _fileNameD;

                product.price = (float)Convert.ToDouble(price.Text);
                product.discount = (float)Convert.ToDouble(discount.Text);
                _monthDcto = Convert.ToInt32(ddlmonth.SelectedValue);
                if (!((float)Convert.ToDouble(discount.Text) > 0) || ddlmonth.SelectedValue == "0")
                {
                    product.discount = 0;
                    _monthDcto = 0;
                }
                product.monthDcto = _monthDcto;
                prodBL = new ProductBL();
                btnSave.Enabled = false;
                prodBL.CRUDProductBL(product, "INS", ref mError);
                if (mError.code == "1")
                {
                    _msg = "<strong> Error! </strong> " + mError.mssg.Trim();
                    ShowMessage(_msg, WarningType.Danger);
                    btnSave.Enabled = true;
                }
                else
                {
                    ShowMessage(_msg, WarningType.Success);
                    Response.AddHeader("REFRESH", "3;URL='Products.aspx'");
                }
                if (mError.code == "1")
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