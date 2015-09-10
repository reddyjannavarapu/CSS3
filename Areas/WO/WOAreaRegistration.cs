using System.Web.Mvc;

namespace CSS2.Areas.WO
{
    public class WOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WO_default",
                "WO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}