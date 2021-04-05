/*******************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to logically manage, read operations 
 * and data persistence in the User table.
 * Company: AIS - NZ
 * */
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserBL
    {
        /// <summary>
        /// Request for User table information to the Data Base.
        /// </summary>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUsersBL(ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.getUsers(ref pMError);

        }
        /// <summary>
        /// Returns information from the UserWeb table of the database, given a user type
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUsersBL(String pType, ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.getUsers(pType, ref pMError);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTop"></param>
        /// <param name="pDateFrom"></param>
        /// <param name="pDateTo"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getTopVisitorsBL(String pTop, String pDateFrom, String pDateTo, ref mError pMError)
        {
            UserDA dataAccess = new UserDA();
            return dataAccess.getTopVisitors(pTop, pDateFrom, pDateTo, ref pMError);
        }

        /// <summary>
        /// Request for information on a specific user.
        /// </summary>
        /// <param name="pIdUser"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public User getUserBL(int pIdUser, ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.getUser(pIdUser, ref pMError);

        }

        /// <summary>
        ///  Request for information on the users that meet a search criteria.
        /// </summary>
        /// <param name="pSearch"></param>
        /// <param name="pType"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public List<User> getUserSearch_BL(String pType, String pSearch, ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.getUserSearch(pType, pSearch, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the User table.
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDUserBL(User pUser, String pOper, ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.CRUDUser(pUser, pOper, ref pMError);

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

            UserDA dataAccess = new UserDA();
            return dataAccess.getUserLoginBL(pUser, pPass, pType, ref pMError);

        }

        /// <summary>
        /// Function to persist data in the Visit table.
        /// </summary>
        /// <param name="pVisit"></param>
        /// <param name="pOper"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int CRUDVisitBL(Visit pVisit, String pOper, ref mError pMError)
        {

            UserDA dataAccess = new UserDA();
            return dataAccess.CRUDVisit(pVisit, pOper, ref pMError);

        }
        /// <summary>
        /// Function to change the password of the user
        /// </summary>
        /// <param name="pEmail"></param>
        /// <param name="pPass"></param>
        /// <param name="pMError"></param>
        /// <returns></returns>
        public int UPD_Pass_UserBL(String pEmail, String pPass, ref mError pMError)
        {
            UserDA dataAccess = new UserDA();
            return dataAccess.UPD_Pass_User(pEmail, pPass, ref pMError);
        }
    }
}
