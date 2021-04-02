/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage directly in the Database, 
 * read operations and data persistence in the Order table.
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
    public class OrderDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();

        
        /// <summary>
        ///  Request for Order table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Order> getOrders(int? pIdClient,ref mError pMError)
        {
            List<Order> _orders = new List<Order>();
            String _sqlClient = !(String.IsNullOrEmpty(pIdClient.ToString())) ? " and o.IdUser="+pIdClient.ToString() : "";
            String _sql = "select o.IdOrder,o.IdUser,o.value,FORMAT (o.date, 'MMM dd yyyy') dateF,CONVERT(VARCHAR(10), o.date, 103) + ' '  + convert(VARCHAR(8), o.date, 14) date,CASE WHEN o.payType=1 THEN 'visa' WHEN o.payType=2 THEN 'amex' WHEN o.payType = 3 THEN 'mastercard' WHEN o.payType = 4 THEN 'diners-club' WHEN o.payType = 5 THEN 'paypal' ELSE 'discover' END as payType, u.name + ' ' + u.lastName as client, u.email,o.address from[Order] o inner join UserWeb u on o.IdUser = u.IdUser " + _sqlClient;
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;                    
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    while (_rstReader.Read())
                    {
                        Order _order = new Order();
                        _order.IdOrder = Convert.ToInt32(_rstReader["IdOrder"]);
                        _order.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _order.value = (float)Convert.ToDouble(_rstReader["value"]);
                        _order.date =_rstReader["date"].ToString();
                        _order.payType = (_rstReader["payType"]).ToString().Trim();
                        _order.client = (_rstReader["client"]).ToString().Trim();
                        _order.email = (_rstReader["email"]).ToString().Trim();
                        _order.dateF = _rstReader["dateF"].ToString();
                        _order.address = _rstReader["address"].ToString();
                        _orders.Add(_order);

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _orders;

        }

        //Request for row information to the Data Base
        /// <summary>
        ///  Request for information on an Order made by a specific user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Order> getOrdersById(int pIdUser, ref mError pMError)
        {
            List<Order> _orders = new List<Order>();

            String _sql = "select o.IdOrder,o.IdUser,o.value,FORMAT (o.date, 'MMM dd yyyy') dateF,CONVERT(VARCHAR(10), o.date, 103) + ' '  + convert(VARCHAR(8), o.date, 14) date,CASE WHEN o.payType=1 THEN 'visa' WHEN o.payType=2 THEN 'amex' WHEN o.payType = 3 THEN 'mastercard' ELSE 'discover' END as payType, u.name + ' ' + u.lastName as client, u.email from[Order] o inner join UserWeb u on o.IdUser = u.IdUser and o.IdUser=@pIdUser";
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
                        Order _order = new Order();
                        _order.IdOrder = Convert.ToInt32(_rstReader["IdOrder"]);
                        _order.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _order.value = (float)Convert.ToDouble(_rstReader["value"]);
                        _order.date = _rstReader["date"].ToString();
                        _order.payType = (_rstReader["payType"]).ToString().Trim(); 
                        _order.client = (_rstReader["client"]).ToString().Trim(); 
                        _order.email = (_rstReader["email"]).ToString().Trim();
                        _order.dateF = _rstReader["dateF"].ToString();
                        _orders.Add(_order);

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _orders;

        }
        
        /// <summary>
        /// Request for information about the Order and its details to the Database, 
        /// given the order number:
        /// </summary>
        /// <param name="pIdOrder"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Order getOrderByOrderId(int pIdOrder, ref mError pMError)
        {            

            String _sql = "select o.IdOrder,o.IdUser,o.value,FORMAT (o.date, 'MMM dd yyyy') dateF,CONVERT(VARCHAR(10), o.date, 103) + ' '  + convert(VARCHAR(8), o.date, 14) date,CASE WHEN o.payType=1 THEN 'visa' WHEN o.payType=2 THEN 'amex' WHEN o.payType = 3 THEN 'mastercard' ELSE 'discover' END as payType, o.voucher, o.address, u.name + ' ' + u.lastName as client, u.email from[Order] o inner join UserWeb u on o.IdUser = u.IdUser and o.IdOrder=@pIdOrder";
            Order _order = new Order();
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@pIdOrder", pIdOrder);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {                        
                        _order.IdOrder = Convert.ToInt32(_rstReader["IdOrder"]);
                        _order.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _order.value = (float)Convert.ToDouble(_rstReader["value"]);
                        _order.date = _rstReader["date"].ToString();
                        _order.payType = (_rstReader["payType"]).ToString().Trim(); 
                        _order.client = (_rstReader["client"]).ToString().Trim(); 
                        _order.email = (_rstReader["email"]).ToString().Trim(); 
                        _order.voucher = (_rstReader["voucher"]).ToString().Trim();
                        _order.dateF = _rstReader["dateF"].ToString();
                        _order.address = _rstReader["address"].ToString();
                    }
                    _order._products = getDetailsByOrderId(pIdOrder, ref pMError);
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _order;

        }

       
        /// <summary>
        /// Request for information on the details of a specific order.
        /// </summary>
        /// <param name="pIdOrder"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<OrderDetail> getDetailsByOrderId(int pIdOrder, ref mError pMError)
        {
            List<OrderDetail> _ordersD = new List<OrderDetail>();

            String _sql = "select o.IdOD,o.idProduct,o.quantity,o.price,p.name product,p.discount,p.brand,p.size,p.photo  from[OrderDetail] o inner join Product p on o.IdProduct = p.IdProduct and o.IdOrder=@pIdOrder";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@pIdOrder", pIdOrder);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    while (_rstReader.Read())
                    {
                        OrderDetail _orderD = new OrderDetail();
                        _orderD.IdOD = Convert.ToInt32(_rstReader["IdOD"]);
                        _orderD.IdOrder = pIdOrder;
                        _orderD.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _orderD.quantity = Convert.ToInt32(_rstReader["quantity"]);
                        _orderD.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _orderD.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _orderD.brand = _rstReader["brand"].ToString().Trim();
                        _orderD.product = _rstReader["product"].ToString().Trim();
                        _orderD.size = _rstReader["size"].ToString().Trim(); 
                        _orderD.photo = _rstReader["photo"].ToString().Trim(); 
                        _ordersD.Add(_orderD);

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _ordersD;

        }

        
        /// <summary>
        /// Request for information on the categories that meet a search 
        /// criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Order> getOrderSearch(int? pIdClient, String pSearch, ref mError pMError)
        {
            List<Order> _orders = new List<Order>();
            String _sqlClient = !(String.IsNullOrEmpty(pIdClient.ToString())) ? " and o.IdUser=" + pIdClient.ToString() : "";

            String _sql = "select o.IdOrder,o.IdUser,o.value,CONVERT(VARCHAR(10), o.date, 103) + ' '  + convert(VARCHAR(8), o.date, 14) date,CASE WHEN o.payType=1 THEN 'visa' WHEN o.payType=2 THEN 'amex' WHEN o.payType = 3 THEN 'mastercard' ELSE 'discover' END as payType, u.name + ' ' + u.lastName as client, u.email from[Order] o inner join UserWeb u on o.IdUser = u.IdUser where ( CAST(o.IdOrder as CHAR) LIKE @pSearch or (u.name like @pSearch or  u.lastName like @pSearch) ) "+ _sqlClient;
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    //_cmd.Parameters.AddWithValue("@pIdUser", pIdUser);
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    _rstReader = _cmd.ExecuteReader();
                    _orders = new List<Order>();
                    while (_rstReader.Read())
                    {
                        Order _order = new Order();
                        _order.IdOrder = Convert.ToInt32(_rstReader["IdOrder"]);
                        _order.IdUser = Convert.ToInt32(_rstReader["IdUser"]);
                        _order.value = (float)Convert.ToDouble(_rstReader["value"]);
                        _order.date = _rstReader["date"].ToString();
                        _order.payType = (_rstReader["payType"]).ToString().Trim(); ;
                        _order.client = (_rstReader["client"]).ToString().Trim(); ;
                        _order.email = (_rstReader["email"]).ToString().Trim(); ;
                        _orders.Add(_order);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _orders;

        }

        /// <summary>
        /// Function to persist data in the Order table.
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDOrder(Order pOrder,String pOper,ref mError pMError)
        {
            string _sql = "Sproc_CRUD_Order";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdOrder",  Value = pOrder.IdOrder, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdUser",  Value = pOrder.IdUser, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@value",  Value = pOrder.value, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@oper",  Value = pOper, Direction = System.Data.ParameterDirection.Input },

                    new SqlParameter(_strOut, SqlDbType.NChar) { Size = 300, Direction = System.Data.ParameterDirection.Output }

            };

                           
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlTransaction _transaction;
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in sqlParams)
                    {
                        cmd.Parameters.Add(item);
                    }
                    _conn.Open();
                    _transaction = _conn.BeginTransaction();
                try
                    { 
                            //Order Insertion:
                            cmd.ExecuteNonQuery();
                            _res = cmd.Parameters[_strOut].Value.ToString();
                            _conn.Close();
                            _pos = _res.IndexOf("-1;", 0);
                            if (_pos >= 0)
                            {                      
                                pMError.code = "1";
                                pMError.mssg = _res.Substring(2);
                            }
                            else
                            {
                                if (pOper != "DEL")
                                {
                                    int _resD = 0;
                                    _pos = Convert.ToInt32(_res.Substring(0,_res.IndexOf(";", 0)-1));
                                    //Order details insertion:
                                    foreach (OrderDetail _detail in pOrder._products) {
                                        _detail.IdOrder = _pos;
                                        _resD = CRUDOrderDetail(_detail,"INS",ref pMError);
                                        if (_resD >= 0)
                                        {
                                            throw new Exception("Failure in the process, Transaction terminated.");
                                        }
                                    }
                                }
                                _transaction.Commit();
                            }
                           
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback();
                        pMError.code = "1";
                        pMError.mssg = ex.Message;
                    }
                }
            
            return _pos;
        }

        /// <summary>
        /// Function to persist data in the OrderDetail table.
        /// </summary>
        /// <param name="pOrderD"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDOrderDetail(OrderDetail pOrderD, String pOper, ref mError pMError)
        {
            string _sql = "Sproc_CRUD_OrderDetail";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdOD",  Value = pOrderD.IdOD, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdOrder",  Value = pOrderD.IdOrder, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdOrder",  Value = pOrderD.IdOrder, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@quantity",  Value = pOrderD.quantity, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@price",  Value = pOrderD.price, Direction = System.Data.ParameterDirection.Input },
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
                    _pos = _res.IndexOf("-1;", 0);
                    if (_pos >= 0)
                    {
                        throw new Exception("Failure in the process, Transaction terminated.");
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
