/*******************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to logically manage, read operations 
 * and data persistence in the Product table.
 * Company: AIS - NZ
 * */
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;

namespace Business
{
    public  class ProductBL
    {
        /// <summary>
        /// Request for Product table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProductsBL(ref mError pMError)
        {

            ProductDA dataAccess = new ProductDA();
            return dataAccess.getProducts(ref pMError);

        }

        /// <summary>
        /// Request for information on a specific product.
        /// </summary>
        /// <param name="pIdProduct"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Product getProductBL(int pIdProduct, ref mError pMError)
        {

            ProductDA dataAccess = new ProductDA();
            return dataAccess.getProduct(pIdProduct,ref pMError);

        }

        /// <summary>
        /// Request for information on the products that meet a search criteria. (Admon. module)
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProductSearch_BL(String pSearch, String pDate, ref mError pMError)
        {

            ProductDA dataAccess = new ProductDA();
            return dataAccess.getProductSearch(pSearch, pDate, ref pMError);

        }

        /// <summary>
        /// Request for information on the products that meet a search criteria. (Client module)
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Product> getProductSearchCliente_BL(String pSearch, String pType, int pOffer, ref mError pMError)
        {

            ProductDA dataAccess = new ProductDA();
            return dataAccess.getProductClientSearch(pSearch, pType, pOffer, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the Product table.
        /// </summary>
        /// <param name="pProduct"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDProductBL(Product pProduct, String pOper, ref mError pMError)
        {

            ProductDA dataAccess = new ProductDA();
            return dataAccess.CRUDProduct(pProduct, pOper, ref pMError);

        }
    }
}
