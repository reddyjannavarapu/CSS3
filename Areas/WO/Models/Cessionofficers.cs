# region Document Header
//Created By       : Sudheer 
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
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    #region Cession officers


    public class Cessionofficers
    {
        private static ILog log = LogManager.GetLogger(typeof(Cessionofficers));

        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string SourceCode { get; set; }

        public int ClientID { get; set; }
        public string ClientSourceID { get; set; }
        public int NatureAppoint { get; set; }
        public int WOID { get; set; }

        public int DependingDirectorID { get; set; }
        public string DependingDirectorSource { get; set; }

        public string DateofResignation { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }

        public string DependingDirector { get; set; }
        public string Position { get; set; }

        public int IsExistsChildPerson { get; set; }


        #endregion

        #region Fetch Data

        private Cessionofficers CessationDirectors(Cessionofficers Directors, SafeDataReader dr)
        {
            Directors.ID = dr.GetInt32("ID");
            Directors.Name = dr.GetString("Name");
            Directors.SourceCode = dr.GetString("SourceCode");
            return Directors;
        }

        private Cessionofficers FetchCessationOfficers(Cessionofficers Directors, SafeDataReader dr)
        {
            Directors.ID = dr.GetInt32("CessionID");
            Directors.Name = dr.GetString("Cessation Officer");
            Directors.DateofResignation = dr.GetString("Date Of Resignation");
            Directors.Position = dr.GetString("Position");
            //  Directors.DependingDirector = dr.GetString("Depending Director");
            Directors.Email = dr.GetString("Email");
            Directors.ContactNo = dr.GetString("Phone");
            Directors.Fax = dr.GetString("Fax");
            Directors.NatureAppoint = dr.GetInt32("NatureAppoint");
            Directors.IsExistsChildPerson = dr.GetInt32("IsExistsChildPerson");
            return Directors;
        }

        private Cessionofficers FetchPositionDetails(Cessionofficers Directors, SafeDataReader dr)
        {
            Directors.Name = dr.GetString("Director Name");
            Directors.Position = dr.GetString("Position");
            Directors.Email = dr.GetString("Email");
            Directors.ContactNo = dr.GetString("Phone");
            Directors.Fax = dr.GetString("Fax");
            return Directors;
        }


        #endregion

        #region DataBaseMethods

        /// <summary>
        /// Description  : Get Directors Details from database.
        /// Created By   : Sudheer
        /// Created Date : 20 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static CessionofficersInfo GetCessationDirectors(int WOID, int DirectorID, string DirectorSource, int NatureAppoint)
        {
            var data = new CessionofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@DirctorID", DirectorID);
                sqlParams[2] = new SqlParameter("@DirectorSource", DirectorSource);
                sqlParams[3] = new SqlParameter("@NatureofOppintment", NatureAppoint);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetDirectorDetailsforCessationOfficer]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var directors = new Cessionofficers();
                    directors.CessationDirectors(directors, safe);
                    data.DirectorsList.Add(directors);
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
        /// Description  : Get Directors Details from database.
        /// Created By   : Sudheer
        /// Created Date : 20 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>           
        public static int InsertCessationDirectors(Cessionofficers cessationdetails, int CreatedBy)
        {
            int Rerurn = -1;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@ClientID", cessationdetails.ClientID);
                sqlParams[1] = new SqlParameter("@ClientSourceID", cessationdetails.ClientSourceID);
                sqlParams[2] = new SqlParameter("@NatureAppoint", cessationdetails.NatureAppoint);
                sqlParams[3] = new SqlParameter("@WOID", cessationdetails.WOID);
                sqlParams[4] = new SqlParameter("@DependingDirectorID", cessationdetails.DependingDirectorID);
                sqlParams[5] = new SqlParameter("@DependingDirectorSource", cessationdetails.DependingDirectorSource);
                sqlParams[6] = new SqlParameter("@DateofResignation", HelperClasses.ConvertDateFormat(cessationdetails.DateofResignation));
                sqlParams[7] = new SqlParameter("@Email", cessationdetails.Email);
                sqlParams[8] = new SqlParameter("@ContactNo", cessationdetails.ContactNo);
                sqlParams[9] = new SqlParameter("@Fax", cessationdetails.Fax);
                sqlParams[10] = new SqlParameter("@CreatedBy", CreatedBy);


                sqlParams[11] = new SqlParameter("@Output", 0);
                sqlParams[11].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertCessationOfficer]", sqlParams);

                Rerurn = Convert.ToInt16(sqlParams[11].Value);

                return Rerurn;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Rerurn;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Cessionofficers Details by WOID
        /// </summary>
        /// <returns></returns>
        public static CessionofficersInfo GetWOCessationDetailsByWOID(int WOID)
        {
            var GetWOCessionData = new CessionofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCessationOfficerDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Details = new Cessionofficers();
                    Details.FetchCessationOfficers(Details, safe);
                    GetWOCessionData.DirectorsList.Add(Details);
                }
                return GetWOCessionData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetWOCessionData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Cessionofficers Details by WOID
        /// </summary>
        /// <returns></returns>
        public static int DeleteCessationOfficerDetails(int ID)
        {
            int Rerurn = -1;
            var GetWOCessionData = new CessionofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Output", 0);
                sqlParams[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPDeleteCessationOfficerDetailsByID]", sqlParams);
                Rerurn = Convert.ToInt16(sqlParams[1].Value);
                return Rerurn;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Rerurn;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);

            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Cessionofficers Details by WOID
        /// </summary>
        /// <returns></returns>
        public static CessionofficersInfo GetWOCessionofficersPossionDetails(int CessId)
        {
            var GetWOCessionData = new CessionofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CessId", CessId);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCessionofficersPossionDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Details = new Cessionofficers();
                    Details.FetchPositionDetails(Details, safe);
                    GetWOCessionData.DirectorsList.Add(Details);
                }
                return GetWOCessionData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetWOCessionData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion

    }

    #region Cession officers Info class

    /// <summary>
    /// Description  : To do all events
    /// Created By   : Sudheer
    /// Created Date : 20 Nov 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class CessionofficersInfo
    {
        public List<Cessionofficers> DirectorsList { get; set; }
        public CessionofficersInfo()
        {
            DirectorsList = new List<Cessionofficers>();
        }
    }

    #endregion
    #endregion
}