using System.Data.Entity;
using System.Web.Http;
using CardIndex.Data;

namespace CardIndex.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents(); 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new CardIndexContextInitializer());
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
