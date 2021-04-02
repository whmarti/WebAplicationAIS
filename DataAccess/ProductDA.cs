/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage directly in the Database, 
 * read operations and data persistence in the Product table.
 * Company: AIS - NZ
 * */
using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public class ProductDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();

        /// <summary>
        /// Request for Product table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProducts(ref mError pMError)
        {

            List<Product> _products = null;
            SqlDataReader _rstReader = null;
            string _sql = "select p.*, c.name as Category from Product p inner join Category c on p.IdCategory=c.IdCategory";            
            //pMError = new mError("0","");

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    _products = new List<Product>();
                    while (_rstReader.Read())
                    {

                        Product _newProduct = new Product();
                        _newProduct.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _newProduct.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _newProduct.name = Convert.ToString(_rstReader["name"]).Trim();
                        _newProduct.stock = Convert.ToInt32(_rstReader["stock"]);
                        _newProduct.color = Convert.ToString(_rstReader["color"]).Trim();
                        _newProduct.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _newProduct.size = Convert.ToString(_rstReader["size"]).Trim();
                        _newProduct.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _newProduct.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _newProduct.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _newProduct.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _newProduct.category = Convert.ToString(_rstReader["category"]).Trim();
                        _newProduct.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _products.Add(_newProduct);

                    }
                    _rstReader.Close();
                    
                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;  
            }
        
            return _products;
        }

        /// <summary>
        /// Request for information on a specific product.
        /// </summary>
        /// <param name="pIdProduct"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Product getProduct(int pIdProduct, ref mError pMError)
        {
            Product _product = new Product();
            String _sql = "Select p.*,c.name as category from product p inner join Category c on p.IdCategory=C.IdCategory where p.IdProduct=@idProduct";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@idProduct", pIdProduct);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {                        
                        _product.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _product.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _product.name = Convert.ToString(_rstReader["name"]).Trim();
                        _product.stock = Convert.ToInt32(_rstReader["stock"]);
                        _product.color = Convert.ToString(_rstReader["color"]).Trim();
                        _product.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _product.size = Convert.ToString(_rstReader["size"]).Trim();
                        _product.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _product.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _product.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _product.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _product.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _product.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _product.category = Convert.ToString(_rstReader["category"]).Trim();

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _product;

        }

        /// <summary>
        /// Request for information on the products that meet a search criteria. (Admon. module)
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProductSearch(String pSearch, String pDate, ref mError pMError)
        {
            List<Product> _products = new List<Product>();
            
            string dateTimeString = pDate; // "dd/mm/yyyy";
            dateTimeString = Regex.Replace(dateTimeString, @"[^\u0000-\u007F]", string.Empty);

            string inputFormat = "mm/dd/yyyy";
            string outputFormat = "yyyy-MM-dd";

            int _month = pDate != "" ? Convert.ToDateTime(pDate).Month : 0;

            String _srchDate = pDate != "" ? "  AND (  monthDcto = " + _month + " )" : "";
            String _sql = "Select p.*,c.name as category from product p inner join Category c on p.IdCategory=C.IdCategory where (p.name like @pSearch OR p.brand like @pSearch OR c.name like @pSearch OR p.size like @pSearch) " + _srchDate;
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    //_cmd.Parameters.AddWithValue("@pDate", _month);
                    _rstReader = _cmd.ExecuteReader();
                    _products = new List<Product>();
                    while (_rstReader.Read())
                    {
                        Product _product = new Product();
                        _product.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _product.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _product.name = Convert.ToString(_rstReader["name"]).Trim();
                        _product.stock = Convert.ToInt32(_rstReader["stock"]);
                        _product.color = Convert.ToString(_rstReader["color"]).Trim();
                        _product.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _product.size = Convert.ToString(_rstReader["size"]).Trim();
                        _product.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _product.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _product.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _product.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _product.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _product.category = Convert.ToString(_rstReader["category"]).Trim();
                        _product.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _products.Add(_product);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _products;

        }

        /// <summary>
        /// Request for information on the products that meet a search criteria. (Client module)
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProductClientSearch(String pSearch, String pType, int pOffer, ref mError pMError)
        {
            List<Product> _products = new List<Product>();
            String _offer = pOffer > 0? " and monthDcto=" + pOffer : "";
            String _sql = "Select p.*,c.name as category from product p inner join Category c on p.IdCategory=C.IdCategory where (p.name like @pSearch OR p.brand like @pSearch or p.size like @pSearch) AND c.name = @pType" + _offer;
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    _cmd.Parameters.AddWithValue("@pType",  pType );
                    _rstReader = _cmd.ExecuteReader();
                    _products = new List<Product>();
                    while (_rstReader.Read())
                    {
                        Product _product = new Product();
                        _product.IdProduct = Convert.ToInt32(_rstReader["IdProduct"]);
                        _product.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _product.name = Convert.ToString(_rstReader["name"]).Trim();
                        _product.stock = Convert.ToInt32(_rstReader["stock"]);
                        _product.color = Convert.ToString(_rstReader["color"]).Trim();
                        _product.brand = Convert.ToString(_rstReader["brand"]).Trim();
                        _product.size = Convert.ToString(_rstReader["size"]).Trim();
                        _product.photo = Convert.ToString(_rstReader["photo"]).Trim();
                        _product.photoD = Convert.ToString(_rstReader["photoD"]).Trim();
                        _product.price = (float)Convert.ToDouble(_rstReader["price"]);
                        _product.discount = (float)Convert.ToDouble(_rstReader["discount"]);
                        _product.monthDcto = Convert.ToInt32(_rstReader["monthDcto"]);
                        _product.category = Convert.ToString(_rstReader["category"]).Trim();
                        _products.Add(_product);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _products;

        }

        /// <summary>
        /// Function to persist data in the Product table.
        /// </summary>
        /// <param name="pProduct"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDProduct(Product pProduct,String pOper,ref mError pMError)
        {
            string _sql = "Sproc_CRUD_Product";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter { ParameterName = "@IdProduct",  Value = pProduct.IdProduct, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@IdCategory",  Value = pProduct.IdCategory, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@name",  Value = pProduct.name, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@stock",  Value = pProduct.stock, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@color",  Value = pProduct.color, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@brand",  Value = pProduct.brand, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@size",  Value = pProduct.size, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@photo",  Value = pProduct.photo, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@photoD",  Value = pProduct.photoD, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@price",  Value = pProduct.price.ToString(), Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@discount",  Value = pProduct.discount.ToString(), Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@monthDcto",  Value = pProduct.monthDcto.ToString(), Direction = System.Data.ParameterDirection.Input },
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
    }
}
