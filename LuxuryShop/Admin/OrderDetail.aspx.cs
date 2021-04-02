/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to print a purchase invoice for a specific Purchase Order.
 * Company: AIS - NZ
 * */

using Business;
using Entity;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

public partial class Admin_OrderDetail : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagErr = ConfigurationManager.AppSettings["pagInvoiceError"];
    public readonly String dirPhoto = ConfigurationManager.AppSettings["dirPhoto"];
    private OrderBL orderBL;
    private mError mError;
    public Order order;
    public float totalDiscount = 0, tax = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        mError = new mError("", "");
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)

                    FillData(Convert.ToInt32(Request.QueryString["Id"]), mError);
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
    }

    /// <summary>
    /// Function that invokes the logic layer to to bring the Purchase Order information with its detail.
    /// </summary>
    protected void FillData(int pIdOrder, mError pMError)
    {
        orderBL = new OrderBL();
        order = new Order();
        order = orderBL.getOrderByOrderIdBL(pIdOrder, ref pMError);
        totalDiscount = order._products.Where(y=>y.discount>0 ).Sum(x => x.discount*x.quantity);
        tax = (order.value * (float)(0.18));
    }

    /// <summary>
    /// Function that returns the ID of the order in generic 6-digit format.
    /// </summary>
    /// <param name="pIdOrder"></param>
    /// <returns></returns>
    public string invoiceFormat(int pIdOrder) {
        string _res = "00000"+ pIdOrder;
        _res = ReverseString(_res).Substring(0,5);
        return ReverseString(_res);
    }

    /// <summary>
    /// Function that returns an inverted string  .
    /// </summary>
    /// <param name="myStr"></param>
    /// <returns></returns>
    private static string ReverseString(string myStr)
    {
        char[] myArr = myStr.ToCharArray();
        Array.Reverse(myArr);
        return new string(myArr);
    }

    /// <summary>
    /// Function that returns a number with their positions in thousands.
    /// </summary>
    /// <param name="pNumber"></param>
    /// <returns></returns>
    public string numberFormat(float pNumber)
    {        
        return pNumber.ToString("N", CultureInfo.InvariantCulture); ;
    }


}