# region Document Header
//Created By       : Anji 
//Created Date     : 
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.WO.Models
{
    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    public class Corporation
    {
        private static ILog log = LogManager.GetLogger(typeof(Corporation));

        #region Properties

        public string NameOfCompany { get; set; }
        public int CountryOfIncorporation { get; set; }
        public string DateOfIncorporation { get; set; }
        public string RegistrationNo { get; set; }
        public string AccPacCode { set; get; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int Country { get; set; }
        public string PostalCode { get; set; }
        public int createdBy { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public string DateOfIncorporationList { set; get; }
        public int ID { get; set; }
        public string CountryNameOfIncorp { get; set; }
        public string CountryName { get; set; }

        #endregion

        #region Fetch Data

        private Corporation FetchCorporation(Corporation Corporation, SafeDataReader dr)
        {
            Corporation.ID = dr.GetInt32("ID");
            Corporation.NameOfCompany = dr.GetString("NameOfCompany");
            Corporation.CountryOfIncorporation = dr.GetInt32("CountryOfIncorporation");
            Corporation.CountryNameOfIncorp = dr.GetString("CountryNameOfIncorp");
            Corporation.DateOfIncorporation = dr.GetDateTime("DateOfIncorporation").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("DateOfIncorporation").ToString("dd/MM/yyyy");
            Corporation.DateOfIncorporationList = Corporation.DateOfIncorporation == "" ? "" : dr.GetDateTime("DateOfIncorporation").ToString("dd MMM yyyy");
            Corporation.RegistrationNo = dr.GetString("RegistrationNo");
            Corporation.AccPacCode = dr.GetString("AccpacCode");
            Corporation.AddressLine1 = dr.GetString("AddressLine1");
            Corporation.AddressLine2 = dr.GetString("AddressLine2");
            Corporation.AddressLine3 = dr.GetString("AddressLine3");
            Corporation.Country = dr.GetInt32("Country");
            Corporation.CountryName = dr.GetString("CountryName");
            Corporation.PostalCode = dr.GetString("PostalCode");
            Corporation.Email = dr.GetString("Email");
            Corporation.ContactNo = dr.GetString("Phone");
            Corporation.Fax = dr.GetString("Fax");
            return Corporation;
        }

        #endregion

        #region DataBaseMethods

        /// <summary>
        /// Description   : To Insert Company
        /// Created By    : Pavan
        /// Created Date  : 22 July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public int InsertCorporation()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[15];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@NameofCompany", this.NameOfCompany);
                sqlParams[2] = new SqlParameter("@CountryofIncorporation", this.CountryOfIncorporation);
                sqlParams[3] = new SqlParameter("@DateofIncorporation", HelperClasses.ConvertDateFormat(this.DateOfIncorporation));
                sqlParams[4] = new SqlParameter("@RegistrationNo", this.RegistrationNo);
                sqlParams[5] = new SqlParameter("@AccPacCode", this.AccPacCode);
                sqlParams[6] = new SqlParameter("@AddressLine1", this.AddressLine1);
                sqlParams[7] = new SqlParameter("@AddressLine2", this.AddressLine2);
                sqlParams[8] = new SqlParameter("@AddressLine3", this.AddressLine3);
                sqlParams[9] = new SqlParameter("@Country", this.Country);
                sqlParams[10] = new SqlParameter("@PostalCode", this.PostalCode);
                sqlParams[11] = new SqlParameter("@Email", this.Email);
                sqlParams[12] = new SqlParameter("@ContactNo", this.ContactNo);
                sqlParams[13] = new SqlParameter("@Fax", this.Fax);
                sqlParams[14] = new SqlParameter("@CreatedBy", this.createdBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertCorporation", sqlParams);
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

        /// <summary>
        /// Description   : To Get Company Data
        /// Created By    : Pavan
        /// Created Date  : 30 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static CorporationInfo GetAllCorporationData(string CompanyName, string OrderBy, int startPage, int resultPerPage)
        {
            var data = new CorporationInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@CompanyName", CompanyName);
                sqlParams[3] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllCorporation]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Corporation = new Corporation();
                    Corporation.FetchCorporation(Corporation, safe);
                    data.CorporationList.Add(Corporation);
                    data.CorporationCount = Convert.ToInt32(reader["CorporationCount"]);
                }
                return data;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return data;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 16 Oct 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get Corporation Details by ID
        /// </summary>    
        public static CorporationInfo GetCorporationDetailsByID(int ID)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var data = new CorporationInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCorporationDetailsByID", sqlParams);
                while (reader.Read())
                {
                    var safe = new SafeDataReader(reader);
                    var Corporation = new Corporation();
                    Corporation.FetchCorporation(Corporation, safe);
                    data.CorporationList.Add(Corporation);
                }
                return data;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return data;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

    }

    #region InfoClass

    public class CorporationInfo
    {
        public List<Corporation> CorporationList { get; set; }
        public int CorporationCount { get; set; }

        public CorporationInfo()
        {
            CorporationList = new List<Corporation>();
        }
    }

    #endregion
}