/*******************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to logically manage, read operations 
 * and data persistence in the Category table.
 * Company: AIS - NZ
 * */
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;

namespace Business
{
    public class CategoryBL
    {
        /// <summary>
        /// Request for Category table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Category> getCategoriesBL(ref mError pMError)
        {

            CategoryDA dataAccess = new CategoryDA();
            return dataAccess.getCategories(ref pMError);

        }

        /// <summary>
        /// Request for information on a specific category.
        /// </summary>
        /// <param name="pIdCategory"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Category getCategoryBL(int pIdCategory, ref mError pMError)
        {

            CategoryDA dataAccess = new CategoryDA();
            return dataAccess.getCategory(pIdCategory, ref pMError);

        }

        /// <summary>
        /// Request for information on the categories that meet a search criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Category> getCategorySearch_BL(String pSearch, ref mError pMError)
        {

            CategoryDA dataAccess = new CategoryDA();
            return dataAccess.getCategorySearch(pSearch, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the Category table.
        /// </summary>
        /// <param name="pCategory"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCategoryBL(Category pCategory, String pOper, ref mError pMError)
        {

            CategoryDA dataAccess = new CategoryDA();
            return dataAccess.CRUDCategory(pCategory, pOper, ref pMError);

        }
    }
}
