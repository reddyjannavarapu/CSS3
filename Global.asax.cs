using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CSS2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense(ConfigurationManager.AppSettings["AsposeLicFileName"].ToString());

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
