using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using CardIndex.Entities;

namespace CardIndex.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<DbBook>("Book");
            builder.EntitySet<DbAuthor>("Author");
            builder.EntitySet<DbGenre>("Genre");
            builder.EntitySet<DbBookDbAuthor>("BookAuthor");
            builder.EntitySet<DbBookDbGenre>("BookGenre");

            var model= builder.GetEdmModel();

            config.MapODataServiceRoute("odata", "odata", model);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }


    }
}
