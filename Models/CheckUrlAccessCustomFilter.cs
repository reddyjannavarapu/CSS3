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

#region Usings
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System;
using CSS2.Models;
using log4net;
using System.Web.Routing;
#endregion
public class CheckUrlAccessCustomFilter : ActionFilterAttribute, IAuthenticationFilter
{
    private static ILog log = LogManager.GetLogger(typeof(CheckUrlAccessCustomFilter));

    public void OnAuthentication(AuthenticationContext filterContext)
    {
        System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
        log.Debug("Start: " + methodBase.Name);
        try
        {
            if (UserLogin.ValidateUserRequest())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Login" }, { "area", "" } });
            }
            else
            {
                string context = filterContext.Controller.ControllerContext.HttpContext.Request.Path;
                int UserID = Convert.ToInt32(filterContext.HttpContext.Session["UserID"]);
                string Role = MenuBinding.CheckURlAndGetUserRole(context, UserID);
                if (Role == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" }, { "area", "" } });
                }
                filterContext.Controller.ViewBag.Role = Role;
            }
        }
        catch (Exception ex)
        {
            log.Error("Error: " + ex);
        }
        log.Debug("End: " + methodBase.Name);
    }

    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
    {
        //var user = filterContext.HttpContext.User;
        //if (user == null || !user.Identity.IsAuthenticated)
        //{
        //    filterContext.Result = new HttpUnauthorizedResult();
        //}
    }
}