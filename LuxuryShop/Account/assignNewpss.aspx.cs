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
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String sessionUser = ConfigurationManager.AppSettings["sessionUser"];
    private readonly String stateUser = ConfigurationManager.AppSettings["stateCliente"];
    private static readonly String cookDateCreate = ConfigurationManager.AppSettings["cookDateCreate"];
    private string pagErr = ConfigurationManager.AppSettings["pagError"];
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
    /// Function that invokes the logic layer to validate the information.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Login_Click(object sender, EventArgs e)
    {
        String _pagErr = pagErr;

        try
        {

            String _msg = "";
            userBl = new UserBL();
            userBl.UPD_Pass_UserBL(email.Text.Trim(), password.Text.Trim(), ref mError);
            //btnLogin.Enabled = false; 
            user = userBl.getUserLoginBL(email.Text.Trim(), password.Text.Trim(),"Admin", ref mError);
           
                if (user.state == "Active")
                {
                    Tools.CreateCookie(cookUser, user.name + " " + user.lastName);
                    Tools.CreateCookie(sessionUser, user.IdUser.ToString());
                    Tools.CreateCookie(stateUser, user.state.ToString());
                    _msg = "<strong> Hello " + user.name + " " + user.lastName + " </strong> access successfully! ";
                    ShowMessage(_msg, WarningType.Success);
                    string _script = "$('#cover-spin').hide();";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
                    Response.Redirect(mpageRed, false);
            }
                else
                {
                     mError.mssg = "Your account is "+ user.state + ". Please contact the Web administrator."; 

                    //btnLogin.Enabled = true;
                    _msg = "<strong> Hello " + user.name + " " + user.lastName + " </strong>. " + mError.mssg;
                    ShowMessage(_msg, WarningType.Warning);
                    string _script = "$('#cover-spin').hide();";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "load", _script, true);
                }
           
            if (mError.code != "1" && mError.code != "")
            {
                Response.Write("Error DB: " + mError.mssg);
                ShowMessage("Error DB: " + mError.mssg, WarningType.Warning);
                //throw new Exception("Error DB: " + mError.mssg);
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
            Response.Redirect(_pagErr);
        }

    }


    
    protected void Email_Click(object sender, EventArgs e)
    {
        //userBl = new UserBL();
        //userBl.UPD_Pass_UserBL(email.Text.Trim(), password.Text.Trim(), ref mError);
        SendPasswordResetEmail(email.Text.Trim(), "Will", "123456");
        lblMessage.Text = "An email with instructions to reset your password is sent to your registered email";
    }

    private void SendPasswordResetEmail(string pToEmail, string pUserName, string pUniqueId)
    {
        String _msg = "Email sent successfully.";
        try
        {
        
            // MailMessage class is present is System.Net.Mail namespace
            MailMessage mailMessage = new MailMessage("guille22w@gmail.com", pToEmail);
        // StringBuilder class is present in System.Text namespace
        StringBuilder sbEmailBody = new StringBuilder();
        sbEmailBody.Append("Dear " + pUserName + ",<br/><br/>");
        sbEmailBody.Append("Please click on the following link to reset your password");
        sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost/Luxury/Account/getPassword.aspx?uId=" + pUniqueId);
        sbEmailBody.Append("<br/><br/>");
        sbEmailBody.Append("<b>Luxury Accessories</b>");

        mailMessage.IsBodyHtml = true;

        mailMessage.Body = sbEmailBody.ToString();
        mailMessage.Subject = "Reset Your Password";
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        smtpClient.Credentials = new System.Net.NetworkCredential()
        {
            UserName = "guille22w@gmail.com",
            Password = "camushaka*33*G"
        };

        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {           
            Session["error"] = ex.Message;
            _msg= ex.Message;
        }
        lblMessage.Text = _msg;
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