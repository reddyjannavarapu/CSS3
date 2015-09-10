using CSS2.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CSS2.Areas.SystemReports.Models
{
    public class SystemReports
    {
        private static ILog log = LogManager.GetLogger(typeof(SystemReports));



        #region Properties

        public string Description { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string ClientNo { set; get; }
        public int CompanyID { set; get; }
        public string Source { set; get; }
        public string GroupCode { set; get; }
        public string ItemNo { set; get; }
        public bool InHouse { set; get; }
        public string WOStatus { set; get; }
        public string Assignee { set; get; }
        public string Billable { set; get; }
        public string WoCode { get; set; }
        //VendorVerificationReport
        public string DIitem { set; get; }
        public string VRID { set; get; }
        public string DIStatus { set; get; } //End

        //ClientFeeServiceReport
        public string FeeServiceType { set; get; }

        //NomineeDirectorReport
        public string NomineeDirector { set; get; }

        //NomineeSecretary
        public string NomineeSecretary { set; get; }

        //FutureBill Report
        public string BillingFrequency { set; get; }
        public string BillingDate { set; get; }
        public int Month { get; set; }

        #endregion



        /// <summary>
        /// Description  : To get Download InVoice From CSS2.
        /// Created By   : sudheer
        /// Created Date : 4 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet DIReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@Description", this.Description);
            sqlParams[1] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
            sqlParams[2] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));
            sqlParams[3] = new SqlParameter("@ClientNo", this.ClientNo);
            sqlParams[4] = new SqlParameter("@CompanyID", this.CompanyID);
            sqlParams[5] = new SqlParameter("@Source", this.Source);
            sqlParams[6] = new SqlParameter("@GroupCode", this.GroupCode);
            sqlParams[7] = new SqlParameter("@ItemNo", this.ItemNo);

            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForDIItems]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To get Download In-House DI InVoice From CSS2.
        /// Created By   : Shiva
        /// Created Date :4 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet InHouseDIReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@Type", this.ItemNo);
            sqlParams[1] = new SqlParameter("@Description", this.Description);
            sqlParams[2] = new SqlParameter("@Group", this.GroupCode);
            sqlParams[3] = new SqlParameter("@InHouse", this.InHouse);
            sqlParams[4] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
            sqlParams[5] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));
            sqlParams[6] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[7] = new SqlParameter("@Source", Source);
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetSystemReportsForInHouseDIItems", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get Vendor Verification Report
        /// Created By   : Sudheer
        /// Created Date : 4th Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet VendorVerificationReport(SystemReports objSystemReports)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(objSystemReports.FromDate));
            sqlParams[1] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(objSystemReports.ToDate));
            sqlParams[2] = new SqlParameter("@DIitem", objSystemReports.DIitem);
            sqlParams[3] = new SqlParameter("@VRID", objSystemReports.VRID);
            sqlParams[4] = new SqlParameter("@DIStatus", objSystemReports.DIStatus);

            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForVendorVerification]", sqlParams);
                // ds.Tables[0].Rows[0].Delete();

                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get Download InVoice From CSS2.
        /// Created By   : sudheer
        /// Created Date : 4 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet ClientFeeServiceReport(SystemReports obCFSR)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(obCFSR.FromDate));
            sqlParams[1] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(obCFSR.ToDate));
            sqlParams[2] = new SqlParameter("@ClientNo", obCFSR.ClientNo);
            sqlParams[3] = new SqlParameter("@CompanyID", obCFSR.CompanyID);
            sqlParams[4] = new SqlParameter("@Source", obCFSR.Source);
            sqlParams[5] = new SqlParameter("@FeeServiceType", obCFSR.FeeServiceType);
            sqlParams[6] = new SqlParameter("@GroupCode", obCFSR.GroupCode);

            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForFeeService]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : To get WO Report.
        /// Created By   : Shiva
        /// Created Date : 5 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet WOReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@GroupCode", this.GroupCode);
            sqlParams[1] = new SqlParameter("@ClientNo", this.ClientNo);
            sqlParams[2] = new SqlParameter("@CompanyID", this.CompanyID);
            sqlParams[3] = new SqlParameter("@Source", this.Source);
            sqlParams[4] = new SqlParameter("@NameOfAssignee", this.Assignee);
            sqlParams[5] = new SqlParameter("@WOStatus", this.WOStatus);
            sqlParams[6] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
            sqlParams[7] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));
            sqlParams[8] = new SqlParameter("@Billable", this.Billable);


            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetSystemReportsForWO", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To get Download InVoice From CSS2.
        /// Created By   : sudheer
        /// Created Date : 6 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet NomineeDirectorReport(SystemReports objSystemReports)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@NomineeDirector", objSystemReports.NomineeDirector);
            sqlParams[1] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(objSystemReports.FromDate));
            sqlParams[2] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(objSystemReports.ToDate));
            sqlParams[3] = new SqlParameter("@ClientNo", objSystemReports.ClientNo);
            sqlParams[4] = new SqlParameter("@CompanyID", objSystemReports.CompanyID);
            sqlParams[5] = new SqlParameter("@Source", objSystemReports.Source);
            sqlParams[6] = new SqlParameter("@GroupCode", objSystemReports.GroupCode);


            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForNomineeDirector]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To get Download InVoice From CSS2.
        /// Created By   : sudheer
        /// Created Date : 6 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet NomineeSecretaryReport(SystemReports objSystemReports)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@NomineeSecretary", objSystemReports.NomineeSecretary);
            sqlParams[1] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(objSystemReports.FromDate));
            sqlParams[2] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(objSystemReports.ToDate));
            sqlParams[3] = new SqlParameter("@ClientNo", objSystemReports.ClientNo);
            sqlParams[4] = new SqlParameter("@CompanyID", objSystemReports.CompanyID);
            sqlParams[5] = new SqlParameter("@Source", objSystemReports.Source);
            sqlParams[6] = new SqlParameter("@GroupCode", objSystemReports.GroupCode);


            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForNomineeSecretary]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To get FEE And DI Report
        /// Created By   : Shiva
        /// Created Date : 5 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet FEEAndDIItemsReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@WOCode", this.WoCode);
            sqlParams[1] = new SqlParameter("@CompanyID", this.CompanyID);
            sqlParams[2] = new SqlParameter("@Source", this.Source);
            sqlParams[3] = new SqlParameter("@GroupCode", this.GroupCode);
            sqlParams[4] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
            sqlParams[5] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));

            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetSystemReportsForDIAndFeeItems", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get Schedule FEE And DI Report
        /// Created By   : Shiva
        /// Created Date : 5 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet ScheduleFEEAndDIItemsReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);


            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@WOCode", this.WoCode);
            sqlParams[1] = new SqlParameter("@CompanyID", this.CompanyID);
            sqlParams[2] = new SqlParameter("@Source", this.Source);
            sqlParams[3] = new SqlParameter("@GroupCode", this.GroupCode);
            sqlParams[4] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
            sqlParams[5] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));

            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetSystemReportsForScheduleDIAndFeeItems]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get Future Bill Report
        /// Created By   : Pavan
        /// Created Date : 19 Jan 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public DataSet FutureBillReport()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@SCCode", this.BillingFrequency);
            sqlParams[1] = new SqlParameter("@ONDate", HelperClasses.ConvertDateFormat(this.BillingDate));
            sqlParams[2] = new SqlParameter("@Month", this.Month);
            sqlParams[3] = new SqlParameter("@ClientCode", this.CompanyID);
            sqlParams[4] = new SqlParameter("@SourceID", this.Source);
            sqlParams[5] = new SqlParameter("@FeeCode", this.FeeServiceType);
            sqlParams[6] = new SqlParameter("@GroupName", this.GroupCode);
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpCABFeeSchCalculationNextBill]", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

    }
}