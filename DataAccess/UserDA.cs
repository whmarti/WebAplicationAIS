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

namespace DataAccess
{
    public class UserDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();

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
                        _newUser.pass = Convert.ToString(_rstReader["pass"]).Trim();
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
                        _newUser.pass = Convert.ToString(_rstReader["pass"]).Trim();
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
            string _sql = "select * from UserWeb where email=@email and pass=@pass and type=@type";            

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    _cmd.Parameters.AddWithValue("@email", pUser);
                    _cmd.Parameters.AddWithValue("@pass", pPass);
                    _cmd.Parameters.AddWithValue("@type", pType);
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
                        _user.pass = Convert.ToString(_rstReader["pass"]).Trim();
                        _user.date = Convert.ToString(_rstReader["date"]).Trim();

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
                        _user.pass = Convert.ToString(_rstReader["pass"]).Trim();

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
    }
}
