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

namespace CSS2.Areas.Masters.Controllers
{
    #region Usings
    using CSS2.Areas.Masters.Models;
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using log4net;
    using System;
    using System.Web;
    using System.Web.Mvc;
    #endregion
    public class WOTypesController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(ServiceController));

        // GET: /Masters/WOTypes/
        [CheckUrlAccessCustomFilter]
        public ActionResult WOTypesA()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult MasterIndividual()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult MasterCorporation()
        {
            return View();
        }

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 15th Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Load the WOType records which are avilable in database       
        /// </summary>    
        [HttpPost]
        public JsonResult GetWOTypeData(string searchText, int status, int startpage, int rowsperpage, string OrderBy)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var Wotypes = WOTypes.GetAllWOTypes(startpage + 1, rowsperpage + startpage, HttpUtility.UrlDecode(searchText), status, OrderBy);
                    return Json(Wotypes);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Sudheer 
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Load the WOType Data which you want to edit      
        /// </summary>     
        [HttpPost]
        public JsonResult GetWOType(int WOTypeid)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var services = WOTypes.GetWOTypeById(WOTypeid);
                    return Json(services);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Delete the WOType A from DataBase
        /// </summary> 
        [HttpPost]
        public JsonResult DeleteWOTypeDoata(int id)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = WOTypes.DeleteWOTypeById(id);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the WOType A
        /// </summary>        
        [HttpPost]
        public JsonResult CreateWOTypeA(int WOTypeId, string WOName, string CategoryCode, string code, string desc, bool status)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                Int32 returnValue = -2;
                int checkSession = UserLogin.AuthenticateRequest();
                int createdBy = Convert.ToInt32(Session["UserID"]);

                if (checkSession == 0)
                {
                    return Json(returnValue);
                }
                else
                {
                    var data = new WOTypes
                    {
                        WOTypeId = WOTypeId,
                        WOCategoryCode = CategoryCode,
                        WOCode = HttpUtility.UrlDecode(code),
                        WOName = HttpUtility.UrlDecode(WOName),
                        WODescription = HttpUtility.UrlDecode(desc),
                        Status = status,
                        CreatedBy = createdBy
                    };

                    int result = data.InsertOrUpdateWOType();
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #region Master Corporation

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 30 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get All Corporation Data
        /// </summary>     
        [HttpPost]
        public JsonResult GetAllCorporationData(string CompanyName, string OrderBy, int startpage, int rowsperpage)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int checkSession = UserLogin.AuthenticateRequest();

            try
            {
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var Companies = Corporation.GetAllCorporationData(HttpUtility.UrlDecode(CompanyName), OrderBy, startpage + 1, rowsperpage + startpage);
                    return Json(Companies);
                }
            }

            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(checkSession);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region Master Individual

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 30 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get All Corporation Data
        /// </summary>     
        [HttpPost]
        public JsonResult GetAllIndividualData(string CompanyName, string OrderBy, int startpage, int rowsperpage)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int checkSession = UserLogin.AuthenticateRequest();

            try
            {
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var Individuals = Individual.GetAllIndividualData(HttpUtility.UrlDecode(CompanyName), OrderBy, startpage + 1, rowsperpage + startpage);
                    return Json(Individuals);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(checkSession);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region CAB Batch

        public ActionResult CabBatch()
        {
            return View();
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get the Batch Type
        /// </summary>        
        [HttpPost]
        public JsonResult GetMBatchType()
        {
            var BatchTypeData = CABBatch.GetMBatchType();
            return Json(BatchTypeData);
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get the Batch Type
        /// </summary>        
        [HttpPost]
        public JsonResult SaveCabBatchDetails(int ID, string BatchType, string BatchID, string FromDate, string ToDate)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int checkSession = UserLogin.AuthenticateRequest();

            try
            {
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                {
                    var objSaveCAb = new CABBatch()
                    {
                        ID = ID,
                        BatchCode = BatchType,
                        BatchID = BatchID,
                        FromDate = FromDate,
                        ToDate = ToDate,
                        SavedBy = checkSession

                    };
                    var BatchTypeData = objSaveCAb.SaveCabBatchDetails();
                    return Json(BatchTypeData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(checkSession);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get All Cab Batch details.
        /// </summary>     
        [HttpPost]
        public JsonResult GetCabBatchDetails(string BatchType, string OrderBy, int startpage, int rowsperpage)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int checkSession = UserLogin.AuthenticateRequest();

            try
            {
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var Companies = CABBatch.GetCabBatchDetails(BatchType, OrderBy, startpage + 1, rowsperpage + startpage);
                    return Json(Companies);
                }
            }

            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(checkSession);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Delete Cab Batch Details By ID.
        /// </summary>   
        [HttpPost]
        public JsonResult DeleteCabBatchDetailsByID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int checkSession = UserLogin.AuthenticateRequest();

            try
            {
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = CABBatch.DeleteCabBatchDetailsByID(ID);
                    return Json(checkSession);
                }
            }

            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(checkSession);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : Gap analysis by BatchType.
        /// Created By   : Shiva
        /// Created Date : 22 Dec 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetBabBatchGapByBatchType(string BatchType)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GapData = CABBatch.GetBabBatchGapByBatchType(BatchType);
                    return Json(GapData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion
    }
}