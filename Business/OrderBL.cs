/*******************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to logically manage, read operations 
 * in the Order table.
 * Company: AIS - NZ
 * */
using System;
using System.Collections.Generic;
using DataAccess;
using Entity;

namespace Business
{
    public class OrderBL
    {
        /// <summary>
        ///  Request for Order table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Order> getOrdersBL(int? pIdClient,ref mError pMError)
        {

            OrderDA dataAccess = new OrderDA();
            return dataAccess.getOrders(pIdClient,ref pMError);

        }

        //Request for row information to the Data Base
        /// <summary>
        ///  Request for information about the Order and its details to the Database, 
        /// given the order number:
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public Order getOrderByOrderIdBL(int pIdOrder, ref mError pMError)
        {
            OrderDA dataAccess = new OrderDA();
            return dataAccess.getOrderByOrderId(pIdOrder, ref pMError);
        }

        /// <summary>
        /// Request for information on the categories that meet a search 
        /// criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<Order> getOrderSearch_BL(int? pIdClien,String pSearch, ref mError pMError)
        {

            OrderDA dataAccess = new OrderDA();
            return dataAccess.getOrderSearch(pIdClien,pSearch, ref pMError);

        }

       
    }
}
