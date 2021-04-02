/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to manage the client shopping cart online.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Website_Car : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLoginM"];
    private readonly String pagErr = "../" + ConfigurationManager.AppSettings["pagError"];
    private readonly String cookClient = ConfigurationManager.AppSettings["cookClient"];
    private readonly String cookAddrClient = ConfigurationManager.AppSettings["cookAddrClient"];
    private readonly String Authentication = ConfigurationManager.AppSettings["Authentication"]; 
    public String dirPhoto = ConfigurationManager.AppSettings["dirPhoto"], addrCliente = "";
    private readonly String sessionClient = ConfigurationManager.AppSettings["sessionClient"];
    private readonly String pagSizeCli = ConfigurationManager.AppSettings["pagSizeCli"];
    private const String currentPage = "Car.aspx";
    private ProductBL prodBL;
    private CarItemBL carItemBL;
    private int CurrentPage;
    String search="",idClient, directory;
    private mError mError;
    //private List<CarItem> carItems;
    public int nCarItems = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
       
        if ((!(Tools.ValidateCookie(cookClient)) || !(Tools.ValidateCookie(sessionClient)) ) && Authentication == "1")
            Response.Redirect(pagLogin + "?pag=" + currentPage);
        idClient = Tools.GetCookie(sessionClient).Value;
        addrCliente = Tools.GetCookie(cookAddrClient).Value;

        Session["urlOld"] = currentPage;
       
        try
        {
            if (!IsPostBack)
            {
                FillData(idClient);
                //carItems=getCar(Convert.ToInt32(idClient));
                ViewState["PageCount"] = 0;
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
    /// Function that invokes the logic layer to fetch the table elements from the database and fill the shopping cart (Datalist with the information.
    /// </summary>
    protected void FillData(String pIdUser)
    {
        
        carItemBL = new CarItemBL();
        System.Collections.Generic.List<CarItem> _lstDt = carItemBL.getCarItemsBL(Convert.ToInt32(idClient),ref mError) ; 
        if (_lstDt != null)
        {
            if (_lstDt.Count > 0)
            {               
                string _json = Newtonsoft.Json.JsonConvert.SerializeObject(_lstDt);
                System.Collections.Generic.List<CarItemW> _carL = JsonConvert.DeserializeObject<List<CarItemW>>(_json);
                DataTable _dt = JsonConvert.DeserializeObject<DataTable>(_json);
                DataListPaging(_dt);
                nCarItems = _lstDt.Count;
            }
            
        }
        else
        {
            return;
        }
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
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["PageCount"];
        CurrentPage -= 1;
        ViewState["PageCount"] = CurrentPage;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["PageCount"];
        CurrentPage += 1;
        ViewState["PageCount"] = CurrentPage;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["TotalCount"] - 1;
        DataListPaging((DataTable)ViewState["PagedDataSurce"]);
    }
    //End of Paginate the DataList control ********************************************
    #endregion


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
    /// Web method that invokes the logic layer to modify the quantity of the chosen item into the shopping cart.
    /// </summary>
    /// <param name="pIdProd"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static object WebM_UpdCarItem(String pIdCarItem, int pQty, String pOper)
    {
        int _showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
        String _sessionClient = ConfigurationManager.AppSettings["sessionClient"];
        mError _mError = new mError("", "");
        String _msg = "Product updated to the car...";
        Boolean _success = false;
        //HttpContext context = HttpContext.Current;
        try
        {
            if (!Tools.ValidateCookie(_sessionClient))
            {
                _msg = "0-Session not started.";
            }
            else if (pIdCarItem != "")
            {

                CarItemBL _carItemBL = new CarItemBL();
                CarItem _carItem = new CarItem();
                _carItem.IdCarItem = Convert.ToInt32(pIdCarItem);
                _carItem.IdUser = Convert.ToInt32(Tools.GetCookie(_sessionClient).Value);
                _carItem.IdProduct = 0;
                _carItem.quantity = pQty;

                _carItemBL.CRUDCarItemBL(_carItem, pOper, ref _mError);
                _success = true;
                if (_mError.code != "0" && _mError.code != "")
                {
                    _msg = _showErr == 1 ? "Error DB: " + _mError.mssg : "There was a failure, operation canceled";
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
    /// Web method that invokes the logic layer to carry out the purchase.
    /// </summary>
    /// <param name="pIdProd"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static object WebM_createOrder(String pIdUser)
    {
        int _showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
        String _sessionClient = ConfigurationManager.AppSettings["sessionClient"];
        mError _mError = new mError("", "");
        String _msg = "Purchase made successfully...";
        Boolean _success = false;
        //HttpContext context = HttpContext.Current;
        try
        {
            if (!Tools.ValidateCookie(_sessionClient))
            {
                _msg = "0-Session not started.";
            }
            else if (pIdUser != "")
            {

                CarItemBL _carItemBL = new CarItemBL();
                CarItem _carItem = new CarItem();
                _carItem.IdCarItem = 0;
                _carItem.IdUser = Convert.ToInt32(pIdUser); // Convert.ToInt32(Tools.GetCookie(_sessionClient).Value);
                _carItem.IdProduct = 0;
                _carItem.quantity = 0;

                _carItemBL.CRUDCarOrderBL(Convert.ToInt32(pIdUser), "INS", ref _mError);
                _success = true;
                if (_mError.code != "0" && _mError.code != "")
                {
                    _msg = _showErr == 1 ? "Error DB: " + _mError.mssg : "There was a failure, operation canceled";
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


   

}