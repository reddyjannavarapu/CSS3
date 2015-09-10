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

    public class Appoinmentofficers
    {
        private static ILog log = LogManager.GetLogger(typeof(Appoinmentofficers));

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
                
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }

        public string DependingDirector { get; set; }
        public string Position { get; set; }


        #endregion

        #region Fetch Data

        private Appoinmentofficers FetchPositionDetails(Appoinmentofficers Directors, SafeDataReader dr)
        {
            Directors.Name = dr.GetString("Director Name");                 
            Directors.Position = dr.GetString("Position");            
            Directors.Email = dr.GetString("Email");
            Directors.ContactNo = dr.GetString("Phone");
            Directors.Fax = dr.GetString("Fax");
            return Directors;
        }

        private Appoinmentofficers FetchCessationOfficers(Appoinmentofficers Directors, SafeDataReader dr)
        {
            Directors.ID = dr.GetInt32("ApptID");
            Directors.Name = dr.GetString("Appoinment Officer");           
            Directors.Position = dr.GetString("Position");
            Directors.NatureAppoint = dr.GetInt32("NatureAppoint");
            Directors.Email = dr.GetString("Email");
            Directors.ContactNo = dr.GetString("Phone");
            Directors.Fax = dr.GetString("Fax");
            return Directors;
        }

        #endregion

        #region DataBaseMethods

        /// <summary>
        /// Description  : insert Directors Details To database.
        /// Created By   : Sudheer
        /// Created Date : 20 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>           
        public static int InsertAppoinmentDirectors(Appoinmentofficers Apptofdetails, int CreatedBy)
        {
            int Rerurn = -1;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@ClientID", Apptofdetails.ClientID);
                sqlParams[1] = new SqlParameter("@ClientSourceID", Apptofdetails.ClientSourceID);
                sqlParams[2] = new SqlParameter("@NatureAppoint", Apptofdetails.NatureAppoint);
                sqlParams[3] = new SqlParameter("@WOID", Apptofdetails.WOID);
                sqlParams[4] = new SqlParameter("@DependingDirectorID", Apptofdetails.DependingDirectorID);
                sqlParams[5] = new SqlParameter("@DependingDirectorSource", Apptofdetails.DependingDirectorSource);
                sqlParams[6] = new SqlParameter("@Email", Apptofdetails.Email);
                sqlParams[7] = new SqlParameter("@ContactNo", Apptofdetails.ContactNo);
                sqlParams[8] = new SqlParameter("@Fax", Apptofdetails.Fax);
                sqlParams[9] = new SqlParameter("@CreatedBy", CreatedBy);

                sqlParams[10] = new SqlParameter("@Output", 0);
                sqlParams[10].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertAppointmentOfficer]", sqlParams);

                Rerurn = Convert.ToInt16(sqlParams[10].Value);

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
        public static AppoinmentofficersInfo GetWOApptOfficersDetailsByWOID(int WOID)
        {
            var GetWOCessionData = new AppoinmentofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAppointmentOfficerDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Details = new Appoinmentofficers();
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
        public static int DeleteAppointmentOfficerDetails(int ID)
        {
            int Rerurn = -1;
            var GetWOCessionData = new Appoinmentofficers();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@Output", 0);
                sqlParams[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPDeleteAppointmentOfficerDetailsByID]", sqlParams);
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

        #endregion
  
        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Cessionofficers Details by WOID
        /// </summary>
        /// <returns></returns>
        public static AppoinmentofficersInfo GetWOApptOfficersPossionDetails(int ApptId)
        {
            var GetWOCessionData = new AppoinmentofficersInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ApptId", ApptId);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAppointmentOfficerPossionDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Details = new Appoinmentofficers();
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
    }

    #region Cession officers Info class

    /// <summary>
    /// Description  : To do all events
    /// Created By   : Sudheer
    /// Created Date : 20 Nov 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class AppoinmentofficersInfo
    {
        public List<Appoinmentofficers> DirectorsList { get; set; }
        public AppoinmentofficersInfo()
        {
            DirectorsList = new List<Appoinmentofficers>();
        }
    }

    #endregion
}