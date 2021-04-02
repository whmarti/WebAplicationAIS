/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to manage the products to sell.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Website_Bag : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = "../" + ConfigurationManager.AppSettings["pagError"];
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    private readonly String cookClientVisit = ConfigurationManager.AppSettings["cookClientVisit"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"];
    private readonly String pagSizeCli = ConfigurationManager.AppSettings["pagSizeCli"];
    public String dirPhoto = ConfigurationManager.AppSettings["dirPhoto"];
    private readonly String sessionClient = ConfigurationManager.AppSettings["sessionClient"];
    private const String currPage = "Bag.aspx";
    private ProductBL prodBL;
    private CarItemBL carItemBL;
    private int CurrentPage;
    public int numRows = 0;
    String prod="b",search="", directory, current_Page="Bag.aspx", legendSearch = "Name, Brand...";
    private mError mError;   
    public int nCarItems = 0, userId;
    public void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        prod = Request.QueryString["s"] != "" ? Request.QueryString["s"] : prod;
        if ((!(Tools.ValidateCookie(cookClient)) || !(Tools.ValidateCookie(sessionClient)) ) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currPage+ "&s=" + prod);
        else
        {
            userId = getUserId();
            ValidateVisit(sessionClient); //Generate visit record of the day
        }
        Session["urlOld"] = current_Page+"?s="+ prod;
        try
        {
            if (!IsPostBack)
            {
                if (Session["searchProd"] != null)
                {
                    txtSearch.Text = Session["searchProd"].ToString();                   
                }
                else
                    Session["searchProd"] = "";

                FillData(prod);                
                nCarItems = getCar(userId, ref mError);
                //ViewState["userId"] = userId;
                ViewState["nCarItems"] = nCarItems;
                ViewState["PageCount"] = 0;
                if (Session["searchProd"].ToString() != "")
                    txtSearch.Text = Session["searchProd"].ToString();
                else
                    txtSearch.Text = legendSearch;

            }
            CurrentPage = (int)ViewState["PageCount"];
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
    /// Function that invokes the logic layer to fetch the table elements from the database and fill the DataList.
    /// with the information.
    /// </summary>
    protected void FillData(String prod)
    {
        String _cat = prod == null ? "" : prod.Trim();
        int _offer = 0;
        if (search == "" && txtSearch.Text.ToString() != "")
            search = txtSearch.Text.ToString();
        switch (_cat)
        {
            case "a":
                _cat = "Accesories";
                this.lblTitle.Text = _cat;                
                break;
            case "b":
                _cat = "Bags";
                this.lblTitle.Text = _cat + " & " + "Purses";
                break;
            case "s":
                _cat = "Shoes";
                this.lblTitle.Text = _cat;
                break;
            default:                
                break;
               
        }
        this.Image2.ImageUrl = "../Images/" + _cat + ".jpg";
        prodBL = new ProductBL();
        System.Collections.Generic.List<Entity.Product> _lstDt = _cat==""? prodBL.getProductsBL(ref mError) : prodBL.getProductSearchCliente_BL(search, _cat, _offer,  ref mError); 
        if (_lstDt != null)
        {
            if (_lstDt.Count > 0)
            {               
                string _json = Newtonsoft.Json.JsonConvert.SerializeObject(_lstDt);
                DataTable _dt = JsonConvert.DeserializeObject<DataTable>(_json);
                DataListPaging(_dt);
                numRows = _lstDt.Count;
                ViewState["numRows"] = _lstDt.Count;
            }
            
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Function that invokes the logic layer to fetch the table elements from the database (based on a filter) 
    /// and fill the DataList with the information.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Search_Click(object sender, EventArgs e)
    {
        int _offer = chkOffer.Checked ? DateTime.Now.Month : 0 ;
        try
        {           
            nCarItems = Convert.ToInt32(ViewState["nCarItems"]);
            String _cat = Request.QueryString["s"] != "" ? Request.QueryString["s"] : "";
            Session["PageIndexProd"] = "0";

            if (txtSearch.Text.Trim() == legendSearch)
                txtSearch.Text = "";
            Session["searchProd"] = txtSearch.Text;
            search = txtSearch.Text;
            prodBL = new ProductBL();
            switch (_cat)
            {
                case "a":
                    _cat = "Accesories";
                    this.lblTitle.Text = _cat;
                    break;
                case "b":
                    _cat = "Bags";
                    this.lblTitle.Text = _cat + " & " + "Purses";
                    break;
                case "s":
                    _cat = "Shoes";
                    this.lblTitle.Text = _cat;
                    break;
                default:
                    break;
                    
                    
            }
            this.Image2.ImageUrl = "../Images/" + _cat + ".jpg";

            System.Collections.Generic.List<Entity.Product> _lstDt = _cat == "" ? prodBL.getProductsBL(ref mError) : prodBL.getProductSearchCliente_BL(search, _cat, _offer, ref mError); //
            if (_lstDt != null)
            {
                if (_lstDt.Count > 0)
                {
                    string _json = Newtonsoft.Json.JsonConvert.SerializeObject(_lstDt);
                    DataTable _dt = JsonConvert.DeserializeObject<DataTable>(_json);
                    DataListPaging(_dt);
                    numRows = _lstDt.Count;
                    ViewState["numRows"]= _lstDt.Count; 
                }
                else
                {
                    EmpDataList.DataSource = null;
                    EmpDataList.DataBind();
                }

            }
            else
            {
                return;
            }

            if (mError.code == "1" && showErr == 1)
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
    /// Function that obtains the information of articles that the client has in the shopping cart if he 
    /// is logged in.
    /// </summary>
    /// <param name="pIdUser"></param>
    /// <param name="pMError"></param>
    /// <returns></returns>
    protected int getCar(int pIdUser, ref mError pMError)
    {
        int _carItems = 0;
        carItemBL = new CarItemBL();
        _carItems = carItemBL.getTotalCarItemsBL(pIdUser, ref pMError);
        return _carItems;
    }

    #region Paginate
    // *******************************************************************************************
    /// <summary>
    /// Functions to: Paginate the DataList control
    /// </summary>
    /// <param name="pDt"></param>
    private void DataListPaging(DataTable pDt)
    {      
        PagedDataSource PD = new PagedDataSource();

        PD.DataSource = pDt.DefaultView;
        PD.PageSize = Convert.ToInt32(pagSizeCli);
        PD.AllowPaging = true;
        PD.CurrentPageIndex = CurrentPage;
        Button1.Enabled = !PD.IsFirstPage;
        Button2.Enabled = !PD.IsFirstPage;
        Button4.Enabled = !PD.IsLastPage;
        Button3.Enabled = !PD.IsLastPage;
        ViewState["TotalCount"] = PD.PageCount;
        EmpDataList.DataSource = PD;
        EmpDataList.DataBind();
        ViewState["PagedDataSurce"] = pDt;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {        
        CurrentPage = 0;        
        EmpDataList.DataSource = (DataTable)ViewState["PagedDataSurce"];
        EmpDataList.DataBind();
        ViewState["PageCount"] = CurrentPage;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
        numRows = (int)ViewState["numRows"];
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["PageCount"];
        CurrentPage -= 1;
        ViewState["PageCount"] = CurrentPage;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
        numRows = (int)ViewState["numRows"];
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["PageCount"];
        CurrentPage += 1;
        ViewState["PageCount"] = CurrentPage;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
        numRows = (int)ViewState["numRows"];
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["TotalCount"] - 1;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
        numRows = (int)ViewState["numRows"];
    }
    //End of Paginate the DataList control ********************************************

    /// <summary>
    /// Function that obtains the identifier of the client that is logged in.
    /// </summary>
    /// <returns></returns>
    protected int getUserId() {
        String _sessionClient = ConfigurationManager.AppSettings["sessionClient"];
        return Convert.ToInt32(Tools.GetCookie(_sessionClient).Value); 
    }
    #endregion
    /// <summary>
    /// Functoin that generate visit record of the day
    /// </summary>
    /// <param name=""></param>
    private void ValidateVisit(String pSessionClient) {
        string _date = String.Format("{0}/{1}/{2}", DateTime.Now.Day.ToString("00"), DateTime.Now.Month.ToString("00"), DateTime.Now.Year);
        UserBL _userBL;
        Visit _visit;
        if (!(Tools.ValidateCookie(cookClientVisit)) ) {
            Tools.CreateCookie(cookClientVisit, _date, _date);
            _userBL = new UserBL();
            _visit = new Visit();
            _visit.IdVisit = 0;
            _visit.IdUser = userId;
            _userBL.CRUDVisitBL(_visit, "INS", ref mError);
         }
    }
    /// <summary>
    /// Web method that invokes the logic layer to insert the chosen item into the shopping cart.
    /// </summary>
    /// <param name="pIdProd"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static object WebM_AddCarItem(String pIdProd)
    {
        int _showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
        String _sessionClient = ConfigurationManager.AppSettings["sessionClient"];
        mError _mError = new mError("", "");
        String _msg = "Product added to the car...";
        Boolean _success = false;
        //HttpContext context = HttpContext.Current;
        try {
            if (!Tools.ValidateCookie(_sessionClient))
            {
                _msg = "0-Session not started.";
            }
            else if (pIdProd!="") {
                
                CarItemBL _carItemBL = new CarItemBL();
                CarItem _carItem = new CarItem();
                _carItem.IdCarItem = 0;
                _carItem.IdUser = Convert.ToInt32(Tools.GetCookie(_sessionClient).Value);
                _carItem.IdProduct = Convert.ToInt32(pIdProd);
                _carItem.quantity = 1;

                _carItemBL.CRUDCarItemBL(_carItem, "INS",ref _mError);
                _success = true;
                if (_mError.code != "0" && _mError.code != "")
                {
                    _msg = _showErr == 1? "Error DB: " + _mError.mssg : "There was a failure, operation canceled";
                    _success = false;
                }
                
            }
            else { _msg = "Operation cancelled"; }

           
        }
        catch (Exception ex)
        {
            if (_mError.code == "0" && _showErr == 1)
                _msg = "Error BL: " + ex.Message;
            else if (_mError.code == "1" && _showErr == 1)
                _msg = "Error DB: " + _mError.mssg;
            else
                _msg = ex.Message;
           
        }
        return new { success = _success, model = "", msg = _msg };
    }

    /// <summary>
    /// Function that returns a number with their positions in thousands and decimals.
    /// </summary>
    /// <param name="pNumber"></param>
    /// <returns></returns>
    public string numberFormat(Object pNumber)
    {
        Int32 _number = Convert.ToInt32(pNumber);
        return string.Format("{0:#,##0}", _number);
    }

}