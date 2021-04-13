/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage directly in the Database, 
 * read operations and data persistence in the User table.
 * Company: AIS - NZ
 * */
using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace DataAccess
{
    public class UserDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();
        private static string serverApp = ConfigurationManager.AppSettings["serverApp"].ToString();
        private static string serverReset = ConfigurationManager.AppSettings["serverReset"].ToString();
        private static string nameApp = ConfigurationManager.AppSettings["nameApp"].ToString();
        private string admEmail = ConfigurationManager.AppSettings["admEmail"];
        private string admEmapsw = ConfigurationManager.AppSettings["admEmapsw"];
        private readonly String EncryptClient = ConfigurationManager.AppSettings["EncryptClient"];
        private readonly String Passkey = ConfigurationManager.AppSettings["Passkey"];

        /// <summary>
        /// Request for User table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUsers(ref mError pMError)
        {
            List<User> _users = null;
            SqlDataReader _rstReader = null;
            string _sql = "select * from UserWeb";            

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    _conn.Open();
                    _rstReader = cmd.ExecuteReader();
                    _users = new List<User>();
                    while (_rstReader.Read())
                    {

                        User _newUser = new User();
                        _newUser.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _newUser.type = Convert.ToString(_rstReader["type"]).Trim();
                        _newUser.name = Convert.ToString(_rstReader["name"]).Trim();
                        _newUser.lastName = Convert.ToString(_rstReader["lastName"]).Trim();
                        _newUser.email = Convert.ToString(_rstReader["email"]).Trim();
                        _newUser.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        //_newUser.pass = Convert.ToString(_rstReader["pass"]).Trim();
                        _newUser.date = Convert.ToString(_rstReader["date"]).Trim();
                        _users.Add(_newUser);

                    }
                    _rstReader.Close();
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _users;
        }


        /// <summary>
        /// Request for User table information to the Data Base.
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUsers(String pType, ref mError pMError)
        {
            List<User> _users = null;
            SqlDataReader _rstReader = null;
            string _sql = "select * from UserWeb where type= @pType";

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pType",  pType );
                    _rstReader = _cmd.ExecuteReader();
                    _users = new List<User>();
                    while (_rstReader.Read())
                    {

                        User _newUser = new User();
                        _newUser.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _newUser.type = Convert.ToString(_rstReader["type"]).Trim();
                        _newUser.name = Convert.ToString(_rstReader["name"]).Trim();
                        _newUser.lastName = Convert.ToString(_rstReader["lastName"]).Trim();
                        _newUser.email = Convert.ToString(_rstReader["email"]).Trim();
                        _newUser.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        //_newUser.pass = Convert.ToString(_rstReader["pass"]).Trim();
                        _newUser.date = Convert.ToString(_rstReader["date"]).Trim();
                        _newUser.state = Convert.ToString(_rstReader["state"]).Trim();
                        _users.Add(_newUser);

                    }
                    _rstReader.Close();
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _users;
        }

        /// <summary>
        /// Request for most N visitors in a period of time.
        /// </summary>
        /// <param name="pTop"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getTopVisitors(String pTop, String pDateFrom, String pDateTo, ref mError pMError)
        {
            String _sqlTop = pTop != "" ? " Top " + pTop : "";
            String _sqlDate = pDateFrom!=""? " and v.[date] between '"+ pDateFrom + "' and '"+ pDateTo+"' " : "";
            List<User> _users = null;
            SqlDataReader _rstReader = null;
            string _sql = "select "+ _sqlTop+ "  v.IdUser,Count(v.IdUser) visits, u.name,u.lastName, u.email,u.phone from visit v inner join UserWeb u on v.IdUser= u.IdUser where v.IdVisit > 0 " + _sqlDate+ " Group by v.IdUser,name,lastName,u.email,u.phone Order by Count(v.IdUser) desc";

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    _conn.Open();
                    //_cmd.Parameters.AddWithValue("@pType", pType);
                    _rstReader = _cmd.ExecuteReader();
                    _users = new List<User>();
                    while (_rstReader.Read())
                    {

                        User _newUser = new User();
                        _newUser.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _newUser.name = Convert.ToString(_rstReader["name"]).Trim();
                        _newUser.lastName = Convert.ToString(_rstReader["lastName"]).Trim();
                        _newUser.email = Convert.ToString(_rstReader["email"]).Trim();
                        _newUser.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        _newUser.address = Convert.ToString(_rstReader["visits"]).Trim();
                        _users.Add(_newUser);

                    }
                    _rstReader.Close();
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _users;
        }


        /// <summary>
        /// Function to authenticate the credentials of a user who intends to enter the system.
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pPass"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public User getUserLoginBL(String pUser, String pPass, String pType, ref mError pMError)
        {
            User _user = null;
            SqlDataReader _rstReader = null;
            string _sql = "select * from UserWeb where email=@email and type=@type and pass=HASHBYTES('SHA2_512', @pass+CAST(@salt AS NVARCHAR(36))) ";            

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    _cmd.Parameters.AddWithValue("@email", pUser);
                    _cmd.Parameters.AddWithValue("@pass", pPass);
                    _cmd.Parameters.AddWithValue("@type", pType);
                    _cmd.Parameters.AddWithValue("@salt", EncryptClient);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {

                        _user = new User();
                        _user.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _user.type = Convert.ToString(_rstReader["type"]).Trim();
                        _user.name = Convert.ToString(_rstReader["name"]).Trim();
                        _user.lastName = Convert.ToString(_rstReader["lastName"]).Trim();
                        _user.email = Convert.ToString(_rstReader["email"]).Trim();
                        _user.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        _user.address = Convert.ToString(_rstReader["address"]).Trim();
                        //_user.pass = Convert.ToString(_rstReader["pass"]).Trim();
                        _user.date = Convert.ToString(_rstReader["date"]).Trim();
                        _user.state = Convert.ToString(_rstReader["state"]).Trim();

                    }
                    else
                    {
                        pMError.code = "1";
                        pMError.mssg = "Incorrect credentials, try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _user;

        }

        /// <summary>
        /// Request for information on a specific user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public User getUser(int pIdUser, ref mError pMError)
        {
            User _user = new User();
            String _sql = "Select p.* from userWeb p where p.IdUser=@idUser";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@idUser", pIdUser);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {
                        _user.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _user.type = Convert.ToString(_rstReader["type"]);
                        _user.name = Convert.ToString(_rstReader["name"]).Trim();
                        _user.lastName = Convert.ToString(_rstReader["lastName"]);
                        _user.email = Convert.ToString(_rstReader["email"]).Trim();
                        _user.address = Convert.ToString(_rstReader["address"]).Trim();
                        _user.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        _user.pass = Passkey;// Convert.ToString(_rstReader["password"]).Trim();
                        _user.state = Convert.ToString(_rstReader["state"]).Trim();

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _user;

        }

        /// <summary>
        ///  Request for information on the users that meet a search criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUserSearch(String pType, String pSearch, ref mError pMError)
        {
            List<User> _users = new List<User>();           
            String _sql = "Select u.* from userWeb u  where (u.name like @pSearch OR u.lastName like @pSearch) and type= @pType";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    _cmd.Parameters.AddWithValue("@pType", pType );
                    _rstReader = _cmd.ExecuteReader();
                    _users = new List<User>();
                    while (_rstReader.Read())
                    {
                        User _user = new User();
                        _user.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _user.type = Convert.ToString(_rstReader["type"]);
                        _user.name = Convert.ToString(_rstReader["name"]).Trim();
                        _user.lastName = Convert.ToString(_rstReader["lastName"]);
                        _user.email = Convert.ToString(_rstReader["email"]).Trim();
                        _user.address = Convert.ToString(_rstReader["address"]).Trim();
                        _user.phone = Convert.ToString(_rstReader["phone"]).Trim();
                        _user.pass = Convert.ToString(_rstReader["pass"]).Trim();
                        _user.state = Convert.ToString(_rstReader["state"]).Trim();
                        _users.Add(_user);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _users;

        }

        /// <summary>
        /// Function to persist data in the User table.
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDUser(User pUser, String pOper, ref mError pMError)
        {
            string _sql = "Sproc_CRUD_User";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdUser",  Value = pUser.IdUser, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@type",  Value = pUser.type, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@name",  Value = pUser.name, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@lastName",  Value = pUser.lastName, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@email",  Value = pUser.email, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@phone",  Value = pUser.phone, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@address",  Value = pUser.address, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@pass",  Value = pUser.pass, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@state",  Value = pUser.state, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@salt",  Value = EncryptClient, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@passkey",  Value = Passkey, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@oper",  Value = pOper, Direction = System.Data.ParameterDirection.Input },

                    new SqlParameter(_strOut, SqlDbType.NChar) { Size = 300, Direction = System.Data.ParameterDirection.Output }

            };

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }


                    _conn.Open();
                    cmd.ExecuteNonQuery();

                    _res = cmd.Parameters[_strOut].Value.ToString();
                    _conn.Close();
                    _pos = _res.IndexOf("1;", 0);
                    if (_pos >= 0)
                    {
                        //_pos = Convert.ToInt32(_res.Substring(2));
                        pMError.code = "1";
                        pMError.mssg = _res.Substring(2);
                    }
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _pos;
        }

        public int UPD_Pass_User(String pEmail, String pPass, ref mError pMError)
        {
            string _sql = "Sproc_UPD_Pass_User";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                   
                    new SqlParameter { ParameterName = "@email",  Value = pEmail, Direction = System.Data.ParameterDirection.Input },                    
                    new SqlParameter { ParameterName = "@pass",  Value = pPass, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@salt",  Value = EncryptClient, Direction = System.Data.ParameterDirection.Input },

                    new SqlParameter(_strOut, SqlDbType.NChar) { Size = 300, Direction = System.Data.ParameterDirection.Output }

            };

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }


                    _conn.Open();
                    cmd.ExecuteNonQuery();

                    _res = cmd.Parameters[_strOut].Value.ToString();
                    _conn.Close();
                    _pos = _res.IndexOf("1;", 0);
                    if (_pos >= 0)
                    {                        
                        pMError.code = "1";
                        pMError.mssg = _res.Substring(2);
                    }
                    else
                        _pos= Convert.ToInt32(_res.Substring(2));
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _pos;
        }

        /// <summary>
        /// Function to persist data in the Visit table.
        /// </summary>
        /// <param name="pVisit"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDVisit(Visit pVisit, String pOper, ref mError pMError)
        {
            string _sql = "Sproc_CRUD_Visit";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdVisit",  Value = pVisit.IdVisit, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdUser",  Value = pVisit.IdUser, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@oper",  Value = pOper, Direction = System.Data.ParameterDirection.Input },

                    new SqlParameter(_strOut, SqlDbType.NChar) { Size = 300, Direction = System.Data.ParameterDirection.Output }

            };

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }


                    _conn.Open();
                    cmd.ExecuteNonQuery();

                    _res = cmd.Parameters[_strOut].Value.ToString();
                    _conn.Close();
                    _pos = _res.IndexOf("1;", 0);
                    if (_pos >= 0)
                    {
                        pMError.code = "1";
                        pMError.mssg = _res.Substring(2);
                    }
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _pos;
        }

        /// <summary>
        /// Function to register a password change request.
        /// </summary>
        /// <param name="pVisit"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public String ResetPasswordRequests(String pEmail, String pType, ref mError pMError)
        {
            string _sql = "Sproc_ResetPassword", _guid="", _userName="";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;            
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@email",  Value = pEmail, Direction = System.Data.ParameterDirection.Input },

                    new SqlParameter(_strOut, SqlDbType.NChar) { Size = 300, Direction = System.Data.ParameterDirection.Output }

            };

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }


                    _conn.Open();
                    //cmd.ExecuteNonQuery();
                    SqlDataReader _rstReader = null;

                    _rstReader = cmd.ExecuteReader();
                   

                    while (_rstReader.Read())
                    {
                        if (Convert.ToBoolean(_rstReader["ReturnCode"]))
                        {
                            _guid = _rstReader["UniqueId"].ToString();
                            _userName = _rstReader["userName"].ToString();
                        }                       
                    }
                    _rstReader.Close();
                    _res = cmd.Parameters[_strOut].Value.ToString();
                    _conn.Close();
                    _pos = _res.IndexOf("1;", 0);
                    SendPasswordResetEmail(pEmail, _userName, _guid, pType, ref pMError);
                    if (_pos >= 0)
                    {
                        pMError.code = "1";
                        pMError.mssg = _res.Substring(2);
                    }
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _guid;  // _pos;
        }

        /// <summary>
        /// Function that sends mail to the user with the instructions and credentials provided to change the password.
        /// </summary>
        /// <param name="pToEmail"></param>
        /// <param name="pUserName"></param>
        /// <param name="pUniqueId"></param>
        private void SendPasswordResetEmail(String pToEmail, String pUserName, String pUniqueId, String pType, ref mError pMError)
        {
            
            try
            {


                MailMessage mailMessage = new MailMessage("guille22w@gmail.com", pToEmail);
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Dear Mr(s). " + pUserName + ",<br/><br/>");
                sbEmailBody.Append("Please click on the following link to verify your password reset request: ");
                sbEmailBody.Append("<br/>"); sbEmailBody.Append(serverReset + nameApp + pType + "/getPassword.aspx?uId=" + pUniqueId);
                sbEmailBody.Append("<br/><br/>");
                sbEmailBody.Append("<b>Luxury Accessories</b>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Reset Your Password - Luxury Accessories";
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
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }

        }



        /// <summary>
        /// Get the user Id encrypted to request change of password.
        /// </summary>
        /// <param name="pEmailUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public byte[] getUserIdChgPass(String pEmailUser, ref mError pMError)
        {
            String _idUser = ""; byte[] binaryString=null;
            String _sql = "Select HASHBYTES('SHA2_256', CAST(IdUser AS NVARCHAR(6))+CAST(@salt AS NVARCHAR(36))) IdUser, u.name+' ' + u.lastName name from userWeb u where u.email=@email";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@email", pEmailUser);
                    _cmd.Parameters.AddWithValue("@salt", EncryptClient);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {
                        //_idUser =_rstReader["IdUser"].ToString() ;
                       
                       binaryString = (byte[])_rstReader["IdUser"];
                        pMError.mssg = Convert.ToString(_rstReader["name"]);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return binaryString; // _idUser;

        }

        /// <summary>
        /// Confirms the validity of the  user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Boolean validateUserIdChgPass(String pIdUser, ref mError pMError)
        {
            Boolean _idUser = false;
            String _sql = "Select r.IdUser, u.name +' '+ u.lastName userName, u.email from ResetPasswordRequests r inner join UserWeb u on r.IdUser=u.IdUser where r.IdReset=@IdUser";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@IdUser", pIdUser);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {
                        _idUser = true;
                        pMError.mssg = _rstReader["email"].ToString().Trim();
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _idUser;

        }

        /// <summary>
        /// Get the user email to allow change of password.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public String getUserEmail(String pIdUser, ref mError pMError)
        {
            String _emailUser = "";
            String _sql = "Select email from userWeb u where HASHBYTES('SHA2_256', CAST(IdUser AS NVARCHAR(6))+CAST(@salt AS NVARCHAR(36)))=@IdUser";

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@IdUser", pIdUser);
                    _cmd.Parameters.AddWithValue("@salt", EncryptClient);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {
                        _emailUser = Convert.ToString(_rstReader["IdUser"]) + ";" + Convert.ToString(_rstReader["name"]);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _emailUser;

        }
    }
}
