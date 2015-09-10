using System.Web.Mvc;

namespace CSS2.Areas.TM
{
    public class TMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TM_default",
                "TM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}