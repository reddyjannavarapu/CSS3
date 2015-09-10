using CSS2.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSS2.Areas.Billing.Models
{
    public class BillingThirdParty
    {
        private static ILog log = LogManager.GetLogger(typeof(BillingThirdParty));

        #region Properties

        public int ID { set; get; }
        public string CompanyName { set; get; }
        public string ACCPACCode { set; get; }
        public string AddressLine1 { set; get; }
        public string AddressLine2 { set; get; }
        public string AddressLine3 { set; get; }
        public int CountryCode { set; get; }
        public string PostalCode { set; get; }
        public int SavedBy { set; get; }
        public string CountryName { set; get; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public int IsUsed { get; set; }

        #endregion

        #region FetchMethods

        private BillingThirdParty FetThirdPartyBillingData(BillingThirdParty objBillingData, SafeDataReader dr)
        {
            objBillingData.ID = dr.GetInt32("ID");
            objBillingData.CompanyName = dr.GetString("CompanyName");
            objBillingData.ACCPACCode = dr.GetString("ACCPACCode");
            objBillingData.AddressLine1 = dr.GetString("AddressLine1");
            objBillingData.AddressLine2 = dr.GetString("AddressLine2");
            objBillingData.AddressLine3 = dr.GetString("AddressLine3");
            objBillingData.CountryCode = dr.GetInt32("CountryCode");
            objBillingData.PostalCode = dr.GetString("PostalCode");
            objBillingData.CountryName = dr.GetString("CountryName");
            objBillingData.Email = dr.GetString("Email");
            objBillingData.ContactNo = dr.GetString("Phone");
            objBillingData.Fax = dr.GetString("Fax");
            objBillingData.IsUsed = dr.GetInt32("IsUsed");
            return objBillingData;
        }


        #endregion


        #region DatabaseMethod

        /// <summary>
        /// Description  : To Save the Billing third party details.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public int SaveBillingThirdPartyDetails()
        {
            int savedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@CompanyName", this.CompanyName);
                sqlParams[2] = new SqlParameter("@ACCPACCode", this.ACCPACCode);
                sqlParams[3] = new SqlParameter("@AddressLine1", this.AddressLine1);
                sqlParams[4] = new SqlParameter("@AddressLine2", this.AddressLine2);
                sqlParams[5] = new SqlParameter("@AddressLine3", this.AddressLine3);
                sqlParams[6] = new SqlParameter("@CountryCode", this.CountryCode);
                sqlParams[7] = new SqlParameter("@PostalCode", this.PostalCode);
                sqlParams[8] = new SqlParameter("@Email", this.Email);
                sqlParams[9] = new SqlParameter("@ContactNo", this.ContactNo);
                sqlParams[10] = new SqlParameter("@Fax", this.Fax);
                sqlParams[11] = new SqlParameter("@CreatedBy", this.SavedBy);
                savedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveBillingThirdPartyDetails", sqlParams);
                return savedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return savedStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Delete the Billing third party details by ID.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int DeleteBillingThirdPartyDetailsByID(int ID)
        {
            int DeletedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);
                DeletedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPDeleteBillingThirdPartyDetailsByID", sqlParams);
                return DeletedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return DeletedStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get all Billing third party details.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static BillingThirdPartyInfo BindThirdPartyBillingDetails(string CompanyName, string OrderBy, int StartPage, int RowsPerPage)
        {
            var billingDataInfo = new BillingThirdPartyInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@StatPage", StartPage);
                sqlParams[1] = new SqlParameter("@RowsPerPage", RowsPerPage);
                sqlParams[2] = new SqlParameter("@CompanyName", CompanyName);
                sqlParams[3] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetBillingThirdPartyDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var billingThirdparty = new BillingThirdParty();
                    billingThirdparty.FetThirdPartyBillingData(billingThirdparty, safe);
                    billingDataInfo.BillingThirdPartyList.Add(billingThirdparty);
                    billingDataInfo.RecordsCount = Convert.ToInt32(reader["RecordsCount"]);
                }
                return billingDataInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return billingDataInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion

        public class BillingThirdPartyInfo
        {
            public List<BillingThirdParty> BillingThirdPartyList { set; get; }
            public int RecordsCount { set; get; }
            public BillingThirdPartyInfo()
            {
                BillingThirdPartyList = new List<BillingThirdParty>();
            }
        }
    }
}