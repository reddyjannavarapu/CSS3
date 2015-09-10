# region Document Header
//Created By       : Pavan 
//Created Date     : 15 September 2014
//Description      : To Bind Menu
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
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    #endregion

    public class MenuBinding
    {
        private static ILog log = LogManager.GetLogger(typeof(MenuBinding));

        #region Properties

        public string MainMenuID { get; set; }
        public string MainMenuClass { get; set; }
        public string MainMenuName { get; set; }

        public string SubMenuID { get; set; }
        public string SubMenuName { get; set; }
        public string URL { get; set; }

        #endregion

        #region Fetch Methods

        private MenuBinding FetchMenu(MenuBinding MenuBinding, SafeDataReader dr)
        {
            MenuBinding.MainMenuID = dr.GetString("MainMenuID");
            MenuBinding.MainMenuClass = dr.GetString("MainMenuClass");
            MenuBinding.MainMenuName = dr.GetString("MainMenuName");

            MenuBinding.SubMenuID = dr.GetString("SubMenuID");
            MenuBinding.SubMenuName = dr.GetString("SubMenuName");
            MenuBinding.URL = dr.GetString("URL");

            return MenuBinding;
        }

        private static List<Menu> FetchMenuToBind(MenuBindingInfo data, List<Menu> MList)
        {
            //var AllData = (from s in data.MenuList select s).ToList();
            var MainMenus = (from s in data.MenuList select s.MainMenuID).Distinct().ToList();
            foreach (var Menu in MainMenus)
            {
                Menu obj = new Menu();
                var SubmenusForMenu = (from s in data.MenuList where s.MainMenuID == Menu select s).ToList();
                foreach (var submenu in SubmenusForMenu)
                {
                    obj.MainMenuID = submenu.MainMenuID;
                    obj.MainMenuName = submenu.MainMenuName;
                    obj.MainMenuClass = submenu.MainMenuClass;

                    SubMenuBinding SubMenu = new SubMenuBinding();
                    SubMenu.SubMenuID = submenu.SubMenuID;
                    SubMenu.SubMenuName = submenu.SubMenuName;
                    SubMenu.URL = submenu.URL;
                    obj.SubMenuList.Add(SubMenu);
                }
                MList.Add(obj);
            }
            return MList;
        }



        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description   : To Bind Menu Data
        /// Created By    : Pavan 
        /// Created Date  : 15 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static List<Menu> GetMenuDataForUser(int UserID)
        {
            var data = new List<Menu>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                MenuBindingInfo objMenu = new MenuBindingInfo();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@UserID", UserID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpBindMenu", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Menudata = new MenuBinding();
                    Menudata.FetchMenu(Menudata, safe);
                    objMenu.MenuList.Add(Menudata);
                }

                FetchMenuToBind(objMenu, data);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return data;
        }

        ///// <summary>
        ///// Description   : To Check Url Access
        ///// Created By    : Pavan 
        ///// Created Date  : 16 September 2014
        ///// Modified By   :  
        ///// Modified Date :  
        ///// <returns></returns>
        ///// </summary>
        //public static int CheckURLAccess(string context, int UserID)
        //{
        //    List<string> arr = new List<string>();
        //    SqlParameter[] sqlParams = new SqlParameter[3];
        //    sqlParams[0] = new SqlParameter("@UserID", UserID);
        //    sqlParams[1] = new SqlParameter("@RequestedUrl", context);
        //    sqlParams[2] = new SqlParameter("@Output", 0);
        //    sqlParams[2].Direction = ParameterDirection.Output;
        //    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCheckURLAccessForUserID", sqlParams);
        //    var safe = new SafeDataReader(reader);
        //    while (reader.Read())
        //    {
        //        arr.Add(safe.GetString("FieldName"));
        //    }
        //    int output = Convert.ToInt32(sqlParams[2].Value);
        //    return output;
        //}

        /// <summary>
        /// Description   : To Check Url Access
        /// Created By    : Pavan 
        /// Created Date  : 16 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static string CheckURlAndGetUserRole(string context, int UserID)
        {
            string Roles = string.Empty;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                string Result = string.Empty;
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@UserID", UserID);
                sqlParams[1] = new SqlParameter("@RequestedUrl", context);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCheckURLAccessForUserID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    Result = (safe.GetString("Result"));
                }
                string[] output = Result.Split(',');

                for (int i = 0; i < output.Length; i++)
                {
                    if (Roles == string.Empty)
                    {
                        Roles = Enum.GetName(typeof(UserType), Convert.ToInt32(output[i]));
                    }
                    else
                    {
                        Roles = Roles + ',' + Enum.GetName(typeof(UserType), Convert.ToInt32(output[i]));
                    }
                }
                Roles = Roles.EndsWith(",") ? Roles.Substring(0, Roles.Length - 1) : Roles;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return Roles;
        }

        #endregion

    }

    #region InfoClass
    public class MenuBindingInfo
    {
        public List<MenuBinding> MenuList { get; set; }

        public MenuBindingInfo()
        {
            MenuList = new List<MenuBinding>();
        }
    }

    public class Menu
    {
        public string MainMenuID { get; set; }
        public string MainMenuClass { get; set; }
        public string MainMenuName { get; set; }

        public List<SubMenuBinding> SubMenuList { get; set; }
        public Menu()
        {
            SubMenuList = new List<SubMenuBinding>();
        }
    }

    public class SubMenuBinding
    {
        public string SubMenuID { get; set; }
        public string SubMenuName { get; set; }
        public string URL { get; set; }
    }

    #endregion

}