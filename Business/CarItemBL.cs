/*******************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to logically manage, read operations 
 * and data persistence in the CarItem table.
 * Company: AIS - NZ
 * */
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;

namespace Business
{
    public class CarItemBL
    {
        /// <summary>
        /// Request for CarItem table information to the Data Base.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<CarItem> getCarItemsBL(int pIdUser, ref mError pMError)
        {

            CarItemDA dataAccess = new CarItemDA();
            return dataAccess.getCarItems(pIdUser, ref pMError);

        }

        /// <summary>
        /// Request for CarItem table  information with a supplied data.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pSearch"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<CarItem> getCarItemSearch_BL(int pIdUser, String pSearch, ref mError pMError)
        {

            CarItemDA dataAccess = new CarItemDA();
            return dataAccess.getCarItemSearch(pIdUser, pSearch, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the table CarItem.
        /// </summary>
        /// <param name="pCarItem"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCarItemBL(CarItem pCarItem, String pOper, ref mError pMError)
        {

            CarItemDA dataAccess = new CarItemDA();
            return dataAccess.CRUDCarItem(pCarItem, pOper, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the Order table.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDCarOrderBL(int IdUser, String pOper, ref mError pMError)
        {

            CarItemDA dataAccess = new CarItemDA();
            return dataAccess.CRUDCarOrder(IdUser, pOper, ref pMError);

        }


        //public int CRUDCarOrderBL1(int IdUser, List<CarItem> pCarItems, String pOper, ref mError pMError)
        //{

        //    CarItemDA dataAccess = new CarItemDA();
        //    return dataAccess.CRUDCarOrder(IdUser, pOper, ref pMError);

        //}

        /// <summary>
        /// Request for quantity of items in the car of a specific user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int getTotalCarItemsBL(int pIdUser, ref mError pMError)
        {
            CarItemDA dataAccess = new CarItemDA();
            return dataAccess.getTotalCarItems(pIdUser, ref pMError);
        }
    }
}
