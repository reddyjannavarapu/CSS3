
# region Document Header
//Created By       : Anji 
//Created Date     : 22 May 2014
//Description      : To Mantian connection string
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
    using System.Configuration;
    #endregion

    /// <summary>
    /// Summary description for ConnectionUtility
    /// </summary>
    public static class ConnectionUtility
    {
        private static ILog log = LogManager.GetLogger(typeof(ConnectionUtility));

        public static string GetConnectionString()
        {
            var connection = string.Empty;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return connection;
        }
    }

}