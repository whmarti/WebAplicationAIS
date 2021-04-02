/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage directly in the Database, 
 * read operations and data persistence in the Category table.
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
    public class CategoryDA
    {
        private static string strConn = ConfigurationManager.AppSettings["DataBase"].ToString();

        
        /// <summary>
        /// Request for Category table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Category> getCategories(ref mError pMError)
        {           
            List<Category> _categories = null;
            SqlDataReader _rstReader = null;  
            string _sql = "select * from Category";

            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(_sql, _conn);
                    _conn.Open();
                    _rstReader = cmd.ExecuteReader();
                    _categories = new List<Category>();
                    while (_rstReader.Read())
                    {

                        Category _newCategory = new Category();
                        _newCategory.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _newCategory.name = Convert.ToString(_rstReader["name"]).Trim();
                        _categories.Add(_newCategory);

                    }
                    _rstReader.Close();
                }            
            }
            catch (Exception ex)
            {               
                pMError.code = "1";
                pMError.mssg = ex.Message;               
            }
            return _categories;
        }


        /// <summary>
        /// Request for information on a specific category.
        /// </summary>
        /// <param name="pIdCategory"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Category getCategory(int pIdCategory, ref mError pMError)
        {
            Category _Category = new Category();
            String _sql = "Select c.* from Category c where c.IdCategory=@idCategory";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _cmd.Parameters.AddWithValue("@idCategory", pIdCategory);
                    _conn.Open();
                    _rstReader = _cmd.ExecuteReader();
                    if (_rstReader.Read())
                    {
                        _Category.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _Category.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _Category.name = Convert.ToString(_rstReader["name"]).Trim();                        

                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _Category;

        }

      
        /// <summary>
        /// Request for information on the categories that meet a search criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Category> getCategorySearch(String pSearch, ref mError pMError)
        {
            List<Category> _Categorys = new List<Category>();
            String _sql = "Select c.IdCategory,c.name  from Category c where c.name like @pSearch";
            try
            {
                using (SqlConnection _conn = new SqlConnection(strConn))
                {
                    SqlCommand _cmd = new SqlCommand(_sql, _conn);
                    SqlDataReader _rstReader = null;
                    _conn.Open();
                    _cmd.Parameters.AddWithValue("@pSearch", "%" + pSearch + "%");
                    _rstReader = _cmd.ExecuteReader();
                    _Categorys = new List<Category>();
                    while (_rstReader.Read())
                    {
                        Category _Category = new Category();
                        _Category.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _Category.IdCategory = Convert.ToInt32(_rstReader["IdCategory"]);
                        _Category.name = Convert.ToString(_rstReader["name"]).Trim();
                        _Categorys.Add(_Category);
                    }

                }
            }
            catch (Exception ex)
            {
                pMError.code = "1";
                pMError.mssg = ex.Message;
            }
            return _Categorys;

        }

        
        /// <summary>
        /// Function to persist data in the Category table.
        /// </summary>
        /// <param name="pCategory"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCategory(Category pCategory, String pOper, ref mError pMError)
        {
            string _sql = "Sproc_CRUD_Category";
            string _strOut = "@P_Mensaje", _res = "";
            int _pos = 0;
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]
            {                   
                    new SqlParameter { ParameterName = "@IdCategory",  Value = pCategory.IdCategory, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter { ParameterName = "@name",  Value = pCategory.name, Direction = System.Data.ParameterDirection.Input },                   
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



