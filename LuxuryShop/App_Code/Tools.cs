/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage different common functions 
 * that may be required on any page of the site.
 * Company: AIS - NZ
 * */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;


public static class Tools
{
    private static readonly String cookDateCreate = ConfigurationManager.AppSettings["cookDateCreate"];
    
    // <summary>
    /// Creates a cookie with specified name
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static void CreateCookie(string pCookieName, string pValue)
    {
        HttpContext.Current.Response.Cookies[pCookieName].Value = pValue;
        HttpContext.Current.Response.Cookies[pCookieName].Expires = Convert.ToDateTime(cookDateCreate);
    }

    // <summary>
    /// Creates a cookie with specified name and date
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static void CreateCookie(string pCookieName, string pValue, string pDate)
    {
        HttpContext.Current.Response.Cookies[pCookieName].Value = pValue;
        HttpContext.Current.Response.Cookies[pCookieName].Expires = Convert.ToDateTime(pDate);
        //HttpContext.Current.Response.Cookies[pCookieName].Expires = Convert.ToDateTime("04/04/2021");
    }

    // <summary>
    /// Assigns a new value to a cookie 
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static void SetCookie(string pCookieName, string pValue)
    {
        HttpContext.Current.Response.Cookies[pCookieName].Value = pValue;
    }


    // <summary>
    /// Validate the existence of a cookie with a specified name
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static Boolean ValidateCookie(String pCook)
    {
        if (GetCookie(pCook) == null) return false;       
            return true;
    }

    // <summary>
    /// Returns a cookie given a name.
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static HttpCookie GetCookie(string cookieName)
    {
        HttpCookie rqstCookie = HttpContext.Current.Request.Cookies.Get(cookieName);
      
        if (rqstCookie != null && !String.IsNullOrEmpty(rqstCookie.Value))
        {           
            return rqstCookie;
        }
        else
        {
            return null;
        }
    }

    // <summary>
    /// Function to validate an active user (Session Variable):
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static Boolean ValidIdUser(Object pIdUser)
    {
        
        if (pIdUser != null && !String.IsNullOrEmpty(pIdUser.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // <summary>
    /// Deletes a cookie with specified name.
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static void DeleteCookie(string cookieName)
    {
        if (HttpContext.Current.Request.Cookies[cookieName] == null)
            return; //cookie doesn't exist

        var _cok = new HttpCookie(cookieName)
        {
            Expires = DateTime.Now.AddDays(-1)
        };
        HttpContext.Current.Response.Cookies.Add(_cok);
    }

    // <summary>
    /// Deletes a Session with specified name.
    /// </summary>
    /// <param name="cookieName">cookie name</param>
    public static void DeleteSession(string sessionName)
    {
        if (HttpContext.Current.Session[sessionName] == null)
            return; 

        HttpContext.Current.Session.Remove(sessionName); 
    }


}