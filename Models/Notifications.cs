using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CSS2.Models
{
    public class Notifications
    {
        private static ILog log = LogManager.GetLogger(typeof(Notifications));

        #region Properties

        public string Type { get; set; }
        public int Number { get; set; }
        public string href { get; set; }

        #endregion

        #region Fetch Methods

        public Notifications FetchNotifications(Notifications NotificationsBinding, SafeDataReader dr)
        {
            NotificationsBinding.Number = dr.GetInt32("Number");
            NotificationsBinding.Type = dr.GetString("Type");
            NotificationsBinding.href = dr.GetString("href");
            return NotificationsBinding;
        }

        public Notifications FetchNotificationsWOType(Notifications NotificationsBinding, SafeDataReader dr)
        {
            NotificationsBinding.Number = dr.GetInt32("Number");
            NotificationsBinding.Type = dr.GetString("Type");
            NotificationsBinding.href = dr.GetString("href");

            return NotificationsBinding;
        }

        #endregion


        #region Database Methods

        /// <summary>
        /// Description   : To Bind Notifications Data
        /// Created By    : Pavan 
        /// Created Date  : 18 March 2015
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static NotificationsFinalInfo GetNotificationsData()
        {
            var obj = new NotificationsFinalInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var WOSTATUS = new List<Notifications>();
                var ACCPAC = new List<Notifications>();
                var WOTYPE = new List<Notifications>();
                var WOGROUP = new List<Notifications>();

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetDashboardNotifications]");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Notification = new Notifications();
                    Notification.FetchNotificationsWOType(Notification, safe);
                    WOSTATUS.Add(Notification);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var Notification = new Notifications();
                    Notification.FetchNotifications(Notification, safe);
                    ACCPAC.Add(Notification);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var Notification = new Notifications();
                    Notification.FetchNotificationsWOType(Notification, safe);
                    WOTYPE.Add(Notification);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var Notification = new Notifications();
                    Notification.FetchNotificationsWOType(Notification, safe);
                    WOGROUP.Add(Notification);
                }

                obj.NotificationListWOStatus = WOSTATUS;
                obj.NotificationCountWOStatus = (from s in obj.NotificationListWOStatus select s.Number).Sum();

                obj.NotificationListACCPAC = ACCPAC;
                obj.NotificationCountACCPAC = (from s in obj.NotificationListACCPAC select s.Number).Sum();

                obj.NotificationListWOType = WOTYPE;
                obj.NotificationCountWOType = (from s in obj.NotificationListWOType select s.Number).Sum();

                obj.NotificationListWOGroup = WOGROUP;
                obj.NotificationCountWOGroup = (from s in obj.NotificationListWOGroup select s.Number).Sum();
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return obj;
        }

        #endregion

    }

    public class NotificationsFinalInfo
    {
        #region Prpperties

        public int NotificationCountWOStatus { get; set; }
        public int NotificationCountACCPAC { get; set; }
        public int NotificationCountWOType { get; set; }
        public int NotificationCountWOGroup { get; set; }

        public List<Notifications> NotificationListWOStatus { get; set; }
        public List<Notifications> NotificationListACCPAC { get; set; }
        public List<Notifications> NotificationListWOType { get; set; }
        public List<Notifications> NotificationListWOGroup { get; set; }

        public NotificationsFinalInfo()
        {
            NotificationListWOStatus = new List<Notifications>();
            NotificationListACCPAC = new List<Notifications>();
            NotificationListWOType = new List<Notifications>();
            NotificationListWOGroup = new List<Notifications>();
        }

        #endregion
    }

}