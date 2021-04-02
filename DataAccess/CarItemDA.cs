/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage directly in the Database, 
 * read operations and data persistence in the CarItem table.
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
    public class CarItemDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();

       
        
        /// <summary>
        /// Request for CarItem table information to the Data Base.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<CarItem> getCarItems(int pIdUser, ref mError pMError)
        {
            List<CarItem> _carItems = new List<CarItem>();
            
            String _sql = "Select i.IdCarItem,i.quantity,i.price priceProd,i.idUser,p.*,c.name as category from CarItem i Inner Join product p on  i.IdProduct=p.IdProduct  inner join Category c on p.IdCategory=C.IdCategory where i.IdUser=@pIdUser";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@pIdUser", pIdUser);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    while (_rstReader.Read())
                    {
                        CarItem _carItem = new CarItem();
                        
                        _carItem.IdCarItem = Convert.ToInt32(_rstReader["IdCarItem"]);
                        _carItem.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _carItem.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _carItem.quantity = Convert.ToInt32(_rstReader["quantity"]);
                        _carItem.price = (float)Convert.ToDouble(_rstReader["priceProd"]);
                        _carItem.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _carItem.name = Convert.ToString(_rstReader["name"]).Trim();
                        _carItem.color = Convert.ToString(_rstReader["color"]).Trim();
                        _carItem.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _carItem.size = Convert.ToString(_rstReader["size"]).Trim();
                        _carItem.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _carItem.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _carItem.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _carItem.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _carItem.category = Convert.ToString(_rstReader["category"]).Trim();
                        _carItem.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                       
                        _carItems.Add(_carItem);

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _carItems;

        }

      
        /// <summary>
        /// Request for CarItem table  information with a supplied data.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<CarItem> getCarItemSearch(int pIdUser, String pSearch, ref mError pMError)
        {
            List<CarItem> _carItems = new List<CarItem>();
            
            String _sql = "Select i.IdCarItem,i.quantity,i.price priceProd,p.*,c.name as category from CarItem i Inner Join product p on  i.IdProduct=p.IdProduct  inner join Category c on p.IdCategory=C.IdCategory where i.IdUser=@pIdUser and ( p.name like @pSearch OR p.brand like @pSearch OR c.name like @pSearch )";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pIdUser", pIdUser);
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    _rstReader = _cmd.ExecuteReader();
                    _carItems = new List<CarItem>();
                    while (_rstReader.Read())
                    {
                        CarItem _carItem = new CarItem();
                        
                        _carItem.IdCarItem = Convert.ToInt32(_rstReader["IdCarItem"]);
                        _carItem.quantity = Convert.ToInt32(_rstReader["quantity"]);
                        _carItem.price = (float)Convert.ToDouble(_rstReader["priceProd"]);
                        _carItem.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _carItem.name = Convert.ToString(_rstReader["name"]).Trim();
                        _carItem.color = Convert.ToString(_rstReader["color"]).Trim();
                        _carItem.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _carItem.size = Convert.ToString(_rstReader["size"]).Trim();
                        _carItem.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _carItem.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _carItem.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _carItem.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _carItem.category = Convert.ToString(_rstReader["category"]).Trim();
                        _carItems.Add(_carItem);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _carItems;

        }

        
        /// <summary>
        /// Function to persist data in the table CarItem.
        /// </summary>
        /// <param name="pCarItem"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCarItem(CarItem pCarItem,String pOper,ref mError pMError)
        {
            string _sql = "Sproc_CRUD_CarItem";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdCarItem",  Value = pCarItem.IdCarItem, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdUser",  Value = pCarItem.IdUser, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdProduct",  Value = pCarItem.IdProduct, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@quantity",  Value = pCarItem.quantity, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@price",  Value = pCarItem.price, Direction = System.Data.ParameterDirection.Input },                    
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
        /// Function to persist data in the Order table.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCarOrder(int pIdUser,String pOper, ref mError pMError)
        {
            string _sql = "Sproc_CRUD_Order";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdOrder",  Value = 0, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdUser",  Value = pIdUser, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@value",  Value = 0, Direction = System.Data.ParameterDirection.Input },
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
        /// Request for quantity of items in the car of a specific user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int getTotalCarItems(int pIdUser, ref mError pMError)
        {
            List<CarItem> _carItems = new List<CarItem>();
            int _res = 0;
            String _sql = "Select count(i.IdCarItem)from CarItem i where i.IdUser=@pIdUser";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);                   
                    _cmd.Parameters.AddWithValue("@pIdUser", pIdUser);
                    _conn.Open();
                    _res  = Convert.ToInt32(_cmd.ExecuteScalar());
                   
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _res;

        }

    
        
    }
}
