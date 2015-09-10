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


namespace CSS2.Areas.WO.Controllers
{
    #region Usings
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.Mvc;
    using log4net;
    #endregion

    public class PartialContentController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(PartialContentController));

        //
        // GET: /WO/ClientChosen/
        public ActionResult _ClientChosen(string ddlId, string clientType, string selectedId, string DisplayName, bool DefaultLoad, string WOID, string ClientSource)
        {
            ViewBag.ddlId = ddlId;
            ViewBag.clientType = clientType;
            ViewBag.selectedId = selectedId;
            ViewBag.DisplayName = DisplayName;
            ViewBag.DefaultLoad = DefaultLoad;
            ViewBag.ClientSource = ClientSource;
            if (string.IsNullOrEmpty(WOID))
                ViewBag.WOID = "";
            else
                ViewBag.WOID = WOID;
            return View();
        }

        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Shiva  
        /// Created Date : 2nd July 2014
        /// Modified By  : Sudheer (Added Billing Party Logic)
        /// Modified Date: 14th Oct 2014
        /// </summary>     
        [HttpPost]
        public JsonResult GetChosenClientInfo(string clientType, string ClientName, string WOID, string ClientSource, string groupcode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (clientType == "BillingParty")
                {
                    var ClientInformation = WO.Models._ChoosenClient.GetBillingPartyInformation(ClientName, WOID);
                    return Json(ClientInformation);
                }
                else
                {
                    var ClientInformation = WO.Models._ChoosenClient.GetClientInformation(clientType, ClientName, WOID, ClientSource, groupcode);
                    return Json(ClientInformation);
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

        //
        // GET: /WO/ClientChosen/
        public ActionResult _GroupInfo(string ddlId, string UserType, string selectedId, string DisplayName)
        {
            ViewBag.ddlId = ddlId;
            ViewBag.UserType = UserType;
            ViewBag.selectedId = selectedId;
            ViewBag.DisplayName = DisplayName;
            return View();
        }

        public ActionResult _DIItemChosen(string Id, string DisplayName, int DefaultLoad)
        {
            ViewBag.Id = Id;
            ViewBag.DisplayName = DisplayName;
            ViewBag.DefaultLoad = DefaultLoad;
            return View();
        }

        /// <summary>
        /// Description  : Get the Group information from CSS1
        /// Created By   : Shiva
        /// Created Date : 10 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetGroupInfo()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var GroupInfo = WO.Models.GroupInfo.GetCSS1GroupDetails();
                return Json(GroupInfo);
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
        /// Created By   : Pavan
        /// Created Date : 4 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To return view Director Details with element Id
        /// </summary>
        /// <returns></returns>
        public ActionResult _DirectorsChosen(string Id, string selectedId, string SearchType, string SearchRole, int WOID, bool DefaultLoad, bool AutoSearch, string NatureAppoint, string DisplayName)
        {
            ViewBag.Id = Id;
            ViewBag.selectedId = selectedId;
            ViewBag.SearchType = SearchType;
            ViewBag.SearchRole = SearchRole;
            ViewBag.WOID = WOID;

            if (string.IsNullOrEmpty(DisplayName))
                ViewBag.DisplayName = "";
            else
                ViewBag.DisplayName = DisplayName;

            if (string.IsNullOrEmpty(NatureAppoint))
                ViewBag.NatureAppoint = "";
            else
                ViewBag.NatureAppoint = NatureAppoint;

            ViewBag.DefaultLoad = DefaultLoad;
            ViewBag.AutoSearch = AutoSearch;
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 31 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get Director Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDirectorDetails(string SearchType, string SearchKeyWord, string SearchRole, int WOID, string NatureAppoint, int ClassOfShare)
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
                    var result = Masters.Models.Masters.Directors.GetDirectorsDetails(SearchType, SearchKeyWord, SearchRole, WOID, NatureAppoint,ClassOfShare);
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
        /// Created By   : Sudheer
        /// Created Date : 6th Aug 2013
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get WO Information Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult WOInformationDetails(string GridCode, string WOID)
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
                    DataSet result = Masters.Models.Masters.WOInformationDetails.GetWOInformationDetails(WOID, GridCode);

                    if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0 && result.Tables[0].Columns.Contains("Dummy"))
                        result.Tables[0].Columns.Remove("Dummy");

                    if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0 && result.Tables[0].Columns.Contains("Not Seeking Reelection"))
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToString(result.Tables[0].Rows[i]["Not Seeking Reelection"]) == "1")
                                result.Tables[0].Rows[i]["Not Seeking Reelection"] = "Yes";
                            else
                                result.Tables[0].Rows[i]["Not Seeking Reelection"] = "No";
                        }
                    }

                    if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0 && result.Tables[0].Columns.Contains("Signing Director"))
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToString(result.Tables[0].Rows[i]["Signing Director"]) == "1")
                                result.Tables[0].Rows[i]["Signing Director"] = "Yes";
                            else
                                result.Tables[0].Rows[i]["Signing Director"] = "No";
                        }
                    }

                    if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0 && result.Tables[0].Columns.Contains("IsSigningDirector"))
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToString(result.Tables[0].Rows[i]["IsSigningDirector"]) == "1")
                                result.Tables[0].Rows[i]["IsSigningDirector"] = "Yes";
                            else
                                result.Tables[0].Rows[i]["IsSigningDirector"] = "No";
                        }
                    }

                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);

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
        /// Created By   : Sudheer
        /// Created Date : 8th Aug 2013
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get WO Information Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOInformationDetails(string WOID, string InfoCode, string PersonID, string PersonSource)
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
                    int result = 0;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(InfoCode) && !string.IsNullOrEmpty(PersonID) && !string.IsNullOrEmpty(PersonSource))
                        result = Masters.Models.Masters.WOInformationDetails.DeleteWOInformationdetails(Convert.ToInt32(WOID), InfoCode, Convert.ToInt32(PersonID), PersonSource);

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
        /// Created By   : Sudheer
        /// Created Date : 6th Aug 2013
        /// Modified By  :
        /// Modified Date:
        /// Description  : Converting DataTable to JsonParameter.
        /// </summary>
        /// <returns>Json Parameter</returns>
        public string CreateJsonParameters(DataTable dt)
        {

            StringBuilder JsonString = new StringBuilder();

            //Exception Handling
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"Table\":[ ");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() +
                                  "\":" + "\"" +
                                  dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" +
                               dt.Columns[j].ColumnName.ToString() + "\":" +
                               "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }

                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        public ActionResult _UsersList(string ddlId, string UserType, string selectedId, int I, int C, string DisplayName)
        {
            ViewBag.ddlId = ddlId;
            ViewBag.UserType = UserType;
            ViewBag.selectedId = selectedId;
            ViewBag.DisplayName = DisplayName;
            ViewBag.I = I;
            ViewBag.C = C;
            return View();
        }

        [HttpPost]
        public JsonResult GetUsersList(string UserType, string selectedId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var users = UserManagement.Models.UserDetails.GetAllUserDetails(1, 1000, string.Empty, string.Empty, -1, string.Empty);
                return Json(users);
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

        [HttpPost]
        public JsonResult GetCountryDetails()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.Country.GetCountryDetails();
                return Json(data);
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
        /// Description  : To Get all Currency details
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will return all names from database</returns>
        [HttpPost]
        public JsonResult GetCurrencyDetails()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.Currency.GetCurrencyDetails();
                return Json(data);
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
        /// Description  : To Get all Class company type details
        /// Created By   : Sudheer
        /// Created Date : 31 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will return all names from database</returns>
        [HttpPost]
        public JsonResult GetCompanyTypeDetails(int IsIncorp)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.CompanyType.GetCompanyTypeDetails(IsIncorp);
                return Json(data);
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
        /// Description  : To Get all Class of share details
        /// Created By   : Sudheer
        /// Created Date : 31 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will return all names from database</returns>
        [HttpPost]
        public JsonResult GetShareClassDetails(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.ShareClass.GetShareClassDetails(WOID);
                return Json(data);
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
        /// Description  : To Get Client Type details
        /// Created By   : Shiva
        /// Created Date : 24 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will return all names from database</returns>
        [HttpPost]
        public JsonResult GetClientType()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.ClientType.GetClientType();
                return Json(data);
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
        /// Description  : To Get Company Status details
        /// Created By   : Shiva
        /// Created Date : 24 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will return all names from database</returns>
        [HttpPost]
        public JsonResult GetCompanyStatus()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Masters.CompanyStatus.GetCompanyStatus();
                return Json(data);
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






        public ActionResult _CreateIndividual()
        {
            return View();
        }

        /// <summary>
        /// Description   : To Insert Individual
        /// Created By    : Pavan
        /// Created Date  : 22 July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult CreateIndividual(Individual Individual)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                Int32 returnValue = 0;
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(returnValue);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    //var data = new Individual
                    //{
                    //    ID = ID,
                    //    Name = Name,
                    //    Nationality = Nationality,
                    //    SingaporePR = SingaporePR,
                    //    Passport = Passport,
                    //    NRICExpiryDate = NRICExpiryDate,
                    //    DateOfBirth = DateOfBirth,
                    //    NricFinNo = NricFinNo,
                    //    AccPacCode = AccPacCode,
                    //    AddressLine1 = AddressLine1,
                    //    AddressLine2 = AddressLine2,
                    //    AddressLine3 = AddressLine3,
                    //    Country = Country,
                    //    PostalCode = PostalCode,
                    //    Occupation = Occupation,
                    //    Email = Email,
                    //    ContactNo = ContactNo,
                    //    Fax = Fax,
                    //    CreatedBy = createdBy
                    //};
                    Individual.CreatedBy = createdBy;
                    int result = Individual.InsertIndividual();
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

        public ActionResult _CreateCompany()
        {
            return View();
        }

        /// <summary>
        /// Description   : To Insert Company
        /// Created By    : Pavan
        /// Created Date  : 22 July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult CreateCorporation(Corporation Corporation)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                Int32 returnValue = 0;
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(returnValue);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    //var data = new Corporation
                    //{
                    //    ID = ID,
                    //    NameOfCompany = NameOfCompany,
                    //    CountryOfIncorporation = CountryOfIncorporation,
                    //    DateOfIncorporation = DateOfIncorporation,
                    //    RegistrationNo = RegistrationNo,
                    //    AccPacCode = AccPacCode,
                    //    AddressLine1 = AdderessLine1,
                    //    AddressLine2 = AddressLine2,
                    //    AddressLine3 = AddressLine3,
                    //    Country = Country,
                    //    PostalCode = PostalCode,
                    //    Email = Email,
                    //    ContactNo = ContactNo,
                    //    Fax = Fax,
                    //    createdBy = createdBy
                    //};
                    Corporation.createdBy = createdBy;
                    int result = Corporation.InsertCorporation();
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

        public ActionResult _DynamicGrid(string ddlId, int I, int C, string GridName, string GridCode, string SearchType, string SearchRole, int WOID, string DisplayName, string DynamicNatureAppoint, int gridDefaultLoad, int gridAutoSearch)
        {
            ViewBag.ddlId = ddlId;
            ViewBag.GridName = GridName;
            ViewBag.GridCode = GridCode;
            ViewBag.I = I;
            ViewBag.C = C;
            ViewBag.gridDefaultLoad = gridDefaultLoad;
            ViewBag.gridAutoSearch = gridAutoSearch;

            if (string.IsNullOrEmpty(DynamicNatureAppoint))
                ViewBag.DynamicNatureAppoint = "";
            else
                ViewBag.DynamicNatureAppoint = DynamicNatureAppoint;

            if (string.IsNullOrEmpty(SearchType))
                ViewBag.SearchType = "DYN";
            else
                ViewBag.SearchType = SearchType;

            if (string.IsNullOrEmpty(DisplayName))
                ViewBag.DisplayName = "";
            else
                ViewBag.DisplayName = DisplayName;



            ViewBag.SearchRole = SearchRole;
            ViewBag.WOID = WOID;

            return View();
        }

        /// <summary>
        /// Description   : To Get Director Address Details
        /// Created By    : Pavan
        /// Created Date  : 12 August 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetDirectorAddressDetails(int PersonId, string SourceCode)
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
                    var result = WOAddress.GetWOAddressDetails(PersonId, SourceCode);
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
        /// Description   : To Insert or Update Director Address Details
        /// Created By    : Pavan
        /// Created Date  : 12 August 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult InsertOrUpdateDirectorAddress(int PersonId, string SourceCode, string DirectorEmail, string DirectorContactNo, string DirectorFax)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                Int32 returnValue = 0;
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(returnValue);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var data = new WOAddress
                    {
                        PersonID = PersonId,
                        PersonSource = SourceCode,
                        Email = DirectorEmail,
                        Phone = DirectorContactNo,
                        Fax = DirectorFax
                    };
                    int result = data.InsertOrUpdateDirectorAddress(createdBy);
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

        [HttpPost]
        public JsonResult WOInformationForm(string gridCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = WOInformation.GetWOInformationForm(gridCode);
                return Json(data);
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

        [HttpPost]
        public JsonResult InsertWoInformation(string givenAnswers)
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

                    string[] strAnswers = givenAnswers.Split('-');

                    for (int iCount = 0; iCount < strAnswers.Length; iCount++)
                    {

                        if (!string.IsNullOrEmpty(strAnswers[iCount]))
                        {
                            string data = strAnswers[iCount];
                            string[] items = data.Split(';');
                            WOInformation.InsertWoInformation(Convert.ToInt32(items[0]), items[1], Convert.ToInt32(items[2]), items[3], items[4], Convert.ToInt32(items[5]), items[6], checkSession, items[7]);
                        }
                    }
                }
                return Json("");
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

        [HttpPost]
        public JsonResult GetRatingScale(int from, int to)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = new List<RatingScale>();
                for (int i = from; i <= to; i++)
                {
                    var scale = new RatingScale
                    {
                        Id = i,
                    };
                    data.Add(scale);
                }
                return Json(data);
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


        [HttpPost]
        public JsonResult GetDDLValues(int from, int to)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                List<RatingScale> data = new List<RatingScale>();

                if (from == 1 && to == 1)
                {
                    data = new List<RatingScale>
                {
                    new RatingScale {Id=76, Text="76", Name="76"},
                    new RatingScale {Id=78,Text="78", Name="78"},
                };
                }

                else if (from == 2 && to == 2)
                {
                    var designations = CSS2.Areas.Masters.Models.Masters.ModeOfDesignations.GetModeOfDesignations();
                    foreach (var i in designations)
                    {
                        var designation = new RatingScale
                        {
                            Id = i.ID,
                            Name = i.Name,
                            Text = i.Name
                        };
                        data.Add(designation);
                    }
                    //data = new List<RatingScale>
                    //{


                    //    new RatingScale {Id=1, Name="Manager", Text="Manager"},
                    //    new RatingScale {Id=2, Name="Director", Text="Director"},
                    //};
                }

                ////*********Added by Sudheer to get the Currency in AllotmentDetails PopUp********
                else if (from == 4 && to == 4)
                {
                    var currencies = CSS2.Areas.Masters.Models.Masters.Currency.GetCurrencyDetails();
                    foreach (var i in currencies)
                    {
                        var currency = new RatingScale
                        {
                            Id = i.CurrencyID,
                            Name = i.CurrencyName,
                            Text = i.CurrencyName
                        };
                        data.Add(currency);
                    }
                }

                return Json(data);
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

        [HttpPost]
        public JsonResult GetListValues(string givenOptions, int includeOthers)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = new List<RatingScale>();
                string[] values = givenOptions.Split(',');
                var last = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    if (!string.IsNullOrEmpty(values[i]))
                    {
                        var scale = new RatingScale
                        {
                            Id = i,
                            Text = values[i],
                            Name = givenOptions,
                        };
                        data.Add(scale);
                        last = i;
                    }

                }
                if (includeOthers == 1)
                {
                    var scale = new RatingScale
                    {
                        Id = last + 1,
                        Text = "Others",
                        Name = givenOptions,
                    };
                    data.Add(scale);
                }
                return Json(data);
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

        public class RatingScale
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
        }




    }

}