<%@ Application Language="C#" %>
<%@ Import Namespace="LuxuryShop" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
    private readonly String cookUser = ConfigurationManager.AppSettings["cookUser"];
    private readonly String usrAccountAuth = ConfigurationManager.AppSettings["usrAccountAuth"];
    protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
    {
        //if (!(Tools.ValidateCookie(cookUser)))
        //   if (Tools.ValidateCookie(usrAccountAuth))
        //      Tools.DeleteCookie(usrAccountAuth);


        if (FormsAuthentication.CookiesSupported == true)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                try
                {
                    //let us take out the username now                
                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    string roles = string.Empty;

                    roles = "Admin";

                    //Let us set the Pricipal with our user specific details
                    HttpContext.Current.User  = new System.Security.Principal.GenericPrincipal(
                      new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                }
                catch (Exception)
                {
                    //somehting went wrong
                }
            }
        }
    }

    // protected void Application_EndRequest()   
    //{   
    //    var context = new HttpContextWrapper(Context);   
    //    if (context.Response.StatusCode == 401 )   
    //    {   
    //        context.Response.Redirect("~/401.aspx");   
    //    }   
    //}   

    //protected void Application_EndRequest(Object sender, EventArgs e)
    //{
    //    HttpContext context = HttpContext.Current;
    //if (context.Response.Status.Substring(0, 3).Equals("401"))
    //{
    //context.Response.ClearContent();
    //string str = "<script language='javascript'>self.location='~/401.aspx' </" + "script>";
    //context.Response.Write(str);
    //}
    //}

    //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    //{
    //    if (!Request.IsAuthenticated)
    //    {
    //        Response.Redirect("~/401.aspx");
    //    }
    //}
</script>
