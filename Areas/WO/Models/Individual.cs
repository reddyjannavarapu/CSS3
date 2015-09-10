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

    public class Individual
    {
        private static ILog log = LogManager.GetLogger(typeof(Individual));

        #region Properties

        public string Name { get; set; }
        public int Nationality { get; set; }
        public string SingaporePR { get; set; }
        public string Passport { get; set; }
        public string NRICExpiryDate { get; set; }
        public string NRICExpiryDateInList { get; set; }
        public string DateOfBirth { get; set; }
        public string NricFinNo { get; set; }
        public string AccPacCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int Country { get; set; }
        public string PostalCode { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public int CreatedBy { get; set; }

        public int ID { get; set; }
        public string NationalityName { get; set; }
        public string CountryName { get; set; }

        #endregion

        #region Fetch Data

        private Individual FetchIndividual(Individual Individual, SafeDataReader dr)
        {
            Individual.ID = dr.GetInt32("ID");
            Individual.Name = dr.GetString("Name");
            Individual.Nationality = dr.GetInt32("Nationality");
            Individual.NationalityName = dr.GetString("NationalityName");
            Individual.SingaporePR = dr.GetString("SingaporePR");
            Individual.Passport = dr.GetString("Passport");
            Individual.NRICExpiryDate = dr.GetDateTime("NRICExpiryDate").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("NRICExpiryDate").ToString("dd/MM/yyyy");
            Individual.DateOfBirth = dr.GetDateTime("DateOfBirth").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("DateOfBirth").ToString("dd/MM/yyyy");
            Individual.NRICExpiryDateInList = Individual.NRICExpiryDate == "" ? "" : dr.GetDateTime("NRICExpiryDate").ToString("dd MMM yyyy");
            Individual.NricFinNo = dr.GetString("NricFinNo");
            Individual.AccPacCode = dr.GetString("AccpacCode");
            Individual.AddressLine1 = dr.GetString("AddressLine1");
            Individual.AddressLine2 = dr.GetString("AddressLine2");
            Individual.AddressLine3 = dr.GetString("AddressLine3");
            Individual.Country = dr.GetInt32("Country");
            Individual.CountryName = dr.GetString("CountryName");
            Individual.PostalCode = dr.GetString("PostalCode");
            Individual.Occupation = dr.GetString("Occupation");
            Individual.Email = dr.GetString("Email");
            Individual.ContactNo = dr.GetString("Phone");
            Individual.Fax = dr.GetString("Fax");
            return Individual;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description   : To Insert Individual
        /// Created By    : Pavan
        /// Created Date  : 22 July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public int InsertIndividual()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[19];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Name", Name);
                sqlParams[2] = new SqlParameter("@Nationality", Nationality);
                sqlParams[3] = new SqlParameter("@SingaporePR", SingaporePR);
                sqlParams[4] = new SqlParameter("@Passport", Passport);
                sqlParams[5] = new SqlParameter("@DateofBirth", HelperClasses.ConvertDateFormat(DateOfBirth));
                sqlParams[6] = new SqlParameter("@NricFinNo", NricFinNo);
                sqlParams[7] = new SqlParameter("@AccPacCode", AccPacCode);
                sqlParams[8] = new SqlParameter("@AddressLine1", AddressLine1);
                sqlParams[9] = new SqlParameter("@AddressLine2", AddressLine2);
                sqlParams[10] = new SqlParameter("@AddressLine3", AddressLine3);
                sqlParams[11] = new SqlParameter("@Country", Country);
                sqlParams[12] = new SqlParameter("@PostalCode", PostalCode);
                sqlParams[13] = new SqlParameter("@Occupation", Occupation);
                sqlParams[14] = new SqlParameter("@Email", Email);
                sqlParams[15] = new SqlParameter("@ContactNumber", ContactNo);
                sqlParams[16] = new SqlParameter("@Fax", Fax);
                sqlParams[17] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[18] = new SqlParameter("@NRICExpiryDate", HelperClasses.ConvertDateFormat(NRICExpiryDate));
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertIndividual", sqlParams);
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
        /// Description   : To Get Individual Data
        /// Created By    : Pavan
        /// Created Date  : 1 October 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static IndividualsInfo GetAllIndividualData(string CompanyName, string OrderBy, int startPage, int resultPerPage)
        {
            var data = new IndividualsInfo();

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
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllIndividuals]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Individual = new Individual();
                    Individual.FetchIndividual(Individual, safe);
                    data.IndividualList.Add(Individual);
                    data.IndividualCount = Convert.ToInt32(reader["IndividualCount"]);
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

    public class IndividualsInfo
    {
        public List<Individual> IndividualList { get; set; }
        public int IndividualCount { get; set; }

        public IndividualsInfo()
        {
            IndividualList = new List<Individual>();
        }
    }

    #endregion

}