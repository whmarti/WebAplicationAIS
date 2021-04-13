/************************************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Page function: Page elaborated to allow an client of Luxury S.A. enter with his credentials
 * to the Web site module of the application to be able to make a purchase.
 * Company: AIS - NZ
 * */
using Business;
using Entity;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Website_Login : System.Web.UI.Page
{
    private readonly int showErr = Convert.ToInt32(ConfigurationManager.AppSettings["DataBaseErr"]);
    private readonly String pagDefaultM = ConfigurationManager.AppSettings["pagDefaultM"];
    private readonly String pagLogin = ConfigurationManager.AppSettings["pagLogin"];
    //private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    //private readonly String sessionUser = ConfigurationManager.AppSettings["sessionUser"];
    //private readonly String stateUser = ConfigurationManager.AppSettings["stateCliente"];
    //private static readonly String cookDateCreate = ConfigurationManager.AppSettings["cookDateCreate"];
    private string pagErr = ConfigurationManager.AppSettings["pagError"];
    private string admEmail = ConfigurationManager.AppSettings["admEmail"];
    private string admEmapsw = ConfigurationManager.AppSettings["admEmapsw"];
    private string serverApp = ConfigurationManager.AppSettings["serverApp"];
    private mError mError;
    private String mpageRed, option;
    private UserBL userBl;
    private User user;   
    protected void Page_Load(object sender, EventArgs e)
    {
        mError = new mError("", "");
        mpageRed = Request.QueryString["pag"] != null ? Request.QueryString["pag"].ToString() : pagDefaultM;
        if (!IsPostBack)
        {

            if (Request.QueryString["opt"] != null)
            {
                option = Request.QueryString["opt"];               
            }

        }

    }


    /// <summary>
    /// Function that calls the mailing routine with credentials brought from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Email_Click(object sender, EventArgs e)
    {
        userBl = new UserBL();
        try
        {
            lkb_Email.Visible = false;
            String _idUser = userBl.ResetPasswordRequestsBl(email.Text.Trim(),"Website", ref mError);
            if (mError.code == "") {
                
                lblMessage.Text = "An email with instructions to reset your password has been sent to your registered email. Please validate the link sent in it. ";
                Response.AddHeader("REFRESH", "6;URL='" + pagLogin + "'");
            }
            else throw new Exception("Error DB: " + mError.mssg); ;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            lblMessage.Text = ex.Message;
           
        }
        lkb_Email.Visible = true;
    }

    /// <summary>
    /// Function that sends mail to the user with the instructions and credentials provided.
    /// </summary>
    /// <param name="pToEmail"></param>
    /// <param name="pUserName"></param>
    /// <param name="pUniqueId"></param>
    private void SendPasswordResetEmail(string pToEmail, string pUserName, string pUniqueId)
    {
        String _msg = "Email sent successfully.";
        try
        {
        
        
        MailMessage mailMessage = new MailMessage("guille22w@gmail.com", pToEmail);       
        StringBuilder sbEmailBody = new StringBuilder();
        sbEmailBody.Append("Dear " + pUserName + ",<br/><br/>");
        sbEmailBody.Append("Please click on the following link to reset your password");
        sbEmailBody.Append("<br/>"); sbEmailBody.Append(serverApp + "/Account/getPassword.aspx?uId=" + pUniqueId); 
        sbEmailBody.Append("<br/><br/>");
        sbEmailBody.Append("<b>Luxury Accessories</b>");

        mailMessage.IsBodyHtml = true;

        mailMessage.Body = sbEmailBody.ToString();
        mailMessage.Subject = "Reset Your Password";
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        smtpClient.Credentials = new System.Net.NetworkCredential()
        {
            UserName = admEmail,
            Password = admEmapsw
        };

        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {           
            Session["error"] = ex.Message;
            _msg= ex.Message;
            lblMessage.Text = _msg;
        }
        
    }


    /// <summary>
    /// Function to send an email..
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