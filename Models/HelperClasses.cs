
# region Document Header
//Created By       : Anji 
//Created Date     : 12 May 2014
//Description      : to write all methods which are usefull in entire application
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
    using System.Data;
    using System.Data.SqlClient;
    using System.Net;
    using System.Net.Mail;
    using System.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ionic.Zip;
    using System.Globalization;
    using System.Collections;
    using Aspose.Words;
    using System.IO;
    using System.Text.RegularExpressions;
    using OfficeOpenXml;
    using log4net;
    #endregion

    public static class HelperClasses
    {
        private static ILog log = LogManager.GetLogger(typeof(HelperClasses));

        #region Get Application Path
        public static string GetFullyQualifiedApplicationPath()
        {
            string appPath = string.Empty;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                //Getting the current context of HTTP request
                HttpContext context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format(
                        "{0}://{1}{2}{3}",
                        context.Request.Url.Scheme,
                        context.Request.Url.Host,
                        context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port,
                        context.Request.ApplicationPath);
                }
                if (!appPath.EndsWith("/"))
                {
                    appPath += "/";
                }

                appPath = appPath.TrimEnd('/');
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return appPath;
        }

        public static string GetFullyQualifiedApplicationPath(string filePath)
        {
            return GetFullyQualifiedApplicationPath() + filePath.Replace("~/", "");
        }
        #endregion

        #region Error handling
        /// <summary>
        /// Created By    : hussain 
        /// Created Date  : 28 AUG 2014
        /// Modified By   :  
        /// Description   : Code that runs when an unhandle error occurs.
        /// </summary>
        /// <param name="ErrorMessage">Receive the Exception details to insert</param>
        /// <returns></returns>
        public static int InsertErrorLogDetails(string ErrorMessage, string ErrorPage)
        {
            int returnValue = 0;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    string strErrorMessage = ErrorMessage.ToString();
                    string errUserID;
                    string errSessionID;
                    string errUserRole = string.Empty;
                    string strHostName = string.Empty;
                    if (System.Web.HttpContext.Current.Session["UserID"] != null)
                    {
                        errUserID = System.Web.HttpContext.Current.Session["UserID"].ToString();

                    }
                    else errUserID = string.Empty;

                    if (System.Web.HttpContext.Current.Session["CSS2SessionID"] != null)
                    {
                        errSessionID = System.Web.HttpContext.Current.Session["CSS2SessionID"].ToString();

                    }
                    else errSessionID = string.Empty;
                    //string errPageName = HttpContext.Current.Request.Url.ToString();
                    string errPageName = ErrorPage.ToString();
                    string errInBrowser = System.Web.HttpContext.Current.Request.Browser.Browser + " - " + System.Web.HttpContext.Current.Request.Browser.Version;
                    string errOnClientIpAddress = string.Empty;
                    strHostName = System.Net.Dns.GetHostName();

                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                    IPAddress[] address = ipEntry.AddressList;

                    errOnClientIpAddress = address[1].ToString();
                    DateTime dtErrorTime = DateTime.Now;
                    string clientIP = System.Web.HttpContext.Current.Request.UserHostAddress + " <br />" + System.Web.HttpContext.Current.Request.UserHostName + " <br />" + errOnClientIpAddress;


                    SqlParameter[] sqlParams = new SqlParameter[8];
                    string connectionString = ConnectionUtility.GetConnectionString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    sqlParams[0] = new SqlParameter("@UserID", errUserID);
                    sqlParams[1] = new SqlParameter("@UserRole", errUserRole);
                    sqlParams[2] = new SqlParameter("@PageName", errPageName);
                    sqlParams[3] = new SqlParameter("@ErrorTime", dtErrorTime);
                    sqlParams[4] = new SqlParameter("@ErrorInBrowser", errInBrowser);
                    sqlParams[5] = new SqlParameter("@IPAddress", clientIP);
                    sqlParams[6] = new SqlParameter("@sessionToken", errSessionID);
                    sqlParams[7] = new SqlParameter("@ErrorMessage", strErrorMessage);

                    returnValue = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[SPInsertErrorLogDetails]", sqlParams);

                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return returnValue;
        }
        #endregion

        #region Mail

        /// <summary>
        /// Created By    : Anji
        /// Created Date  : 22 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   :  To send mail from client ti client 
        /// </summary>
        /// <param name="from">from mail address sender of the mail</param>
        /// <param name="to">to mail address reciver of the mail</param>
        /// <param name="subject">subject of the mail </param>
        /// <param name="body">body of the mail</param>
        /// <returns></returns>
        public static void SendMail(string from, string to, string subject, string body)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(to));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCredentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Emailusr"], System.Configuration.ConfigurationManager.AppSettings["Emailpwd"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCredentials;
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

        }

        /// <summary>
        /// Created By    : Anji
        /// Created Date  : 22 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   :  To send mail from application to client 
        /// </summary>
        /// <param name="to">to mail address reciver of the mail</param>
        /// <param name="subject">subject of the mail </param>
        /// <param name="body">body of the mail</param>
        /// <returns></returns>
        public static bool SendMail(string to, string subject, string body)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["Emailusr"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(to));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCredentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Emailusr"], System.Configuration.ConfigurationManager.AppSettings["Emailpwd"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCredentials;
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return true;

        }

        /// <summary>
        /// Created By    : Anji
        /// Created Date  : 22 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   :  To send mail from application to project lead 
        /// </summary>
        /// <param name="subject">subject of the mail </param>
        /// <param name="body">body of the mail</param>
        /// <returns></returns>
        public static bool SendMail(string subject, string body)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["Emailusr"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ToEmail"]));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCredentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Emailusr"], System.Configuration.ConfigurationManager.AppSettings["Emailpwd"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCredentials;
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return true;

        }

        /// <summary>
        /// Created By    : Anji
        /// Created Date  : 22 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : Code that unhandle email.
        /// </summary>
        /// <returns></returns>
        public static bool SendMail()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["Emailusr"]);
                mailMessage.Subject = "Test mail";
                mailMessage.Body = "<span style='color:red'>CSS-2 Demo test mail<span>";
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ToEmail"]));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCredentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Emailusr"], System.Configuration.ConfigurationManager.AppSettings["Emailpwd"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCredentials;
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return true;

        }
        #endregion

        #region List to DataTable
        /// Created By    : SUDHEER
        /// Created Date  : 17 July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <summary>     
        /// This Method is used to convert Generic list into DataTable 
        /// based on the Generic list Properties
        /// </summary>    
        /// <param name="varlist">List of DIFields</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTable<T>(IList<T> varlist)
        {
            DataTable dt = new DataTable();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (varlist == null)
                {
                    //find all the public properties of this Type using reflection
                    PropertyInfo[] propT = typeof(T).GetProperties();

                    foreach (PropertyInfo pi in propT)
                    {
                        //create a datacolumn for each property
                        DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                        dt.Columns.Add(dc);
                    }

                }
                else
                {

                    //special handling for value types and string
                    //In value type, the DataTable is expected to contain the values of all the variables (items) present in List.
                    //Hence I create only one column in the DataTable named “Values”,
                    //Though String is a reference type, due to its behavior 
                    //I treat it as a special case and handle it as value type only.
                    if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
                    {
                        DataColumn dc = new DataColumn("Values");

                        dt.Columns.Add(dc);

                        foreach (T item in varlist)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item;
                            dt.Rows.Add(dr);
                        }
                    }
                    //for reference types other than  string
                    // Used PropertyInfo class of System.Reflection
                    else
                    {

                        //find all the public properties of this Type using reflection
                        PropertyInfo[] propT = typeof(T).GetProperties();

                        foreach (PropertyInfo pi in propT)
                        {
                            //create a datacolumn for each property
                            DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                            dt.Columns.Add(dc);
                        }

                        //now we iterate through all the items , take the corresponding values and add a new row in dt
                        for (int item = 0; item < varlist.Count(); item++)
                        {
                            DataRow dr = dt.NewRow();

                            for (int property = 0; property < propT.Length; property++)
                            {
                                dr[property] = propT[property].GetValue(varlist[item], null);
                            }

                            dt.Rows.Add(dr);
                        }
                    }
                    dt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return dt;
        }
        #endregion

        #region Zip


        public static void ArchiveFiles(List<Document> lstGeneratedFiles)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                //string[] Files = (string[])arrFiles.ToArray(typeof(string));
                ZipFile createZipFile = new ZipFile();

                int iCount = 1;
                foreach (Document doc in lstGeneratedFiles)
                {
                    MemoryStream docStream = new MemoryStream();
                    doc.Save(docStream, SaveFormat.Docx);

                    createZipFile.AddEntry("Doc_" + iCount.ToString(), docStream);

                    //doc.Save(@"d:/agmfiles/Doc_" + iCount.ToString() + ".docx");
                    iCount++;

                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                string zipName = String.Format("SomeName" + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                HttpContext.Current.Response.ContentType = "application/zip";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                createZipFile.Save(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

        }

        public static void ArchiveFiles(ArrayList arrFiles, string zipFloderName)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                string[] Files = (string[])arrFiles.ToArray(typeof(string));
                ZipFile createZipFile = new ZipFile();
                string timestamp = DateTime.Now.ToString("yyyy-MMM-dd-HHmmss");
                createZipFile.AddFiles(Files, zipFloderName + "_" + timestamp);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                string zipName = String.Format(zipFloderName + "_{0}.zip", timestamp);
                HttpContext.Current.Response.ContentType = "application/zip";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                createZipFile.Save(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.End();
                DeleteFiles(Files);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

        }

        public static void DeleteFiles(string[] Files)
        {
            for (int i = 0; i < Files.Length; i++)
            {
                if (System.IO.File.Exists(Files[i]) && !Files[i].Contains("ReadMe"))
                {
                    System.IO.File.Delete(Files[i]);
                }
            }
        }

        #endregion

        /// <summary>
        /// Description  : To convert Date format form dd/MM/yyyy to MM/dd/yyyy
        /// Created By   : Sudheer  
        /// Created Date : 31 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>date string MM/dd/yyyy format</returns>
        public static string ConvertDateFormat(string InputDate)
        {
            if (InputDate == "" || InputDate == null)
            {
                return null;
            }
            else
            {
                DateTime dt = DateTime.ParseExact(InputDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                // for both "1/1/2000" or "25/1/2000" formats
                return dt.ToString("MM/dd/yyyy");
            }
        }

        /// <summary>
        /// Description  : To convert Date format form dd/MM to MM/dd
        /// Created By   : Sudheer  
        /// Created Date : 9th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>date string MM/dd/yyyy format</returns>
        public static string ConvertDateFormatDDMM(string InputDate)
        {
            if (InputDate == "")
            {
                return "";
            }
            else
            {
                DateTime dt = Convert.ToDateTime(InputDate + " " + DateTime.Now.Year);
                // DateTime.ParseExact(InputDate+"/"+DateTime.Now.Year, "d/MM/yyyy", CultureInfo.InvariantCulture);
                // for both "1/1/2000" or "25/1/2000" formats
                return dt.ToString("MM/dd/yyyy");
            }
        }

        /// <summary>
        /// Description  : To Download Excel report
        /// Created By   : Sudheer  
        /// Created Date : 3 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static void DownloadSystemReport(DataSet dsInvoice, string Name)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            var fileName = Name + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            var outputDir = HttpContext.Current.Server.MapPath("~/ExcelTemplate/Downloads/");
            FileInfo file = new FileInfo(outputDir + fileName);
            try
            {

                using (var excely = new ExcelPackage(file))
                {
                    for (int k = 0; k < dsInvoice.Tables.Count; k++)
                    {

                        ExcelWorksheet worksheet = excely.Workbook.Worksheets.Add(dsInvoice.Tables[k].TableName);
                        //for Columns Names
                        //worksheet.Cells[1, 1].LoadFromDataTable(dsInvoice.Tables[k], true);
                        worksheet.Cells.LoadFromDataTable(dsInvoice.Tables[k], true);

                        worksheet.Row(1).Style.Font.Bold = true;
                        worksheet.Row(1).Style.Font.Size = 12;

                        ////For Data
                        //for (int i = 0; i < dsInvoice.Tables[k].Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < dsInvoice.Tables[k].Columns.Count; j++)
                        //    {
                        //        string cellvalue = dsInvoice.Tables[k].Rows[i][j].ToString();
                        //        worksheet.Cells[i + 2, j + 1].Value = cellvalue;

                        //        worksheet.Cells[i + 2, j + 1].Style.Border.Top.Style =
                        //        worksheet.Cells[i + 2, j + 1].Style.Border.Bottom.Style =
                        //        worksheet.Cells[i + 2, j + 1].Style.Border.Left.Style =
                        //        worksheet.Cells[i + 2, j + 1].Style.Border.Right.Style =
                        //        OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        //        if (i == 0) //First Row
                        //        {
                        //            worksheet.Cells[i + 1, j + 1].Style.Font.Bold = true;
                        //            worksheet.Cells[i + 1, j + 1].Style.Font.Size = 12;
                        //            worksheet.Cells[i + 1, j + 1].Style.Border.Top.Style =
                        //            worksheet.Cells[i + 1, j + 1].Style.Border.Bottom.Style =
                        //            worksheet.Cells[i + 1, j + 1].Style.Border.Left.Style =
                        //            worksheet.Cells[i + 1, j + 1].Style.Border.Right.Style =
                        //            OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        //        }
                        //    }
                        //}

                        //string range = Convert.ToString(dsInvoice.Tables[k].Rows.Count);

                    }

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                    HttpContext.Current.Response.BinaryWrite(excely.GetAsByteArray());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                if (System.IO.File.Exists(Convert.ToString(file)))
                    System.IO.File.Delete(Convert.ToString(file));
            }
            log.Debug("End: " + methodBase.Name);

        }

        public static DataTable GetCamaSeparetedValue(DataTable dt, string[] ColumnNames)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < ColumnNames.Length; j++)
                    {
                        if (dt.Columns.Contains(ColumnNames[j]))
                        {
                            dt.Rows[i][ColumnNames[j]] = AddThousandSeparators(dt.Rows[i][ColumnNames[j]].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return dt;
        }

        public static string AddThousandSeparators(string Value)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (Value != String.Empty)
                {
                    if (Value.Contains("."))
                    {
                        string[] str = Value.Split('.');
                        Value = Convert.ToInt64(str[0]).ToString("#,##0") + "." + str[1];
                        if (str[1].Length != 4)
                        {
                            Value = Convert.ToDecimal(Value).ToString("0.0000");
                        }
                        string[] result = Value.Split('.');
                        if (Convert.ToInt64(result[1]) == 0)
                        {
                            Value = result[0];
                        }
                    }
                    else
                    {
                        Value = Convert.ToInt64(Value).ToString("#,##0");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return Value;
        }

        public static int InsertScheduleHistory(string ScheduleName, string Parameter, string ScheduleDescription, bool RunbySystem)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@ScheduleName", ScheduleName);
                sqlParams[1] = new SqlParameter("@Parameter", Parameter);
                sqlParams[2] = new SqlParameter("@ScheduleDescription", ScheduleDescription);
                sqlParams[3] = new SqlParameter("@RunbySystem", RunbySystem);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertScheduleHistory", sqlParams);
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);

                return result;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

    }
}