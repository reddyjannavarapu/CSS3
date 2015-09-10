
# region Document Header
//Created By       : Anji 
//Created Date     : 08 May 2014
//Description      : To Check User Authorization
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Models
{

    #region Usings
    using System;
    using System.Web;
    #endregion

    public class UserValidation
    {
        public static bool CheckUserAuthentication()
        {
            int userid = Convert.ToInt32(HttpContext.Current.Session["userId"]);           
            if (userid > 0)
                return true;
            else return false;           
        }

        public static bool CheckUserAuthorization()
        {
            bool status = Convert.ToBoolean(HttpContext.Current.Session["status"]);
            return status;
        }
    }
}