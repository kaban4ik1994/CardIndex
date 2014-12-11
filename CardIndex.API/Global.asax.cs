using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using CardIndex.Data;
using CardIndex.Mappings;

namespace CardIndex.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents(); 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            InitilizeMapper();
            Database.SetInitializer(new CardIndexContextInitializer());
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        private static void InitilizeMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<GenreMappingProfile>();
                config.AddProfile<AuthorMappingProfile>();
                config.AddProfile<BookMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
