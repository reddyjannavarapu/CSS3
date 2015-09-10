# region Document Header
//Created By       : Anji 
//Created Date     : 05 May 2014
//Description      : To Manage Services
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.Masters.Models
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using CSS2.Models;
    using CSS2.Areas.UserManagement.Models;
    using log4net;
    #endregion

    [MetadataType(typeof(ServiceMetadata))]
    public partial class Service
    {
        private static ILog log = LogManager.GetLogger(typeof(Service));

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }

        #endregion

        #region Fetching Data
        private Service FetchService(Service service, SafeDataReader dr)
        {
            service.Id = dr.GetInt32("ID");
            service.Code = dr.GetString("Code");
            service.Description = dr.GetString("Description");
            service.Status = dr.GetBoolean("Status");
            return service;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Show all the Services in View
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the Service details available in database</returns>
        public static ServiceInfo GetAllServices(int startPage, int resultPerPage, string code, int status, string OrderBy)
        {
            var data = new ServiceInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@code", code);
                sqlParams[3] = new SqlParameter("@status", status);
                sqlParams[4] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMServices]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var service = new Service();
                    service.FetchService(service, safe);
                    data.ServiceList.Add(service);
                    data.ServiceCount = Convert.ToInt32(reader["serviceCount"]);
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
        /// Description  : To insert/update service details into database
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="e">pass the Service object to insert details</param>
        /// <returns></returns>
        public int InsertService()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", this.Id);
                sqlParams[1] = new SqlParameter("@Code", this.Code);
                sqlParams[2] = new SqlParameter("@Description", this.Description);
                sqlParams[3] = new SqlParameter("@Status", this.Status);
                sqlParams[4] = new SqlParameter("@CreatedBy", this.CreatedBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertMService", sqlParams);
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
        /// Description  : To Get Service Which you want to Edit
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="serviceId">pass the Service id which Service details you wont </param>
        /// <returns>it will give service </returns>
        public static Service GetServiceById(int? id)
        {
            var services = new Service();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@id", id);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetServiceById", sqlParams);

                while (reader.Read())
                {
                    services.FetchService(services, new SafeDataReader(reader));
                }
                return services;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return services;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Delete Service By Service Id
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="serviceId">pass the Service id which you wont to delete</param>
        /// <returns></returns>
        public static int DeleteServiceById(int id)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@id", id);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteMServiceById", sqlParams);
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

        #endregion
    }

    #region InfoClass
    /// <summary>
    /// Description  : To do all events in same view
    /// Created By   : Anji
    /// Created Date : 05 May 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class ServiceInfo
    {
        public List<Service> ServiceList { get; set; }
        public int ServiceCount { get; set; }

        public ServiceInfo()
        {
            ServiceList = new List<Service>();
        }
    }
    #endregion
}