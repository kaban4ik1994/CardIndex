using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using CardIndex.Context;
using CardIndex.Mappings;

namespace CardIndex.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InitilizeMapper();
            Database.SetInitializer(new CardIndexContextInitializer());
        }

        private static void InitilizeMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<BookMappingProfile>();
                config.AddProfile<GenreMappingProfile>();
                config.AddProfile<AuthorMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
