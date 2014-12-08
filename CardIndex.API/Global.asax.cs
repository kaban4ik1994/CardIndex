using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using CardIndex.Context;
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
